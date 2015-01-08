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
using ws.winx.platform;
using System.Runtime.InteropServices;
using ws.winx.devices;
using UnityEngine;
using ws.winx.input;



namespace ws.winx.drivers
{
    public class XInputDriver : IDriver
    {

 

       


        public enum XTYPE : int
        {
            XBOX = 0,
            XBOX360,
            XBOX360W
        }


       /// <summary>
       /// 
       /// </summary>
        public XInputDriver()
        {
           

        }

        int[] XPAD_DEVICE = new int[] {
                 0x045e, 0x02A1, /*My XBOX test Xbox 360 Wireless Receiver */(int)XTYPE.XBOX360W,
 	             0x045e, 0x0202, /* "Microsoft X-Box pad v1 (US)",0*/  (int)XTYPE.XBOX ,
	             0x045e, 0x0289, /* "Microsoft X-Box pad v2 (US)", 0*/ (int)XTYPE.XBOX ,
 	             0x045e, 0x0285,  /*"Microsoft X-Box pad (Japan)", 0*/ (int)XTYPE.XBOX ,
 	             0x045e, 0x0287,  /*"Microsoft Xbox Controller S", 0*/(int) XTYPE.XBOX ,
	             0x045e, 0x0289,  /*"Microsoft X-Box pad v2 (US)", 0*/ (int)XTYPE.XBOX ,
	             0x045e, 0x028e, /*Microsoft X-Box 360 pad", 0,*/ (int)XTYPE.XBOX360, 
	             0x045e, 0x0291,/*Xbox 360 Wireless Receiver (XBOX)", MAP_DPAD_TO_BUTTONS*/(int) XTYPE.XBOX360W ,
 	             0x045e, 0x0719,/*Xbox 360 Wireless Receiver", MAP_DPAD_TO_BUTTONS*/(int) XTYPE.XBOX360W ,
	             0x0c12, 0x8809,/*RedOctane Xbox Dance Pad", DANCEPAD_MAP_CONFIG*/ (int)XTYPE.XBOX, 
 	             0x044f, 0x0f07,/*Thrustmaster, Inc. Controller", 0*/ (int)XTYPE.XBOX ,
 	             0x046d, 0xc242,/*Logitech Chillstream Controller", 0*/ (int)XTYPE.XBOX360 ,
 	             0x046d, 0xca84,/*Logitech Xbox Cordless Controller", 0*/ (int)XTYPE.XBOX ,
 	             0x0738, 0x4540,/*Mad Catz Beat Pad", MAP_DPAD_TO_BUTTONS*/ (int)XTYPE.XBOX ,
 	             0x0738, 0x4556,/*Mad Catz Lynx Wireless Controller", 0*/ (int)XTYPE.XBOX ,
 	             0x0738, 0x4716,/*Mad Catz Wired Xbox 360 Controller", */ (int)XTYPE.XBOX360 ,
	             0x0738, 0x4728,/*Mad Catz Street Fighter IV FightPad", XTYPE_XBOX360 
 	             0x0738, 0x4738,/*Mad Catz Wired Xbox 360 Controller (SFIV)", MAP_TRIGGERS_TO_BUTTONS,*/ (int)XTYPE.XBOX360 ,
 	             0x0738, 0x6040,/*Mad Catz Beat Pad Pro", MAP_DPAD_TO_BUTTONS,*/ (int)XTYPE.XBOX, 
	             0x0738, 0xbeef,/*Mad Catz JOYTECH NEO SE Advanced GamePad",*/ (int)XTYPE.XBOX360 ,
 	             0x0c12, 0x8802,/*Zeroplus Xbox Controller", */ (int)XTYPE.XBOX, 
	             0x0c12, 0x8809,/*RedOctane Xbox Dance Pad", DANCEPAD_MAP_CONFIG,*/ (int)XTYPE.XBOX, 
 	             0x0c12, 0x880a,/*Pelican Eclipse PL-2023", */ (int)XTYPE.XBOX, 
 	             0x0c12, 0x8810,/*Zeroplus Xbox Controller", */ (int)XTYPE.XBOX, 
 	             0x0c12, 0x9902,/*HAMA VibraX - *FAULTY HARDWARE*", */ (int)XTYPE.XBOX, 
 	             0x0e6f, 0x0003,/*Logic3 Freebird wireless Controller", */ (int)XTYPE.XBOX, 
 	             0x0e6f, 0x0005,/*Eclipse wireless Controller", */ (int)XTYPE.XBOX, 
 	             0x0e6f, 0x0006,/*Edge wireless Controller", */ (int)XTYPE.XBOX, 
	             0x0e6f, 0x0006,/*Pelican 'TSZ' Wired Xbox 360 Controller", */ (int)XTYPE.XBOX360 ,
	             0x0e6f, 0x0105,/*HSM3 Xbox360 dancepad", MAP_DPAD_TO_BUTTONS,*/ (int)XTYPE.XBOX360 ,
 	             0x0e6f, 0x0201,/*Pelican PL-3601 'TSZ' Wired Xbox 360 Controller", */ (int)XTYPE.XBOX360 ,
	             0x0e6f, 0x0213,/*Afterglow Gamepad for Xbox 360", */ (int)XTYPE.XBOX360 ,
 	             0x0e8f, 0x0201,/*SmartJoy Frag Xpad/PS2 adaptor", */ (int)XTYPE.XBOX, 
	             0x0f0d, 0x000d,/*Hori Fighting Stick EX2", MAP_TRIGGERS_TO_BUTTONS,*/ (int)XTYPE.XBOX360 ,
	             0x0f0d, 0x0016,/*Hori Real Arcade Pro.EX", MAP_TRIGGERS_TO_BUTTONS, */(int)XTYPE.XBOX360 ,
 	             0x0f30, 0x0202,/*Joytech Advanced Controller", */ (int)XTYPE.XBOX, 
 	             0x0f30, 0x8888,/*BigBen XBMiniPad Controller", */ (int)XTYPE.XBOX, 
 	             0x102c, 0xff0c,/*Joytech Wireless Advanced Controller", */ (int)XTYPE.XBOX, 
	             0x12ab, 0x8809,/*Xbox DDR dancepad", MAP_DPAD_TO_BUTTONS, */(int)XTYPE.XBOX, 
 	             0x12ab, 0x0004,/*Honey Bee Xbox360 dancepad", MAP_DPAD_TO_BUTTONS,*/ (int)XTYPE.XBOX360 ,
	             0x0e6f, 0x0105,/*HSM3 Xbox360 dancepad", MAP_DPAD_TO_BUTTONS,*/ (int)XTYPE.XBOX360 ,
	             0x12ab, 0x8809,/*Xbox DDR dancepad", MAP_DPAD_TO_BUTTONS,*/ (int)XTYPE.XBOX, 
 	             0x1430, 0x4748,/*RedOctane Guitar Hero X-plorer", */ (int)XTYPE.XBOX360 ,
 	             0x1430, 0x8888,/*TX6500+ Dance Pad (first generation)", MAP_DPAD_TO_BUTTONS, (int)XTYPE.XTYPE_XBOX, 
 	             0x146b, 0x0601,/*BigBen Interactive XBOX 360 Controller", */ (int)XTYPE.XBOX360 ,
	             0x045e, 0x028e,/*Microsoft X-Box 360 pad", */ (int)XTYPE.XBOX360 ,
	             0x1689, 0xfd00,/*Razer Onza Tournament Edition", MAP_DPAD_TO_BUTTONS,*/ (int)XTYPE.XBOX360 ,
 	             0x1bad, 0x0002,/*Harmonix Rock Band Guitar", */ (int)XTYPE.XBOX360 ,
 	             0x1bad, 0x0003,/*Harmonix Rock Band Drumkit", MAP_DPAD_TO_BUTTONS, */(int)XTYPE.XBOX360 ,
	             0x0f0d, 0x0016,/*Hori Real Arcade Pro.EX", MAP_TRIGGERS_TO_BUTTONS,*/ (int)XTYPE.XBOX360 ,
	             0x0f0d, 0x000d,/*Hori Fighting Stick EX2", MAP_TRIGGERS_TO_BUTTONS, */(int)XTYPE.XBOX360 ,
	             0x1689, 0xfd00,/*Razer Onza Tournament Edition", MAP_DPAD_TO_BUTTONS,*/ (int)XTYPE.XBOX360 ,
	             0x1bad, 0xf016,/*Mad Catz Xbox 360 Controller", */ (int)XTYPE.XBOX360 ,
	             0x1bad, 0xf028,/*Street Fighter IV FightPad", */ (int)XTYPE.XBOX360 ,
	             0x1bad, 0xf901,/*Gamestop Xbox 360 Controller", */ (int)XTYPE.XBOX360 ,
	             0x1bad, 0xf903,/*Tron Xbox 360 controller", */ (int)XTYPE.XBOX360 ,
	             0x24c6, 0x5300,/*PowerA MINI PROEX Controller", */ (int)XTYPE.XBOX360 ,
 	             0xffff, 0xffff,/*Chinese-made Xbox Controller", */ (int)XTYPE.XBOX
                 
 	            
 };
        private IHIDInterface _hidInterface;







