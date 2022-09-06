using System;
using System.Diagnostics;
using System.IO.Ports;

namespace ITM3124ProgrammableDCPowerSupply
{
    public class SerialITM3124Device
    {
        private SerialPort serialPort;
        public  SerialITM3124Device()
        {
            try
            {
                // automatically find ports by Name
                string FindPorts = RunProcess("IT-M3124"); // Using the autofind to find the unit's
                string[] Ports = FindPorts.Split(';'); // Split the AutoFind string into individual ports

                //Initialise COM port with all settings.
                foreach (var Port in Ports)
                {
                    serialPort = new SerialPort();
                    serialPort.PortName = Port;
                    serialPort.BaudRate = 115200;
                    serialPort.DataBits = 8;
                    serialPort.StopBits = StopBits.One;
                    serialPort.Parity = Parity.None;
                    serialPort.Handshake = Handshake.None;

                    serialPort.DataReceived += new SerialDataReceivedEventHandler(this.serialPort_DataReceived);
                    serialPort.ReadTimeout = 100; // Required for end of packet Timeout notification
                    serialPort.Open();
                }
            }
            // Exception Handling
            catch
            {
            }

        }
        private string RunProcess(String command)
        {
            // Create a new process object
            Process ProcessObj = new Process();

            // StartInfo contains the startup information of
            // the new process
            ProcessObj.StartInfo.FileName = command;
            ProcessObj.StartInfo.Arguments = "";

            // These two optional flags ensure that no DOS window
            // appears
            ProcessObj.StartInfo.UseShellExecute = false;
            ProcessObj.StartInfo.CreateNoWindow = true;

            // This ensures that you get the output from the DOS application
            ProcessObj.StartInfo.RedirectStandardOutput = true;

            // Start the process
            ProcessObj.Start();

            // Wait that the process exits
            ProcessObj.WaitForExit();

            // Now read the output of the DOS application
            return ProcessObj.StandardOutput.ReadToEnd();
        }
        private void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {

                //I not sure it's working or not because of I do not know power supply default packet Id
                //but I try to Initialized Deviced to correct porotocal and get to resulted
                // Read all the current buffer
                String[] spearator = { "|" };
                String[] StatusList = this.serialPort.ReadByte().ToString().Split(spearator, 3, StringSplitOptions.RemoveEmptyEntries);

                string VoltageOutPut = StatusList[0];//for actual Voltage output               
                string CurrentOutPut = StatusList[1];//for actual Current output

                // Parse power supply data
                // I can't test it. 
                PowerSupplyData PSD = new PowerSupplyData();
                PSD.SetCurrent(Convert.ToDouble(CurrentOutPut));
                PSD.SetVoltage(Convert.ToDouble(VoltageOutPut));
            }
            catch //(TimeoutException x)
            {
            }
        }
    }
}
