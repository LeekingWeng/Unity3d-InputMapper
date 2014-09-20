﻿#if UNITY_STANDALONE_WIN || UNITY_EDITOR
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Threading;
using System.Runtime.InteropServices;
using ws.winx.devices;
using System.Timers;
using System.Diagnostics;
namespace ws.winx.platform.windows
{
    public enum DeviceMode
    {
        NonOverlapped = 0,
        Overlapped = 1
    }


    public class HidAsyncState
    {
        private readonly object _callerDelegate;
        private readonly object _callbackDelegate;

        public HidAsyncState(object callerDelegate, object callbackDelegate)
        {
            _callerDelegate = callerDelegate;
            _callbackDelegate = callbackDelegate;
        }

        public object CallerDelegate { get { return _callerDelegate; } }
        public object CallbackDelegate { get { return _callbackDelegate; } }
    }


   



    public class GenericHIDDevice :  HIDDevice,IDisposable
    {


        private DeviceMode _deviceReadMode = DeviceMode.Overlapped;

        public DeviceMode DeviceReadMode
        {
            get { return _deviceReadMode; }
            set { _deviceReadMode = value; }
        }
        private DeviceMode _deviceWriteMode = DeviceMode.Overlapped;

        public DeviceMode DeviceWriteMode
        {
            get { return _deviceWriteMode; }
            set { _deviceWriteMode = value; }
        }

        protected delegate HIDReport ReadDelegate();
        private delegate bool WriteDelegate(byte[] data);
        
        public IntPtr ReadHandle { get; private set; }
        public IntPtr WriteHandle { get; private set; }
        public bool IsOpen { get; private set; }
        volatile bool IsReadInProgress = false;

   

      
        private HIDReport __lastHIDReport;


        private int _InputReportByteLength=8;

        override public int InputReportByteLength
        {
            get { return _InputReportByteLength; }
            set {
                if (value < 2) throw new Exception("InputReportByteLength should be >1 ");  _InputReportByteLength = value; }
        }
        private int _OutputReportByteLength=8;

        override public int OutputReportByteLength
        {
            get { return _OutputReportByteLength; }
            set { if (value < 2) throw new Exception("InputReportByteLength should be >1 ");  _OutputReportByteLength = value; }
        }



       



        public GenericHIDDevice(int index, int VID, int PID, IntPtr deviceHandle, IHIDInterface hidInterface, string devicePath, string name = ""):base(index,VID,PID,deviceHandle,hidInterface,devicePath,name)
        {
            try
            {
                var hidHandle = OpenDeviceIO(this.DevicePath,Native.ACCESS_NONE);

                CloseDeviceIO(hidHandle);

                __lastHIDReport = new HIDReport(this.index, CreateInputBuffer(),HIDReport.ReadStatus.Success);
                
            }
            catch (Exception exception)
            {
                throw new Exception(string.Format("Error querying HID device '{0}'.", devicePath), exception);
            }
        }


      


      


        #region IHIDDeviceInfo implementation




        public void OpenDevice()
        {
            OpenDevice(_deviceReadMode, _deviceWriteMode);
           // OpenDevice(DeviceMode.NonOverlapped, DeviceMode.NonOverlapped);
        }

        public void OpenDevice(DeviceMode readMode, DeviceMode writeMode)
        {
            if (IsOpen) return;

           // _deviceReadMode = readMode;
          //  _deviceWriteMode = writeMode;

            try
            {
                ReadHandle = OpenDeviceIO(this.DevicePath, readMode, Native.GENERIC_READ);
                WriteHandle = OpenDeviceIO(this.DevicePath, writeMode, Native.GENERIC_WRITE);
            }
            catch (Exception exception)
            {
                IsOpen = false;
                throw new Exception("Error opening HID device.", exception);
            }

            IsOpen = ReadHandle.ToInt32() != Native.INVALID_HANDLE_VALUE & WriteHandle.ToInt32() != Native.INVALID_HANDLE_VALUE;
        }

       

        public void CloseDevice()
        {
            if (!IsOpen) return;
            CloseDeviceIO(ReadHandle);
            CloseDeviceIO(WriteHandle);

            UnityEngine.Debug.Log("Clossing device handles");

                ReadHandle=IntPtr.Zero;
                 WriteHandle=IntPtr.Zero;
                

            IsOpen = false;
        }

        

        override public void Read(ReadCallback callback)
        {
            if (IsReadInProgress)
            {
                callback.Invoke(__lastHIDReport);
                return;
            }

            IsReadInProgress = true;

            //TODO make this fields or use pool
            var readDelegate = new ReadDelegate(Read);
            var asyncState = new HidAsyncState(readDelegate, callback);
             readDelegate.BeginInvoke(EndRead, asyncState);
        }

       
  
        //private void onReadTimeOut(object sender, ElapsedEventArgs args)
        //{
        //    UnityEngine.Debug.Log("Timeout");
        //    // cal
        //}

        protected HIDReport Read()
        {
            return Read(0);
        }

