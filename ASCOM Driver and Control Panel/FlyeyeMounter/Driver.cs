//tabs=4
// --------------------------------------------------------------------------------
// //
// ASCOM Telescope driver for FMounterV1
//
// Description:	Driver for F-Mounter v0.5
//
// Implements:	ASCOM Telescope interface version: 1.0
// Author:		(AVP) Alexey V. Popov <9141866@gmail.com>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	6.0.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------
//


// This is used to define code in the template that is specific to one class implementation
// unused code canbe deleted and this definition removed.
#define Telescope

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Astrometry.NOVAS;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;

using FConnection;

namespace ASCOM.FMounterV1
{
    //
    // Your driver's DeviceID is ASCOM.FMounterV1.Telescope
    //
    // The Guid attribute sets the CLSID for ASCOM.FMounterV1.Telescope
    // The ClassInterface/None addribute prevents an empty interface called
    // _FMounterV1 from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //

    /// <summary>
    /// ASCOM Telescope Driver for FMounterV1.
    /// </summary>
    [Guid("875245b2-b8e8-46ee-a161-e0869a7483a7")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Telescope : ITelescopeV3
    {
        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        internal static string driverID = "ASCOM.FMounterV1.Telescope";
        // TODO Change the descriptive string for your driver then remove this line
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        private static string driverDescription = "ASCOM Telescope Driver for FMounterV1.";

        internal static string comPortProfileName = "COM Port"; // Constants used for Profile persistence
        internal static string comPortDefault = "COM1";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";
        internal static string BaudrateProfileName = "Baudrate";
        internal static string BaudrateDefault = "115200";
        internal static string ApertureAreaProfileName = "ApertureArea";
        internal static string ApertureAreaDefault = "0";
        internal static string ApertureDiameterProfileName = "ApertureDiameter";
        internal static string ApertureDiameterDefault = "0";
        internal static string FocalLengthProfileName = "FocalLength";
        internal static string FocalLengthDefault = "0";
        internal static string ElevationProfileName = "Elevation";
        internal static string ElevationDefault = "0";
        internal static string LatitudeProfileName = "Latitude";
        internal static string LatitudeDefault = "0";
        internal static string LongitudeProfileName = "Longitude";
        internal static string LongitudeDefault = "0";
        internal static string RAGearProfileName = "RAGear";
        internal static string RAGearDefault = "180885";
        internal static string DEGearProfileName = "DEGear";
        internal static string DEGearDefault = "6000";
        //internal static string ProfileName = "";
        //internal static string Default = "0";

        internal static string comPort; // Variables to hold the currrent device configuration
        internal static int baudRate; // Variables to hold the currrent device configuration
        internal static bool traceState;

        internal TFMounter fMounter;
        internal static bool isCreated = false;

        internal static double fApertureArea = 0;
        internal static double fApertureDiameter = 0;
        internal static double fFocalLength = 0;

        internal static double fElevation = 0;
        internal static double fLatitude = 0;
        internal static double fLongitude = 0;

        internal static double fRAGear = 0;
        internal static double fDEGear = 0;

      
        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Private variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        private TraceLogger tl;

        private NOVAS31 fNOVAS;

        private ASCOM.Astrometry.Transform.Transform fTransform;

        /// <summary>
        /// Initializes a new instance of the <see cref="FMounterV1"/> class.
        /// Must be public for COM registration.
        /// </summary>
        public Telescope()
        {
            ReadProfile(); // Read device configuration from the ASCOM Profile store

            tl = new TraceLogger("", "FMounterV1");
            tl.Enabled = traceState;
            tl.LogMessage("Telescope", "Starting initialisation");

            utilities = new Util(); //Initialise util object
            astroUtilities = new AstroUtils(); // Initialise astro utilities object
            fNOVAS = new NOVAS31();
            fTransform = new Astrometry.Transform.Transform();

            //TODO: Implement your additional construction here

            tl.LogMessage("Telescope", "Completed initialisation");

            fMounter = new TFMounter(true);            

            isCreated = true;
        }        


        //
        // PUBLIC COM INTERFACE ITelescopeV3 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected
            if (IsConnected)
                System.Windows.Forms.MessageBox.Show("Already connected, just press OK");

            using (SetupDialogForm F = new SetupDialogForm())
            {
                var result = F.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                }
            }
        }

