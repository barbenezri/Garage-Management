using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        internal void InitiatGarageMenu()
        {
            eGarageMenu? userInput = null;

            while (userInput != eGarageMenu.Exit)
            {
                Console.Clear();
                userInput = gettingUserInputForGeneralEnum<eGarageMenu>("valid pick");
                switch (userInput)
                {
                    case eGarageMenu.InsertNewVehicle:
                        addVehicleToGarage();
                        break;
                    case eGarageMenu.DisplayLicensePlates:
                        gettingAndPrintVehicleLicenseList();
                        break;
                    case eGarageMenu.ChangeVehicleStaus:
                        changeVehicleStatusInGarge();
                        break;
                    case eGarageMenu.InflateWheelsToMaximum:
                        fillingAirInWheelsToMaximum();
                        break;
                    case eGarageMenu.RefulVehicle:
                        refulVehicle();
                        break;
                    case eGarageMenu.ChargeVehicle:
                        chargeVehicle();
                        break;
                    case eGarageMenu.DisplayVehicleDetails:
                        fullInformationOfVehicleInGarage();
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

        private static void printSuccsedGreenmessage(string i_Message)
        {
            Console.WriteLine($"{i_Message} successfully", Console.ForegroundColor = ConsoleColor.Green);
            Console.ResetColor();
        }

        private static float getFloatNumberFromUser(string i_DesireThing)
        {
            string userInput;

            do
            {
                userInput = printAndGet($"Please enter your {i_DesireThing}: ");
            } 
            while (!checkIfFloat(userInput));

            return float.Parse(userInput);
        }

        private static string getIntAsStringFromUser(string i_DesireThing)
        {
            string userInput;

            do
            {
                userInput = printAndGet($"Please enter your {i_DesireThing}: ");
            } 
            while (!checkIfInt(userInput));

            return userInput;
        }

        private static string getStringFromUser(string i_DesireThing)
        {
            string userInput;

            do
            {
                userInput = printAndGet($"Please enter your {i_DesireThing}: ");
            } 
            while (!checkIfStringValid(userInput));

            return userInput;
        }

        private static string printAndGet(string i_Message)
        {
            Console.WriteLine(i_Message);

            return Console.ReadLine();
        }

        private static bool checkIfFloat(string i_UserChoise)
        {
            bool isFloat = float.TryParse(i_UserChoise, out _);

            if(!isFloat)
            {
                Console.Clear();
                printRedWarning();
                Console.Write("This number isn't valid, ");
            }

            return isFloat;
        }

        private static bool checkIfInt(string i_UserChoise)
        {
            bool isInt = int.TryParse(i_UserChoise, out _);

            if (!isInt)
            {
                Console.Clear();
                printRedWarning();
                Console.Write("This number isn't valid, ");
            }

            return isInt;
        }

        private static bool checkIfStringValid(string i_UserChoise)
        {
            bool isStringValid = i_UserChoise != string.Empty;

            if (!isStringValid)
            {
                Console.Clear();
                printRedWarning();
                Console.Write("This string isn't valid, ");
            }

            return isStringValid;
        }

        private static bool checkIfCheckIfTheValueEnumIsValid(string i_UserInput, int i_EnumLength)
        {
            bool isUserInputValid = int.TryParse(i_UserInput, out int userChoose);

            isUserInputValid &= userChoose >= 1 && userChoose <= i_EnumLength;
            if (!isUserInputValid)
            {
                Console.Clear();
                printRedWarning();
                Console.WriteLine("You entered unvalid value, Please enter valid value from the list below");
            }

            return isUserInputValid;
        }

        private static void printEnumOption<T>()
        {
            foreach (var option in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"Enter [{(int)option}] to {addSpacesBeforeUpperLetter(option.ToString())}");
            }
        }

        private static string addSpacesBeforeUpperLetter(string i_Option)
        {
            return string.Concat(i_Option.Select(x => char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
        }

        private static T gettingUserInputForGeneralEnum<T>(string i_MessageToPrint)
        {
            string userChoise;
            int enumLength = Enum.GetNames(typeof(T)).Length;

            do
            {
                Console.WriteLine(string.Format($"Please enter {i_MessageToPrint}: "));
                printEnumOption<T>();
                userChoise = Console.ReadLine();
            } 
            while (!checkIfCheckIfTheValueEnumIsValid(userChoise, enumLength));

            return (T)Enum.Parse(typeof(T), userChoise);
        }

        private void addVehicleToGarage()
        {
            string fullName, ownerPhoneNumber, licenceNumber, stringForUnWantedInput;
            Vehicle vehicle;

            Console.Clear();
            licenceNumber = getIntAsStringFromUser("license number");
            if (r_Garage.IsVehicleExsistInDataStruct(licenceNumber) == false)
            {
                fullName = getStringFromUser("full name");
                ownerPhoneNumber = getIntAsStringFromUser("phone number");
                vehicle = addInformationVehicle(licenceNumber);
                r_Garage.InsertVehicleToGarge(fullName, ownerPhoneNumber, vehicle);
                printSuccsedGreenmessage("The vehicle added to garage");
            }
            else
            {
                r_Garage.ChangeStatusOfVehicleInGarage(InformationOfVehicleInGarage.eStatusInGarge.InRepair, licenceNumber);
                Console.WriteLine("Vehicle already exsist, Status changed to \"in reapir\"");
            }

            stringForUnWantedInput = printAndGet("Press 'Enter' to continue.");
        }
        
        private void gettingAndPrintVehicleLicenseList()
        {
            string userInput, stringForUnWantedInput;
            List<string> vehicleLicenseListFromGarage;
            InformationOfVehicleInGarage.eStatusInGarge statusInGargeByUser;
            string messageToSendToFunaction = "the status from the list below";

            Console.WriteLine("Please enter 1 for see all the list of licence number otherwise enter any key to see list of fileters");
            userInput = Console.ReadLine();
            if (userInput == "1")
            {
                vehicleLicenseListFromGarage = r_Garage.ListOfVehicleLicenseNumbers();
            }
            else
            {
                statusInGargeByUser = gettingUserInputForGeneralEnum<InformationOfVehicleInGarage.eStatusInGarge>(messageToSendToFunaction);
                vehicleLicenseListFromGarage = r_Garage.ListOfVehicleLicenseNumbersByFiltering(statusInGargeByUser);
            }

            Console.Clear();
            if (vehicleLicenseListFromGarage.Any() == false)
            {
                Console.WriteLine("The list of licence number is empty");
            }
            else
            {
                Console.WriteLine("The list of licence number is : ");
                foreach (string licenseNumber in vehicleLicenseListFromGarage)
                {
                    Console.WriteLine(licenseNumber);
                }
            }

            stringForUnWantedInput = printAndGet("Press 'Enter' to continue.");
        }
        
        private void changeVehicleStatusInGarge()
        {
            bool isUpdateStatus = false;
            string keyToReturnToMenu = null, licenceNumber, stringForUnWantedInput;
            string messageToSendToFunaction = "status to change the vehicle from the list below";
            InformationOfVehicleInGarage.eStatusInGarge statusOfVehicleToChangeTo;

            while ((isUpdateStatus == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    Console.Clear();
                    licenceNumber = getIntAsStringFromUser("license number");
                    statusOfVehicleToChangeTo = gettingUserInputForGeneralEnum<InformationOfVehicleInGarage.eStatusInGarge>(messageToSendToFunaction);
                    r_Garage.ChangeStatusOfVehicleInGarage(statusOfVehicleToChangeTo, licenceNumber);
                    isUpdateStatus = true;
                    printSuccsedGreenmessage("The Vehicle status changed");
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);

                    keyToReturnToMenu = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            stringForUnWantedInput = printAndGet("Press 'Enter' to continue.");
        }

        private void fillingAirInWheelsToMaximum()
        {
            string licenseNumber, stringForUnWantedInput;
            bool isFilledToMaximum = false;
            string keyToReturnToMenu = null;

            while ((isFilledToMaximum == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    Console.Clear();
                    licenseNumber = getIntAsStringFromUser("license number");
                    r_Garage.FillingAirWheelsToMax(licenseNumber);
                    isFilledToMaximum = true;
                    printSuccsedGreenmessage("The air in the wheels was filled to the maximum");
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    keyToReturnToMenu = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            stringForUnWantedInput = printAndGet("Press 'Enter' to continue.");
        }
        
        private void refulVehicle()
        {
            string licenseNumber, userInput = null, stringForUnWantedInput;
            float amountToFill;
            bool isRefulVehicleWork = false;
            FuelEngine.eKindOfFuel kindOfFuelToFill;
            string messageToSendToFuncation = "type of fuel from the list below";

            while ((isRefulVehicleWork == false) && (userInput != r_KeyToExitToMenu))
            {
                try
                {
                    Console.Clear();
                    licenseNumber = getIntAsStringFromUser("license number");
                    amountToFill = getFloatNumberFromUser("amount of fuel you want to fill");
                    kindOfFuelToFill = gettingUserInputForGeneralEnum<FuelEngine.eKindOfFuel>(messageToSendToFuncation);
                    r_Garage.RefuelVehicle(licenseNumber, kindOfFuelToFill, amountToFill);
                    isRefulVehicleWork = true;
                    printSuccsedGreenmessage("The vehicle refuel");
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchRangeExPrintToConsole(ex);
                    userInput = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    userInput = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            stringForUnWantedInput = printAndGet("Press 'Enter' to continue.");
        }
        
        private void chargeVehicle()
        {
            string licenseNumber, userInput = null, stringForUnWantedInput;
            float amountToCharge;
            bool isCharge = false;

            while (isCharge == false && userInput != r_KeyToExitToMenu)
            {
                try
                {
                    Console.Clear();
                    licenseNumber = getIntAsStringFromUser("license number");
                    amountToCharge = getFloatNumberFromUser("amount of minutes that you want to charge in vehicle");
                    r_Garage.ChargingVehicle(licenseNumber, amountToCharge);
                    isCharge = true;
                    printSuccsedGreenmessage("The vehicle battery charged");
                }
                catch (ValueOutOfRangeException ex)
                {
                    ex.MaxValue = ex.MaxValue * 60f;
                    ex.MinValue = ex.MinValue * 60f;
                    catchRangeExPrintToConsole(ex);
                    userInput = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
                catch (Exception ex)
                {
                    Console.Clear();
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    userInput = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            stringForUnWantedInput = printAndGet("Press 'Enter' to continue.");
        }
        
        private void fullInformationOfVehicleInGarage()
        {
            string licenseNumber, fullInformationOfVehicle, stringForUnWantedInput;
            bool isFullInformationRecived = false;
            string keyToReturnToMenu = null;

            while ((isFullInformationRecived == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    Console.Clear();
                    licenseNumber = getIntAsStringFromUser("license number");
                    fullInformationOfVehicle = r_Garage.GettingFullInformationOfVehicleInGarage(licenseNumber);
                    Console.Clear();
                    Console.WriteLine("*************************");
                    Console.WriteLine(fullInformationOfVehicle);
                    Console.WriteLine("*************************");
                    isFullInformationRecived = true;
                }
                catch (Exception ex)
                {
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    keyToReturnToMenu = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            stringForUnWantedInput = printAndGet("Press 'Enter' to continue.");
        }

        private Vehicle addInformationVehicle(string i_LicenceNumber)
        {
            string vehicleModel;
            VehicleFactory.eVehicleType vehicleType;
            Vehicle vehicleToReturn;
            string messageToSendToFuncationVehicleType = "type of vehicle from the list below";

            vehicleType = gettingUserInputForGeneralEnum<VehicleFactory.eVehicleType>(messageToSendToFuncationVehicleType);
            vehicleToReturn = VehicleFactory.MakeVehicle(vehicleType);
            vehicleModel = getStringFromUser("vehicle model");
            vehicleToReturn.SettingVehicleInformation(i_LicenceNumber, vehicleModel);
            insertAirPressureToWheelOfVehicle(vehicleToReturn);
            insertEnergyLeftInEngine(vehicleType, vehicleToReturn);
            gettingAndSettingUniqueInformationFromUser(vehicleToReturn);

            return vehicleToReturn;
        }

        private void insertAirPressureToWheelOfVehicle(Vehicle i_Vehicle)
        {
            bool isAirPressureInsertGood = false;
            float currentWheelAirPressure;
            string wheelManufacturerName;

            wheelManufacturerName = getStringFromUser("wheel manufacturer name");
            while (isAirPressureInsertGood == false)
            {
                try
                {
                    currentWheelAirPressure = getFloatNumberFromUser("current wheel air pressure");
                    i_Vehicle.SetWheelInformation(currentWheelAirPressure, wheelManufacturerName);
                    isAirPressureInsertGood = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchRangeExPrintToConsole(ex);
                }
            }
        }

        private void gettingAndSettingUniqueInformationFromUser(Vehicle i_VehicleToReturn)
        {
            bool isUniqueInformationThrowEx = false;
            List<string> listOfUniqueInformation = new List<string>();
            string stringWhichUniqueInformationToEnter;
            int numberOfUniqueInformation;

            stringWhichUniqueInformationToEnter = i_VehicleToReturn.GettingWithSpecialInformationOfVehicleUiNeedToEnter(out numberOfUniqueInformation);
            Console.WriteLine(stringWhichUniqueInformationToEnter);
            while (isUniqueInformationThrowEx == false)
            {
                try
                {
                    for (int i = 0; i < numberOfUniqueInformation; i++)
                    {
                        listOfUniqueInformation.Add(Console.ReadLine());
                    }

                    i_VehicleToReturn.SetVehicleUniqueInformation(listOfUniqueInformation);
                    isUniqueInformationThrowEx = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    listOfUniqueInformation.Clear();
                    catchRangeExPrintToConsole(ex);
                }
                catch (Exception ex)
                {
                    listOfUniqueInformation.Clear();
                    printRedWarning();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again");
                    Console.WriteLine(stringWhichUniqueInformationToEnter);
                }
            }
        }

        private void insertEnergyLeftInEngine(VehicleFactory.eVehicleType i_VehicleType, Vehicle i_Vehicle)
        {
            bool isEnergyIsInRange = false;
            string stringMessageToPrintToConsole = string.Format($"energy left in {i_VehicleType.ToString()} engine");
            float capacityOfEnergyLeftInEngine;

            while (isEnergyIsInRange == false)
            {
                try
                {
                    capacityOfEnergyLeftInEngine = getFloatNumberFromUser(stringMessageToPrintToConsole);
                    i_Vehicle.InsertEngineInformation(capacityOfEnergyLeftInEngine);
                    isEnergyIsInRange = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchRangeExPrintToConsole(ex);
                }
            }
        }

        public static void catchRangeExPrintToConsole(ValueOutOfRangeException i_Ex)
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