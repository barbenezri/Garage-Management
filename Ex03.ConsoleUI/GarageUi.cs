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
        private static readonly Garage sr_Garage = new Garage();

        private eGarageFunction m_GarageFunction;

        public enum eGarageFunction
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
            printGarageFunctions();
            int userinput = getUserPickFromMenu();
        }

        private int getUserPickFromMenu()
        {
            string userInput;
            do
            {
                Console.Write("Please enter valid input: ");
                userInput = Console.ReadLine();
            } while (!isPickValid(userInput));

            return int.Parse(userInput);
        }


        private bool isPickValid(string i_userInput)
        {

            /// Not ready yet
            int inputRequireRange = Enum.GetNames(typeof(eGarageFunction)).Length;
            bool isNumber = int.TryParse(i_userInput, out int inputAsNumber);
            bool isInRange = false;

            if (isNumber)
            {
                isInRange = inputAsNumber >= 1 && inputAsNumber <= inputRequireRange;
            }

            else if (inputAsNumber >= 1 && inputAsNumber <= inputRequireRange)
            {

            }



            printGarageFunctions();
            printRedWarning();
            Console.WriteLine("This is not a number, ");
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