        public ArrayList SupportedActions
        {
            get
            {
                tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            // Call CommandString and return as soon as it finishes
            this.CommandString(command, raw);
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            // TODO decode the return string and return true or false
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBool");
        }

        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            // it's a good idea to put all the low level communication with the device here,
            // then all communication calls this function
            // you need something to ensure that only one command is in progress at a time

            throw new ASCOM.MethodNotImplementedException("CommandString");
        }

        public void Dispose()
        {
            // Clean up the tracelogger and util objects
            tl.Enabled = false;
            tl.Dispose();
            tl = null;
            utilities.Dispose();
            utilities = null;
            astroUtilities.Dispose();
            astroUtilities = null;
            fMounter.Disconnect();
        }

        public bool Connected
        {
            get
            {
                tl.LogMessage("Connected Get", IsConnected.ToString());
                return IsConnected;
            }
            set
            {
                tl.LogMessage("Connected Set", value.ToString());
                if (value == IsConnected)
                    return;

                if (value)
                {
                    fMounter.Connect(comPort, baudRate);                    

                    if (fMounter.IsConnected)
                    {
                        fMounter.fGearRatioRA = fRAGear;
                        fMounter.fGearRatioDE = fDEGear;
                        fMounter.IsTimeRunning = true;                        
                    }
                    tl.LogMessage("Connected Set", "Connecting to port " + comPort);
                    // TODO connect to the device
                }
                else
                {
                    fMounter.Disconnect();
                    tl.LogMessage("Connected Set", "Disconnecting from port " + comPort);
                    // TODO disconnect from the device
                }
            }
        }

        public string Description
        {
            // TODO customise this device description
            get
            {
                tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                // TODO customise this driver description
                string driverInfo = "Information about the driver itself. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                tl.LogMessage("InterfaceVersion Get", "3");
                return Convert.ToInt16("3");
            }
        }

        public string Name
        {
            get
            {
                string name = "FMounterV1";
                tl.LogMessage("Name Get", name);
                return name;
            }
        }

        #endregion

        #region ITelescope Implementation
        public void AbortSlew()
        {
            tl.LogMessage("AbortSlew", "Stop mounter");
            fMounter.Stop(MOTOR.DE);
            fMounter.Stop(MOTOR.RA);
        }

        public AlignmentModes AlignmentMode
        {
            get
            {
                tl.LogMessage("AlignmentMode Get", "German Polar Alignment");
                return AlignmentModes.algGermanPolar;
            }
        }

        public double Altitude
        {
            get
            {
                fTransform.SiteElevation = fElevation;
                fTransform.SiteLatitude = fLatitude;
                fTransform.SiteLongitude = fLongitude;
                fTransform.JulianDateTT = utilities.DateLocalToJulian(DateTime.Now);
                fTransform.SetJ2000(fMounter.PosRA_EQ, fMounter.PosDE_EQ);
                return fTransform.ElevationTopocentric;
            }
        }

        public double ApertureArea
        {
            get
            {
                tl.LogMessage("ApertureArea Get", "Not implemented");
                return fApertureArea;
            }
        }

        public double ApertureDiameter
        {
            get
            {
                tl.LogMessage("ApertureDiameter Get", "Not implemented");
                return fApertureDiameter;
            }
        }

        protected bool fIsAtHome = false;
        protected double fHomeAzimuth = 0;
        protected double fHomeDE = 90;
        public bool AtHome
        {
            get
            {
                tl.LogMessage("AtHome", "Get - " + fIsAtHome.ToString());
                return fIsAtHome;
            }
        }

        protected bool fIsAtPark = false;
        public bool AtPark
        {
            get
            {
                tl.LogMessage("AtPark", "Get - " + false.ToString());
                return false;
            }
        }

        public IAxisRates AxisRates(TelescopeAxes Axis)
        {
            tl.LogMessage("AxisRates", "Get - " + Axis.ToString());            
            AxisRates axisrates = new AxisRates(Axis);            
            return axisrates;
        }

        public double Azimuth
        {
            get
            {
                fTransform.SiteElevation = fElevation;
                fTransform.SiteLatitude = fLatitude;
                fTransform.SiteLongitude = fLongitude;
//                fTransform.JulianDateTT = utilities.DateLocalToJulian(DateTime.Now);
                fTransform.SetJ2000(fMounter.PosRA_EQ, fMounter.PosDE_EQ);
                return fTransform.AzimuthTopocentric;
            }
        }

