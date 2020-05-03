using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private const int k_CarColor = 1;
        private const int k_NumberOfDoors = 2;
        private eCarColor m_CarColor;
        private eNumberOfDoorsInCar m_NumOfDoorsInCar;
        Dictionary<int, string> m_UniqueAttribute;

        public Car(int i_NumOfWheels, float i_MaxAirPressure, Engine i_Engine, string i_LicensePlateNumber) : base(i_Engine, i_LicensePlateNumber)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_Wheels = new List<Wheel>(i_NumOfWheels);
            m_UniqueAttribute = new Dictionary<int, string>();
        }

        public enum eCarColor
        {
            Yellow = 1,
            White,
            Red,
            Black
        }

        public enum eNumberOfDoorsInCar
        {
            Two = 1,
            Three,
            Four,
            Five
        }

        public eCarColor CarColor
        {
            get
            {
                return m_CarColor;
            }

            set
            {
                switch (value)
                {
                    case eCarColor.Yellow:
                        m_CarColor = eCarColor.Yellow;
                        break;
                    case eCarColor.White:
                        m_CarColor = eCarColor.White;
                        break;
                    case eCarColor.Red:
                        m_CarColor = eCarColor.Red;
                        break;
                    case eCarColor.Black:
                        m_CarColor = eCarColor.Black;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public eNumberOfDoorsInCar CarNumOfDoors
        {
            get
            {
                return m_NumOfDoorsInCar;
            }

            set
            {
                switch (value)
                {
                    case eNumberOfDoorsInCar.Two:
                        m_NumOfDoorsInCar = eNumberOfDoorsInCar.Two;
                        break;
                    case eNumberOfDoorsInCar.Three:
                        m_NumOfDoorsInCar = eNumberOfDoorsInCar.Three;
                        break;
                    case eNumberOfDoorsInCar.Four:
                        m_NumOfDoorsInCar = eNumberOfDoorsInCar.Four;
                        break;
                    case eNumberOfDoorsInCar.Five:
                        m_NumOfDoorsInCar = eNumberOfDoorsInCar.Five;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public override Dictionary<int, string> RequestForVehicleUniqueAttributes()
        {
            string msgRequest = @"Please choose color:
1. Yellow
2. White
3. Red
4. Black";
            m_UniqueAttribute.Add(k_CarColor, msgRequest);

            msgRequest = @"Please choose the number of doors:
1. Two
2. Three
3. Four
4. Five";
            m_UniqueAttribute.Add(k_NumberOfDoors, msgRequest);

            return m_UniqueAttribute;
        }

        public override void SetUniqueAttributes(Dictionary<int, string> i_UniqueRequirements)
        {
            SetCarColor(i_UniqueRequirements[k_CarColor]);
            SetCarNumOfDoors(i_UniqueRequirements[k_NumberOfDoors]);
        }

        public void SetCarColor(string i_Color)
        {
            try
            {
                int carColor = int.Parse(i_Color);
                CarColor = (eCarColor)carColor;
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

        public void SetCarNumOfDoors(string i_NumOfDoors)
        {
            try
            {
                int parsedNumOfDoors = int.Parse(i_NumOfDoors);
                CarNumOfDoors = (eNumberOfDoorsInCar)parsedNumOfDoors;
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

        public override string ToString()
        {
            StringBuilder carInfo = new StringBuilder();
            carInfo.Append(base.ToString());
            carInfo.Append(string.Format("The car color is: {0}{1}", m_CarColor, Environment.NewLine));
            carInfo.Append(string.Format("The car number of doors is: {0}{1}", m_NumOfDoorsInCar, Environment.NewLine));
            return carInfo.ToString();
        }
    }
}
