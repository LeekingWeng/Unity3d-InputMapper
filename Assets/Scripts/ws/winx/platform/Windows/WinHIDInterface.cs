//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
using System;
using UnityEngine;
using System.Runtime.InteropServices;
using ws.winx.devices;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using System.IO;


namespace ws.winx.platform.windows
{
    public class WinHIDInterface : IHIDInterface
    {




        #region Fields
        private List<IDriver> __drivers;//


        private IDriver __defaultJoystickDriver;

        private const string NOTIFICATION_WND_CLS = "InputManager Device Change Notification Reciver Wnd";

		private string[] __ports;

        private DeviceProfiles __profiles;


        private static readonly object syncRoot = new object();


        delegate IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        private WndProc m_wnd_proc_delegate;

        private const int ERROR_CLASS_ALREADY_EXISTS = 1410;

        //GUID_DEVINTERFACE_HID	Class GUID{4D1E55B2-F16F-11CF-88CB-001111000030}
        private static readonly Guid GUID_DEVINTERFACE_HID = new Guid("4D1E55B2-F16F-11CF-88CB-001111000030"); // HID devices

        public IntPtr hidDeviceNotificationReceiverWindowHandle;



        private static IntPtr notificationHandle;
        private Dictionary<string, HIDDevice> __Generics;



        public event EventHandler<DeviceEventArgs<string>> DeviceDisconnectEvent;
        public event EventHandler<DeviceEventArgs<IDevice>> DeviceConnectEvent;




        #endregion


        #region IHIDInterface implementation

		public void SetProfiles(DeviceProfiles profiles){
			
			__profiles = profiles;
		}
		
		public void LoadProfiles(string fileName){
			
			__profiles=Resources.Load<DeviceProfiles> ("DeviceProfiles");
			
		}
		
		
		public DeviceProfile LoadProfile(string key){
			
			DeviceProfile profile=null;
			
			if (__profiles.vidpidProfileNameDict.ContainsKey (key)) {
				
				string profileName=__profiles.vidpidProfileNameDict[key];
				
				
				
				if(__profiles.runtimePlatformDeviceProfileDict[profileName].ContainsKey(RuntimePlatform.WindowsPlayer)){
					
					profile=__profiles.runtimePlatformDeviceProfileDict[profileName][RuntimePlatform.WindowsPlayer];
				}
				
			}
			
			
			return profile;
		}

       





        public void AddDriver(IDriver driver)
        {
            __drivers.Add(driver);
        }



        public bool Contains(string id)
        {
            return __Generics != null && __Generics.ContainsKey(id);
        }


        public HIDReport ReadDefault(string id)
        {

            return this.__Generics[id].ReadDefault();
        }

        public HIDReport ReadBuffered(string id)
        {
            return __Generics[id].ReadBuffered();
        }

        public void Read(string id, HIDDevice.ReadCallback callback, int timeout)
        {
            this.__Generics[id].Read(callback, timeout);

        }




        public void Read(string id, HIDDevice.ReadCallback callback)
        {
            this.__Generics[id].Read(callback, 0);

        }

        public void Write(object data, string id)
        {
            this.__Generics[id].Write(data);
        }

        public void Write(object data, string id, HIDDevice.WriteCallback callback)
        {
            this.__Generics[id].Write(data, callback, 0);
        }

        public void Write(object data, string id, HIDDevice.WriteCallback callback, int timeout)
        {
            this.__Generics[id].Write(data, callback, timeout);
        }


        /// <summary>
        /// add or get default driver (Overall driver for unhanlded devices by other specialized driver)
        /// </summary>
        public IDriver defaultDriver
        {
            get { if (__defaultJoystickDriver == null) { __defaultJoystickDriver = new WinMMDriver(); } return __defaultJoystickDriver; }
            set
            {
                __defaultJoystickDriver = value;

                if (value is ws.winx.drivers.UnityDriver)
                {
                    Debug.LogWarning("UnityDriver set as default driver.\n Warring:Unity doesn't make distinction between triggers/axis/poitOfView and doesn't make controller distinction as multiply instances of same type have same name and can hard code index of devices no matter position in GetJoystickNames list");
					//LoadProfiles("profiles_uni.txt");
				}
				
			}

        }






        public Dictionary<string, HIDDevice> Generics
        {
            get { return __Generics; }
        }