        public bool CanFindHome
        {
            get
            {
                tl.LogMessage("CanFindHome", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanMoveAxis(TelescopeAxes Axis)
        {
            tl.LogMessage("CanMoveAxis", "Get - " + Axis.ToString());
            switch (Axis)
            {
                case TelescopeAxes.axisPrimary: return true;
                case TelescopeAxes.axisSecondary: return true;
                case TelescopeAxes.axisTertiary: return false;
                default: throw new InvalidValueException("CanMoveAxis", Axis.ToString(), "0 to 2");
            }
        }

        public bool CanPark
        {
            get
            {
                tl.LogMessage("CanPark", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanPulseGuide
        {
            get
            {
                tl.LogMessage("CanPulseGuide", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSetDeclinationRate
        {
            get
            {
                tl.LogMessage("CanSetDeclinationRate", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSetGuideRates
        {
            get
            {
                tl.LogMessage("CanSetGuideRates", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSetPark
        {
            get
            {
                tl.LogMessage("CanSetPark", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSetPierSide
        {
            get
            {
                tl.LogMessage("CanSetPierSide", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSetRightAscensionRate
        {
            get
            {
                tl.LogMessage("CanSetRightAscensionRate", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSetTracking
        {
            get
            {
                tl.LogMessage("CanSetTracking", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSlew
        {
            get
            {
                tl.LogMessage("CanSlew", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSlewAltAz
        {
            get
            {
                tl.LogMessage("CanSlewAltAz", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSlewAltAzAsync
        {
            get
            {
                tl.LogMessage("CanSlewAltAzAsync", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanSlewAsync
        {
            get
            {
                tl.LogMessage("CanSlewAsync", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSync
        {
            get
            {
                tl.LogMessage("CanSync", "Get - " + true.ToString());
                return true;
            }
        }

        public bool CanSyncAltAz
        {
            get
            {
                tl.LogMessage("CanSyncAltAz", "Get - " + false.ToString());
                return false;
            }
        }

        public bool CanUnpark
        {
            get
            {
                tl.LogMessage("CanUnpark", "Get - " + false.ToString());
                return false;
            }
        }

        public double Declination
        {
            get
            {
                double declination = fMounter.PosDE_EQ;
                tl.LogMessage("Declination", "Get - " + utilities.DegreesToDMS(declination, ":", ":"));
                return declination;
            }
        }

        public double DeclinationRate  // arcseconds per sec
        {
            get
            {
                tl.LogMessage("DeclinationRate", "Get - " + fMounter.DailySpeedDE.ToString());
                return fMounter.DailySpeedDE;
            }
            set
            {
                fMounter.DailySpeedDE = value;
            }
        }

        public PierSide DestinationSideOfPier(double RightAscension, double Declination)
        {
            tl.LogMessage("DestinationSideOfPier Get", "Not implemented");
            throw new ASCOM.PropertyNotImplementedException("DestinationSideOfPier", false);
        }

        public bool DoesRefraction
        {
            get
            {
                tl.LogMessage("DoesRefraction Get", "No refraction");
                return false;
            }
            set
            {
                tl.LogMessage("DoesRefraction Set", "No refraction");
                throw new ASCOM.PropertyNotImplementedException("DoesRefraction", true);
            }
        }

        public EquatorialCoordinateType EquatorialSystem
        {
            get
            {
                EquatorialCoordinateType equatorialSystem = EquatorialCoordinateType.equJ2000;
                tl.LogMessage("DeclinationRate", "Get - " + equatorialSystem.ToString());
                return equatorialSystem;
            }
        }
        
        public void FindHome()
        {
            SlewToCoordinates(fMounter.PosRA_EQ, fHomeDE);
            fIsAtHome = true;
        }
        
        public double FocalLength
        {
            get
            {
                return fFocalLength;
            }
        }

        protected double fGuideRateDeclination = 0.0055555;  // degrees/sec
        public double GuideRateDeclination
        {
            get
            {
                return fGuideRateDeclination;
            }
            set
            {
                fGuideRateDeclination = value;
            }
        }

        protected double fGuideRateRightAscension = 0.055555;
        public double GuideRateRightAscension
        {
            get
            {
                return fGuideRateRightAscension;
            }
            set
            {
                fGuideRateRightAscension = value;
            }
        }

        protected bool fIsGuiding = false;
        public bool IsPulseGuiding
        {
            get
            {
                return fIsGuiding;
            }
        }

        public void MoveAxis(TelescopeAxes Axis, double Rate)
        {
            tl.LogMessage("MoveAxis", " - " + Axis.ToString() + ", rate = " + Rate.ToString());
            STEPDIR dir;
            if (Rate >= 0)
                dir = STEPDIR.STEP_BACKWARD;
            else
                dir = STEPDIR.STEP_FORWARD;
            
            switch (Axis)
            { 
                case TelescopeAxes.axisPrimary:
                    {
                        if (Math.Abs(Rate) < 0.01)
                        {
                            fMounter.Stop(MOTOR.RA);
                        }
                        else
                        {
                            AxisRates axe = new AxisRates(TelescopeAxes.axisPrimary);
                            if ((Math.Abs(Rate) < axe[1].Minimum) || Math.Abs(Rate)> axe[1].Maximum)
                                throw new ASCOM.InvalidValueException("Invalid value");
                            fMounter.NavSpeed = Math.Abs(Rate);
                            fMounter.Roll(MOTOR.RA, dir);                            
                        }
                        break;
                    }
                case TelescopeAxes.axisSecondary:
                    {
                        if (Math.Abs(Rate) < 0.01)
                        {
                            fMounter.Stop(MOTOR.DE);
                        }
                        else
                        {
                            AxisRates axe = new AxisRates(TelescopeAxes.axisSecondary);
                            if ((Math.Abs(Rate) < axe[1].Minimum) || Math.Abs(Rate) > axe[1].Maximum)
                                throw new ASCOM.InvalidValueException("Invalid value");
                            fMounter.NavSpeed = Math.Abs(Rate);
                            fMounter.Roll(MOTOR.DE, dir);
                        }                        
                    }
                    break;
                case TelescopeAxes.axisTertiary:
                    {
                        throw new ASCOM.InvalidValueException("Invalid value");
                    }
                    break;
            }

            fIsAtHome = false;
        }

        public void Park()
        {
            tl.LogMessage("Park", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("Park");
        }

        public void PulseGuide(GuideDirections Direction, int Duration)
        {
            switch (Direction)
            {
                case GuideDirections.guideEast:
                    {
                        fMounter.NavSpeed = GuideRateRightAscension;
                        fMounter.Roll(MOTOR.RA, STEPDIR.STEP_BACKWARD, (short)Duration);
                        break;
                    }
                case GuideDirections.guideWest:
                    {
                        fMounter.NavSpeed = GuideRateRightAscension;
                        fMounter.Roll(MOTOR.RA, STEPDIR.STEP_FORWARD, (short)Duration);
                        break;
                    }
                case GuideDirections.guideNorth:
                    {
                        fMounter.NavSpeed = GuideRateDeclination;
                        fMounter.Roll(MOTOR.DE, STEPDIR.STEP_BACKWARD, (short)Duration);
                        break;
                    }
                case GuideDirections.guideSouth:
                    {
                        fMounter.NavSpeed = GuideRateDeclination;
                        fMounter.Roll(MOTOR.DE, STEPDIR.STEP_FORWARD, (short)Duration);
                        break;
                    }
            }
            fMounter.OnStopWaitEvent.WaitOne(Duration+100);
        }

        public double RightAscension
        {
            get
            {
                double rightAscension = fMounter.PosRA_EQ;
                tl.LogMessage("RightAscension", "Get - " + utilities.HoursToHMS(rightAscension));
                return rightAscension;
            }
        }

        protected double fRightAscensionRate = 0;
        public double RightAscensionRate
        {
            get
            {
                tl.LogMessage("RightAscensionRate", "Get - " + fRightAscensionRate.ToString());
                return fRightAscensionRate;
            }
            set
            {                
                fRightAscensionRate = value * 0.9972695677;
                fMounter.DailySpeedRA = fRightAscensionRate + fTrackingRate;
            }
        }

        public void SetPark()
        {
            tl.LogMessage("SetPark", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SetPark");
        }

        public PierSide SideOfPier
        {
            get
            {
                tl.LogMessage("SideOfPier Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SideOfPier", false);
            }
            set
            {
                tl.LogMessage("SideOfPier Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SideOfPier", true);
            }
        }

        public double SiderealTime
        {
            get
            {
                if (IsConnected)
                {
                    double siderealTime = 0;
                    
                    //fMounter.RTC.DeviceDateTime                    
                    double juliandate = fNOVAS.JulianDate((short)DateTime.Now.Year, (short)DateTime.Now.Month, (short)DateTime.Now.Day, DateTime.Now.Hour);
                    double dt = fNOVAS.DeltaT(juliandate);                    
                    double jh = Math.Truncate(juliandate);
                    double jl = juliandate - jh;
                    
                    fNOVAS.SiderealTime(jh, jl, dt, GstType.GreenwichMeanSiderealTime, Method.CIOBased, Accuracy.Full, ref siderealTime);
                    //tl.LogMessage("SiderealTime", "Get - " + siderealTime.ToString());
                    //return siderealTime + 2;

                    siderealTime = (18.697374558 + 24.065709824419081 * (utilities.DateLocalToJulian(DateTime.Now) - 2451545.0)) % 24.0;
                    tl.LogMessage("SiderealTime", "Get - " + siderealTime.ToString());
                    return siderealTime + fLongitude/15;
                    
                }
                else
                {
                    tl.LogMessage("SiderealTime Get", "Not connected");
                    throw new ASCOM.NotConnectedException("SiderealTime Get");
                }                                
            }
        }
        
        public double SiteElevation
        {
            get
            {
                return fElevation;
            }
            set
            {
                if ((value < -300) || (value > 10000))
                {
                    throw new ASCOM.InvalidValueException("Invalid value");                    
                }
                fElevation = value;
            }
        }

        public double SiteLatitude
        {
            get
            {
                return fLatitude;
            }
            set
            {
                if ((value < -90) || (value > 90))
                {
                    throw new ASCOM.InvalidValueException("Invalid value");
                }
                fLatitude = value;
            }
        }

        public double SiteLongitude
        {
            get
            {
                return fLongitude;
            }
            set
            {
                if ((value < -180) || (value > 180))
                {
                    throw new ASCOM.InvalidValueException("Invalid value");
                }
                fLongitude = value;
            }
        }

        public short SlewSettleTime
        {
            get
            {
                tl.LogMessage("SlewSettleTime Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SlewSettleTime", false);
            }
            set
            {
                tl.LogMessage("SlewSettleTime Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("SlewSettleTime", true);
            }
        }

        public void SlewToAltAz(double Azimuth, double Altitude)
        {
            tl.LogMessage("SlewToAltAz", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SlewToAltAz");
        }

        public void SlewToAltAzAsync(double Azimuth, double Altitude)
        {
            tl.LogMessage("SlewToAltAzAsync", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SlewToAltAzAsync");
        }

        public void SlewToCoordinates(double RightAscension, double Declination)
        {
            fTargetDeclination = Declination;
            fTargetRightAscension = RightAscension;
            SlewToTarget();
            fIsAtHome = false;
        }

        public void SlewToCoordinatesAsync(double RightAscension, double Declination)
        {
            fTargetDeclination = Declination;
            fTargetRightAscension = RightAscension;
            SlewToTargetAsync();
        }

        public void SlewToTarget()
        {
            fMounter.GotoPositionEQ(fTargetRightAscension, fTargetDeclination);
            fMounter.OnGotoWaitEvent.WaitOne();
            fIsAtHome = false;
        }

        public void SlewToTargetAsync()
        {
            fMounter.GotoPositionEQ(fTargetRightAscension, fTargetDeclination);
            fIsAtHome = false;
        }

        public bool Slewing
        {
            get
            {
                return fMounter.IsRolling;
            }
        }

        public void SyncToAltAz(double Azimuth, double Altitude)
        {
            tl.LogMessage("SyncToAltAz", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("SyncToAltAz");
        }

        public void SyncToCoordinates(double RightAscension, double Declination)
        {
            fTargetDeclination = Declination;
            fTargetRightAscension = RightAscension;
            SyncToTarget();
            fIsAtHome = false;
        }

        public void SyncToTarget()
        {
            fMounter.PosDE_EQ = fTargetDeclination;
            fMounter.PosRA_EQ = fTargetRightAscension;
            fIsAtHome = false;
        }

        protected double fTargetDeclination = 0;
        private bool fTargetDEInit = false;
        public double TargetDeclination
        {
            get
            {
                tl.LogMessage("TargetDeclination Get", " - " + fTargetDeclination.ToString());
                if (!fTargetDEInit)
                    throw new ASCOM.ValueNotSetException("Invalid value");
                return fTargetDeclination;
            }
            set
            {
                if ((value < -90) || (value > 90))
                {
                    throw new ASCOM.InvalidValueException("Invalid value");
                }
                fTargetDEInit = true;
                tl.LogMessage("TargetDeclination Set", " - " + value.ToString());
                fTargetDeclination = value;
            }
        }

        protected double fTargetRightAscension = 0;
        private bool fTargetRAInit = false;
        public double TargetRightAscension
        {
            get
            {
                tl.LogMessage("TargetRightAscension Get", fTargetRightAscension.ToString());
                if (!fTargetRAInit)
                    throw new ASCOM.ValueNotSetException("Invalid value");
                return fTargetRightAscension;
            }
            set
            {
                if ((value < 0) || (value > 24))
                {
                    throw new ASCOM.InvalidValueException("Invalid value");
                }
                fTargetRAInit = true;
                tl.LogMessage("TargetRightAscension Set", " - " + value.ToString());
                fTargetRightAscension = value;
            }
        }

        public bool Tracking
        {
            get
            {
                bool tracking = fMounter.IsTurning;
                tl.LogMessage("Tracking", "Get - " + tracking.ToString());
                return tracking;
            }
            set
            {
                tl.LogMessage("Tracking Set", " - " + value.ToString());
                if (fMounter.IsTurning != value)
                    fMounter.IsTurning = value;
            }
        }

        protected double fTrackingRate = 0;
        protected DriveRates fTrackingRateType = new DriveRates();
        public DriveRates TrackingRate
        {
            get
            {
                tl.LogMessage("TrackingRate Get", " - " + fTrackingRateType.ToString());
                return fTrackingRateType;
            }
            set
            {
                tl.LogMessage("TrackingRate Set", " - " + value.ToString());
                fTrackingRateType = value;
                switch (value)
                {
                    case DriveRates.driveSidereal:
                        fTrackingRate = 15.0;
                        break;
                    case DriveRates.driveLunar:
                        fTrackingRate = 14.685;
                        break;
                    case DriveRates.driveSolar:
                        fTrackingRate = 15.0;
                        break;
                }
                fMounter.DailySpeedRA = fRightAscensionRate + fTrackingRate;
            }
        }

        public ITrackingRates TrackingRates
        {
            get
            {
                ITrackingRates trackingRates = new TrackingRates();                
                tl.LogMessage("TrackingRates", "Get - ");
                foreach (DriveRates driveRate in trackingRates)
                {                    
                    tl.LogMessage("TrackingRates", "Get - " + driveRate.ToString());
                }
                return trackingRates;
            }
        }

        public DateTime UTCDate
        {
            get
            {
                if (IsConnected)
                {                    
                    DateTime utc = fMounter.RTC.DeviceDateTime.ToUniversalTime();
                    tl.LogMessage("UTCDate Get", " - " + String.Format("MM/dd/yy HH:mm:ss", utc));
                    return utc;
                }
                else
                {
                    tl.LogMessage("UTCDate Get", "Not connected");
                    throw new ASCOM.NotConnectedException("UTCDate Get");
                }
                    
            }
            set
            {
                if (IsConnected)
                {                   
                    tl.LogMessage("UTCDate Set", " - " + String.Format("MM/dd/yy HH:mm:ss", value));
                    fMounter.RTC.DeviceDateTime = value.ToLocalTime();
                }                                                                
            }
        }

        public void Unpark()
        {
            tl.LogMessage("Unpark", "Not implemented");
            throw new ASCOM.MethodNotImplementedException("Unpark");
        }

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        #region ASCOM Registration

        // Register or unregister driver for ASCOM. This is harmless if already
        // registered or unregistered. 
        //
        /// <summary>
        /// Register or unregister the driver with the ASCOM Platform.
        /// This is harmless if the driver is already registered/unregistered.
        /// </summary>
        /// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        private static void RegUnregASCOM(bool bRegister)
        {
            using (var P = new ASCOM.Utilities.Profile())
            {
                P.DeviceType = "Telescope";
                if (bRegister)
                {
                    P.Register(driverID, driverDescription);
                }
                else
                {
                    P.Unregister(driverID);
                }
            }
        }

        /// <summary>
        /// This function registers the driver with the ASCOM Chooser and
        /// is called automatically whenever this class is registered for COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is successfully built.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        /// </remarks>
        [ComRegisterFunction]
        public static void RegisterASCOM(Type t)
        {
            RegUnregASCOM(true);
        }

        /// <summary>
        /// This function unregisters the driver from the ASCOM Chooser and
        /// is called automatically whenever this class is unregistered from COM Interop.
        /// </summary>
        /// <param name="t">Type of the class being registered, not used.</param>
        /// <remarks>
        /// This method typically runs in two distinct situations:
        /// <list type="numbered">
        /// <item>
        /// In Visual Studio, when the project is cleaned or prior to rebuilding.
        /// For this to work correctly, the option <c>Register for COM Interop</c>
        /// must be enabled in the project settings.
        /// </item>
        /// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        /// </list>
        /// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        /// </remarks>
        [ComUnregisterFunction]
        public static void UnregisterASCOM(Type t)
        {
            RegUnregASCOM(false);
        }

        #endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
                // TODO check that the driver hardware connection exists and is connected to the hardware
                if (!isCreated)
                    return false;               
                return fMounter.IsConnected;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        internal void ReadProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Telescope";
                traceState = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
                comPort = driverProfile.GetValue(driverID, comPortProfileName, string.Empty, comPortDefault);
                baudRate = Convert.ToInt32(driverProfile.GetValue(driverID, BaudrateProfileName, string.Empty, BaudrateDefault));
                fApertureArea = Convert.ToDouble(driverProfile.GetValue(driverID, ApertureAreaProfileName, string.Empty, ApertureAreaDefault));
                fApertureDiameter = Convert.ToDouble(driverProfile.GetValue(driverID, ApertureDiameterProfileName, string.Empty, ApertureDiameterDefault));
                fFocalLength = Convert.ToDouble(driverProfile.GetValue(driverID, FocalLengthProfileName, string.Empty, FocalLengthDefault));
                fElevation = Convert.ToDouble(driverProfile.GetValue(driverID, ElevationProfileName, string.Empty, ElevationDefault));
                fLatitude= Convert.ToDouble(driverProfile.GetValue(driverID, LatitudeProfileName, string.Empty, LatitudeDefault));
                fLongitude = Convert.ToDouble(driverProfile.GetValue(driverID, LongitudeProfileName, string.Empty, LongitudeDefault));
                fRAGear= Convert.ToDouble(driverProfile.GetValue(driverID, RAGearProfileName, string.Empty, RAGearDefault));
                fDEGear = Convert.ToDouble(driverProfile.GetValue(driverID, DEGearProfileName, string.Empty, DEGearDefault));
                //= Convert.ToDouble(driverProfile.GetValue(driverID, ProfileName, string.Empty, Default));
            }
        }

        /// <summary>
        /// Write the device configuration to the  ASCOM  Profile store
        /// </summary>
        internal void WriteProfile()
        {
            using (Profile driverProfile = new Profile())
            {
                driverProfile.DeviceType = "Telescope";
                driverProfile.WriteValue(driverID, traceStateProfileName, traceState.ToString());
                driverProfile.WriteValue(driverID, comPortProfileName, comPort.ToString());
                driverProfile.WriteValue(driverID, BaudrateProfileName, Convert.ToString(baudRate));
                driverProfile.WriteValue(driverID, ApertureAreaProfileName, Convert.ToString(fApertureArea));
                driverProfile.WriteValue(driverID, ApertureDiameterProfileName, Convert.ToString(fApertureDiameter));
                driverProfile.WriteValue(driverID, FocalLengthProfileName, Convert.ToString(fFocalLength));
                driverProfile.WriteValue(driverID, ElevationProfileName, Convert.ToString(fElevation));
                driverProfile.WriteValue(driverID, LatitudeProfileName, Convert.ToString(fLatitude));
                driverProfile.WriteValue(driverID, LongitudeProfileName, Convert.ToString(fLongitude));
                driverProfile.WriteValue(driverID, RAGearProfileName, Convert.ToString(fRAGear));
                driverProfile.WriteValue(driverID, DEGearProfileName, Convert.ToString(fDEGear));
                //driverProfile.WriteValue(driverID, ProfileName, Convert.ToString(f));
            }
        }

        #endregion

    }
}