        #region IJoystickDriver implementation

        public void Update(IDevice device)
		 {


            if (device!=null && _hidInterface!=null && _hidInterface.Contains(device.ID))
            {

                //  Debug.Log("Update Joy"+device.Index);
                HIDReport data = _hidInterface.ReadBuffered(device.ID);

                if(data!=null)
                onRead(data);

            }


        }


		#if UNITY_STANDALONE_OSX

		//!!! Colin Munro tattlebogie didn't carry about correct xbox wireless controller ReportDescriptor
		//so buttons and axis are mixed reversed and so on
		//The best option to have just driver for MS usb wirelless receiver and use direct reading of XBOx conntroller.
		//I have 3 options redo driver(you would dowload 100% from his site and not my version) or change XInputDriver.cs.


		void onRead(object data)
		{

			HIDReport report = data as HIDReport;
			IDevice device = InputEx.Devices.GetDeviceAt (report.index);// _hidInterface.Devices[report.index];
			
			//  UnityEngine.Debug.Log("report.index"+report.index+"device.PID"+device.PID+" Name:"+device.Name);
			// UnityEngine.Debug.Log(BitConverter.ToString(report.Data));
			
			if (report.Data!=null && (report.Status == HIDReport.ReadStatus.Success || report.Status == HIDReport.ReadStatus.Buffered))
			{
				byte[] buff=report.Data;


				//|||||Forward|Backward|Left|Right|Start|Back|||Left Bumper|Right Bumper|xbox|A|B|X|Y|
				//	Left Stick X|Left Stick Y|Right Stick X|Right Stick X|Left Trigger|Right Trigger

				////byte 11
				///////////////////////////////////// BUTTONS ////////////////////////////////////////
	
				device.Buttons[9].value = (buff[2] & 0x10) != 0 ? 1f : 0f;//Start

				device.Buttons[10].value = (buff[2] & 0x20) != 0 ? 1f : 0f;//Back

				device.Buttons[13].value = (buff[3] & 0x02) != 0 ? 1f : 0f;//Left Bumper

				device.Buttons[14].value = (buff[3] & 0x01) != 0 ? 1f : 0f;//Right Bumper

				device.Buttons[16].value = (buff[3] & 0x10) != 0 ? 1f : 0f;//A

				device.Buttons[17].value = (buff[3] & 0x20) != 0 ? 1f : 0f;//B

				device.Buttons[18].value = (buff[3] & 0x40) != 0 ? 1f : 0f;//X

				device.Buttons[19].value = (buff[3] & 0x80) != 0 ? 1f : 0f;//Y



				////////////////////////////////  POV ////////////////////////////////////////
				float x = 0, y = 0;


				int pov=buff[2] & 0x0F;

				if(pov>0){
					if((pov & 0xC)!=0){

						if((pov&0x8)!=0) x=1;
						   else x=-1;
					}


					if((pov & 0x3) !=0) {
						if((pov & 0x1) !=0)  y=1 ;
						else y=-1;

					}
				}
				 
				
				
				
				device.Axis[JoystickAxis.AxisPovX].value = x;
				device.Axis[JoystickAxis.AxisPovY].value = y;
				
				// UnityEngine.Debug.Log("x=" + x+" y="+y);


				IAxisDetails axisDetails;
				float value;
				value=(short)(buff[8] | (buff[9] << 8));
				// UnityEngine.Debug.Log("raw valueX=" + value);
				axisDetails = device.Axis[JoystickAxis.AxisY];
				axisDetails.value = NormalizeAxis(value, -32727, 32727, 0.1f);


				value=(short)(buff[6] | (buff[7] << 8));
				// UnityEngine.Debug.Log("raw valueX=" + value);
				axisDetails = device.Axis[JoystickAxis.AxisX];
				axisDetails.value = NormalizeAxis(value, -32727, 32727, 0.1f);

				value=(short)(buff[10] | (buff[11] << 8));
				// UnityEngine.Debug.Log("raw valueX=" + value);
				axisDetails = device.Axis[JoystickAxis.AxisZ];
				axisDetails.value = NormalizeAxis(value, -32727, 32727, 0.1f);



				value=(short)(buff[12] | (buff[13] << 8));
				// UnityEngine.Debug.Log("raw valueX=" + value);
				axisDetails = device.Axis[JoystickAxis.AxisR];
				axisDetails.value = NormalizeAxis(value, -32727, 32727, 0.1f);

				//byte 9,10??? (triggerL 80->FF  and triggerR 80->00)
				value=buff[4];
				
				axisDetails = device.Axis[JoystickAxis.AxisV];

				axisDetails.value =1 - NormalizeTrigger(value, 0, 255,0.05f);

				value=buff[5];
				
				axisDetails = device.Axis[JoystickAxis.AxisU];
				
				axisDetails.value =1 - NormalizeTrigger(value, 0, 255,0.05f);

				
				//  UnityEngine.Debug.Log("LTigger=" + axisDetails.value);


				//UnityEngine.Debug.Log("Normal:"+BitConverter.ToString(report.Data)+" "+axisDetails.value);

				return;
			}
		}
		#endif
		
		
		#if UNITY_STANDALONE_WIN
		void onRead(object data)
		{
			
			
			HIDReport report = data as HIDReport;
			IDevice device = InputEx.Devices.GetDeviceAt (report.index);// _hidInterface.Devices[report.index];
			
			//  UnityEngine.Debug.Log("report.index"+report.index+"device.PID"+device.PID+" Name:"+device.Name);
			// UnityEngine.Debug.Log(BitConverter.ToString(report.Data));

            if (report.Data!=null && (report.Status == HIDReport.ReadStatus.Success || report.Status == HIDReport.ReadStatus.Buffered))
            {
              
                byte[] buff = report.Data;

            //C3-85-47-7B-2A-76-6D-7A-00-80-00-80-00-8E
               // UnityEngine.Debug.Log("Normal:"+BitConverter.ToString(report.Data));

		

            //controlTransfer(0x21, 0x09, 0x0240, 0, __outputBuffer, __outputBuffer.length, 0) > -1;

            //pad are last three bytes  (demo left click)
            //    82-81-47-7B-2A-76-6D-7A-00-80-00-9C-00-8E
            //    82-81-47-7B-2A-76-6D-7A-00-80-00-80-10-D0
            //    82-81-47-7B-2A-76-6D-7A-00-80-00-9C-80-E6
            //    82-81-47-7B-2A-76-6D-7A-00-80-00-80-00-8E




             ////byte 11
            ///////////////////////////////////// BUTTONS ////////////////////////////////////////
                //device.Buttons[9].value = (buff[11] & 0x80) != 0 ? 1f : 0f;//Start
                //device.Buttons[10].value =  (buff[11] & 0x40) != 0 ? 1f : 0f;//Back
                //device.Buttons[13].value = (buff[11] & 0x10) != 0 ? 1f : 0f;//Left Bumper
                //device.Buttons[14].value = (buff[11] & 0x20) != 0 ? 1f : 0f;//Right Bumper
                //device.Buttons[16].value = (buff[11] & 0x01) != 0 ? 1f : 0f;//A
                //device.Buttons[17].value = (buff[11] & 0x02) != 0 ? 1f : 0f;//B
                //device.Buttons[18].value = (buff[11] & 0x04) != 0 ? 1f : 0f;//X
                //device.Buttons[19].value = (buff[11] & 0x08) != 0 ? 1f : 0f;//Y


                device.Buttons[7].value = (buff[11] & 0x80) != 0 ? 1f : 0f;//Start
                device.Buttons[6].value = (buff[11] & 0x40) != 0 ? 1f : 0f;//Back
                device.Buttons[4].value = (buff[11] & 0x10) != 0 ? 1f : 0f;//Left Bumper
                device.Buttons[5].value = (buff[11] & 0x20) != 0 ? 1f : 0f;//Right Bumper
                device.Buttons[0].value = (buff[11] & 0x01) != 0 ? 1f : 0f;//A
                device.Buttons[1].value = (buff[11] & 0x02) != 0 ? 1f : 0f;//B
                device.Buttons[2].value = (buff[11] & 0x04) != 0 ? 1f : 0f;//X
                device.Buttons[3].value = (buff[11] & 0x08) != 0 ? 1f : 0f;//Y

          
				//00-13-00-00-00-00-62-F2-27-FF-59-03-45-FE-00
           


           
                



            ////byte 12

                // 80   1000 0000 -neutral

                // AO   1010 0000 LT(-,+)
                // 84   1000 0100  Y+
                // 88   1000 1000 RT(+,+)
                // 8C   1000 1100  X+

                // 90   1001 0000  RB(+,-)
                // 94   1001 0100  Y-
                // 98   1001 1000  LB(-,-)
                // 9C   1001 1100  X-

                // 00   00 0000 -neutral

                // 2O   10 0000 LT(-,+)
                // 04   00 0100  Y+
                // 08   00 1000 RT(+,+)
                // 0C   00 1100  X+

                // 10   01 0000  RB(+,-)
                // 14   01 0100  Y-
                // 18   01 1000  LB(-,-)
                // 1C   01 1100  X-



            //    //////////////////////////////  POV ////////////////////////////////////////
                float x = 0, y = 0;
                int sign = 1;

                buff[12] =(byte)( buff[12] & 0x3F);

                if(buff[12] != 0x00){
                    if ((buff[12] >> 4) == 1)
                    {
                        sign = -1;
                        
                    }
                   


                    switch (buff[12] & 0x0C)
                    {
                        case 0x0:
                            x = sign * (-1);
                            y=sign * 1;
                            break;

                        case 0x4:
                            y = sign * 1;
                            break;

                        case 0x8:
                            x = sign * 1;
                            y=sign * 1;
                            break;

                        case 0xC:
                              x = sign * 1;
                          
                            break;
                    }
                }
               

                device.Axis[JoystickAxis.AxisPovX].value = x;
                device.Axis[JoystickAxis.AxisPovY].value = y;

          //  UnityEngine.Debug.Log("x=" + x+" y="+y);


            //    ////////////////////////// AXIS //////////////////////////////////

            ////0-3 bytes left joystick
            ////4-8 bytes right joystick
            IAxisDetails axisDetails;
            float value;
           

            value=(buff[1] | (buff[2] << 8));
           // UnityEngine.Debug.Log("raw valueX=" + value);
            axisDetails = device.Axis[JoystickAxis.AxisX];
            axisDetails.value = NormalizeAxis(value, axisDetails.min, axisDetails.max, 0.1f);
          //  UnityEngine.Debug.Log("valueX=" + axisDetails.value);

            value = (buff[3] | (buff[4] << 8));
         //   UnityEngine.Debug.Log("valueY=" + value);
            axisDetails = device.Axis[JoystickAxis.AxisY];
            axisDetails.value = NormalizeAxis(value, axisDetails.min, axisDetails.max, 0.1f);
          


            value = (buff[5] | (buff[6] << 8));
            axisDetails = device.Axis[JoystickAxis.AxisU];
            axisDetails.value = NormalizeAxis(value, axisDetails.min, axisDetails.max, 0.1f);
          //  UnityEngine.Debug.Log("valueZ=" + axisDetails.value);

            value = (buff[7] | (buff[8] << 8));
            axisDetails = device.Axis[JoystickAxis.AxisR];
            axisDetails.value = NormalizeAxis(value, axisDetails.min, axisDetails.max,0.1f);
         //   UnityEngine.Debug.Log("valueR=" + axisDetails.value);

            //    00-DA-8C-DB-78-4D-85-99-5D-80-00-00-00-00-E0

             //byte 9,10??? (triggerL 80->FF  and triggerR 80->00)
            value=buff[10];

            axisDetails = device.Axis[JoystickAxis.AxisZ];
            if(value>128){

                axisDetails.value =1 - NormalizeTrigger(value-128, axisDetails.min, axisDetails.max,0.05f);
            }else axisDetails.value=0f;

         //   UnityEngine.Debug.Log("LTigger=" + axisDetails.value);


            axisDetails = device.Axis[JoystickAxis.AxisV];

            if (value < 128)
            {
                axisDetails.value = NormalizeTrigger((float)value, axisDetails.min, axisDetails.max, 0.05f);
            }
            else axisDetails.value = 0f;

          //  UnityEngine.Debug.Log("RTigger=" + axisDetails.value);



            }


         //  // SetLed((XInputDevice)device, 0x1);
         //   SetMotor((XInputDevice)device, 0xff, 0xff);

            ((JoystickDevice)device).isReady = true;
         ////   device.Write(new byte[] { 0x00, 0x01, 0x0f, 0xc0, 0x00, 0x80, 0x80, 0x00, 0x00, 0x00, 0x00, 0x00 });

          }
#endif

