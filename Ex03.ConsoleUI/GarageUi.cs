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

        public static GarageUi Singelton()
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
                userInput = GettingUserInputForGeneralEnum<eGarageMenu>("valid pick");
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

        private static void printRedWarning() //add
        {
            Console.Write("Warning: ", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
        }

        private static void printSuccsedGreenMassage(string i_massage) //add
        {
            Console.Write($"You succeeded to {i_massage}", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
        }

        private static float GetFloatNumberFromUser(string i_DesireThings) //fixed
        {
            string userInput;

            do
            {
                userInput = printAndGet($"Please enter your {i_DesireThings}: ");
            } while (!CheckIfFloat(userInput));

            return float.Parse(userInput);
        }

        private static string GetIntAsStringFromUser(string i_DesireThings) //fixed
        {
            string userInput;

            do
            {
                userInput = printAndGet($"Please enter your {i_DesireThings}: ");
            } while (!CheckIfInt(userInput));

            return userInput;
        }

        private static string GetStringFromUser(string i_DesireThings) //fixed
        {
            string userInput;

            do
            {
                userInput = printAndGet($"Please enter your {i_DesireThings}: ");
            } while (!CheckIfStringValid(userInput));

            return userInput;
        }

        private static string printAndGet(string i_massage)
        {
            Console.WriteLine(i_massage);

            return Console.ReadLine();
        }

        private static bool CheckIfFloat(string i_UserChoise) //fixed
        {
            bool isFloat = float.TryParse(i_UserChoise, out _);

            if(!isFloat)
            {
                printRedWarning();
                Console.Write("This number isn't valid, ");
            }

            return isFloat;
        }

        private static bool CheckIfInt(string i_UserChoise) //fixed
        {
            bool isInt = int.TryParse(i_UserChoise, out _);

            if (!isInt)
            {
                printRedWarning();
                Console.Write("This number isn't valid, ");
            }

            return isInt;
        }

        private static bool CheckIfStringValid(string i_UserChoise) //fixed
        {
            bool isStringValid = i_UserChoise != string.Empty;

            if (!isStringValid)
            {
                printRedWarning();
                Console.Write("This string isn't  valid, ");
            }

            return isStringValid;
        }

        private static bool CheckIfCheckIfTheValueEnumIsValid(string i_UserInput, int i_EnumLength) //fixed
        {
            bool isUserInputValid = int.TryParse(i_UserInput, out int userChoose);

            isUserInputValid &= (userChoose >= 1 && userChoose <= i_EnumLength);
            if (!isUserInputValid)
            {
                Console.Clear();
                printRedWarning();
                Console.WriteLine("You entered unvalid value, Please enter valid value from the list below");
            }

            return isUserInputValid;
        }

        private static void PrintEnumOption<T>() //fixed
        {
            foreach (var genral in Enum.GetValues(typeof(T)))
            {
                Console.WriteLine($"Enter [{(int)genral}] for {genral}");
            }
        }

        private static T GettingUserInputForGeneralEnum<T>(string i_MessageToPrint) //fixed
        {
            string userChoise;
            int enumLength = Enum.GetNames(typeof(T)).Length;

            do
            {
                Console.WriteLine(String.Format($"Please enter {i_MessageToPrint}: "));
                PrintEnumOption<T>();
                userChoise = Console.ReadLine();
            } while (!CheckIfCheckIfTheValueEnumIsValid(userChoise, enumLength));

            return (T)Enum.Parse(typeof(T), userChoise);
        }





        //need to check bellow here
        //1
        private void addVehicleToGarage()
        {
            string fullName, ownerPhoneNumber, licenceNumber;
            Vehicle vehicle;

            licenceNumber = GetIntAsStringFromUser("license number");
            if (r_Garage.IsVehicleExsistInDataStruct(licenceNumber) == false)
            {
                fullName = GetStringFromUser("full name");
                ownerPhoneNumber = GetIntAsStringFromUser("phone number");
                vehicle = addInformationVehicle(licenceNumber);
                r_Garage.InsertVehicleToGarge(fullName, ownerPhoneNumber, vehicle);
                Console.WriteLine("Your vehicle successfully added to garage");
            }
            else
            {
                r_Garage.ChangeStatusOfVehicleInGarage(InformationOfVehicleInGarage.eStatusInGarge.InRepair, licenceNumber);
                Console.WriteLine("Vehicle already exsist, Status changed to \"in reapir\"");
            }
        }
        //2
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
                statusInGargeByUser = GettingUserInputForGeneralEnum<InformationOfVehicleInGarage.eStatusInGarge>(messageToSendToFunaction);
                vehicleLicenseListFromGarage = r_Garage.ListOfVehicleLicenseNumbersByFiltering(statusInGargeByUser);
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

            stringForUnWantedInput = printAndGet("Press 'Enter' to continue.");
        }
        //3
        private void changeVehicleStatusInGarge()
        {
            bool isUpdateStatus = false;
            string keyToReturnToMenu = null, licenceNumber;
            string messageToSendToFunaction = "status to change the vehicle from the list below";
            InformationOfVehicleInGarage.eStatusInGarge statusOfVehicleToChangeTo;

            while ((isUpdateStatus == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    licenceNumber = GetIntAsStringFromUser("license number");
                    statusOfVehicleToChangeTo = GettingUserInputForGeneralEnum<InformationOfVehicleInGarage.eStatusInGarge>(messageToSendToFunaction);
                    r_Garage.ChangeStatusOfVehicleInGarage(statusOfVehicleToChangeTo, licenceNumber);
                    isUpdateStatus = true;
                    printSuccsedGreenMassage("change status of your vehicle");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    keyToReturnToMenu = AskingIfYouWantToExit(r_KeyToExitToMenu);
                }
            }

            printAndGet("Press 'Enter' to continue.");
        }
        //4
        private void fillingAirInWheelsToMaximum()
        {
            string licenseNumber;
            bool isFilledToMaximum = false;
            string keyToReturnToMenu = null;

            while ((isFilledToMaximum == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    licenseNumber = GetIntAsStringFromUser("license number");
                    r_Garage.FillingAirWheelsToMax(licenseNumber);
                    isFilledToMaximum = true;
                    printSuccsedGreenMassage("fill air in wheels to maximum");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    keyToReturnToMenu = AskingIfYouWantToExit(r_KeyToExitToMenu);
                }
            }
        }
        //5
        private void refulVehicle()
        {
            string licenseNumber, userInput = null;
            float amountToFill;
            bool isRefulVehicleWork = false;
            FuelEngine.eKindOfFuel kindOfFuelToFill;
            string messageToSendToFuncation = "type of fuel from the list below";

            while ((isRefulVehicleWork == false) && (userInput != r_KeyToExitToMenu))
            {
                try
                {
                    licenseNumber = GetIntAsStringFromUser("license number");
                    amountToFill = GetFloatNumberFromUser("amount of fuel you want to fill");
                    kindOfFuelToFill = GettingUserInputForGeneralEnum<FuelEngine.eKindOfFuel>(messageToSendToFuncation);
                    r_Garage.RefuelVehicle(licenseNumber, kindOfFuelToFill, amountToFill);
                    isRefulVehicleWork = true;
                    printSuccsedGreenMassage("refuel the vehicle");
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
        //6
        private void chargeVehicle()
        {
            string licenseNumber, userInput = null;
            float amountToCharge;
            bool isCharge = false;

            while (isCharge == false && userInput != r_KeyToExitToMenu)
            {
                try
                {
                    licenseNumber = GetIntAsStringFromUser("license number");
                    amountToCharge = GetFloatNumberFromUser("amount of minutes that you want to charge in vehicle");
                    r_Garage.ChargingVehicle(licenseNumber, amountToCharge);
                    isCharge = true;
                    printSuccsedGreenMassage("charge your vehicle");
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
        //7
        private void fullInformationOfVehicleInGarage()
        {
            string licenseNumber, fullInformationOfVehicle;
            bool isFullInformationRecived = false;
            string keyToReturnToMenu = null;

            while ((isFullInformationRecived == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    licenseNumber = GetIntAsStringFromUser("license number");
                    fullInformationOfVehicle = r_Garage.GettingFullInformationOfVehicleInGarage(licenseNumber);
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






        private Vehicle addInformationVehicle(string i_LicenceNumber)
        {
            string vehicleModel;
            VehicleFactory.eVehicleType vehicleType;
            Vehicle vehicleToReturn;
            string messageToSendToFuncationVehicleType = "type of vehicle from the list below";

            vehicleType = GettingUserInputForGeneralEnum<VehicleFactory.eVehicleType>(messageToSendToFuncationVehicleType);
            vehicleToReturn = VehicleFactory.MakeVehicle(vehicleType);
            vehicleModel = GetStringFromUser("vehicle model");
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

            wheelManufacturerName = GetStringFromUser("wheel manufacturer name");
            while (isAirPressureInsertGood == false)
            {
                try
                {
                    currentWheelAirPressure = GetFloatNumberFromUser("current wheel air pressure");
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
            bool isEnergyIsInRange = false;
            string stringMessageToPrintToConsole = string.Format($"energy left in {i_VehicleType.ToString()} engine");
            float capacityOfEnergyLeftInEngine;

            while (isEnergyIsInRange == false)
            {
                try
                {
                    capacityOfEnergyLeftInEngine = GetFloatNumberFromUser(stringMessageToPrintToConsole);
                    i_Vehicle.InsertEngineInformation(capacityOfEnergyLeftInEngine);
                    isEnergyIsInRange = true;
                }
                catch (ValueOutOfRangeException voore)
                {
                    CatchRangeExPrintToConsole(voore);
                }
            }
        }

        public static void CatchRangeExPrintToConsole(ValueOutOfRangeException i_Voore)
        {
            string messageToPrint = string.Format("{0}.{1}range should be between {2} to {3}",
                i_Voore.Message,
                Environment.NewLine,
                i_Voore.MinValue,
                i_Voore.MaxValue);
            printRedWarning();
            Console.WriteLine(messageToPrint);
            Console.WriteLine("Please try again");
        }

        public static string AskingIfYouWantToExit(string i_KeyToExitToMenu)
        {
            string strToExit = string.Format($"for exit to menu enter {i_KeyToExitToMenu}, to try again enter anything");
            string userInput = printAndGet(strToExit);
            Console.Clear();

            return userInput;
        }
    }
}