        public void Update()
        {
            Enumerate();
        }

        #endregion

        //public static readonly Guid GUID_DEVCLASS_HIDCLASS = new Guid(0x745a17a0, 0x74d3, 0x11d0, 0xb6, 0xfe, 0x00, 0xa0, 0xc9, 0x0f, 0x57, 0xda);
        //public static readonly Guid GUID_DEVCLASS_USB = new Guid(0x36fc9e60, 0xc465, 0x11cf, 0x80, 0x56, 0x44, 0x45, 0x53, 0x54, 0x00, 0x00);



        //private static readonly Guid GuidDevinterfaceUSBDevice = new Guid("A5DCBF10-6530-11D2-901F-00C04FB951ED"); // USB devices




        #region Constructor
        public WinHIDInterface()
        {
            __drivers = new List<IDriver>();

            __Generics = new Dictionary<string, HIDDevice>();

           

			__ports = new string[20];



			
		}
        #endregion


        // Specify what you want to happen when the Elapsed event is raised.
        //private void enumerateTimedEvent(object source, ElapsedEventArgs e)
        //{
        //    Update();

        //}




       
        /// <summary>
        /// Registers a window to receive notifications when USB devices are plugged or unplugged.
        /// </summary>
     
        /// <param name="windowHandle"></param>
        /// <returns></returns>
        public IntPtr RegisterHIDDeviceNotification(IntPtr windowHandle)
        {
            Native.DEV_BROADCAST_DEVICEINTERFACE dbi = new Native.DEV_BROADCAST_DEVICEINTERFACE
            {
                dbcc_size = 0,

                dbcc_devicetype = (int)Native.DBT_DEVTYP_DEVICEINTERFACE,

                dbcc_reserved = 0,

                dbcc_classguid = GUID_DEVINTERFACE_HID.ToByteArray()

            };


			


            dbi.dbcc_size = Marshal.SizeOf(dbi);
            IntPtr buffer = Marshal.AllocHGlobal(dbi.dbcc_size);
            Marshal.StructureToPtr(dbi, buffer, true);

          return  Native.RegisterDeviceNotification(windowHandle, buffer, 0);


        }


        /// <summary>
        /// Creates window that would receive plug in/out device events
        /// </summary>
        /// <returns></returns>
        IntPtr CreateReceiverWnd()
        {




            IntPtr wndHnd = IntPtr.Zero;

         

            m_wnd_proc_delegate = CustomWndProc;

            // Create WNDCLASS
            Native.WNDCLASS wind_class = new Native.WNDCLASS();
            wind_class.lpszClassName = NOTIFICATION_WND_CLS;
            wind_class.lpfnWndProc = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(m_wnd_proc_delegate);

            UInt16 class_atom = Native.RegisterClassW(ref wind_class);

            int last_error = System.Runtime.InteropServices.Marshal.GetLastWin32Error();

            if (class_atom == 0 && last_error != ERROR_CLASS_ALREADY_EXISTS)
            {
                Exception e = new System.Exception("Could not register window class");

                UnityEngine.Debug.LogException(e);

                return IntPtr.Zero;
            }


            try
            {
                // Create window
                wndHnd = Native.CreateWindowExW(
                    0,
                    wind_class.lpszClassName,
                    String.Empty,
                    0,
                    0,
                    0,
                    0,
                    0,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero,
                    IntPtr.Zero
                    );
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }


            return wndHnd;

        }

        /// <summary>
        /// Custom receiver window procedure where WM_MESSAGES are handled (WM_DEVICECHANGE)
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        protected IntPtr CustomWndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            int devType = 0;

