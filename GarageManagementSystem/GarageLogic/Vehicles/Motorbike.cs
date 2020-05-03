using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorbike : Vehicle
    {
        private const int k_LicenseType = 1;
        private const int k_EngineVolume = 2;
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        private Dictionary<int, string> m_UniqueAttribute;

        public Motorbike(int i_NumOfWheels, float i_MaxAirPressure, Engine i_Engine, string i_LicensePlateNumber) : base(i_Engine, i_LicensePlateNumber)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Wheels = new List<Wheel>(i_NumOfWheels);
            m_UniqueAttribute = new Dictionary<int, string>();
        }

        public enum eLicenseType
        {
            A = 1,
            A1,
            AB,
            B1
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }

            set
            {
                switch (value)
                {
                    case eLicenseType.A:
                        m_LicenseType = eLicenseType.A;
                        break;
                    case eLicenseType.A1:
                        m_LicenseType = eLicenseType.A1;
                        break;
                    case eLicenseType.AB:
                        m_LicenseType = eLicenseType.AB;
                        break;
                    case eLicenseType.B1:
                        m_LicenseType = eLicenseType.B1;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public override Dictionary<int, string> RequestForVehicleUniqueAttributes()
        {
            string msgRequest = @"Please choose license type: (choose 1 to 4)
1. A
2. A1
3. AB
4. B1";
            m_UniqueAttribute.Add(k_LicenseType, msgRequest);
            m_UniqueAttribute.Add(k_EngineVolume, "Please enter the engine volume:");

            return m_UniqueAttribute;
        }

        public override void SetUniqueAttributes(Dictionary<int, string> i_UniqueRequirements)
        {
            SetLicenseType(i_UniqueRequirements[k_LicenseType]);
            SetEngineVolume(i_UniqueRequirements[k_EngineVolume]);
        }

        public void SetLicenseType(string i_LicenseType)
        {
            try
            {
                int parsedLicenseType = int.Parse(i_LicenseType);
                LicenseType = (eLicenseType)parsedLicenseType;
            }
            catch (FormatException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
        }

        public void SetEngineVolume(string i_EngineVolume)
        {
            try
            {
                int engineVolume = int.Parse(i_EngineVolume);
                if (engineVolume > 0)
                {
                    m_EngineVolume = engineVolume;
                }
                else
                {
                    throw new Exception("Engine volume must be a positive number, Please try again.");
                }
            }
            catch (FormatException)
            {
                throw;
            }
        }

        public override string ToString()
        {
            StringBuilder motorbikeInfo = new StringBuilder();
            motorbikeInfo.Append(base.ToString());
            motorbikeInfo.Append(string.Format("The motorbike license type is: {0}{1}", m_LicenseType, Environment.NewLine));
            motorbikeInfo.Append(string.Format("The motorbike engine volume is: {0}{1}", m_EngineVolume, Environment.NewLine));
            return motorbikeInfo.ToString();
        }
    }
}
