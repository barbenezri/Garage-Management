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
                Console.Clear();
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

        private static void printSuccsedGreenMessage(string i_Message)
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

        private static void printAndWait(string i_Message)
        {
            Console.WriteLine(i_Message);
            Console.ReadLine();
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

        private List<string> createListOfVehicleByChoice(string i_UserInput)
        {
            List<string> vehicleLicenseListFromGarage;
            string messageToSendToFunaction = "the status from the list below";
            InformationOfVehicleInGarage.eStatusInGarge statusInGargeByUser;

            if (i_UserInput == "1")
            {
                vehicleLicenseListFromGarage = r_Garage.ListOfVehicleLicenseNumbers();
            }
            else
            {
                statusInGargeByUser = gettingUserInputForGeneralEnum<InformationOfVehicleInGarage.eStatusInGarge>(messageToSendToFunaction);
                vehicleLicenseListFromGarage = r_Garage.ListOfVehicleLicenseNumbersByFiltering(statusInGargeByUser);
            }

            return vehicleLicenseListFromGarage;
        }

        private static void printVehicleList(List<string> i_VehicleList)
        {
            if (i_VehicleList.Count == 0)
            {
                Console.WriteLine("The list of licence number is empty");
            }
            else
            {
                Console.WriteLine("The list of licence number is : ");
                foreach (string licenseNumber in i_VehicleList)
                {
                    Console.WriteLine(licenseNumber);
                }
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

        private void gettingAndPrintVehicleLicenseList()
        {
            List<string> vehicleLicenseListFromGarage;

            Console.WriteLine("Please enter [1] for see all licences number or any other key to see list of fileters");
            vehicleLicenseListFromGarage = createListOfVehicleByChoice(Console.ReadLine());
            Console.Clear();
            printVehicleList(vehicleLicenseListFromGarage);
            printAndWait("Press 'Enter' to continue.");
        }
        
        private void changeVehicleStatusInGarge()
        {
            bool isStatusUpdate = false;
            string exitKey = null, licenceNumber;
            string message = "status to change the vehicle from the list below";
            InformationOfVehicleInGarage.eStatusInGarge vehicleDesireStatus;

            while ((isStatusUpdate == false) && (exitKey != r_KeyToExitToMenu))
            {
                Console.Clear();
                try
                {
                    licenceNumber = getIntAsStringFromUser("license number");
                    vehicleDesireStatus = gettingUserInputForGeneralEnum<InformationOfVehicleInGarage.eStatusInGarge>(message);
                    r_Garage.ChangeStatusOfVehicleInGarage(vehicleDesireStatus, licenceNumber);
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
            string fullName, ownerPhoneNumber, licenceNumber;
            Vehicle vehicle;

            licenceNumber = getIntAsStringFromUser("license number");
            if (r_Garage.IsVehicleExsistInDataStruct(licenceNumber) == false)
            {
                fullName = getStringFromUser("full name");
                ownerPhoneNumber = getIntAsStringFromUser("phone number");
                vehicle = addInformationVehicle(licenceNumber);
                r_Garage.InsertVehicleToGarge(fullName, ownerPhoneNumber, vehicle);
                printSuccsedGreenMessage("The vehicle added to garage");
            }
            else
            {
                r_Garage.ChangeStatusOfVehicleInGarage(InformationOfVehicleInGarage.eStatusInGarge.InRepair, licenceNumber);
                Console.WriteLine("Vehicle already exsist, Status changed to \"in reapir\"");
            }

            printAndWait("Press 'Enter' to continue.");
        }

        private void fillingAirInWheelsToMaximum()
        {
            string licenseNumber;
            bool isFilledToMaximum = false;
            string exitKey = null;

            while ((isFilledToMaximum == false) && (exitKey != r_KeyToExitToMenu))
            {
                Console.Clear();
                try
                {
                    licenseNumber = getIntAsStringFromUser("license number");
                    r_Garage.FillingAirWheelsToMax(licenseNumber);
                    isFilledToMaximum = true;
                    printSuccsedGreenMessage("The air in the wheels was filled to the maximum");
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
        
        private void refulVehicle()
        {
            string licenseNumber, exitKey = null;
            float amountToFill;
            bool isRefulVehicleWork = false;
            FuelEngine.eKindOfFuel kindOfFuelToFill;
            string messageToSendToFuncation = "type of fuel from the list below";

            while ((isRefulVehicleWork == false) && (exitKey != r_KeyToExitToMenu))
            {
                Console.Clear();
                try
                {
                    licenseNumber = getIntAsStringFromUser("license number");
                    amountToFill = getFloatNumberFromUser("amount of fuel you want to fill");
                    kindOfFuelToFill = gettingUserInputForGeneralEnum<FuelEngine.eKindOfFuel>(messageToSendToFuncation);
                    r_Garage.RefuelVehicle(licenseNumber, kindOfFuelToFill, amountToFill);
                    isRefulVehicleWork = true;
                    printSuccsedGreenMessage("The vehicle refuel");
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchRangeExPrintToConsole(ex);
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
            string licenseNumber, exitKey = null;
            float amountToCharge;
            bool isCharge = false;

            while (isCharge == false && exitKey != r_KeyToExitToMenu)
            {
                Console.Clear();
                try
                {
                    licenseNumber = getIntAsStringFromUser("license number");
                    amountToCharge = getFloatNumberFromUser("amount of minutes that you want to charge in vehicle");
                    r_Garage.ChargingVehicle(licenseNumber, amountToCharge);
                    isCharge = true;
                    printSuccsedGreenMessage("The vehicle battery charged");
                }
                catch (ValueOutOfRangeException ex)
                {
                    ex.MaxValue *= 60f;
                    ex.MinValue *= 60f;
                    catchRangeExPrintToConsole(ex);
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
        
        private void fullInformationOfVehicleInGarage()
        {
            string licenseNumber, fullInformationOfVehicle, exitKey = null;
            bool isFullInformationRecived = false;

            while ((isFullInformationRecived == false) && (exitKey != r_KeyToExitToMenu))
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
                    exitKey = printAndGet(string.Format($"for exit to menu enter {r_KeyToExitToMenu}"));
                }
            }

            printAndWait("Press 'Enter' to continue.");
        }

        private Vehicle addInformationVehicle(string i_LicenceNumber)
        {
            string vehicleModel, message = "type of vehicle from the list below";
            VehicleFactory.eVehicleType vehicleType;
            Vehicle vehicleToReturn;

            vehicleType = gettingUserInputForGeneralEnum<VehicleFactory.eVehicleType>(message);
            vehicleToReturn = VehicleFactory.MakeVehicle(vehicleType);
            vehicleModel = getStringFromUser("vehicle model");
            vehicleToReturn.SettingVehicleInformation(i_LicenceNumber, vehicleModel);
            insertAirPressureToWheelOfVehicle(vehicleToReturn);
            insertEnergyLeftInEngine(vehicleType, vehicleToReturn);
            getAndSetUniqueInfo(vehicleToReturn);

            return vehicleToReturn;
        }

        private void insertAirPressureToWheelOfVehicle(Vehicle i_Vehicle)
        {
            bool isAirPressureValid = false;
            float currentWheelAirPressure;
            string wheelManufacturerName;

            wheelManufacturerName = getStringFromUser("wheel manufacturer name");
            while (isAirPressureValid == false)
            {
                try
                {
                    currentWheelAirPressure = getFloatNumberFromUser("current wheel air pressure");
                    i_Vehicle.SetWheelInformation(currentWheelAirPressure, wheelManufacturerName);
                    isAirPressureValid = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchRangeExPrintToConsole(ex);
                }
            }
        }

        private void getAndSetUniqueInfo(Vehicle i_VehicleToReturn)
        {
            bool isExWasThrow = false;
            List<string> UniqueInfoList = new List<string>();
            string uniqueInfoMessage;

            uniqueInfoMessage = i_VehicleToReturn.GetSpecialInfoMessage(out int numberOfUniqueInformation);
            while (isExWasThrow == false)
            {
                Console.WriteLine(uniqueInfoMessage);
                try
                {
                    for (int i = 0; i < numberOfUniqueInformation; i++)
                    {
                        UniqueInfoList.Add(Console.ReadLine());
                    }

                    i_VehicleToReturn.SetVehicleUniqueInformation(UniqueInfoList);
                    isExWasThrow = true;
                }
                catch (ValueOutOfRangeException ex)
                {
                    catchRangeExPrintToConsole(ex);
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

        private void insertEnergyLeftInEngine(VehicleFactory.eVehicleType i_VehicleType, Vehicle i_Vehicle)
        {
            bool isEnergyIsInRange = false;
            string message= string.Format($"energy left in {i_VehicleType} engine");
            float capacityOfEnergyLeftInEngine;

            while (isEnergyIsInRange == false)
            {
                try
                {
                    capacityOfEnergyLeftInEngine = getFloatNumberFromUser(message);
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