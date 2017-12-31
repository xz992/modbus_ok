using System;
using System.IO.Ports;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using FtdAdapter;
using Modbus.Data;
using Modbus.Device;
using Modbus.Utility;

namespace MySample
{
	/// <summary>
	/// Demonstration of NModbus
	/// </summary>
	public class Driver
	{
		static void Main(string[] args)
		{
           log4net.Config.XmlConfigurator.Configure();
		    try
			{
	     		ModbusSerialRtuMasterWriteRegisters();
            
            }
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			Console.ReadKey();
		}

		/// <summary>
		/// Simple Modbus serial RTU master write holding registers example.
		/// </summary>
		public static void ModbusSerialRtuMasterWriteRegisters()
		{
			using (SerialPort port = new SerialPort("COM5"))
			{
				// configure serial port
				port.BaudRate = 115200;
				port.DataBits = 8;
				port.Parity = Parity.None;
				port.StopBits = StopBits.One;
                port.ReadTimeout = 1000;
                
                port.Open();

				// create modbus master
				IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

				byte slaveId = 1;
				ushort startAddress = 0;
				ushort[] registers = new ushort[] { 111, 222, 333 };

				// write three registers

                 
                    master.WriteMultipleRegisters(slaveId, startAddress, registers);

             
               // master.WriteMultipleCoils(slaveId, startAddress, new bool[] { true, false, true });

			}
		}


		/// <summary>
		/// Simple Modbus serial USB RTU master write multiple coils example.
		/// </summary>
		public static void ModbusSerialUsbRtuMasterWriteCoils()
		{
			using (FtdUsbPort port = new FtdUsbPort())
			{
				// configure usb port
				port.BaudRate = 9600;
				port.DataBits = 8;
				port.Parity = FtdParity.None;
				port.StopBits = FtdStopBits.One;
				port.OpenByIndex(0);

				// create modbus master
				IModbusSerialMaster master = ModbusSerialMaster.CreateRtu(port);

				byte slaveId = 1;
				ushort startAddress = 1;

				// write three coils
				master.WriteMultipleCoils(slaveId, startAddress, new bool[] { true, false, true });
			}
		}
		
		}
}
