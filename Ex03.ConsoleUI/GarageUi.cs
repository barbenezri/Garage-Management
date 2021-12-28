using System;
using System.Collections.Generic;
using System.Linq;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUi
    {
        private static GarageUi s_GarageUi;
        private readonly Garage r_Garage = new Garage();
        private readonly string r_KeyToExitToMenu = "Q";

        public enum eGarageMenu
        {
            InsertNewVehicle = 1,
            DisplayLicensePlates,
            ChangeVehicleStaus,
            InflateWheelsToMaximum,
            RefulVehicle,
            ChargeVehicle,
            DisplayVehicleDetails,
            Exit,
        }

        private GarageUi()
        { 
        }

        internal static GarageUi Singelton()
        {
            if (s_GarageUi == null)
            {
                s_GarageUi = new GarageUi();
            }

            return s_GarageUi;
        }

        internal void InitiateGarageMenu()
        {
            eGarageMenu? userInput = null;

            while (userInput != eGarageMenu.Exit)
            {
                Console.Clear();
                userInput = getInputFromEnum<eGarageMenu>("a valid pick");
                Console.Clear();
                switch (userInput)
                {
                    case eGarageMenu.InsertNewVehicle:
                        addVehicleToGarage();
                        break;
                    case eGarageMenu.DisplayLicensePlates:
                        printVehicleLicenseList();
                        break;
                    case eGarageMenu.ChangeVehicleStaus:
                        changeVehicleStatus();
                        break;
                    case eGarageMenu.InflateWheelsToMaximum:
                        inflateWheelToMax();
                        break;
                    case eGarageMenu.RefulVehicle:
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
                }
            }
        }

        private static void printRedWarning()
        {
            Console.Write("Warning: ", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
        }

        private static void printSuccsedGreenMessage(string i_Message)
        {
            Console.WriteLine($"{i_Message} successfully!", Console.ForegroundColor = ConsoleColor.Green);
            Console.ResetColor();
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

        private static bool isType<T>(string text)
        {
            bool isValid = false;

            try
            {
                T value = (T)Convert.ChangeType(text, typeof(T));
                isValid = true;
            }
            catch
            {
                Console.Clear();
                printRedWarning();
                Console.Write("This input isn't valid, ");
            }

            return isValid;
        }

        private static string printAndGet(string i_Message)
        {
            Console.WriteLine(i_Message);

            return Console.ReadLine();
        }

        private static void printAndWait(string i_Message)
        {
            Console.WriteLine(i_Message);
            Console.ReadLine();
        }

        private static bool checkIfEnumValueIsValid(string i_UserInput, int i_EnumLength)
        {
            bool isUserInputValid = int.TryParse(i_UserInput, out int userChoise);

            isUserInputValid &= userChoise >= 1 && userChoise <= i_EnumLength;
            if (!isUserInputValid)
            {
                Console.Clear();
                printRedWarning();
                Console.WriteLine("You entered invalid value, Please enter valid value from the list below");
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
        }
        
        private static string addSpacesBeforeUpperLetters(string i_Option)
        {
            return string.Concat(i_Option.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }
        
        private static T getInputFromEnum<T>(string i_MessageToPrint)
        {
            string userChoise;
            int enumLength = Enum.GetNames(typeof(T)).Length;

            do
            {
                Console.WriteLine(string.Format($"Please enter {i_MessageToPrint}: "));
                printEnumOption<T>();
                userChoise = Console.ReadLine();
            } 
            while (!checkIfEnumValueIsValid(userChoise, enumLength));

            return (T)Enum.Parse(typeof(T), userChoise);
        }
        
        private List<string> createListOfVehicleByChoice(string i_UserInput)
        {
            List<string> vehicleLicenseList;
            string message = "the status from the list below";
            GarageAccount.eStatusInGarge statusInGargeByUser;

            if (i_UserInput == "1")
            {
                vehicleLicenseList = r_Garage.ListOfVehiclelicensePlates();
            }
            else
            {
                statusInGargeByUser = getInputFromEnum<GarageAccount.eStatusInGarge>(message);
                vehicleLicenseList = r_Garage.ListOfVehiclelicensePlatesByFiltering(statusInGargeByUser);
            }

            return vehicleLicenseList;
        }

        private void printVehicleLicenseList()
        {
            List<string> vehicleLicenseListFromGarage;

            Console.WriteLine("Please enter [1] to see all licences plate or any other key to see fileters list");
            vehicleLicenseListFromGarage = createListOfVehicleByChoice(Console.ReadLine());
            Console.Clear();
            printVehicleList(vehicleLicenseListFromGarage);
            printAndWait("Press 'Enter' to continue.");
        }
        
        private void changeVehicleStatus()
        {
            bool isStatusUpdate = false;
            string exitKey = null, licencePlate;
            string message = "status to change the vehicle from the list below";
            GarageAccount.eStatusInGarge vehicleDesireStatus;

            while ((isStatusUpdate == false) && (exitKey != r_KeyToExitToMenu))
            {
                Console.Clear();
                try
                {
                    licencePlate = getInput<int>("license plate").ToString();
                    vehicleDesireStatus = getInputFromEnum<GarageAccount.eStatusInGarge>(message);
                    r_Garage.ChangeStatusOfVehicle(vehicleDesireStatus, licencePlate);
                    isStatusUpdate = true;
                    printSuccsedGreenMessage("The Vehicle status changed");
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            printAndWait("Press 'Enter' to continue.");
        }

        private void addVehicleToGarage()
        {
            string fullName, ownerPhoneNumber, licencePlate;
            Vehicle vehicle;

            licencePlate = getInput<int>("license plate").ToString();
            if (r_Garage.IsVehicleExist(licencePlate) == false)
            {
                fullName = getInput<string>("full name");
                ownerPhoneNumber = getInput<int>("phone number").ToString();
                vehicle = addVehicleInfo(licencePlate);
                r_Garage.InsertVehicle(fullName, ownerPhoneNumber, vehicle);
                printSuccsedGreenMessage("The vehicle added to garage");
            }
            else
            {
                r_Garage.ChangeStatusOfVehicle(GarageAccount.eStatusInGarge.InRepair, licencePlate);
                Console.WriteLine("Vehicle already exist, Status changed to \"in reapir\"");
            }

            printAndWait("Press 'Enter' to continue.");
        }

        private void inflateWheelToMax()
        {
            string licensePlate;
            bool isFulled = false;
            string exitKey = null;

            while ((isFulled == false) && (exitKey != r_KeyToExitToMenu))
            {
                Console.Clear();
                try
                {
                    licensePlate = getInput<int>("license plate").ToString();
                    r_Garage.InflatingWheelsToMax(licensePlate);
                    isFulled = true;
                    printSuccsedGreenMessage("The wheels were inflated to the maximum");
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            printAndWait("Press 'Enter' to continue.");
        }
        
        private void refuelVehicle()
        {
            string licensePlate, exitKey = null;
            float amountToFill;
            bool isVehicleRefuel = false;
            CombustionEngine.eFuelKind fuelKind;
            string message = "type of fuel from the list below";

            while ((isVehicleRefuel == false) && (exitKey != r_KeyToExitToMenu))
            {
                Console.Clear();
                try
                {
                    licensePlate = getInput<int>("license plate").ToString();
                    amountToFill = getInput<float>("amount of fuel you want to fill");
                    fuelKind = getInputFromEnum<CombustionEngine.eFuelKind>(message);
                    r_Garage.RefuelVehicle(licensePlate, fuelKind, amountToFill);
                    isVehicleRefuel = true;
                    printSuccsedGreenMessage("The vehicle refuel");
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchAndPrintExceptions(ex);
                    exitKey = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            printAndWait("Press 'Enter' to continue.");
        }
        
        private void chargeVehicle()
        {
            string licensePlate, exitKey = null;
            float amountToCharge;
            bool isCharged = false;

            while (isCharged == false && exitKey != r_KeyToExitToMenu)
            {
                Console.Clear();
                try
                {
                    licensePlate = getInput<int>("license plate").ToString();
                    amountToCharge = getInput<float>("amount of minutes that you want to charge in vehicle");
                    r_Garage.ChargingVehicle(licensePlate, amountToCharge);
                    isCharged = true;
                    printSuccsedGreenMessage("The vehicle battery charged");
                }
                catch (ValueOutOfRangeException ex)
                {
                    ex.MaxValue *= 60f;
                    ex.MinValue *= 60f;
                    catchAndPrintExceptions(ex);
                    exitKey = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            printAndWait("Press 'Enter' to continue.");
        }
        
        private void vehicleReport()
        {
            string licensePlate, vehicleInfo, exitKey = null;
            bool isInfoRecived = false;

            while ((isInfoRecived == false) && (exitKey != r_KeyToExitToMenu))
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
                    isInfoRecived = true;
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    exitKey = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            printAndWait("Press 'Enter' to continue.");
        }

        private Vehicle addVehicleInfo(string i_LicencePlate)
        {
            string vehicleModel, message = "type of vehicle from the list below";
            VehicleFactory.eVehicleType vehicleType;
            Vehicle returnVehicle;

            vehicleType = getInputFromEnum<VehicleFactory.eVehicleType>(message);
            returnVehicle = VehicleFactory.MakeVehicle(vehicleType);
            vehicleModel = getInput<string>("vehicle model");
            returnVehicle.SettingVehicleInfo(i_LicencePlate, vehicleModel);
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
            List<string> UniqueInfoList = new List<string>();
            string uniqueInfoMessage;

            uniqueInfoMessage = i_Vehicle.GetSpecialInfoMessage(out int uniqueInfoAmount);
            while (isExceptionWasThrown == false)
            {
                Console.WriteLine(uniqueInfoMessage);
                try
                {
                    for (int i = 0; i < uniqueInfoAmount; i++)
                    {
                        UniqueInfoList.Add(Console.ReadLine());
                    }

                    i_Vehicle.SetVehicleUniqueInformation(UniqueInfoList);
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

                UniqueInfoList.Clear();
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

        public static void catchAndPrintExceptions(ValueOutOfRangeException i_Ex)
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