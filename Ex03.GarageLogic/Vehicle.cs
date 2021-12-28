using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected int m_NumberOfWheels;
        protected string m_VehicleModel;
        protected string m_licensePlate;
        protected Wheel[] m_Wheels;
        protected Engine m_Engine;

        public string LicensePlate
        {
            get => m_licensePlate;
        }

        public Wheel[] Wheels
        {
            get => m_Wheels;
        }

        public Engine VehicleEngine
        {
            get => m_Engine;
            set => m_Engine = value;
        }

        public abstract void SetVehicleUniqueInformation(List<string> i_ListOfUniqueInformation);

        public void SettingVehicleInformation(string i_LicensePlate, string i_VehicleModel)
        {
            m_licensePlate = i_LicensePlate;
            m_VehicleModel = i_VehicleModel;
        }

        public void SetWheelInformation(float i_CurrentWheelAirPressure, string i_ManufacturerName)
        {
            for (int i = 0; i < m_Wheels.Length; i++)
            {
                m_Wheels[i].SetInformationOfWheel(i_ManufacturerName, i_CurrentWheelAirPressure);
            }
        }

        public string GetWheelInformationOfVehicle()
        {
            return string.Format(
@"Number of Wheels : {0}
Wheels manufacturer Name : {1}
Current air pressure : {2}
Max air pressure : {3}",
m_Wheels.Length,
m_Wheels[0].ManufacturerName,
m_Wheels[0].CurrentAirPressure,
m_Wheels[0].MaxAirPressure);
        }

        public abstract override string ToString();

        public abstract string GetSpecialInfoMessage(out int o_AmountOfUniqueInformation);

        public abstract void InsertEngineInformation(float i_CurrentEngineCapcityLeft);

        protected abstract float MaxEngineCapacity();

        protected void SetEnergyEngineCapacityLeft(float i_EnergyEngineCapacityLeft, float i_EngineMaxCapacity)
        {
            m_Engine.SetEnergyLeftAndMaxInEngineCapacity(i_EngineMaxCapacity, i_EnergyEngineCapacityLeft);
        }

        protected void InitialWheelsForFirstTime(float i_MaxAirPressure)
        {
            if (m_Wheels == null)
            {
                m_Wheels = new Wheel[m_NumberOfWheels];
                for (int i = 0; i < m_NumberOfWheels; i++)
                {
                    m_Wheels[i] = new Wheel(i_MaxAirPressure);
                }
            }
            else
            {
                throw new ArgumentException("Wheels is already initialized");
            }
        }
    }
}
