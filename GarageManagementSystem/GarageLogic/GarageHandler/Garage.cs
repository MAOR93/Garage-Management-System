using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class r_Garage
    {
        private readonly Dictionary<string, Vehicle> r_Vehicles;

        public r_Garage()
        {
            r_Vehicles = new Dictionary<string, Vehicle>();
        }

        public Dictionary<string, Vehicle> Vehicles
        {
            get
            {
                return r_Vehicles;
            }
        }

        public bool IsVehicleFound(string i_LicensePlateNumber)
        {
            return r_Vehicles.ContainsKey(i_LicensePlateNumber);
        }

        public void AddVehicleToGarage(string i_LicensePlateNumber, Vehicle i_Vehicle)
        {
            r_Vehicles.Add(i_LicensePlateNumber, i_Vehicle);
        }

        public void ChangeStatusOfVehicle(string i_LicensePlateNumber, ClientsOfGarage.eStatuesOfVehicle i_NewVehicleStatus)
        {
            r_Vehicles[i_LicensePlateNumber].ClientsOfGarage.VehicleStatus = i_NewVehicleStatus;
        }

        public void FillWheelsAirToMax(string i_LicensePlateNumber)
        {
            r_Vehicles[i_LicensePlateNumber].FillMaxAirToWheels();
        }

        public Vehicle DisplayVehicleInfo(string i_LicensePlateNumber)
        {
            Vehicle vehicle = r_Vehicles[i_LicensePlateNumber];
            return vehicle;
        }

        public string DisplayVehiclelicensePlateNumbers(bool i_DispalyAlllicensePlateNumbers, ClientsOfGarage.eStatuesOfVehicle i_StatuesOfVehicle)
        {
            StringBuilder displayLicensePlateNumbers = new StringBuilder();

            if ((int)i_StatuesOfVehicle < 1 || (int)i_StatuesOfVehicle > 4)
            {
                throw new ArgumentException();
            }

            foreach (KeyValuePair<string, Vehicle> vehicle in r_Vehicles)
            {
                if (i_DispalyAlllicensePlateNumbers == true)
                {
                    displayLicensePlateNumbers.Append(string.Format("{0}{1}", vehicle.Key, Environment.NewLine));
                }
                else if (vehicle.Value.ClientsOfGarage.VehicleStatus == i_StatuesOfVehicle)
                {
                    displayLicensePlateNumbers.Append(string.Format("{0}{1}", vehicle.Key, Environment.NewLine));
                }
            }

            if (displayLicensePlateNumbers.Length == 0)
            {
                if (i_DispalyAlllicensePlateNumbers == true)
                {
                    displayLicensePlateNumbers.Append("There are no vehicles in the garage.");
                }
                else
                {
                    displayLicensePlateNumbers.Append("There are no vehicles in the garage that you selected.");
                }
            }

            return displayLicensePlateNumbers.ToString();
        }

        public void ChargeElectricVehicle(string i_LicensePlateNumber, float i_HoursToCharge)
        {
            ElectricEngine electricEngine = r_Vehicles[i_LicensePlateNumber].Engine as ElectricEngine;

            if (electricEngine != null)
            {
                electricEngine.ChargeBattery(i_HoursToCharge);
            }
            else
            {
                throw new ArgumentException();
            }

            r_Vehicles[i_LicensePlateNumber].UpdateVehicleEngineEnergyPrecentage();
        }

        public void FuelVehicle(string i_LicensePlateNumber, float i_FillFuelQuantity, FuelEngine.eFuelType i_FuelType)
        {
            FuelEngine FuelEngine = r_Vehicles[i_LicensePlateNumber].Engine as FuelEngine;

            if (FuelEngine != null)
            {
                FuelEngine.VehicleFueling(i_FuelType, i_FillFuelQuantity);
            }
            else
            {
                throw new ArgumentException("Incorrect engine");
            }

            r_Vehicles[i_LicensePlateNumber].UpdateVehicleEngineEnergyPrecentage();
        }
    }
}