        public IDevice ResolveDevice(IHIDDevice hidDevice)
        //public IDevice<IAxisDetails, IButtonDetails, IDeviceExtension> ResolveDevice(IHIDDeviceInfo info)
        {
            int type = -1;
            int len = XPAD_DEVICE.Length;
            for (int inx = 0; inx < len; inx += 3)
            {
                if (hidDevice.VID == XPAD_DEVICE[inx] && hidDevice.PID == XPAD_DEVICE[inx + 1])
                {
                    type = XPAD_DEVICE[inx + 2];
                    break;
                }
            }

            if (type < 0) return null;

            XInputDevice device;
            int i = 0;

           _hidInterface = hidDevice.hidInterface;


			//check for profile
			DeviceProfile profile = null;
			
			if (hidDevice.hidInterface.Profiles.ContainsKey (hidDevice.Name)) {
				
				
				
				profile = hidDevice.hidInterface.LoadProfile (hidDevice.hidInterface.Profiles [hidDevice.Name]);
			}
           


            device = new XInputDevice(hidDevice.index, hidDevice.PID, hidDevice.VID,hidDevice.ID, 8, 20, this, type);
			device.Name = hidDevice.Name;
			device.profile = profile;

		

			
			
			//inti button structure
            for (; i < 20; i++)
            {
                device.Buttons[i] = new ButtonDetails();

				if (profile != null && profile.buttonNaming.Length > i) {
					device.Buttons [i].name = profile.buttonNaming [i];
				}
			}
			
			
			
			AxisDetails axisDetails;

            //LX
            axisDetails = new AxisDetails();
            axisDetails.max = 65535;
            axisDetails.min = 0;
            device.Axis[JoystickAxis.AxisX] = axisDetails;


            //LY
            axisDetails = new AxisDetails();
            axisDetails.max = 65535;
            axisDetails.min = 0;
            device.Axis[JoystickAxis.AxisY] = axisDetails;

            //RX
#if UNITY_STANDALONE_OSX 
            axisDetails = new AxisDetails();
            axisDetails.max = 65535;
            axisDetails.min = 0;
            device.Axis[JoystickAxis.AxisZ] = axisDetails;
#endif

 //RX
#if UNITY_STANDALONE_WIN
            axisDetails = new AxisDetails();
            axisDetails.max = 65535;
            axisDetails.min = 0;
            device.Axis[JoystickAxis.AxisU] = axisDetails;
#endif

            //RY
            axisDetails = new AxisDetails();
            axisDetails.max = 65535;
            axisDetails.min = 0;
            device.Axis[JoystickAxis.AxisR] = axisDetails;


            //TRIGGERS

            //LEFT TRIGGER
            #if UNITY_STANDALONE_OSX
            axisDetails = new AxisDetails();
            axisDetails.max = 128;
            axisDetails.min = 0;
            device.Axis[JoystickAxis.AxisU] = axisDetails;
            #endif


            //LEFT TRIGGER
            #if UNITY_STANDALONE_WIN
            axisDetails = new AxisDetails();
            axisDetails.max = 128;
            axisDetails.min = 0;
            device.Axis[JoystickAxis.AxisZ] = axisDetails;
            #endif

            //RIGHT TRIGGER
            axisDetails = new AxisDetails();
            axisDetails.max = 128;
            axisDetails.min = 0;
            device.Axis[JoystickAxis.AxisV] = axisDetails;


            //POV
            axisDetails = new AxisDetails();
            axisDetails.isHat = true;
            device.Axis[JoystickAxis.AxisPovX] = axisDetails;
            axisDetails = new AxisDetails();
            axisDetails.isHat = true;
            device.Axis[JoystickAxis.AxisPovY] = axisDetails;

            ((HIDDevice)hidDevice).InputReportByteLength = 15;
            ((HIDDevice)hidDevice).OutputReportByteLength = 12;

			int numAxis=device.Axis.Count;
			for (i=0; i < numAxis; i++) {

				if (profile != null && profile.axisNaming.Length > i) {
					device.Axis [i].name = profile.axisNaming [i];
					
				}
				
			}
			
			
			return device;
			//return (IDevice<AxisDetails, ButtonDetails, XInputExtension>)joystick;
        }
        #endregion


