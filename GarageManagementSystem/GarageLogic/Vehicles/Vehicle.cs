using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly Engine r_Engine;
        protected readonly string r_licensePlateNumber;
        protected List<Wheel> m_Wheels;
        protected float m_MaxAirPressure;
        protected float m_CurrentPercentageEnergy;
        protected string m_ModelName;
        protected ClientsOfGarage m_OwnerInfo;

        public Vehicle(Engine i_Engine, string i_LicensePlateNumber)
        {
            r_Engine = i_Engine;
            r_licensePlateNumber = i_LicensePlateNumber;
        }

        public Engine Engine
        {
            get
            {
                return r_Engine;
            }
        }

        public float MaxAirPressure
        {
            get
            {
                return m_MaxAirPressure;
            }
        }

        public ClientsOfGarage ClientsOfGarage
        {
            get
            {
                return m_OwnerInfo;
            }

            set
            {
                m_OwnerInfo = value;
            }
        }

        public float CurrentPercentageEnergy
        {
            get
            {
                return m_CurrentPercentageEnergy;
            }

            set
            {
                if (value >= 0 && value <= 100)
                {
                    Engine.SetEnergyByPercentage(value);
                    m_CurrentPercentageEnergy += value;
                }
                else
                {
                    throw new ValueOutOfRangeException("Current percentage of engine energy", 0.0f, 100.0f);
                }
            }
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }

            set
            {
                m_ModelName = value;
            }
        }

        public List<Wheel> Wheels
        {
            get
            {
                return m_Wheels;
            }
        }

        public void UpdateVehicleEngineEnergyPrecentage()
        {
            m_CurrentPercentageEnergy = (Engine.AmountOfLeftEnergy / Engine.MaxEnergy) * 100;
        }

        public void FillMaxAirToWheels()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.VehicleTireInflationMax();
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleInfo = new StringBuilder();
            vehicleInfo.Append(string.Format("The license number of the vehicle is {0}{1}", r_licensePlateNumber, Environment.NewLine));
            vehicleInfo.Append(string.Format("The model of the vehicle is {0}{1}", m_ModelName, Environment.NewLine));
            vehicleInfo.Append(m_OwnerInfo.ToString());
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                vehicleInfo.Append(string.Format("Wheel number {0}{1}", i + 1, Environment.NewLine));
                vehicleInfo.Append(m_Wheels[i].ToString());
            }

            vehicleInfo.Append(r_Engine.ToString());
            vehicleInfo.Append(string.Format("The current percentage of engine energy is {0}{1}", m_CurrentPercentageEnergy, Environment.NewLine));
            return vehicleInfo.ToString();
        }

        public abstract Dictionary<int, string> RequestForVehicleUniqueAttributes();

        public abstract void SetUniqueAttributes(Dictionary<int, string> i_UniqueRequirements);
    }
}