namespace Ex03.GarageLogic
{
    using System;
    using System.Collections.Generic;

    internal class Car : Vehicle
    {
        private eNumberOfDoor m_NumberOfDoors;
        private eCarColor m_CarColor;

        public Car()
        {
            float maxWheelAirPressure = 32;

            this.m_NumberOfWheels = 4;
            InitialWheelsForFirstTime(maxWheelAirPressure);
        }

        public enum eNumberOfDoor
        {
            Two = 2,
            Three,
            Four,
            Five,
        }

        public enum eCarColor
        {
            Red = 1,
            Silver,
            White,
            Black,
        }

        public override void SetVehicleUniqueInformation(List<string> i_ListOfUniqueInformation)
        {
            setUniqueFirstInformation(i_ListOfUniqueInformation[0]);
            setUniqueSecondInformation(i_ListOfUniqueInformation[1]);
        }

        public override string ToString()
        {
            return string.Format(
@"Vehicle type : Car
License plate : {0}
Model name : {1}
Color : {2}
Number of doors : {3}  
Engine details : 
{4}
Wheels details : 
{5}",
m_LicenseNumber, m_VehicleModel, Enum.GetName(typeof(eCarColor), m_CarColor),
Enum.GetName(typeof(eNumberOfDoor), m_NumberOfDoors), m_Engine.ToString(), GetWheelInformationOfVehicle());
        }

        public override string GettingWithSpecialInformationOfVehicleUiNeedToEnter(out int o_AmountOfUniqueInformation)
        {
            o_AmountOfUniqueInformation = 2;

            return string.Format(
@"Please enter amount of doors that the car have, the options is: 2, 3, 4, 5,AFTER THAT PUSH ENTER
Please enter the color of the car the option is : Red,Silver,White,Black");
        }

        public override void InsertEngineInformation(float i_CurrentEngineCapcityLeft)
        {
            float engineMaxCapacity = MaxEngineCapacity();

            SetEnergyEngineCapacityLeft(i_CurrentEngineCapcityLeft, engineMaxCapacity);
        }

        protected override float MaxEngineCapacity()
        {
            float engineMaxCapacity;

            FuelEngine fuelEngineCar = this.m_Engine as FuelEngine;

            if (fuelEngineCar != null)
            {
                engineMaxCapacity = 45f;
            }
            else
            { /*if is not fuel its electric engine*/
                engineMaxCapacity = 3.2f;
            }

            return engineMaxCapacity;
        }

        private void setUniqueFirstInformation(string i_FirstUniqueInformation)
        {
            eNumberOfDoor numberOfDoors;

            numberOfDoors = (eNumberOfDoor)Enum.Parse(typeof(eNumberOfDoor), i_FirstUniqueInformation);
            if (Enum.IsDefined(typeof(eNumberOfDoor), numberOfDoors) == false)
            {
                throw new ArgumentException("You try to set a number of doors that doesnt exsist");
            }
            else
            {
                m_NumberOfDoors = numberOfDoors;
            }
        }

        private void setUniqueSecondInformation(string i_SecondUniqueInformation)
        {
            bool isInsideEnum = Enum.IsDefined(typeof(eCarColor), i_SecondUniqueInformation);

            if (isInsideEnum == true)
            {
                m_CarColor = (eCarColor)Enum.Parse(typeof(eCarColor), i_SecondUniqueInformation);
            }
            else
            {
                throw new ArgumentException("You try to set a color of car that doesnt exsist");
            }
        }
    }
}
