namespace Ex03.ConsoleUI
{
    using System;
    using Ex03.GarageLogic;

    public class UiFuncationHelper
    {
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
            UiFuncationHelper.PrintEnumOption<T>();
            userChoise = Console.ReadLine();
            isTypeValid = UiFuncationHelper.IsCheckIfTheValueEnumIsValid(userChoise, enumLength);
            while (isTypeValid == false)
            {
                Console.WriteLine("You entered not valid value, Please enter valid value from the list below");
                UiFuncationHelper.PrintEnumOption<T>();
                userChoise = Console.ReadLine();
                isTypeValid = UiFuncationHelper.IsCheckIfTheValueEnumIsValid(userChoise, enumLength);
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