            if (msg == Native.WM_DEVICECHANGE)
            {

                if (lParam != IntPtr.Zero)
                    devType = Marshal.ReadInt32(lParam, 4);

                switch ((int)wParam)
                {
                    case Native.DBT_DEVICEREMOVECOMPLETE:

                        if (devType == Native.DBT_DEVTYP_DEVICEINTERFACE)
                        {
                            try
                            {

                              

                                if (lParam == IntPtr.Zero)
                                {
                                    Debug.LogWarning("lParam was Zerro on Removing");
                                    break;
                                }
                         
                                //devicePath is used as ID
                                string devicePath = PointerToDevicePath(lParam);

                                if (String.IsNullOrEmpty(devicePath))
                                {
                                    Debug.LogWarning("DevicPath was Empty or Null on Arrival");
                                    break;
                                }



                                HIDDevice hidDevice=null;   //= CreateHIDDeviceFrom(devicePath);

                                bool hidDeviceExisted = false;

                                lock (syncRoot)
                                {
                                    hidDeviceExisted = this.Generics.ContainsKey(devicePath);

                                    if (hidDeviceExisted)
                                    {

                                        hidDevice=this.Generics[devicePath];
                                        this.Generics.Remove(devicePath);



                                       
                                    }

                                }

                                if(hidDevice!=null)
                                   UnityEngine.Debug.Log("WinHIDInterface: " + hidDevice.Name + " Removed"); 
                                else
                                    UnityEngine.Debug.Log("WinHIDInterface: Not resolved device " + devicePath + " Removed"); 

                              
                                    this.DeviceDisconnectEvent(null, new DeviceEventArgs<string>(devicePath));



                            }
                            catch (Exception e)
                            {
                                UnityEngine.Debug.LogException(e);
                            }
                        }







                        break;
                    case Native.DBT_DEVICEARRIVAL:
                        if (devType == Native.DBT_DEVTYP_DEVICEINTERFACE)
                        {
                            try
                            {

                                if (lParam == IntPtr.Zero)
                                {
                                    Debug.LogWarning("lParam was Zerro on Arrival");
                                    break;
                                }

                                string devicePath = PointerToDevicePath(lParam);

                                if (String.IsNullOrEmpty(devicePath))
                                {
                                    Debug.LogWarning("DevicPath was Empty or Null on Arrival");
                                    break;
                                }

                                HIDDevice hidDevice = CreateHIDDeviceFrom(devicePath);

                                if (hidDevice != null)
                                {
                                    if (!Generics.ContainsKey(hidDevice.ID))
                                    {
                                        // string name = ReadRegKey(Native.HKEY_CURRENT_USER, @"SYSTEM\CurrentControlSet\Control\MediaProperties\PrivateProperties\Joystick\OEM\VID_" + hidDevice.VID.ToString("X4") + "&PID_" + hidDevice.PID.ToString("X4"), Native.REGSTR_VAL_JOYOEMNAME);

                                        UnityEngine.Debug.Log("WinHIDInterface: " + hidDevice.Name + " Connected");

                                        ResolveDevice(hidDevice);
                                    }
                                    else
                                    {
                                        UnityEngine.Debug.Log("WinHIDInterface: " + hidDevice.Name + " Already Connected.");
                                    }
                                }
                                else
                                {
                                    UnityEngine.Debug.Log("Can't create Device on "+devicePath);

                                }
                            }
                            catch (Exception e)
                            {
                                UnityEngine.Debug.LogException(e);
                            }
                        }

                        break;
                }
            }

            return Native.DefWindowProcW(hWnd, msg, wParam, lParam);
        }



        /// <summary>
        /// Convert (WM_DEVICECHANGE)WM_MESSAGE pointer to data structure
        /// </summary>
        /// <param name="lParam"></param>
        /// <returns></returns>
        protected string PointerToDevicePath(IntPtr lParam)
        {
            Native.DEV_BROADCAST_DEVICEINTERFACE devBroadcastDeviceInterface =
                               new Native.DEV_BROADCAST_DEVICEINTERFACE();
            Native.DEV_BROADCAST_HDR devBroadcastHeader = new Native.DEV_BROADCAST_HDR();
            Marshal.PtrToStructure(lParam, devBroadcastHeader);

            Int32 stringSize = Convert.ToInt32((devBroadcastHeader.dbch_size - 32) / 2);
            Array.Resize(ref devBroadcastDeviceInterface.dbcc_name, stringSize);
            Marshal.PtrToStructure(lParam, devBroadcastDeviceInterface);
            return new String(devBroadcastDeviceInterface.dbcc_name, 0, stringSize);
        }


        /// <summary>
        /// Unregisters the window for USB device notifications
        /// </summary>
        public static void UnregisterHIDDeviceNotification()
		{	
			
            if (notificationHandle != IntPtr.Zero)
                Native.UnregisterDeviceNotification(notificationHandle);
            
            notificationHandle = IntPtr.Zero;
        }








