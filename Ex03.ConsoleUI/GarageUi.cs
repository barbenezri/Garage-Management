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
        private readonly Garage r_Garage = new Garage();

        public enum eMenuInput
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
            eMenuInput? userInput = null;

            while (userInput != eMenuInput.Exit)
            {
                printGarageFunctions();
                userInput = (eMenuInput)getUserPickFromMenu();
                switch (userInput)
                {
                    case eMenuInput.InsertNewVehicle:
                        //AddVehicle();
                        break;
                    case eMenuInput.DisplayLicensePlates:
                        //AddVehicle();
                        break;
                    case eMenuInput.ChangeVehicleStaus:
                        //AddVehicle();
                        break;
                    case eMenuInput.InflateWheelsToMaximum:
                        //AddVehicle();
                        break;
                    case eMenuInput.RefulVehicle:
                        //AddVehicle();
                        break;
                    case eMenuInput.ChargeVehicle:
                        //AddVehicle();
                        break;
                    case eMenuInput.DisplayVehicleDetails:
                        //AddVehicle();
                        break;
                    case eMenuInput.Exit:
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
            int inputRequireRange = Enum.GetNames(typeof(eMenuInput)).Length;
            bool isNumber = int.TryParse(i_userInput, out int inputAsNumber);
            bool isInRange = isNumber ? (inputAsNumber >= 0 && inputAsNumber <= inputRequireRange) : false;

            if (!isNumber || !isInRange)
            {
                printGarageFunctions();
                printRedWarning();
                if (!isNumber)
                {
                    Console.WriteLine("This is not a number!");
                }
                else
                {
                    Console.WriteLine("This is not an option!");
                }
            }

            return isNumber && isInRange;
        }

        private void printGarageFunctions()
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
    }
}