        protected HIDReport Read(int timeout)
        {
            
                if (IsOpen == false) OpenDevice();
                try
                {
                    return ReadData(timeout);
                }
                catch
                {
                    return new HIDReport(-1,null,HIDReport.ReadStatus.ReadError);
                }

           
           
        }

        public override void Write(object data, HIDDevice.WriteCallback callback)
        {

            this.Write((byte[])data, callback);

        }

        public override void Write(object data)
        {

            this.Write((byte[])data);

        }
       

        protected void Write(byte[] data,WriteCallback callback)
        {
            var writeDelegate = new WriteDelegate(Write);
            var asyncState = new HidAsyncState(writeDelegate, callback);
            writeDelegate.BeginInvoke(data, EndWrite, asyncState);
        }

        protected bool Write(byte[] data)
        {
            return Write(data, 0);
        }

        protected bool Write(byte[] data, int timeout)
        {
            
                if (IsOpen == false) OpenDevice();
                try
                {
                    return WriteData(data, timeout);
                }
                catch
                {
                    return false;
                }
            
        }

      

     

       

      

     



        override public void Dispose()
        {
           

            if (IsOpen) CloseDevice();
        }



        #endregion

        #region private

        protected void EndRead(IAsyncResult ar)
        {
           
            var hidAsyncState = (HidAsyncState)ar.AsyncState;
            var callerDelegate = (ReadDelegate)hidAsyncState.CallerDelegate;
            var callbackDelegate = (ReadCallback)hidAsyncState.CallbackDelegate;
            var data = callerDelegate.EndInvoke(ar);
           
            if ((callbackDelegate != null)) callbackDelegate.Invoke(data);
            IsReadInProgress = false;
        }

       
        private static void EndWrite(IAsyncResult ar)
        {
            var hidAsyncState = (HidAsyncState)ar.AsyncState;
            var callerDelegate = (WriteDelegate)hidAsyncState.CallerDelegate;
            var callbackDelegate = (WriteCallback)hidAsyncState.CallbackDelegate;
            var result = callerDelegate.EndInvoke(ar);

            if ((callbackDelegate != null)) callbackDelegate.Invoke(result);
        }

       

        private byte[] CreateInputBuffer()
        {
            return CreateBuffer((int)InputReportByteLength - 1);
        }

        private byte[] CreateOutputBuffer()
        {
            return CreateBuffer((int)OutputReportByteLength - 1);
        }

        

        private static byte[] CreateBuffer(int length)
        {
            byte[] buffer = null;
            Array.Resize(ref buffer, length + 1);
            return buffer;
        }

       

      

        private bool WriteData(byte[] data, int timeout)
        {
            

            var buffer = CreateOutputBuffer();
            uint bytesWritten = 0;

            Array.Copy(data, 0, buffer, 0, Math.Min(data.Length, OutputReportByteLength));

            if (_deviceWriteMode == DeviceMode.Overlapped)
            {
                
                var security = new Native.SECURITY_ATTRIBUTES();
               
                var overlapped = new NativeOverlapped();

                var overlapTimeout = timeout <= 0 ? Native.WAIT_INFINITE : timeout;

                security.lpSecurityDescriptor = IntPtr.Zero;
                security.bInheritHandle = true;
                security.nLength = Marshal.SizeOf(security);

                overlapped.OffsetLow = 0;
                overlapped.OffsetHigh = 0;
                overlapped.EventHandle = Native.CreateEvent(ref security, Convert.ToInt32(false), Convert.ToInt32(true), "");

                bool success;

                
                    success = Native.WriteFile(WriteHandle, buffer, (uint)buffer.Length, out bytesWritten, ref overlapped);
                    UnityEngine.Debug.Log("WriteFile happend " + success + " " + bytesWritten);

                    if (Marshal.GetLastWin32Error() > 0)
                    {
                        UnityEngine.Debug.LogWarning("Error during Write Data"+Marshal.GetLastWin32Error());
                    }
              
               // UnityEngine.Debug.LogError(Marshal.GetLastWin32Error());


                if (!success && Marshal.GetLastWin32Error() == Native.ERROR_IO_PENDING)
                {
                    var result = Native.WaitForSingleObject(overlapped.EventHandle, overlapTimeout);

                    switch (result)
                    {
                        case Native.WAIT_OBJECT_0:
                          
                            // System.Threading.Overlapped.Unpack(overlapped);
                            return true;
                        case Native.WAIT_TIMEOUT:
                            UnityEngine.Debug.Log("WriteData WAIT_TIMEOUT");
                            return false;
                        case Native.WAIT_FAILED:
                            UnityEngine.Debug.Log("WriteData WAIT_FAILED");
                            return false;
                        default:
                            return false;
                    }
                }

                return success;
            }
            else
            {
                try
                {
                    var overlapped = new NativeOverlapped();
                    bool success;
                    success = Native.WriteFile(WriteHandle, buffer, (uint)buffer.Length, out bytesWritten, ref overlapped);

                    //if (!success)
                    //{
                    //    UnityEngine.Debug.LogError(Marshal.GetLastWin32Error());
                    //}

                    return success;

                }
                catch { return false; }
            }
        }

