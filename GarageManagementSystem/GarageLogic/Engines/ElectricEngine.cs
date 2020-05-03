using System;

namespace Ex03.GarageLogic
{
    public class ElectricEngine : Engine
    {
        public ElectricEngine(float i_MaxEnergy) : base(i_MaxEnergy)
        {
        }

        public void ChargeBattery(float i_HoursToAddToBattery)
        {
            if (AmountOfLeftEnergy == MaxEnergy)
            {
                throw new Exception("Battery is already full.");
            }
            else
            {
                try
                {
                    AmountOfLeftEnergy += i_HoursToAddToBattery;
                }
                catch (ValueOutOfRangeException)
                {
                    throw new ValueOutOfRangeException("Legal hours amount", 1.0f, r_MaxEnergy - m_AmountOfLeftEnergy);
                }
            }
        }
    }
}