        /// <summary>
        ///  Normalize raw axis value to 0-1 range.
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="dreadZone"></param>
        /// <returns></returns>
        public float NormalizeTrigger(float pos, int min, int max, float dreadZone = 0.001f)
        {
            float value =1-pos / (max - min);
            if (value < dreadZone && value > -dreadZone)
                return 0;

            return value;

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="dreadZone"></param>
        /// <returns></returns>
        private float NormalizeAxis(float pos, int min, int max, float dreadZone = 0.001f)
        {
            //UnityEngine.Debug.Log(Min[axis]+" Max:"+Max[axis]);

            float offset = (2 * (pos - min)) / (max - min) - 1;
            if (offset > 1)
                return 1;
            else if (offset < -1)
                return -1;
            else if (offset < dreadZone && offset > -dreadZone)
                return 0;
            else
                return offset;
        }


      




  
        ///!!! Sorry I couldn't found yet SetMotor and SetMotor

      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="mode"></param>
        internal void SetLed(XInputDevice device, byte mode)
        {
           

            _hidInterface.Write(new byte[]{0x1, 0x3, mode},device.ID);
        
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="device"></param>
        /// <param name="leftMotor"></param>
        /// <param name="rightMotor"></param>
        internal void SetMotor(XInputDevice device, byte leftMotor, byte rightMotor)
        {

			////char buf[] = {0x00, 0x01, 0x0f, 0xc0, 0x00, large, small, 0x00, 0x00, 0x00, 0x00, 0x00};
           // _hidInterface.Write(new byte[] { 0x00, 0x01, 0x0f, 0xc0, 0x00, leftMotor, rightMotor, 0x00, 0x00, 0x00, 0x00, 0x00 }, 
//			_hidInterface.Write (new byte[]{ 0x01, 0x0f, 0xc0, 0x00, leftMotor, rightMotor},device.PID);
//			_hidInterface.Write (new byte[]{ leftMotor, rightMotor},device.PID);
//			_hidInterface.Write (new byte[]{ 0x40, leftMotor, rightMotor},device.PID);
			throw new Exception ("Motor byte combination unknown");

  
        }

        internal void onSetMotor(bool suc)
        {
            UnityEngine.Debug.Log("Motor was "+(suc? "not set." :"set."));
        }

     


        #region ButtonDetails
        public sealed class ButtonDetails : IButtonDetails
        {

            #region Fields

            float _value;
            uint _uid;
            ButtonState _buttonState;
            string _name;

            #region IDeviceDetails implementation

            public string name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                }
            }


            public uint uid
            {
                get
                {
                    return _uid;
                }
                set
                {
                    _uid = value;
                }
            }




            public ButtonState buttonState
            {
                get { return _buttonState; }
            }



            public float value
            {
                get
                {
                    return _value;
                    //return (_buttonState==JoystickButtonState.Hold || _buttonState==JoystickButtonState.Down);
                }
                set
                {

                    _value = value;
                    //if pressed==TRUE
                    //TODO check the code with triggers
                    if (value > 0)
                    {
                        if (_buttonState == ButtonState.None
                            || _buttonState == ButtonState.Up)
                        {

                            _buttonState = ButtonState.Down;



                        }
                        else
                        {
                            //if (buttonState == JoystickButtonState.Down)
                            _buttonState = ButtonState.Hold;

                        }


                    }
                    else
                    { //
                        if (_buttonState == ButtonState.Down
                            || _buttonState == ButtonState.Hold)
                        {
                            _buttonState = ButtonState.Up;
                        }
                        else
                        {//if(buttonState==JoystickButtonState.Up){
                            _buttonState = ButtonState.None;
                        }

                    }
                }
            }
            #endregion
            #endregion

            #region Constructor
            public ButtonDetails(uint uid = 0) { this.uid = uid; }
            #endregion






        }

