using System;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class r_GarageConsoleUI
    {
        private readonly r_Garage r_Garage;
        private bool m_ConsoleOn;

        public r_GarageConsoleUI()
        {
            r_Garage = new r_Garage();
            m_ConsoleOn = true;
        }

        public enum eMessage
        {
            DisplayMenu,
            InvalidInput,
            InvalidSelection
        }

        private enum eMenuOptions
        {
            AddVehicle = 1,
            DisplayLicenses,
            ChangeVehicleState,
            AddAirToTires,
            FuelVehicle,
            CharageVehicle,
            ShowVehicleInfo,
            Exit
        }

        public void GarageExecutor()
        {
            while (m_ConsoleOn)
            {
                Console.Clear();
                displayMenu();
                doMenuOperations();
            }
        }

        private static void displayMessages(eMessage i_Message)
        {
            switch (i_Message)
            {
                case eMessage.DisplayMenu:
                    Console.WriteLine(@"Welcome to the garage,
Please choose what you would like to do:
1. Add a new vehicle to the garage.
2. Show a list of vehicle license plates.
3. Change a vehicle status.
4. Inflate tires to the maximum.
5. Fuel a vehicle.
6. Charge an electric vehicle.
7. Display vehicle information.
8. Exit
");
                    break;
                case eMessage.InvalidInput:
                    Console.WriteLine("Invalid input. Please try again");
                    break;
                case eMessage.InvalidSelection:
                    Console.WriteLine("Invalid selection. Please try again");
                    break;
            }
        }

        private static void displayMenu()
        {
            displayMessages(eMessage.DisplayMenu);
        }

        private void doMenuOperations()
        {
            bool isLegalChoice = false;
            while (isLegalChoice == false)
            {
                try
                {
                    doUserChoice((eMenuOptions)getAIntNumberFromUser());
                    isLegalChoice = true;
                }
                catch (ArgumentException)
                {
                    displayMessages(eMessage.InvalidSelection);
                }
            }
        }

        private void doUserChoice(eMenuOptions i_MenuOption)
        {
            switch (i_MenuOption)
            {
                case eMenuOptions.AddVehicle:
                    addNewVehicleToGarage();
                    break;
                case eMenuOptions.DisplayLicenses:
                    displayFilteredLicensePlates();
                    break;
                case eMenuOptions.ChangeVehicleState:
                    changeStatusOfVehicle();
                    break;
                case eMenuOptions.AddAirToTires:
                    inflateWheels();
                    break;
                case eMenuOptions.FuelVehicle:
                    fuelAVehicle();
                    break;
                case eMenuOptions.CharageVehicle:
                    chargeAVehicle();
                    break;
                case eMenuOptions.ShowVehicleInfo:
                    displayVehicleInfo();
                    break;
                case eMenuOptions.Exit:
                    m_ConsoleOn = false;
                    break;
                default:
                    // For handling invalid input
                    throw new ArgumentException();
            }

            Console.WriteLine("Your operation was successful, Please press enter to continue.");
            Console.ReadLine();
        }

        private void addNewVehicleToGarage()
        {
            Vehicle vehicle;
            string licensePlateNumber = getlicensePlateNumber();
            if (r_Garage.IsVehicleFound(licensePlateNumber) == false)
            {
                vehicle = chooseVehicleTypeAndCreate(licensePlateNumber);
                addInformationAboutVehicle(vehicle);
                r_Garage.AddVehicleToGarage(licensePlateNumber, vehicle);
            }
            else
            {
                Console.WriteLine("Vehicle is already in the garage, change state to : Repair.");
                r_Garage.ChangeStatusOfVehicle(licensePlateNumber, ClientsOfGarage.eStatuesOfVehicle.Repair);
            }
        }

        private string getlicensePlateNumber()
        {
            Console.WriteLine("Please enter license plate number: ");

            return getStringFromUser();
        }

        private Vehicle chooseVehicleTypeAndCreate(string i_LicensePlateNumber)
        {
            Vehicle newVehicle = null;
            string[] TypesOfSupportedVehicle = Enum.GetNames(typeof(VehicleCreator.eTypesOfSupportedVehicle));

            string msg = string.Format("Select vehicle type: (choose options between {0} - {1})", 1, TypesOfSupportedVehicle.Length);
            Console.WriteLine(msg);

            for (int i = 1; i <= TypesOfSupportedVehicle.Length; i++)
            {
                msg = string.Format("{0}. {1}", i, TypesOfSupportedVehicle[i - 1]);
                Console.WriteLine(msg);
            }

            VehicleCreator.eTypesOfSupportedVehicle vehicleType;

            bool isLegalSelection = false;
            while (isLegalSelection == false)
            {
                vehicleType = (VehicleCreator.eTypesOfSupportedVehicle)getAIntNumberFromUser();
                try
                {
                    newVehicle = VehicleCreator.CreateANewVehicle((int)vehicleType, i_LicensePlateNumber);
                    isLegalSelection = true;
                }
                catch (ArgumentException)
                {
                    displayMessages(eMessage.InvalidSelection);
                }
            }

            return newVehicle;
        }

        private void addInformationAboutVehicle(Vehicle i_Vehicle)
        {
            addOwnerInfo(i_Vehicle);
            addModelName(i_Vehicle);
            addCurrentEngineEnergy(i_Vehicle);
            addWheelsInfo(i_Vehicle);
            addUniqueVehicleAttributes(i_Vehicle);
        }

        private void addOwnerInfo(Vehicle i_Vehicle)
        {
            string ownerName = getOwnerName();
            string ownerPhone = getOwnerPhone();

            i_Vehicle.ClientsOfGarage = new ClientsOfGarage(ownerName, ownerPhone);
        }

        private void addModelName(Vehicle i_Vehicle)
        {
            Console.WriteLine("Please enter the Vehicle Model: ");

            i_Vehicle.ModelName = getStringFromUser();
        }

        private void addCurrentEngineEnergy(Vehicle i_Vehicle)
        {
            bool isLegalEnergy = false;
            Console.WriteLine("Please enter the current energy (in percentage) :(choose between 0 to 100)");
            while (isLegalEnergy == false)
            {
                try
                {
                    i_Vehicle.CurrentPercentageEnergy = getAFloatNumberFromUser();
                    isLegalEnergy = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void addWheelsInfo(Vehicle i_Vehicle)
        {
            for (int i = 1; i <= i_Vehicle.Wheels.Capacity; i++)
            {
                Console.WriteLine("Please enter details for wheel number {0}:", i);
                Console.WriteLine("Manufacturer name:");
                string wheelManufacturer = getStringFromUser();
                bool isLegalWheel = false;
                while (isLegalWheel == false)
                {
                    try
                    {
                        int currentAirPressure = getAirPressureFromUser();
                        i_Vehicle.Wheels.Add(new Wheel(wheelManufacturer, i_Vehicle.MaxAirPressure, currentAirPressure));
                        isLegalWheel = true;
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
            }
        }

        private int getAirPressureFromUser()
        {
            Console.WriteLine("Current wheel's air pressure:");
            int airPressureNumber;
            string userInput = Console.ReadLine();
            while (!int.TryParse(userInput, out airPressureNumber))
            {
                displayMessages(eMessage.InvalidInput);
                userInput = Console.ReadLine();
            }

            return airPressureNumber;
        }

        private void addUniqueVehicleAttributes(Vehicle i_Vehicle)
        {
            Dictionary<int, string> requests = i_Vehicle.RequestForVehicleUniqueAttributes();
            try
            {
                Dictionary<int, string> uniqueAttributes = getUniqueAttributeFromUser(requests);
                i_Vehicle.SetUniqueAttributes(uniqueAttributes);
            }
            catch (FormatException)
            {    
                displayMessages(eMessage.InvalidInput);
            }
            catch (ArgumentException)
            {  
                displayMessages(eMessage.InvalidSelection);
            }
            catch (ValueOutOfRangeException ex)
            {   
                Console.WriteLine(ex.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private Dictionary<int, string> getUniqueAttributeFromUser(Dictionary<int, string> i_UniqueAttribute)
        {
            Dictionary<int, string> uniqueAttributes = new Dictionary<int, string>();
            string attribueValue;

            foreach (KeyValuePair<int, string> attribute in i_UniqueAttribute)
            {
                Console.WriteLine(attribute.Value);
                attribueValue = getStringFromUser();
                uniqueAttributes.Add(attribute.Key, attribueValue);
            }

            return uniqueAttributes;
        }

        private void displayFilteredLicensePlates()
        {
            Console.WriteLine(@"Choose one of this options:
1. Dispaly license plates with status Repair.
2. Dispaly license plates with status Repaired.
3. Dispaly license plates with status Paid.
4. Display all license numbers.");

            int userChoice = getAIntNumberFromUser();
            try
            {
                switch ((ClientsOfGarage.eStatuesOfVehicle)userChoice)
                {
                    case ClientsOfGarage.eStatuesOfVehicle.Repair:
                        Console.WriteLine(r_Garage.DisplayVehiclelicensePlateNumbers(false, (ClientsOfGarage.eStatuesOfVehicle)userChoice));
                        break;
                    case ClientsOfGarage.eStatuesOfVehicle.Repaired:
                        Console.WriteLine(r_Garage.DisplayVehiclelicensePlateNumbers(false, (ClientsOfGarage.eStatuesOfVehicle)userChoice));
                        break;
                    case ClientsOfGarage.eStatuesOfVehicle.Paid:
                        Console.WriteLine(r_Garage.DisplayVehiclelicensePlateNumbers(false, (ClientsOfGarage.eStatuesOfVehicle)userChoice));
                        break;
                    default:
                        Console.WriteLine(r_Garage.DisplayVehiclelicensePlateNumbers(true, (ClientsOfGarage.eStatuesOfVehicle)userChoice));
                        break;
                }
            }
            catch (ArgumentException)
            {
                displayMessages(eMessage.InvalidSelection);
            }
        }

        private void changeStatusOfVehicle()
        {
            bool IsVehicleFound = false;
            string licensePlateNumber = getlicensePlateNumber();
            IsVehicleFound = r_Garage.IsVehicleFound(licensePlateNumber);
            if (IsVehicleFound == true)
            {
                string[] vehicleStatuses = Enum.GetNames(typeof(ClientsOfGarage.eStatuesOfVehicle));
                string msg = string.Format("Select new status: (choose options between 1 to {0})", vehicleStatuses.Length);
                Console.WriteLine(msg);
                for (int i = 1; i <= vehicleStatuses.Length; i++)
                {
                    Console.WriteLine("{0}. {1}", i, vehicleStatuses[i - 1]);
                }

                bool isLegalSelection = false;
                while (isLegalSelection == false)
                {
                    int newStatus = getAIntNumberFromUser();
                    try
                    {
                        r_Garage.ChangeStatusOfVehicle(licensePlateNumber, (ClientsOfGarage.eStatuesOfVehicle)newStatus);
                        isLegalSelection = true;
                    }
                    catch (ArgumentException)
                    {
                        displayMessages(eMessage.InvalidSelection);
                    }
                }
            }
            else
            {
                Console.WriteLine("Vehicle does not found in the garage.");
            }
        }

        private void inflateWheels()
        {
            bool IsVehicleFound = false;
            string licensePlateNumber = getlicensePlateNumber();
            IsVehicleFound = r_Garage.IsVehicleFound(licensePlateNumber);
            if (IsVehicleFound == false)
            {
                Console.WriteLine("Vehicle does not found in the garage.");
            }
            else
            {
                r_Garage.FillWheelsAirToMax(licensePlateNumber);
            }
        }

        private void fuelAVehicle()
        {
            bool IsVehicleFound = false;
            bool isOperationSucceeded = false;
            string licensePlateNumber = getlicensePlateNumber();
            IsVehicleFound = r_Garage.IsVehicleFound(licensePlateNumber);
            if (IsVehicleFound == false)
            {
                Console.WriteLine("Vehicle does not found in the garage.");
            }
            else
            {
                bool isIllegalOperation = false;
                string[] supportedFuelTypes = Enum.GetNames(typeof(FuelEngine.eFuelType));

                while (isIllegalOperation == false && isOperationSucceeded == false)
                {
                    Console.WriteLine("Select fuel type:");
                    for (int i = 1; i <= supportedFuelTypes.Length; i++)
                    {
                        Console.WriteLine("{0}. {1}", i, supportedFuelTypes[i - 1]);
                    }

                    int fuelType = getAIntNumberFromUser();
                    Console.WriteLine("Please enter the amount of liters to fuel:");
                    float amountOfLitersToCharge = getAFloatNumberFromUser();
                    try
                    {
                        r_Garage.FuelVehicle(licensePlateNumber, amountOfLitersToCharge, (FuelEngine.eFuelType)fuelType);
                        isOperationSucceeded = true;
                    }
                    catch (ArgumentException ex)
                    {
                        if (ex.Message == "Incorrect engine")
                        {
                            Console.WriteLine("The vehicle is not a fuel based vehicle.");
                            isIllegalOperation = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect fuel type, please try again.");
                        }
                    }
                    catch (ValueOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        isIllegalOperation = true;
                    }
                }
            }
        }

        private void chargeAVehicle()
        {
            bool IsVehicleFound = false;
            bool isOperationSucceeded = false;
            string licensePlateNumber = getlicensePlateNumber();
            IsVehicleFound = r_Garage.IsVehicleFound(licensePlateNumber);
            if (IsVehicleFound == false)
            {
                Console.WriteLine("Vehicle does not found in the garage.");
            }
            else
            {
                Console.WriteLine("Please enter the amount of hours to charge: (for exmple press 1 for an hour)");
                bool isIllegalOperation = false;
                while (isIllegalOperation == false && isOperationSucceeded == false)
                {
                    float amountOfHoursToCharge = getAFloatNumberFromUser();
                    try
                    {
                        r_Garage.ChargeElectricVehicle(licensePlateNumber, amountOfHoursToCharge);
                        isOperationSucceeded = true;
                    }
                    catch (ArgumentException)
                    {
                        Console.WriteLine("The vehicle is not an electric based vehicle.");
                        isIllegalOperation = true;
                    }
                    catch (ValueOutOfRangeException voore)
                    {
                        Console.WriteLine(voore.ToString());
                    }
                    catch (Exception e)
                    {
                        // handle battery is full exception
                        Console.WriteLine(e.Message);
                        isIllegalOperation = true;
                    }
                }
            }
        }

        private void displayVehicleInfo()
        {
            bool IsVehicleFound = false;
            string licensePlateNumber = getlicensePlateNumber();
            IsVehicleFound = r_Garage.IsVehicleFound(licensePlateNumber);
            if (IsVehicleFound == true)
            {
                Console.WriteLine(r_Garage.DisplayVehicleInfo(licensePlateNumber).ToString());
            }
            else
            {
                Console.WriteLine("Vehicle does not found in the garage.");
            }
        }

        private int getAIntNumberFromUser()
        {
            int num;
            string userInput = Console.ReadLine();
            while (!int.TryParse(userInput, out num))
            {
                displayMessages(eMessage.InvalidInput);
                userInput = Console.ReadLine();
            }

            return num;
        }

        private float getAFloatNumberFromUser()
        {
            float num;
            string userInput = Console.ReadLine();
            while (!float.TryParse(userInput, out num))
            {
                displayMessages(eMessage.InvalidInput);
                userInput = Console.ReadLine();
            }

            return num;
        }

        private string getStringFromUser()
        {
            string userInput = Console.ReadLine();
            while (userInput.Length == 0)
            {
                displayMessages(eMessage.InvalidInput);
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        private string getOwnerName()
        {
            Console.WriteLine("Please enter vehicle's owner name: ");

            return getStringFromUser();
        }

        private string getOwnerPhone()
        {
            Console.WriteLine("Please enter vehicle's owner phone number: ");

            return getStringFromUser();
        }

        private string getLicense()
        {
            Console.WriteLine("Please enter license number: ");

            return getStringFromUser();
        }

        private string getWheelManufacturer()
        {
            Console.WriteLine("Please enter the vehicle wheel's manufacturer: ");

            return getStringFromUser();
        }

        private string getStringInput()
        {
            bool inputStrEmpty;
            string userString;

            do
            {
                userString = Console.ReadLine();

                inputStrEmpty = string.IsNullOrEmpty(userString);
                if (inputStrEmpty)
                {
                    Console.WriteLine("Ilegal input, Please try again.");
                }
            }
            while (inputStrEmpty);

            return userString;
        }
    }
}
