using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private const int k_DangerousMaterial = 1;
        private const int k_CargoValume = 2;
        private bool m_IsCarryDangerousMaterial;
        private float m_CargoVolume;
        Dictionary<int, string> m_UniqueAttribute;

        public Truck(int i_NumOfWheels, float i_MaxAirPressure, Engine i_Engine, string i_LicensePlateNumber) : base(i_Engine, i_LicensePlateNumber)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Wheels = new List<Wheel>(i_NumOfWheels);
            m_UniqueAttribute = new Dictionary<int, string>();
        }

        public override Dictionary<int, string> RequestForVehicleUniqueAttributes()
        {
            string msgRequest = @"is Carry Dangerous Material?
1. Yes
2. No";

            m_UniqueAttribute.Add(k_DangerousMaterial, msgRequest);
            m_UniqueAttribute.Add(k_CargoValume, "Please enter the cargo volume:");

            return m_UniqueAttribute;
        }

        public override void SetUniqueAttributes(Dictionary<int, string> i_UniqueRequirements)
        {
            IsCarryDangerousMaterial(i_UniqueRequirements[k_DangerousMaterial]);
            SetCargoVolume(i_UniqueRequirements[k_CargoValume]);
        }

        public void IsCarryDangerousMaterial(string i_IsCarryDangerousMaterial)
        {
            string isCarry;
            if (i_IsCarryDangerousMaterial.ToLower() == "1")
            {
                isCarry = "True";
            }
            else if (i_IsCarryDangerousMaterial.ToLower() == "2")
            {
                isCarry = "False";
            }
            else
            {
                throw new FormatException("Invalid input. please press 1 for Yes or 2 for No.?");
            }

            m_IsCarryDangerousMaterial = bool.Parse(isCarry);
        }

        public void SetCargoVolume(string i_CargoVolume)
        {
            try
            {
                int parsedCargoVolume = int.Parse(i_CargoVolume);
                if (parsedCargoVolume > 0)
                {
                    m_CargoVolume = parsedCargoVolume;
                }
                else
                {
                    throw new Exception("Cargo volume must be a positive number, please try again.");
                }
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public override string ToString()
        {
            StringBuilder truckInfo = new StringBuilder();
            truckInfo.Append(base.ToString());
            if (m_IsCarryDangerousMaterial == true)
            {
                truckInfo.Append(string.Format("Yes it carrys dangerous material.{0}", Environment.NewLine));
            }
            else
            {
                truckInfo.Append(string.Format("No it doesn't carry dangerous material.{0}", Environment.NewLine));
            }

            truckInfo.Append(string.Format("The truck cargo volume is: {0:f}{1}", m_CargoVolume, Environment.NewLine));
            return truckInfo.ToString();
        }
    }
}
