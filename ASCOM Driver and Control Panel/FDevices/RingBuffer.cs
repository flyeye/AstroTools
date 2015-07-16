// Ring buffer
//
//
//  2015.03.10  
//  Impementation of the Ring buffer on C#
//  Author: Alexey V. Popov, 9141866@gmail.com
//
//

#define RING_BUFFER
using System;
using System.Text;

namespace RingBuffer
{ 

class ByteBuffer
{
  
    private byte[] fData;
    private int fCapacity;
	private int fPosition;
	private int fLength;
	       

	public ByteBuffer(int capacity = 2048)
    {
        fCapacity = capacity;
        fData = new byte[fCapacity];        
    }

    
	// This method resets the buffer into an original state (with no data)	
    public void Clear() {
        fPosition = 0;
        fLength = 0;
    }


	public byte Peek(int index){
        byte b = fData[(fPosition+index)%fCapacity]; 
        return b;
    }	// This method returns the byte that is located at index in the buffer but doesn't modify the buffer like the get methods (doesn't remove the retured byte from the buffer)


    public void Skip(int i)
    {
        if (i > 0)
        {
            fPosition = (fPosition + i) % fCapacity;
            fLength -= i;
        }
    }

	public int Put(byte b){
        if(fLength < fCapacity){
		    // save data byte at end of buffer
		    fData[(fPosition+fLength) % fCapacity] = b;
		    // increment the length
		    fLength++;
		    return 1;
	    }
	    // return failure
	    return 0;
    }

    public int Put(String s)
    {
        int res = 0;
        for (int i = 0; i != Math.Min(s.Length, fCapacity-fLength); i++)
            res += Put((byte)s[i]);
        return res;
    }

    public int Put(byte[] buf, int count)
    {
        int res = 0;
        for (int i = 0; i != Math.Min(count, fCapacity - fLength); i++)
            res += Put(buf[i]);
        return res;
    }

        
	public byte Get(){
        byte b = 0;
        if (fLength > 0)
        {
            b = fData[fPosition];
            // move index down and decrement length
            fPosition = (fPosition + 1) % fCapacity;
            fLength--;
        }
        return b;
    }
    

	unsafe public short GetWord(){         
        short res = 0;
        byte* pointer = (byte*)&res;
        pointer[1] = Get();
        pointer[0] = Get();
        return res;
    }

	unsafe public long GetInt(){
        int res = 0;
        byte* pointer = (byte*)&res;
        pointer[3] = Get();
        pointer[2] = Get();
        pointer[1] = Get();
        pointer[0] = Get();
        return res; 
    }

    unsafe public long GetLong()
    {
        long res = 0;
        byte* pointer = (byte*)&res;
        pointer[7] = Get();
        pointer[6] = Get();
        pointer[5] = Get();
        pointer[4] = Get();
        pointer[3] = Get();
        pointer[2] = Get();
        pointer[1] = Get();
        pointer[0] = Get();
        return res; 
    }

    unsafe public float GetFloat()
    {
        float res = 0;
        byte* pointer = (byte*)&res;
        pointer[3] = Get();
        pointer[2] = Get();
        pointer[1] = Get();
        pointer[0] = Get();
        return res; 
    }

	
    public String ExtractCommand(byte fb, byte lb1, byte lb2){
        String s = "";
        // найдем начало
        if (fLength == 0)
            return s;
        for (int i = 0; i != fLength; i++ )
            if (Peek(i)==fb)
            {
                Skip(i+1);
                break;
            }        
        // Найдем конец
        if (fLength == 0)
            return s;
        int cmdlen = 0;
        for (int i = 0; i != fLength-1; i++)        
            if ((Peek(i) == lb1) && (Peek(i + 1) == lb2))
            {
                cmdlen = i;
                break;
            }
        // скопируем команду
        if (cmdlen == 0)
            return s;        
        for (int i = 0; i != cmdlen; i++)        
            s += (char)Get();
        
        Skip(2);

        return s;
    }

    public int ExtractCommand(byte fb, byte lb1, byte lb2, ref byte[] buf)
    {        
        // найдем начало
        if (fLength == 0)
            return 0;
        for (int i = 0; i != fLength; i++)
            if (Peek(i) == fb)
            {
                Skip(i + 1);
                break;
            }
        // Найдем конец
        if (fLength == 0)
            return 0;
        int cmdlen = 0;
        for (int i = 0; i != fLength - 1; i++)
            if ((Peek(i) == lb1) && (Peek(i + 1) == lb2))
            {
                cmdlen = i;
                break;
            }
        // скопируем команду
        if (cmdlen == 0)
            return 0;

        buf = new byte[cmdlen];
        for (int i = 0; i != cmdlen; i++)
            buf[i] = Get();

        Skip(2);

        return cmdlen;
    }

    }
}





