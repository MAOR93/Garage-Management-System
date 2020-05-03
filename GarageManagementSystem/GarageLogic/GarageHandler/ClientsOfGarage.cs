using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ClientsOfGarage
    {
        private eStatuesOfVehicle m_VehicleStatus;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;

        public ClientsOfGarage(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_VehicleStatus = eStatuesOfVehicle.Repair;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        public enum eStatuesOfVehicle
        {
            Repair = 1,
            Repaired,
            Paid
        }

        public eStatuesOfVehicle VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }

            set
            {
                switch (value)
                {
                    case eStatuesOfVehicle.Repair:
                        m_VehicleStatus = eStatuesOfVehicle.Repair;
                        break;
                    case eStatuesOfVehicle.Repaired:
                        m_VehicleStatus = eStatuesOfVehicle.Repaired;
                        break;
                    case eStatuesOfVehicle.Paid:
                        m_VehicleStatus = eStatuesOfVehicle.Paid;
                        break;
                    default:
                        throw new ArgumentException();
                }
            }
        }

        public override string ToString()
        {
            StringBuilder ownerInfo = new StringBuilder();
            ownerInfo.Append(string.Format("The vehicle current status is: {0}{1}", m_VehicleStatus, Environment.NewLine));
            ownerInfo.Append(string.Format("The owner name is: {0}{1}", m_OwnerName, Environment.NewLine));
            ownerInfo.Append(string.Format("The owner phone number is: {0}{1}", m_OwnerPhoneNumber, Environment.NewLine));
            return ownerInfo.ToString();
        }
    }
}