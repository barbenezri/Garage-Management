﻿using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Car : Vehicle
    {
        private eNumberOfDoor m_NumberOfDoors;
        private eCarColor m_CarColor;
        private const float k_maxWheelAirPressure = 29;

        public Car()
        {
            m_NumberOfWheels = 4;
            InitialWheelsForFirstTime(k_maxWheelAirPressure);
        }

        public enum eCarColor
        {
            Red = 1,
            White,
            Black,
            Blue,
        }

        public enum eNumberOfDoor
        {
            Two = 2,
            Three,
            Four,
            Five,
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
Amount of doors : {3}  
-----Engine details-----
{4}
-----Wheels details-----
{5}",
m_licensePlate,
m_VehicleModel,
Enum.GetName(typeof(eCarColor), m_CarColor),
Enum.GetName(typeof(eNumberOfDoor), m_NumberOfDoors),
m_Engine.ToString(),
GetWheelInformationOfVehicle());
        }

        public override string GetSpecialInfoMessage(out int o_AmountOfUniqueInformation)
        {
            o_AmountOfUniqueInformation = 2;

            return string.Format(
@"Please enter amount of doors that the car have <2,3,4,5>.
Then please enter car color <Red,Silver,White,Black>.
Notice: the system is case sensetive.");
        }

        public override void InsertEngineInformation(float i_CurrentEngineCapcityLeft)
        {
            float engineMaxCapacity = MaxEngineCapacity();

            SetEnergyEngineCapacityLeft(i_CurrentEngineCapcityLeft, engineMaxCapacity);
        }

        protected override float MaxEngineCapacity()
        {
            float engineMaxCapacity;

            if (m_Engine is CombustionEngine)
            {
                engineMaxCapacity = 48f;
            }
            else
            {
                engineMaxCapacity = 2.6f;
            }

            return engineMaxCapacity;
        }

        private void setUniqueFirstInformation(string i_FirstUniqueInformation)
        {
            if (Enum.IsDefined(typeof(eNumberOfDoor), i_FirstUniqueInformation) == true)
            {
                m_NumberOfDoors = (eNumberOfDoor)Enum.Parse(typeof(eNumberOfDoor), i_FirstUniqueInformation);
            }
            else
            {
                throw new ArgumentException("You try to set a number of doors that doesnt exsist");
            }
        }

        private void setUniqueSecondInformation(string i_SecondUniqueInformation)
        {
            if (Enum.IsDefined(typeof(eCarColor), i_SecondUniqueInformation) == true)
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