        #endregion

        #region AxisDetails
        public sealed class AxisDetails : IAxisDetails
        {

            #region Fields
            float _value;
            int _uid;
            int _min;
            int _max;
            ButtonState _buttonState;
            bool _isNullable;
            bool _isHat;
            bool _isTrigger;
            string _name;


            #region IAxisDetails implementation
            public string name
            {
                get
                {
                    return _name;
                }
                set
                {
                    _name = value;
                }
            }


            public bool isTrigger
            {
                get
                {
                    return _isTrigger;
                }
                set
                {
                    _isTrigger = value;
                }
            }



            public int min
            {
                get
                {
                    return _min;
                }
                set
                {
                    _min = value;
                }
            }


            public int max
            {
                get
                {
                    return _max;
                }
                set
                {
                    _max = value;
                }
            }


            public bool isNullable
            {
                get
                {
                    return _isNullable;
                }
                set
                {
                    _isNullable = value;
                }
            }


            public bool isHat
            {
                get
                {
                    return _isHat;
                }
                set
                {
                    _isHat = value;
                }
            }


            #endregion


            #region IDeviceDetails implementation


            public uint uid
            {
                get
                {
                    throw new NotImplementedException();
                }
                set
                {
                    throw new NotImplementedException();
                }
            }


            #endregion

