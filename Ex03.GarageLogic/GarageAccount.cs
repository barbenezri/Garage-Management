using System;

namespace Ex03.GarageLogic
{
    public class GarageAccount
    {
        private readonly VehicleOwner r_VehicleOwner;
        private readonly Vehicle r_VehicleInfo;
        private eStatusInGarage m_StatusInGarage;

        public enum eStatusInGarage
        {
            InRepair = 1,
            Fixed,
            Paid,
        }

        public GarageAccount(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleInfo)
        {
            r_VehicleOwner = new VehicleOwner(i_OwnerName, i_OwnerPhoneNumber);
            r_VehicleInfo = i_VehicleInfo;
            StatusInGarage = eStatusInGarage.InRepair;
        }

        public string OwnerFullName
        {
            get => r_VehicleOwner.OwnerFullName;
        }

        public Vehicle VehicleInfo
        {
            get => r_VehicleInfo;
        }

        public string OwnerPhoneNumber
        {
            get => r_VehicleOwner.OwnerPhoneNumber;
        }

        public eStatusInGarage StatusInGarage
        {
            get => m_StatusInGarage;
            set => m_StatusInGarage = value;
        }

        public override string ToString()
        {
            return string.Format(
@"-----Vehicle information-----
Status in garage is : {0}
{1}
-----Owner information-----
Owner name is : {2}
Owner Phone is : {3}",
Enum.GetName(typeof(eStatusInGarage), m_StatusInGarage),
r_VehicleInfo,
r_VehicleOwner.OwnerFullName,
r_VehicleOwner.OwnerPhoneNumber);
        }
    }
}
