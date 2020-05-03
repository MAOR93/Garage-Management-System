using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelEngine : Engine
    {
        private readonly eFuelType r_FuelTypeOfVehicle;

        public FuelEngine(eFuelType i_FuelType, float i_MaxEnergy) : base(i_MaxEnergy)
        {
            r_FuelTypeOfVehicle = i_FuelType;
        }

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        public void VehicleFueling(eFuelType i_FuelType, float i_AmountOfFuelToAddInLiters)
        {
            if (i_FuelType == r_FuelTypeOfVehicle)
            {
                if (AmountOfLeftEnergy == MaxEnergy)
                {
                    throw new Exception("Tank is already full.");
                }
                else
                {
                    try
                    {
                        AmountOfLeftEnergy += i_AmountOfFuelToAddInLiters;
                    }
                    catch (ValueOutOfRangeException)
                    {
                        throw new ValueOutOfRangeException("Valid liters amount must be bewteen: ", 1.0f, r_MaxEnergy - m_AmountOfLeftEnergy);
                    }
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            StringBuilder FuelEngineInfo = new StringBuilder();
            FuelEngineInfo.Append(string.Format("The Fuel type of the vehicle is: {0}{1}", r_FuelTypeOfVehicle, Environment.NewLine));
            FuelEngineInfo.Append(base.ToString());
            return FuelEngineInfo.ToString();
        }
    }
}