            public ButtonState buttonState
            {
                get { return _buttonState; }
            }
            public float value
            {
                get { return _value; }
                set
                {

					if (value == -1 || value==1)
					{
						if (_buttonState == ButtonState.None)
							//|| _buttonState == ButtonState.PosToUp || _buttonState==ButtonState.NegToUp)
						{
							
							_buttonState = ButtonState.Down;
							
							//Debug.Log("val:"+value+"_buttonState:"+_buttonState);
							
						}
						else
						{
							_buttonState = ButtonState.Hold;
							//Debug.Log("val:"+value+"_buttonState:"+_buttonState);
						}
	
						
					}
					else
					{
						
						if (_buttonState == ButtonState.Down
						    || _buttonState == ButtonState.Hold)
						{
							
							//if previous value was >0 => PosToUp
							if (_value>0)
								_buttonState = ButtonState.PosToUp;
							else
								_buttonState = ButtonState.NegToUp;

							Debug.Log("val:"+value+"_buttonState:"+_buttonState);
							
						}
						else
						{//if(buttonState==JoystickButtonState.Up){
							_buttonState = ButtonState.None;

							//Debug.Log("val:"+value+"_buttonState:"+_buttonState);
						}
	
						
					}


                    _value = value;



                }//set
            }

            #endregion

        }

        #endregion



        sealed class XInputExtension : IDeviceExtension
        {

        }




    }
}

