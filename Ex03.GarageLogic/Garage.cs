﻿using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<int, GarageAccount> r_DictionaryOfVehicles;

        public Garage()
        {
            r_DictionaryOfVehicles = new Dictionary<int, GarageAccount>();
        }

        public void InsertVehicle(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleToInsert)
        {
            string licensePlate = i_VehicleToInsert.LicensePlate;

            if (IsVehicleExist(licensePlate) == false)
            {
                GarageAccount informationOfVehicle = new GarageAccount(i_OwnerName, i_OwnerPhoneNumber, i_VehicleToInsert);
                r_DictionaryOfVehicles.Add(licensePlate.GetHashCode(), informationOfVehicle);
            }
            else
            { 
                GarageAccount vehicleForChangeStatus = getVehicleInfoFromGarage(licensePlate);
                vehicleForChangeStatus.StatusInGarge = GarageAccount.eStatusInGarge.InRepair;
            }
        }

        public List<string> ListOfVehiclelicensePlatesByFiltering(GarageAccount.eStatusInGarge i_StatusInGarage)
        {
            List<string> licensePlates = new List<string>();

            foreach (GarageAccount vehicleInGarage in r_DictionaryOfVehicles.Values)
            {
                if (vehicleInGarage.StatusInGarge == i_StatusInGarage)
                {
                    licensePlates.Add(vehicleInGarage.VehicleInfo.LicensePlate);
                }
            }

            return licensePlates;
        }

        public List<string> ListOfVehiclelicensePlates()
        {
            List<string> licensePlates = new List<string>();

            foreach (GarageAccount vehicleInGarage in r_DictionaryOfVehicles.Values)
            {
                licensePlates.Add(vehicleInGarage.VehicleInfo.LicensePlate);
            }

            return licensePlates;
        }

        public void ChangeStatusOfVehicle(GarageAccount.eStatusInGarge i_ChangeStatus, string i_LicensePlate)
        {
            if (IsVehicleExist(i_LicensePlate) == true)
            {
                if (Enum.IsDefined(typeof(GarageAccount.eStatusInGarge), i_ChangeStatus) == true)
                {
                    r_DictionaryOfVehicles[i_LicensePlate.GetHashCode()].StatusInGarge = i_ChangeStatus;
                }
                else
                {
                    throw new ArgumentException("The desired status doesn't exist.");
                }
            }
            else
            {
                throwExceptionOfVehicleDoesntexist();
            }
        }

        public void InflatingWheelsToMax(string i_LicensePlate)
        {
            if (IsVehicleExist(i_LicensePlate) == true)
            {
                foreach (Wheel wheelOfVehicle in r_DictionaryOfVehicles[i_LicensePlate.GetHashCode()].VehicleInfo.Wheels)
                {
                    wheelOfVehicle.InflatingWheelToMax();
                }
            }
            else
            {
                throwExceptionOfVehicleDoesntexist();
            }
        }

        public bool IsVehicleExist(string i_LicensePlate)
        {
            return r_DictionaryOfVehicles.ContainsKey(i_LicensePlate.GetHashCode());
        }

        public void RefuelVehicle(string i_LicensePlate, CombustionEngine.eFuelKind i_KindOfFuels, float i_AmountOfRefuel)
        {
            if (IsVehicleExist(i_LicensePlate) == true)
            {
                GarageAccount vehicleInfo = getVehicleInfoFromGarage(i_LicensePlate);

                if (vehicleInfo.VehicleInfo.VehicleEngine is CombustionEngine currentFuelEngine)
                {
                    currentFuelEngine.Refueling(i_AmountOfRefuel, i_KindOfFuels);
                }
                else
                {
                    throw new ArgumentException("Elon Mask doesn't allowed to fill electric car with fuel!!!");
                }
            }
            else
            {
                throwExceptionOfVehicleDoesntexist();
            }
        }

        public void ChargingVehicle(string i_LicensePlate, float i_AmountOfMinutesToCharge)
        {
            if (IsVehicleExist(i_LicensePlate) == true)
            {
                GarageAccount currentInformationOfVehicle = getVehicleInfoFromGarage(i_LicensePlate);

                if (currentInformationOfVehicle.VehicleInfo.VehicleEngine is ElectricEngine currentElectricEngine)
                {
                    i_AmountOfMinutesToCharge /= 60;
                    currentElectricEngine.ChargeEngineBattery(i_AmountOfMinutesToCharge);
                }
                else
                {
                    throw new ArgumentException("This car isn't a Tesla, its just a regular car.");
                }
            }
            else
            {
                throwExceptionOfVehicleDoesntexist();
            }
        }

        public string GetVehicleReport(string i_LicensePlate)
        {
            GarageAccount currentVehicleInformation = null;

            if (IsVehicleExist(i_LicensePlate) == true)
            {
                currentVehicleInformation = getVehicleInfoFromGarage(i_LicensePlate);
            }
            else
            {
                throwExceptionOfVehicleDoesntexist();
            }

            return currentVehicleInformation.ToString();
        }

        private void throwExceptionOfVehicleDoesntexist()
        {
            throw new ArgumentException("This vehice doesn't exist in our garage, Maybe it is in another garage");
        }

        private GarageAccount getVehicleInfoFromGarage(string i_LicensePlate)
        {
            return r_DictionaryOfVehicles[i_LicensePlate.GetHashCode()];
        }
    }
}
