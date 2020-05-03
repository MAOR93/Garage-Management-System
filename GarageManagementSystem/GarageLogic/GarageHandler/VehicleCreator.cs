using System;

namespace Ex03.GarageLogic
{
    public static class VehicleCreator
    {
        private const int k_RegularMotorbikeNumberOfWheels = 2;
        private const float k_RegularMotorbikeMaxAirPreasure = 28.0f;
        private const FuelEngine.eFuelType k_FuelMotoerbikeType = FuelEngine.eFuelType.Octan95;
        private const float k_FuelMotorbikeTankCapacityInLiters = 5.5f;

        private const int k_ElectricMotorbikeNumberOfWheels = 2;
        private const float k_ElectricMotorbikeMaxAirPreasure = 28.0f;
        private const float k_ElectricMotorbikeMaxBatteryInHours = 1.6f;

        private const int k_FuelCarNumberOfWheels = 4;
        private const float k_FuelCarMaxAirPreasure = 30.0f;
        private const FuelEngine.eFuelType k_FuelCarType = FuelEngine.eFuelType.Octan96;
        private const float k_FuelCarTankCapacityInLiters = 42.0f;

        private const int k_ElectricCarNumberOfWheels = 4;
        private const float k_ElectricCarMaxAirPreasure = 30.0f;
        private const float k_ElectricCarMaxBatteryInHours = 2.5f;

        private const int k_TruckNumberOfWheels = 16;
        private const float k_TruckMaxAirPreasure = 26.0f;
        private const FuelEngine.eFuelType k_TruckFuelType = FuelEngine.eFuelType.Soler;
        private const float k_FuelTruckTankCapacityInLiters = 120.0f;

        public enum eTypesOfSupportedVehicle
        {
            FuelMotorbike = 1,
            ElectricMotorbike,
            FuelCar,
            ElectricCar,
            Truck,
        }

        public static Vehicle CreateANewVehicle(int i_VehicleType, string i_LicensePlateNumber)
        {
            Vehicle newVehicle = null;
            Engine engine = null;
            switch ((eTypesOfSupportedVehicle)i_VehicleType)
            {
                case eTypesOfSupportedVehicle.FuelMotorbike:
                    engine = new FuelEngine(k_FuelMotoerbikeType, k_FuelMotorbikeTankCapacityInLiters);
                    newVehicle = new Motorbike(k_RegularMotorbikeNumberOfWheels, k_RegularMotorbikeMaxAirPreasure, engine, i_LicensePlateNumber);
                    break;
                case eTypesOfSupportedVehicle.ElectricMotorbike:
                    engine = new ElectricEngine(k_ElectricMotorbikeMaxBatteryInHours);
                    newVehicle = new Motorbike(k_ElectricMotorbikeNumberOfWheels, k_ElectricMotorbikeMaxAirPreasure, engine, i_LicensePlateNumber);
                    break;
                case eTypesOfSupportedVehicle.FuelCar:
                    engine = new FuelEngine(k_FuelCarType, k_FuelCarTankCapacityInLiters);
                    newVehicle = new Car(k_FuelCarNumberOfWheels, k_FuelCarMaxAirPreasure, engine, i_LicensePlateNumber);
                    break;
                case eTypesOfSupportedVehicle.ElectricCar:
                    engine = new ElectricEngine(k_ElectricCarMaxBatteryInHours);
                    newVehicle = new Car(k_ElectricCarNumberOfWheels, k_ElectricCarMaxAirPreasure, engine, i_LicensePlateNumber);
                    break;
                case eTypesOfSupportedVehicle.Truck:
                    engine = new FuelEngine(k_TruckFuelType, k_FuelTruckTankCapacityInLiters);
                    newVehicle = new Truck(k_TruckNumberOfWheels, k_TruckMaxAirPreasure, engine, i_LicensePlateNumber);
                    break;
                default:
                    throw new ArgumentException();
            }

            return newVehicle;
        }
    }
}
