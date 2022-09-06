using System;

namespace ITM3124ProgrammableDCPowerSupply
{
    public class PowerSupplyData
    {

        //Output State storage variables
        private   double VoltageOutPut = 0.00;
        private  double CurrentOutPut = 0.00;
        public double MinimumVoltage => 0.0;
        public double MaximumVoltage => 300.0;
        public double MinimumCurrent => 0.0;
        public double MaximumCurrent => 6.0;
        public double GetVoltage()
        {
            return VoltageOutPut ;
        }

        public double GetCurrent()
        {
            return CurrentOutPut;
        }

        public void SetVoltage(double value)
        {
            value = Math.Max(Math.Min(value, this.MaximumVoltage), this.MinimumVoltage);
            string number = value.ToString("0#.##");
            this.GetVoltage();
            return;
        }

        public void SetCurrent(double value)
        {
            value = Math.Max(Math.Min(value, this.MaximumCurrent), this.MinimumCurrent);
            string number = value.ToString("0#.##");
            this.GetCurrent();
            return;
        }

    }
}