        public void Enumerate()
        {

           // if (Application.isPlaying)
           // {
                if (hidDeviceNotificationReceiverWindowHandle == IntPtr.Zero)
                {

                   // hidDeviceNotificationReceiverWindowHandle=Native.FindWindow(NOTIFICATION_WND_CLS, null);

                   // if (hidDeviceNotificationReceiverWindowHandle == IntPtr.Zero)
                 //   {
                        hidDeviceNotificationReceiverWindowHandle = CreateReceiverWnd();
                  //  }

                        if (hidDeviceNotificationReceiverWindowHandle != IntPtr.Zero)
                            notificationHandle = RegisterHIDDeviceNotification(hidDeviceNotificationReceiverWindowHandle);
                    
                }
           // }
            //else
            //{
            //    UnityEngine.Debug.Log("Plug&play isn't support in InputMapper Editor until I found way to destroy safely add/remove notification handle");
            //}


            uint deviceCount = 0;
            var deviceSize = (uint)Marshal.SizeOf(typeof(Native.RawInputDeviceList));

            // first call retrieves the number of raw input devices
            var result = Native.GetRawInputDeviceList(
                IntPtr.Zero,
                ref deviceCount,
                deviceSize);



            if ((int)result == -1)
            {
                // call failed, 
                UnityEngine.Debug.LogError("WinHIDInterface failed to enumerate devices");

                return;
            }
            else if (deviceCount == 0)
            {
                // call failed, 
                UnityEngine.Debug.LogWarning("WinHIDInterface found no HID devices");

                return;
            }






            // allocates memory for an array of Win32.RawInputDeviceList
            IntPtr ptrDeviceList = Marshal.AllocHGlobal((int)(deviceSize * deviceCount));

            result = Native.GetRawInputDeviceList(
                ptrDeviceList,
                ref deviceCount,
                deviceSize);



            if ((int)result != -1)
            {
                Native.RawInputDeviceList rawInputDeviceList;
                // enumerates array of Win32.RawInputDeviceList,
                // and populates array of managed RawInputDevice objects
                for (var index = 0; index < deviceCount; index++)
                {

                    rawInputDeviceList = (Native.RawInputDeviceList)Marshal.PtrToStructure(
                        new IntPtr((ptrDeviceList.ToInt32() +
                                (deviceSize * index))),
                        typeof(Native.RawInputDeviceList));



                    if (rawInputDeviceList.DeviceType == Native.RawInputDeviceType.HumanInterfaceDevice)
                    {
                        HIDDevice hidDevice = CreateHIDDeviceFrom(rawInputDeviceList);

                        if (!__Generics.ContainsKey(hidDevice.ID))
                            ResolveDevice(hidDevice);
                    }

                }
            }

            Marshal.FreeHGlobal(ptrDeviceList);

        }









