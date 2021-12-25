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
            } while (!checkIfPickIsValid(userInput));
            Console.Clear();

            return int.Parse(userInput);
        }

        private bool checkIfPickIsValid(string i_userInput)
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

        private void printGarageMenu()//fixed
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

        private static void printRedWarning() //add
        {
            Console.Write("Warning: ", Console.ForegroundColor = ConsoleColor.Red);
            Console.ResetColor();
        }

        private static float GetFloatNumberFromUser() //fixed
        {
            string userInput;

            do
            {
                Console.WriteLine("Please enter a valid number");
                userInput = Console.ReadLine();
            } while (!CheckIfFloat(userInput));

            return float.Parse(userInput);
        }

        private static int GetIntNumberFromUser() //fixed
        {
            string userInput;

            do
            {
                Console.WriteLine("Please enter a valid number");
                userInput = Console.ReadLine();
            } while (!CheckIfInt(userInput));

            return int.Parse(userInput);
        }

        private static string GetIntStringFromUser() //fixed
        {
            string userInput;

            do
            {
                Console.WriteLine("Please enter a valid number");
                userInput = Console.ReadLine();
            } while (!CheckIfStringValid(userInput));

            return userInput;
        }

        private static bool CheckIfFloat(string i_UserChoise) //fixed
        {
            bool isFloat = float.TryParse(i_UserChoise, out _);

            if(!isFloat)
            {
                printRedWarning();
                Console.Write("This Number is not valid, ");
            }

            return isFloat;
        }

        private static bool CheckIfInt(string i_UserChoise) //fixed
        {
            bool isInt = int.TryParse(i_UserChoise, out _);

            if (!isInt)
            {
                printRedWarning();
                Console.Write("This number is not valid, ");
            }

            return isInt;
        }

        private static bool CheckIfStringValid(string i_UserChoise) //fixed
        {
            bool isStringValid = i_UserChoise != string.Empty;

            if (!isStringValid)
            {
                printRedWarning();
                Console.Write("This String is not valid, ");
            }

            return isStringValid;
        }

        private string gettingPhoneNumberFromUser() //fixed
        {
            string phoneNumber;

            do
            {
                Console.WriteLine("Please Enter Your Phone Number :");
                phoneNumber = Console.ReadLine();

            } while (!CheckIfInt(phoneNumber));

            return phoneNumber;
        }

        private float gettingAirPressureFromUser() //fixed
        {
            string AirPressure;

            do
            {
                Console.WriteLine("Please Enter Your current wheel air pressure :");
                AirPressure = Console.ReadLine();
            } while (!CheckIfFloat(AirPressure));

            return float.Parse(AirPressure);

        }

        private string gettingNameOfVehicleOwner() //fixed
        {
            string fullName;

            do
            {
                Console.WriteLine("Please Enter Your full name :");
                fullName = Console.ReadLine();

            } while (!CheckIfStringValid(fullName));

            return fullName;
        }

        private string getLicenseNumber() //fixed
        {
            string licenseNumber;

            do
            {
                Console.WriteLine("Please enter your license number");
                licenseNumber = Console.ReadLine();
            } while (!CheckIfInt(licenseNumber));

            return licenseNumber;
        }

        private string getVehicleModel() //fixed
        {
            string VehicleModel;

            do
            {
                Console.WriteLine("Please enter your Vehicle Model");
                VehicleModel = Console.ReadLine();
            } while (!CheckIfStringValid(VehicleModel));

            return VehicleModel;
        }

        private string GetWheelManufacturerName() //fixed
        {
            string wheelManufacturerName;

            do
            {
                Console.WriteLine("Please Enter Wheel Manufacturer Name :");
                wheelManufacturerName = Console.ReadLine();

            } while (!CheckIfStringValid(wheelManufacturerName));

            return wheelManufacturerName;
        }

        private static bool CheckIfCheckIfTheValueEnumIsValid(string i_UserInput, int i_EnumLength) //fixed
        {
            bool isUserInputValid = int.TryParse(i_UserInput, out int userChoose);

            isUserInputValid &= (userChoose >= 1 && userChoose <= i_EnumLength);
            if (!isUserInputValid)
            {
                printRedWarning();
                Console.WriteLine("You entered unvalid value, Please enter valid value from the list below");
            }

            return isUserInputValid;
        }

        private static void PrintEnumOption<T>() //fixed
        {
            foreach (string genral in Enum.GetNames(typeof(T)))
            {
                Console.WriteLine("Enter {1:D} for {0}", genral, Enum.Parse(typeof(T), genral));
            }
        }

        private static T GettingUserInputForGeneralEnum<T>(string i_MessageToPrint) //fixed
        {
            string userChoise;
            int enumLength = Enum.GetNames(typeof(T)).Length;

            do
            {
                Console.WriteLine(i_MessageToPrint);
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

            licenceNumber = getLicenseNumber();
            if (m_Garage.IsVehicleExsistInDataStruct(licenceNumber) == false)
            {
                fullName = gettingNameOfVehicleOwner();
                ownerPhoneNumber = gettingPhoneNumberFromUser();
                vehicle = addInformationVehicle(licenceNumber);
                m_Garage.InsertVehicleToGarge(fullName, ownerPhoneNumber, vehicle);
                Console.WriteLine("Your vehicle successfully added to garage");
            }
            else
            {
                m_Garage.ChangeStatusOfVehicleInGarage(InformationOfVehicleInGarage.eStatusInGarge.InRepair, licenceNumber);
                Console.WriteLine("Vehicle already exsist, Changing status to in reapir");
            }
        }
        //2
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
        //3
        private void changeVehicleStatusInGarge()
        {
            bool isUpdateStatus = false;
            string keyToReturnToMenu = null;
            string licenceNumber;
            InformationOfVehicleInGarage.eStatusInGarge statusOfVehicleToChangeTo;
            string messageToSendToFunaction = "Please enter status to change the car from the list below : ";

            while ((isUpdateStatus == false) && (keyToReturnToMenu != r_KeyToExitToMenu))
            {
                try
                {
                    licenceNumber = getLicenseNumber();
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
                    licenseNumber = getLicenseNumber();
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
        //5
        private void refulVehicle()
        {
            string licenseNumber, userInput = null;
            float amountToFill;
            bool isRefulVehicleWork = false;
            FuelEngine.eKindOfFuel kindOfFuelToFill;
            string messageToSendToFuncation = "Please enter type of fuel from the list below";

            while ((isRefulVehicleWork == false) && (userInput != r_KeyToExitToMenu))
            {
                try
                {
                    licenseNumber = getLicenseNumber();
                    Console.WriteLine("Please enter the amount of fuel you want to fill");
                    amountToFill = GetFloatNumberFromUser();
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
                    licenseNumber = getLicenseNumber();
                    Console.WriteLine("Please enter the amount of minutes that you want to charge in vehicle");
                    amountToCharge = GetFloatNumberFromUser();
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
                    licenseNumber = getLicenseNumber();
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






        private Vehicle addInformationVehicle(string i_LicenceNumber)
        {
            string vehicleModel;
            VehicleFactory.eVehicleType vehicleType;
            Vehicle vehicleToReturn;
            string messageToSendToFuncationVehicleType = "Please enter type of Vehicle from the list below";

            vehicleType = GettingUserInputForGeneralEnum<VehicleFactory.eVehicleType>(messageToSendToFuncationVehicleType);
            vehicleToReturn = VehicleFactory.MakeVehicle(vehicleType);
            vehicleModel = getVehicleModel();
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

            wheelManufacturerName = GetWheelManufacturerName();
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
            while (isEnergyIsInRange == false)
            {
                try
                {
                    capacityOfEnergyLeftInEngine = GetFloatNumberFromUser();
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

        // UIFunctionhellper

        public static void CatchRangeExPrintToConsole(ValueOutOfRangeException i_Voore)
        {
            string messageToPrint = string.Format("{0}. range should be between {1} to {2}", i_Voore.Message, i_Voore.MinValue, i_Voore.MaxValue);

            Console.WriteLine(messageToPrint);
            Console.WriteLine("Please try again");
        }

        public static string AskingIfYouWantToExit(string i_KeyToExitToMenu)
        {
            string strToExit = string.Format($"for exit to menu enter {i_KeyToExitToMenu}, to try again enter anything");
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