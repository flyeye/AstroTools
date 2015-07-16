using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FConnection;
using System.Threading;

namespace FMounter
{
    class TFGrower : TFBasicCom
    {
        public TFGrower(Form parent = null)            
            : base (false, parent)
        {
            fDeviceType = FDEV.FD_GROWER;            

            fDeviceDescription = " \r\n \r\n Grower Control Panel \r\n ver 1.0.net \r\n \r\n Created by Alexey V. Popov \r\n 9141866@gmail.com \r\n St.-Petersburg \r\n Russia \r\n \r\n 2015 \r\n";
        }        

        protected override void OnHandshakeRouting()
        {
            base.OnHandshakeRouting();
            Thread.Sleep(10);         
        }

        protected override bool ParseCommand(byte[] cmd, int cmdlen)
        {
            if (base.ParseCommand(cmd, cmdlen))
                return true;

            switch ((byte)cmd[0])
            {
                case (byte)FBC.FBC_GH_GET_VALUE:
                    {                                     
                        return true;
                    }
            };

            return false;
        }
    }
}