        /// <summary>
        /// 
        /// </summary>
        /// <param name="devicePath"></param>
        /// <returns></returns>
        protected HIDDevice CreateHIDDeviceFrom(string devicePath)
        {

            string[] Parts = devicePath.Split('#');

            if (Parts.Length >= 3)
            {
                // string DevType = Parts[0].Substring(Parts[0].IndexOf(@"?\") + 2);//HID
                string DeviceInstanceId = Parts[1];

                String[] VID_PID_Parts = DeviceInstanceId.Split('&');


                //if we need in later code expansion
                // string DeviceUniqueID = Parts[2];//{fas232fafs2345faf}



                // string RegPath = @"SYSTEM\CurrentControlSet\Enum\" + DevType + "\\" + DeviceInstanceId + "\\" + DeviceUniqueID;


                //return ReadRegKey(HKEY_LOCAL_MACHINE, RegPath, "FriendlyName")+ReadRegKey(HKEY_LOCAL_MACHINE, RegPath, "DeviceDesc");



                //devicePath is used as ID
                //!!! deviceHandle set to IntPtr.Zero (think not needed in widows)
                return new GenericHIDDevice(GetIndexForDeviceWithID(devicePath), Convert.ToInt32(VID_PID_Parts[0].Replace("VID_", ""), 16), Convert.ToInt32(VID_PID_Parts[1].Replace("PID_", ""), 16), devicePath, IntPtr.Zero, this, devicePath);
            }

            return null;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rawInputDeviceList"></param>
        /// <returns></returns>
        protected HIDDevice CreateHIDDeviceFrom(Native.RawInputDeviceList rawInputDeviceList)
        {



            Native.DeviceInfo deviceInfo = GetDeviceInfo(rawInputDeviceList.DeviceHandle);
          //  UnityEngine.Debug.Log("PID:" + deviceInfo.HIDInfo.ProductID + " VID:" + deviceInfo.HIDInfo.VendorID);

            string devicePath = GetDevicePath(rawInputDeviceList.DeviceHandle);




            // string name = ReadRegKey(Native.HKEY_LOCAL_MACHINE, @"SYSTEM\CurrentControlSet\Control\MediaProperties\PrivateProperties\Joystick\OEM\" + "VID_" + deviceInfo.HIDInfo.VendorID.ToString("X4") + "&PID_" + deviceInfo.HIDInfo.ProductID.ToString("X4"), Native.REGSTR_VAL_JOYOEMNAME);


			//devicePath used as ID
			return new GenericHIDDevice(GetIndexForDeviceWithID(devicePath), Convert.ToInt32(deviceInfo.HIDInfo.VendorID), Convert.ToInt32(deviceInfo.HIDInfo.ProductID),devicePath, rawInputDeviceList.DeviceHandle, this, devicePath);

            //this have problems with   
            // return GetHIDDeviceInfo(GetDevicePath(rawInputDeviceList.DeviceHandle));
        }





        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        private static IntPtr GetDeviceData(IntPtr deviceHandle, Native.RawInputDeviceInfoCommand command)
        {
            uint dataSize = 0;
            var ptrData = IntPtr.Zero;

            Native.GetRawInputDeviceInfo(
                deviceHandle,
                command,
                ptrData,
                ref dataSize);

            if (dataSize == 0) return IntPtr.Zero;

            ptrData = Marshal.AllocHGlobal((int)dataSize);

            var result = Native.GetRawInputDeviceInfo(
                deviceHandle,
                command,
                ptrData,
                ref dataSize);

            if (result == 0)
            {
                Marshal.FreeHGlobal(ptrData);
                return IntPtr.Zero;
            }

            return ptrData;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <returns></returns>
        private static string GetDevicePath(IntPtr deviceHandle)
        {
            var ptrDeviceName = GetDeviceData(
                deviceHandle,
                Native.RawInputDeviceInfoCommand.DeviceName);

            if (ptrDeviceName == IntPtr.Zero)
            {
                return string.Empty;
            }

            var deviceName = Marshal.PtrToStringAnsi(ptrDeviceName);
            Marshal.FreeHGlobal(ptrDeviceName);
            return deviceName;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="deviceHandle"></param>
        /// <returns></returns>
        private static Native.DeviceInfo GetDeviceInfo(IntPtr deviceHandle)
        {
            var ptrDeviceInfo = GetDeviceData(
                deviceHandle,
                Native.RawInputDeviceInfoCommand.DeviceInfo);

            if (ptrDeviceInfo == IntPtr.Zero)
            {
                return new Native.DeviceInfo();
            }

            Native.DeviceInfo deviceInfo = (Native.DeviceInfo)Marshal.PtrToStructure(
                ptrDeviceInfo, typeof(Native.DeviceInfo));

            Marshal.FreeHGlobal(ptrDeviceInfo);
            return deviceInfo;
        }


		/// <summary>
		/// Gets the index for device with ID.
		/// </summary>
		/// <returns>Old index for device after reconnection or new if first connection.</returns>
		/// <param name="ID">I.</param>
		int GetIndexForDeviceWithID(string ID){
			


          
                int inx=-1;



                if (__ports != null && __ports.Length > 0)
                {
                    //find if this device was using same port before (during same app runitime)
                    inx = Array.IndexOf(__ports, ID);

                    if (inx < 0)//if not found => use next available(20 ports in total) position
                        inx = Array.IndexOf(__ports, null);

                    __ports[inx] = ID;
                }

                return inx;

            
		
			
		}


        /// <summary>
        /// Try to attach compatible driver based on device PID and VID
        /// </summary>
        /// <param name="hidDevice"></param>
        protected void ResolveDevice(HIDDevice hidDevice)
        {

            IDevice joyDevice = null;



            //loop thru drivers and attach the driver to device if compatible
            if (__drivers != null)
                foreach (var driver in __drivers)
                {



                    joyDevice = driver.ResolveDevice(hidDevice);
                    if (joyDevice != null)
                    {
                        joyDevice.Name = hidDevice.Name;

                        lock (syncRoot)
                        {
                            Generics[hidDevice.ID] = hidDevice;
                        }




                        Debug.Log("Device" + hidDevice.index + " PID:" + hidDevice.PID + " VID:" + hidDevice.VID + "[" + hidDevice.Name + "] attached to " + driver.GetType().ToString()

                          // +hidDevice.DevicePath 
                            );

                        break;
                    }
                }

            if (joyDevice == null)
            {//set default driver as resolver if no custom driver match device

               // System.Threading.Thread.CurrentThread.n

     

                joyDevice = defaultDriver.ResolveDevice(hidDevice);




                if (joyDevice != null)
                {
                    joyDevice.Name = hidDevice.Name;


                    lock (syncRoot)
                    {
                        Generics[hidDevice.ID] = hidDevice;
                    }






                    Debug.Log("Device" + hidDevice.index + "  PID:" + hidDevice.PID + " VID:" + hidDevice.VID + "[" + hidDevice.Name + "] attached to " + __defaultJoystickDriver.GetType().ToString() + " Path:" + hidDevice.DevicePath);

                }
                else
                {
                    Debug.LogWarning("Device PID:" + hidDevice.PID + " VID:" + hidDevice.VID + " not found compatible driver thru WinHIDInterface!");

                }

            }



            if (joyDevice != null && DeviceConnectEvent!=null) DeviceConnectEvent(null, new DeviceEventArgs<IDevice>(joyDevice));


        }












        public void Dispose()
        {
            int error = 0;


            UnityEngine.Debug.Log("Dispose() Thread id:"+System.Threading.Thread.CurrentThread.ManagedThreadId);

			UnityEngine.Debug.Log("Thread: System.Threading.Thread.CurrentThread.ManagedThreadId");

            UnityEngine.Debug.Log("Try to dispose NotificationHandle");
            UnregisterHIDDeviceNotification();

            error = Marshal.GetLastWin32Error();
            if (error > 0)

                UnityEngine.Debug.Log(" NotificationHandle Erorr" + error);

            UnityEngine.Debug.Log("Try to dispose receiverWindowHandle");


            if (hidDeviceNotificationReceiverWindowHandle != IntPtr.Zero)
            {
                try
                {


                    UnityEngine.Debug.Log("Try to destroy plug&play Notificaiton window");


                    if (Application.isPlaying)
                    {
                        IntPtr wndHnd = Native.FindWindow(NOTIFICATION_WND_CLS, null);

                        if (wndHnd == IntPtr.Zero)
                            UnityEngine.Debug.Log("wndow not found");
                        else
                        {
                            bool result = Native.DestroyWindow(wndHnd);

                            UnityEngine.Debug.Log("Destroy Result " + result);
                        }
                    }

                    //!!! Problem with InputMapper EditorWindow doesn't clean close Notification Window
                    //so several open close or playing Aplication which also try to create Notification Window
                    //cousing Unity to blow (No solution yet)
                 
                  //  if (Application.isPlaying)
                   //    Native.PostMessage(new HandleRef(this, this.hidDeviceNotificationReceiverWindowHandle), Native.WM_DESTROY, IntPtr.Zero, IntPtr.Zero);
                    error = Marshal.GetLastWin32Error();
                    if (error > 0)

                        UnityEngine.Debug.Log(" Destroy Erorr" + error);

                    hidDeviceNotificationReceiverWindowHandle = IntPtr.Zero;


                }
                catch (Exception ex)
                {


                    UnityEngine.Debug.LogError(Native.GetLastError());
                    UnityEngine.Debug.LogException(ex);
                }

                //
            }

            UnityEngine.Debug.Log("Try to clean Genrics");

            lock (syncRoot)
            {
                if (__Generics != null)
                {
                    foreach (KeyValuePair<string, HIDDevice> entry in __Generics)
                    {
                        entry.Value.Dispose();
                    }

                    __Generics.Clear();
                }

                error = Marshal.GetLastWin32Error();
                if (error > 0)

                    UnityEngine.Debug.Log(" NotificationHandle Erorr" + error);

                Debug.Log("Try to remove Drivers");
                if (__drivers != null) __drivers.Clear();
            }

            if (error > 0)

                UnityEngine.Debug.Log(" NotificationHandle Erorr" + error);


        }








    }

}

#endif