        protected HIDReport ReadData(int timeout)
        {
            var buffer = new byte[] { };
            var status = HIDReport.ReadStatus.NoDataRead;
            int error = 0;

            if (InputReportByteLength > 0)
            {
                uint bytesRead = 0;

                buffer = CreateInputBuffer();

                if (_deviceReadMode == DeviceMode.Overlapped)
                {
                    var security = new Native.SECURITY_ATTRIBUTES();
                    var overlapped = new NativeOverlapped();
                    var overlapTimeout = timeout <= 0 ? Native.WAIT_INFINITE : timeout;

                    security.lpSecurityDescriptor = IntPtr.Zero;
                    security.bInheritHandle = true;
                    security.nLength = Marshal.SizeOf(security);

                    overlapped.OffsetLow = 0;
                    overlapped.OffsetHigh = 0;
                    overlapped.EventHandle = Native.CreateEvent(ref security, Convert.ToInt32(false), Convert.ToInt32(true), string.Empty);

                    try
                    {
                       var success=Native.ReadFile(ReadHandle, buffer, (uint)buffer.Length, out bytesRead, ref overlapped);

                       UnityEngine.Debug.Log("Read happend " + success + " " + bytesRead);

                        error=Marshal.GetLastWin32Error();

                       if (error > 0)
                       {
                           UnityEngine.Debug.LogWarning("Error during Read Data" + error);
                       }



                       if (!success && (error == Native.ERROR_IO_PENDING || bytesRead < buffer.Length))
                       {
                           UnityEngine.Debug.LogWarning("Wait reading...");

                           var result = Native.WaitForSingleObject(overlapped.EventHandle, overlapTimeout);

                           switch (result)
                           {
                               case Native.WAIT_OBJECT_0: status = HIDReport.ReadStatus.Success; break;
                               case Native.WAIT_TIMEOUT:
                                   status = HIDReport.ReadStatus.WaitTimedOut;
                                   UnityEngine.Debug.Log("ReadData_WAIT_TIMEOUT");
                                   // buffer = new byte[] { };
                                   break;
                               case Native.WAIT_FAILED:
                                   status = HIDReport.ReadStatus.WaitFail;
                                   UnityEngine.Debug.Log("ReadData_WAIT_FAILED");
                                   //  buffer = new byte[] { };
                                   break;
                                
                               case Native.WAIT_ABANDONED:
                                   status = HIDReport.ReadStatus.Success;
                                    
                                    UnityEngine.Debug.Log("ReadData_WAIT_ABANDONED" + result);
                                //   buffer = new byte[] { };
                               break;

                               default:
                                    status = HIDReport.ReadStatus.NoDataRead;
                                
                                   UnityEngine.Debug.Log("ReadData Default" + result);
                                  
                                   break;
                           }
                       }
                       else
                       {
                           status = HIDReport.ReadStatus.Success;
                       }

                       
                    }
                    catch { status = HIDReport.ReadStatus.ReadError; }
                    finally { CloseDeviceIO(overlapped.EventHandle); }
                }
                else
                {
                    try
                    {
                        var overlapped = new NativeOverlapped();

                        Native.ReadFile(ReadHandle, buffer, (uint)buffer.Length, out bytesRead, ref overlapped);
                        status = HIDReport.ReadStatus.Success;
                    }
                    catch { status = HIDReport.ReadStatus.ReadError; }
                }
            }


            __lastHIDReport.Data = buffer;

            __lastHIDReport.index = this.index;

            __lastHIDReport.Status = status;


            return __lastHIDReport;// new HIDReport(this.index, buffer, status);
        }

        private static IntPtr OpenDeviceIO(string devicePath, uint deviceAccess)
        {
            return OpenDeviceIO(devicePath, DeviceMode.Overlapped, deviceAccess);
           // return OpenDeviceIO(devicePath, DeviceMode.NonOverlapped, deviceAccess);
        }

        private static IntPtr OpenDeviceIO(string devicePath, DeviceMode deviceMode, uint deviceAccess)
        {
            var security = new Native.SECURITY_ATTRIBUTES();
            var flags = 0;

            if (deviceMode == DeviceMode.Overlapped) flags = Native.FILE_FLAG_OVERLAPPED;

            security.lpSecurityDescriptor = IntPtr.Zero;
            security.bInheritHandle = true;
            security.nLength = Marshal.SizeOf(security);


            //Handle = CreateFile(didetail->DevicePath, GENERIC_READ|GENERIC_WRITE,
												//	FILE_SHARE_READ,
												//	NULL, OPEN_EXISTING,
												//	FILE_FLAG_OVERLAPPED, NULL);

            return Native.CreateFile(devicePath, deviceAccess, Native.FILE_SHARE_READ | Native.FILE_SHARE_WRITE, ref security, Native.OPEN_EXISTING, flags, 0);
        }

        private static void CloseDeviceIO(IntPtr handle)
        {
            if (Environment.OSVersion.Version.Major > 5)
            {
                Native.CancelIoEx(handle, IntPtr.Zero);
            }
            Native.CloseHandle(handle);
        }

        #endregion
    }
}
#endif