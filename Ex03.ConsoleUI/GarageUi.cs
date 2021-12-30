using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private const string k_KeyToExit = "Q";
        private static GarageUI s_GarageUI;
        private readonly Garage r_Garage = new Garage();
        private eGarageMenu m_MenuChoice;

        public enum eGarageMenu
        {
            InsertNewVehicle = 1,
            DisplayLicensePlates,
            ChangeVehicleStatus,
            InflateWheelsToMaximum,
            RefuelVehicle,
            ChargeVehicle,
            DisplayVehicleDetails,
            Exit,
        }

        private GarageUI()
        { 
        }

        internal static GarageUI Singelton()
        {
            if (s_GarageUI == null)
            {
                s_GarageUI = new GarageUI();
            }

            return s_GarageUI;
        }

        internal void InitiateGarageMenu()
        {
            while (m_MenuChoice != eGarageMenu.Exit)
            {
                Console.Clear();
                m_MenuChoice = getInputFromEnum<eGarageMenu>("a valid pick");
                Console.Clear();
                switch (m_MenuChoice)
                {
                    case eGarageMenu.InsertNewVehicle:
                        addVehicleToGarage();
                        break;
                    case eGarageMenu.DisplayLicensePlates:
                        printVehicleLicenseList();
                        break;
                    case eGarageMenu.ChangeVehicleStatus:
                        changeVehicleStatus();
                        break;
                    case eGarageMenu.InflateWheelsToMaximum:
                        inflateWheelToMax();
                        break;
                    case eGarageMenu.RefuelVehicle:
                        refuelVehicle();
                        break;
                    case eGarageMenu.ChargeVehicle:
                        chargeVehicle();
                        break;
                    case eGarageMenu.DisplayVehicleDetails:
                        vehicleReport();
                        break;
                    case eGarageMenu.Exit:
                        break;
                    default:
                        Console.WriteLine("This options isn't valid");
                        break;
                }
            }
        }

        private static void printRedWarning()
        {
            Console.Write("Warning: ", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
        }

        private static void printSucceedGreenMessage(string i_Message)
        {
            Console.WriteLine($"{i_Message} successfully!", Console.ForegroundColor = ConsoleColor.Green);
            Console.ResetColor();
            printAndWait("Press 'Enter' to continue.");
        }

        private static T getInput<T>(string i_DesireThing)
        {
            string userInput;

            do
            {
                userInput = printAndGet($"Please enter your {i_DesireThing}: ");
            }
            while (!isType<T>(userInput));

            return (T)Convert.ChangeType(userInput, typeof(T));
        }

        private static void printAndWait(string i_Message)
        {
            Console.WriteLine(i_Message);
            Console.ReadLine();
        }

        private static bool isType<T>(string i_Text)
        {
            bool isValid = false;

            try
            {
                T value = (T)Convert.ChangeType(i_Text, typeof(T));
                if (i_Text == string.Empty)
                {
                    throw new Exception();
                }

                isValid = true;
            }
            catch
            {
                Console.Clear();
                printRedWarning();
                Console.Write("Invalid input, ");
            }

            return isValid;
        }

        private static string printAndGet(string i_Message)
        {
            Console.WriteLine(i_Message);

            return Console.ReadLine();
        }

        private static bool checkIfEnumValueIsValid(string i_UserInput, int i_EnumLength)
        {
            bool isUserInputValid = int.TryParse(i_UserInput, out int userChoice);

            isUserInputValid &= userChoice >= 1 && userChoice <= i_EnumLength;
            if (!isUserInputValid)
            {
                Console.Clear();
                printRedWarning();
                Console.WriteLine("Invalid value, Please enter valid value from the list below");
            }

            return isUserInputValid;
        }

        private static void printEnumOption<T>()
        {
            foreach (var option in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"Enter [{(int)option}] to {addSpacesBeforeUpperLetters(option.ToString())}");
            }
        }

        private static void printVehicleList(List<string> i_VehicleList)
        {
            if (i_VehicleList.Count == 0)
            {
                Console.WriteLine("The license plate's list is empty.");
            }
            else
            {
                Console.WriteLine("The license plate's list is : ");
                foreach (string licensePlate in i_VehicleList)
                {
                    Console.WriteLine(licensePlate);
                }
            }

            printAndWait("Press 'Enter' to continue.");
        }

        private static string addSpacesBeforeUpperLetters(string i_Sentence)
        {
            return string.Concat(i_Sentence.Select(i_Letter => char.IsUpper(i_Letter) ? " " + i_Letter : i_Letter.ToString())).TrimStart(' ');
        }
        
        private static T getInputFromEnum<T>(string i_MessageToPrint)
        {
            string userChoice;

            int enumLength = Enum.GetNames(typeof(T)).Length;

            do
            {
                Console.WriteLine(string.Format($"Please enter {i_MessageToPrint}: "));
                printEnumOption<T>();
                userChoice = Console.ReadLine();
            } 
            while (!checkIfEnumValueIsValid(userChoice, enumLength));

            return (T)Enum.Parse(typeof(T), userChoice);
        }
        
        private List<string> createListOfVehicleByChoice(string i_UserInput)
        {
            List<string> vehicleLicenseList;
            const string message = "the status from the list below";
            GarageAccount.eStatusInGarage statusInGarageByUser;

            if (i_UserInput == "1")
            {
                vehicleLicenseList = r_Garage.ListOfVehicleLicensePlates();
            }
            else
            {
                statusInGarageByUser = getInputFromEnum<GarageAccount.eStatusInGarage>(message);
                vehicleLicenseList = r_Garage.ListOfVehicleLicensePlatesByFiltering(statusInGarageByUser);
            }

            return vehicleLicenseList;
        }

        private void printVehicleLicenseList()
        {
            List<string> vehicleLicenseList;

            Console.WriteLine("Please enter [1] to see all licenses plate or any other key to see filters list");
            vehicleLicenseList = createListOfVehicleByChoice(Console.ReadLine());
            Console.Clear();
            printVehicleList(vehicleLicenseList);
        }
        
        private void changeVehicleStatus()
        {
            bool isStatusUpdate = false;
            string exitKey = null, licensePlate;
            const string message = "status to change the vehicle from the list below";
            GarageAccount.eStatusInGarage vehicleDesireStatus;

            while ((isStatusUpdate == false) && (exitKey != k_KeyToExit))
            {
                Console.Clear();
                try
                {
                    licensePlate = getInput<int>("license plate").ToString();
                    vehicleDesireStatus = getInputFromEnum<GarageAccount.eStatusInGarage>(message);
                    r_Garage.ChangeStatusOfVehicle(vehicleDesireStatus, licensePlate);
                    isStatusUpdate = true;
                    printSucceedGreenMessage("The Vehicle status changed");
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"Press {k_KeyToExit} for menu, any other key for retry"));
                }
            }
        }

        private void addVehicleToGarage()
        {
            string fullName, ownerPhoneNumber, licensePlate;
            Vehicle vehicle;

            licensePlate = getInput<int>("license plate").ToString();
            if (r_Garage.IsVehicleExist(licensePlate) == false)
            {
                fullName = getInput<string>("full name");
                ownerPhoneNumber = getInput<int>("phone number").ToString();
                vehicle = addVehicleInfo(licensePlate);
                r_Garage.InsertVehicle(fullName, ownerPhoneNumber, vehicle);
                printSucceedGreenMessage("The vehicle added to garage");
            }
            else
            {
                r_Garage.ChangeStatusOfVehicle(GarageAccount.eStatusInGarage.InRepair, licensePlate);
                Console.WriteLine("Vehicle already exist, Status changed to \"in repair\"");
                printAndWait("Press 'Enter' to continue.");
            }
        }

        private void inflateWheelToMax()
        {
            string licensePlate;
            bool isFulled = false;
            string exitKey = null;

            while ((isFulled == false) && (exitKey != k_KeyToExit))
            {
                Console.Clear();
                try
                {
                    licensePlate = getInput<int>("license plate").ToString();
                    r_Garage.InflatingWheelsToMax(licensePlate);
                    isFulled = true;
                    printSucceedGreenMessage("The wheels were inflated to the maximum");
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"Press {k_KeyToExit} for menu, any other key for retry"));
                }
            }
        }
        
        private void refuelVehicle()
        {
            string licensePlate, exitKey = null;
            float amountToFill;
            bool isVehicleRefuel = false;
            CombustionEngine.eFuelKind fuelKind;
            const string message = "fuel type from the list below";

            while ((isVehicleRefuel == false) && (exitKey != k_KeyToExit))
            {
                Console.Clear();
                try
                {
                    licensePlate = getInput<int>("license plate").ToString();
                    amountToFill = getInput<float>("amount of fuel you want to fill");
                    fuelKind = getInputFromEnum<CombustionEngine.eFuelKind>(message);
                    r_Garage.RefuelVehicle(licensePlate, fuelKind, amountToFill);
                    isVehicleRefuel = true;
                    printSucceedGreenMessage("The vehicle refuel");
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchAndPrintExceptions(ex);
                    exitKey = printAndGet(string.Format($"Press {k_KeyToExit} for menu, any other key for retry"));
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"Press {k_KeyToExit} for menu, any other key for retry"));
                }
            }
        }
        
        private void chargeVehicle()
        {
            string licensePlate, exitKey = null;
            float amountToCharge;
            bool isCharged = false;

            while (isCharged == false && exitKey != k_KeyToExit)
            {
                Console.Clear();
                try
                {
                    licensePlate = getInput<int>("license plate").ToString();
                    amountToCharge = getInput<float>("amount of minutes that you want to charge in vehicle");
                    r_Garage.ChargingVehicle(licensePlate, amountToCharge);
                    isCharged = true;
                    printSucceedGreenMessage("The vehicle battery charged");
                }
                catch (ValueOutOfRangeException ex)
                {
                    ex.MaxValue *= 60f;
                    ex.MinValue *= 60f;
                    catchAndPrintExceptions(ex);
                    exitKey = printAndGet(string.Format($"Press {k_KeyToExit} for menu, any other key for retry"));
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"Press {k_KeyToExit} for menu, any other key for retry"));
                }
            }
        }
        
        private void vehicleReport()
        {
            string licensePlate, vehicleInfo, exitKey = null;
            bool isInfoReceived = false;

            while ((isInfoReceived == false) && (exitKey != k_KeyToExit))
            {
                try
                {
                    Console.Clear();
                    licensePlate = getInput<int>("license plate").ToString();
                    vehicleInfo = r_Garage.GetVehicleReport(licensePlate);
                    Console.Clear();
                    Console.WriteLine("*************************");
                    Console.WriteLine(vehicleInfo);
                    Console.WriteLine("*************************");
                    isInfoReceived = true;
                    printAndWait("Press 'Enter' to continue.");
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"Press {k_KeyToExit} for menu, any other key for retry"));
                }
            }
        }

        private Vehicle addVehicleInfo(string i_LicensePlate)
        {
            string vehicleModel;
            const string message = "type of vehicle from the list below";
            VehicleFactory.eVehicleType vehicleType;
            Vehicle returnVehicle;

            vehicleType = getInputFromEnum<VehicleFactory.eVehicleType>(message);
            returnVehicle = VehicleFactory.MakeVehicle(vehicleType);
            vehicleModel = getInput<string>("vehicle model");
            returnVehicle.SettingVehicleInfo(i_LicensePlate, vehicleModel);
            setVehicleWheels(returnVehicle);
            setEngineEnergy(vehicleType, returnVehicle);
            setUniqueInfo(returnVehicle);

            return returnVehicle;
        }

        private void setVehicleWheels(Vehicle i_Vehicle)
        {
            bool isAirPressureValid = false;
            float currentWheelAirPressure;
            string wheelManufacturerName;

            wheelManufacturerName = getInput<string>("wheel manufacturer name");
            while (isAirPressureValid == false)
            {
                try
                {
                    currentWheelAirPressure = getInput<float>("current wheel air pressure");
                    i_Vehicle.SetWheelInformation(currentWheelAirPressure, wheelManufacturerName);
                    isAirPressureValid = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchAndPrintExceptions(ex);
                }
            }
        }

        private void setUniqueInfo(Vehicle i_Vehicle)
        {
            bool isExceptionWasThrown = false;
            List<string> uniqueInfoList = new List<string>();
            string uniqueInfoMessage = i_Vehicle.GetSpecialInfoMessage(out int uniqueInfoAmount);
            while (isExceptionWasThrown == false)
            {
                Console.WriteLine(uniqueInfoMessage);
                try
                {
                    for (int i = 0; i < uniqueInfoAmount; i++)
                    {
                        uniqueInfoList.Add(Console.ReadLine());
                    }

                    i_Vehicle.SetVehicleUniqueInformation(uniqueInfoList);
                    isExceptionWasThrown = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchAndPrintExceptions(ex);
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again");
                }

                uniqueInfoList.Clear();
            }
        }

        private void setEngineEnergy(VehicleFactory.eVehicleType i_VehicleType, Vehicle i_Vehicle)
        {
            bool isEnergyInRange = false;
            string message = string.Format($"energy left in {i_VehicleType} engine");
            float energyLeftInEngine;

            while (isEnergyInRange == false)
            {
                try
                {
                    energyLeftInEngine = getInput<float>(message);
                    i_Vehicle.InsertEngineInformation(energyLeftInEngine);
                    isEnergyInRange = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchAndPrintExceptions(ex);
                }
            }
        }

        private void catchAndPrintExceptions(ValueOutOfRangeException i_Ex)
        {
            string messageToPrint = string.Format(
                "{0}.{1}range should be between {2} to {3}{4}Please try again",
                i_Ex.Message,
                Environment.NewLine,
                i_Ex.MinValue,
                i_Ex.MaxValue,
                Environment.NewLine);

            printRedWarning();
            Console.WriteLine(messageToPrint);
        }
    }
}