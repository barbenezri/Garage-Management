using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUi
    {
        private static GarageUi m_GarageUi;
        private Garage m_Garage = new Garage();
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

        public static GarageUi Singelton()
        {
            if (m_GarageUi == null)
            {
                m_GarageUi = new GarageUi();
            }

            return m_GarageUi;
        }

        internal void InitiatGarageMenu()
        {
            eGarageMenu? userInput = null;

            while (userInput != eGarageMenu.Exit)
            {
                printGarageMenu();
                userInput = (eGarageMenu)getUserPickFromMenu();
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

        private int getUserPickFromMenu()
        {
            string userInput;

            do 
            {
                Console.Write("Please enter valid input: ");
                userInput = Console.ReadLine();
            } while (!isPickValid(userInput));

            Console.Clear();
            return int.Parse(userInput);
        }

        private bool isPickValid(string i_userInput)
        {
            int inputRequireRange = Enum.GetNames(typeof(eGarageMenu)).Length;
            bool isNumber = int.TryParse(i_userInput, out int inputAsNumber);
            bool isInRange = isNumber ? (inputAsNumber >= 0 && inputAsNumber <= inputRequireRange) : false;

            if (!isNumber || !isInRange)
            {
                printGarageMenu();
                printRedWarning();
                if (!isNumber)
                {
                    Console.WriteLine("This is not a natural number!");
                }
                else
                {
                    Console.WriteLine("This option not available right now.");
                }
            }

            return isNumber && isInRange;
        }

        private void printGarageMenu()
        {
            Console.Clear();
            Console.WriteLine(
@"Choose your action in garage: 
[1] Insert new vehicle
[2] Display vehicles license number by status
[3] Change vehicle status in the garage
[4] Inflate vehicle wheels to maximum air pressure
[5] Refuel vehicle
[6] Charge electric vehicle
[7] Display full information on vehicle in garage
[8] Exit");
        }

        private static void printRedWarning()
        {
            Console.Write("Warning: ", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
        }



        //need to check bellow here

        private void addVehicleToGarage()
        {
            string fullName, ownerPhoneNumber, licenceNumber;
            string licenseNumberStringToSendToFunactionToPrint = "Please enter license number";
            Vehicle vehicle;

            licenceNumber = GettingStringToWorkWith(licenseNumberStringToSendToFunactionToPrint);
            if (m_Garage.IsVehicleExsistInDataStruct(licenceNumber) == false)
            {
                vehicle = addInformationVehicle(licenceNumber);
                fullName = gettingNameOfVehicleOwner();
                ownerPhoneNumber = gettingPhoneNumberFromUser();
                m_Garage.InsertVehicleToGarge(fullName, ownerPhoneNumber, vehicle);
                Console.WriteLine("Your vehicle successfully added to garage");
            }
            else
            {
                InformationOfVehicleInGarage.eStatusInGarge statusToChangeTheVehicle = InformationOfVehicleInGarage.eStatusInGarge.InRepair;

                m_Garage.ChangeStatusOfVehicleInGarage(statusToChangeTheVehicle, licenceNumber);
                Console.WriteLine("Vehicle already exsist, Changing status to in reapir");
            }
        }

        private void gettingAndPrintVehicleLicenseList()
        {
            InformationOfVehicleInGarage.eStatusInGarge statusInGargeByUser;
            string userInput;
            List<string> vehicleLicenseListFromGarage = null;
            string messageToSendToFunaction = "Please enter the status from the list below";

            Console.WriteLine("Please enter 1 for see all the list of licence number otherwise enter any key to see list of fileters");
            userInput = Console.ReadLine();
            if (userInput == "1")
            {
                vehicleLicenseListFromGarage = m_Garage.ListOfVehicleLicenseNumbers();
            }
            else
            {
                statusInGargeByUser = GettingUserInputForGeneralEnum<InformationOfVehicleInGarage.eStatusInGarge>(messageToSendToFunaction);
                vehicleLicenseListFromGarage = m_Garage.ListOfVehicleLicenseNumbersByFiltering(statusInGargeByUser);
            }

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
        }

        private void changeVehicleStatusInGarge()
        {
            bool isUpdateStatus = false;
            string keyToReturnToMenu = null;
            string licenceNumber;
            string licenseNumberStringToSendToFunactionToPrint = "Please enter license number";
            InformationOfVehicleInGarage.eStatusInGarge statusOfVehicleToChangeTo;
            string messageToSendToFunaction = "Please enter status to change the car from the list below : ";

            while ((isUpdateStatus == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    licenceNumber = GettingStringToWorkWith(licenseNumberStringToSendToFunactionToPrint);
                    statusOfVehicleToChangeTo = GettingUserInputForGeneralEnum<InformationOfVehicleInGarage.eStatusInGarge>(messageToSendToFunaction);
                    m_Garage.ChangeStatusOfVehicleInGarage(statusOfVehicleToChangeTo, licenceNumber);
                    isUpdateStatus = true;
                    Console.WriteLine("You succsed to change status of car");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    keyToReturnToMenu = AskingIfYouWantToExit(r_KeyToExitToMenu);
                }
            }
        }

        private void fillingAirInWheelsToMaximum()
        {
            string licenseNumber;
            bool isFilledToMaximum = false;
            string licenseNumberStringToSendToFunactionToPrint = "Please enter license number";
            string keyToReturnToMenu = null;

            while ((isFilledToMaximum == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    licenseNumber = GettingStringToWorkWith(licenseNumberStringToSendToFunactionToPrint);
                    m_Garage.FillingAirWheelsToMax(licenseNumber);
                    isFilledToMaximum = true;
                    Console.WriteLine("Filling air in wheels to maximum succsed");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    keyToReturnToMenu = AskingIfYouWantToExit(r_KeyToExitToMenu);
                }
            }
        }

        private void refulVehicle()
        {
            string licenseNumber, userInput = null;
            float amountToFill;
            bool isRefulVehicleWork = false;
            FuelEngine.eKindOfFuel kindOfFuelToFill;
            string messageToSendToFuncation = "Please enter type of fuel from the list below";
            string licenseNumberStringToSendToFunactionToPrint = "Please enter license number";

            while ((isRefulVehicleWork == false) && (userInput != r_KeyToExitToMenu))
            {
                try
                {
                    licenseNumber = GettingStringToWorkWith(licenseNumberStringToSendToFunactionToPrint);
                    Console.WriteLine("Please enter the amount of fuel you want to fill");
                    userInput = Console.ReadLine();
                    amountToFill = ChekingFloatNumberFromUser(userInput);
                    kindOfFuelToFill = GettingUserInputForGeneralEnum<FuelEngine.eKindOfFuel>(messageToSendToFuncation);
                    m_Garage.RefuelVehicle(licenseNumber, kindOfFuelToFill, amountToFill);
                    Console.WriteLine("You succsed to reful the vehicle");
                    isRefulVehicleWork = true;
                }
                catch (ValueOutOfRangeException voore)
                {
                    CatchRangeExPrintToConsole(voore);
                    userInput = AskingIfYouWantToExit(r_KeyToExitToMenu);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    userInput = AskingIfYouWantToExit(r_KeyToExitToMenu);
                }
            }
        }

        private void chargeVehicle()
        {
            string licenseNumber, userInput = null;
            float amountToCharge;
            bool isCharge = false;
            string licenseNumberStringToSendToFunactionToPrint = "Please enter license number";

            while (isCharge == false && userInput != r_KeyToExitToMenu)
            {
                try
                {
                    licenseNumber = GettingStringToWorkWith(licenseNumberStringToSendToFunactionToPrint);
                    Console.WriteLine("Please enter the amount of minutes that you want to charge in vehicle");
                    userInput = Console.ReadLine();
                    amountToCharge = ChekingFloatNumberFromUser(userInput);
                    m_Garage.ChargingVehicle(licenseNumber, amountToCharge);
                    Console.WriteLine("succsed charging vehicle");
                    isCharge = true;
                }
                catch (ValueOutOfRangeException voore)
                {
                    voore.MaxValue = voore.MaxValue * 60f;
                    voore.MinValue = voore.MinValue * 60f;
                    CatchRangeExPrintToConsole(voore);
                    userInput = AskingIfYouWantToExit(r_KeyToExitToMenu);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    userInput = AskingIfYouWantToExit(r_KeyToExitToMenu);
                }
            }
        }

        private void fullInformationOfVehicleInGarage()
        {
            string licenseNumber, fullInformationOfVehicle;
            bool isFullInformationRecived = false;
            string licenseNumberStringToSendToFunactionToPrint = "Please enter license number";
            string keyToReturnToMenu = null;

            while ((isFullInformationRecived == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    licenseNumber = GettingStringToWorkWith(licenseNumberStringToSendToFunactionToPrint);
                    fullInformationOfVehicle = m_Garage.GettingFullInformationOfVehicleInGarage(licenseNumber);
                    Console.WriteLine(fullInformationOfVehicle);
                    isFullInformationRecived = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    keyToReturnToMenu = AskingIfYouWantToExit(r_KeyToExitToMenu);
                }
            }
        }







        private string gettingNameOfVehicleOwner()
        {
            string fullName;
            bool validFullName;

            Console.WriteLine("Please Enter Your full name : ");
            fullName = Console.ReadLine();
            validFullName = IsStringValidName(fullName);
            while (validFullName == false)
            {
                Console.WriteLine("Please try again, you enterd not valid  full name");
                Console.WriteLine("Please Enter Your full name : ");
                fullName = Console.ReadLine();
                validFullName = IsStringValidName(fullName);
            }

            return fullName;
        }

        private Vehicle addInformationVehicle(string i_LicenceNumber)
        {
            string vehicleModel;
            VehicleFactory.eVehicleType vehicleType;
            Vehicle vehicleToReturn;
            string messageToSendToFuncationVehicleType = "Please enter type of Vehicle from the list below";
            string vehicleModelString = "Please enter your vehicle model name";

            vehicleType = GettingUserInputForGeneralEnum<VehicleFactory.eVehicleType>(messageToSendToFuncationVehicleType);
            vehicleToReturn = VehicleFactory.MakeVehicle(vehicleType);
            vehicleModel = GettingStringToWorkWith(vehicleModelString);
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
            string wheelManufacturerNameString = "Please enter your wheel manufacturer name";
            string wheelManufacturerName;

            wheelManufacturerName = GettingStringToWorkWith(wheelManufacturerNameString);
            while (isAirPressureInsertGood == false)
            {
                try
                {
                    currentWheelAirPressure = gettingAirPressureFromUser();
                    i_Vehicle.SetWheelInformation(currentWheelAirPressure, wheelManufacturerName);
                    isAirPressureInsertGood = true;
                }
                catch (ValueOutOfRangeException voore)
                {
                    CatchRangeExPrintToConsole(voore);
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
                catch (ValueOutOfRangeException voore)
                {
                    listOfUniqueInformation.Clear();
                    CatchRangeExPrintToConsole(voore);
                }
                catch (Exception ex)
                {
                    listOfUniqueInformation.Clear();
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Please try again");
                    Console.WriteLine(stringWhichUniqueInformationToEnter);
                }
            }
        }

        private void insertEnergyLeftInEngine(VehicleFactory.eVehicleType i_VehicleType, Vehicle i_Vehicle)
        {
            string userInput;
            bool isEnergyIsInRange = false;
            string stringMessageToPrintToConsole = string.Format("Please Enter your energy left in {0} engine : ", i_VehicleType.ToString());
            float capacityOfEnergyLeftInEngine;

            Console.WriteLine(stringMessageToPrintToConsole);
            userInput = Console.ReadLine();
            while (isEnergyIsInRange == false)
            {
                try
                {
                    capacityOfEnergyLeftInEngine = ChekingFloatNumberFromUser(userInput);
                    i_Vehicle.InsertEngineInformation(capacityOfEnergyLeftInEngine);
                    isEnergyIsInRange = true;
                }
                catch (ValueOutOfRangeException voore)
                {
                    CatchRangeExPrintToConsole(voore);
                    userInput = Console.ReadLine();
                }
            }
        }

        private float gettingAirPressureFromUser()
        {
            string userInput;
            float airPressure;

            Console.WriteLine("Please Enter Your current wheel air pressure : ");
            userInput = Console.ReadLine();
            airPressure = ChekingFloatNumberFromUser(userInput);

            return airPressure;
        }

        private string gettingPhoneNumberFromUser()
        {
            string phoneNumber;
            bool isPhoneNumber;

            Console.WriteLine("Please Enter Your Phone Number : ");
            phoneNumber = Console.ReadLine();
            isPhoneNumber = IsValidNumberPhone(phoneNumber);
            while (isPhoneNumber == false)
            {
                Console.WriteLine("You entered not valid phone number, Please enter again");
                phoneNumber = Console.ReadLine();
                isPhoneNumber = IsValidNumberPhone(phoneNumber);
            }

            return phoneNumber;
        }



        // UIFunctionhellper

        public static float ChekingFloatNumberFromUser(string i_UserInput)
        {
            float generalFloatNumber;
            bool isFloatNumber = IsFloat(i_UserInput, out generalFloatNumber);

            while (isFloatNumber == false || (generalFloatNumber < 0))
            {
                Console.WriteLine("you didnt enter a good number, please try again");
                i_UserInput = Console.ReadLine();
                isFloatNumber = IsFloat(i_UserInput, out generalFloatNumber);
            }

            return generalFloatNumber;
        }

        public static bool IsFloat(string i_UserChoise, out float o_Number)
        {
            bool isCorrect = float.TryParse(i_UserChoise, out o_Number);

            return isCorrect;
        }

        // $G$ CSS-015 (-3) Bad variable name (should be in the form of: ref io_CamelCase).
        public static void CheckInputOfStringAndIfNotValidAskingForNewString(ref string o_UserInput)
        {
            bool isStringNull, isOnlyWhiteSpace;

            isStringNull = string.IsNullOrEmpty(o_UserInput);
            isOnlyWhiteSpace = CheckIfStringIsOnlyWhiteSpace(o_UserInput);
            while ((isStringNull == true) || (isOnlyWhiteSpace == true))
            {
                Console.WriteLine("Your entered not valid value, Please try again");
                o_UserInput = Console.ReadLine();
                isStringNull = string.IsNullOrEmpty(o_UserInput);
                isOnlyWhiteSpace = CheckIfStringIsOnlyWhiteSpace(o_UserInput);
            }
        }

        public static bool CheckIfStringIsOnlyWhiteSpace(string i_StringToCheck)
        {
            bool isOnlyWhiteSpace = true;

            foreach (char charInString in i_StringToCheck)
            {
                if (char.IsWhiteSpace(charInString) == false)
                {
                    isOnlyWhiteSpace = false;
                    break;
                }
            }

            return isOnlyWhiteSpace;
        }

        public static string GettingStringToWorkWith(string i_MessageToPrint)
        {
            string generalStringToReturn;

            Console.WriteLine(i_MessageToPrint);
            generalStringToReturn = Console.ReadLine();
            CheckInputOfStringAndIfNotValidAskingForNewString(ref generalStringToReturn);

            return generalStringToReturn;
        }

        public static void CatchRangeExPrintToConsole(ValueOutOfRangeException i_Voore)
        {
            string messageToPrint = string.Format("{0}. range should be between {1} to {2}", i_Voore.Message, i_Voore.MinValue, i_Voore.MaxValue);

            Console.WriteLine(messageToPrint);
            Console.WriteLine("Please try again");
        }

        public static bool IsCheckIfTheValueEnumIsValid(string i_UserInput, int i_EnumLength)
        {
            int userChoose;
            bool isUserInputValid = int.TryParse(i_UserInput, out userChoose);

            isUserInputValid = isUserInputValid && (userChoose >= 1 && userChoose <= i_EnumLength);

            return isUserInputValid;
        }

        public static void PrintEnumOption<T>()
        {
            foreach (string genral in Enum.GetNames(typeof(T)))
            {
                Console.WriteLine("Enter {1:D} for {0}", genral, Enum.Parse(typeof(T), genral));
            }
        }

        public static bool IsStringValidName(string i_StringToCheck)
        {
            bool isStringValidName = true;

            foreach (char charOfString in i_StringToCheck)
            {
                if ((char.IsLetter(charOfString) == false) && (char.IsWhiteSpace(charOfString) == false))
                {
                    isStringValidName = false;
                    break;
                }
            }

            if (isStringValidName == true)
            {
                if (CheckIfStringIsOnlyWhiteSpace(i_StringToCheck) == true)
                {
                    isStringValidName = false;
                }
            }

            return isStringValidName;
        }

        public static T GettingUserInputForGeneralEnum<T>(string i_MessageToPrint)
        {
            string userChoise;
            bool isTypeValid;
            T typeOfObjectToReturn;
            int enumLength = Enum.GetNames(typeof(T)).Length;

            Console.WriteLine(i_MessageToPrint);
            PrintEnumOption<T>();
            userChoise = Console.ReadLine();
            isTypeValid = IsCheckIfTheValueEnumIsValid(userChoise, enumLength);
            while (isTypeValid == false)
            {
                Console.WriteLine("You entered not valid value, Please enter valid value from the list below");
                PrintEnumOption<T>();
                userChoise = Console.ReadLine();
                isTypeValid = IsCheckIfTheValueEnumIsValid(userChoise, enumLength);
            }

            typeOfObjectToReturn = (T)Enum.Parse(typeof(T), userChoise);

            return typeOfObjectToReturn;
        }

        public static bool IsValidNumberPhone(string i_Phone)
        {
            bool isPhoneNumber = true;

            if (i_Phone.Length == 0)
            {
                isPhoneNumber = false;
            }

            foreach (char charString in i_Phone)
            {
                if (char.IsNumber(charString) == false)
                {
                    isPhoneNumber = false;
                    break;
                }
            }

            return isPhoneNumber;
        }

        public static string AskingIfYouWantToExit(string i_KeyToExitToMenu)
        {
            string strToExit = string.Format("for exit to menu enter {0}, to try again enter anything", i_KeyToExitToMenu);
            string userInput;

            Console.WriteLine(strToExit);
            userInput = Console.ReadLine();
            if (userInput == i_KeyToExitToMenu)
            {
                Console.Clear();
            }

            return userInput;
        }
    }
}