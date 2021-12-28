using System;

namespace Ex03.GarageLogic
{
    public class GarageAccount
    {
        private readonly VehicleOwner r_VehicleOwner;
        private readonly Vehicle r_VehicleInfo;
        private eStatusInGarge m_StatusInGarge;
        public enum eStatusInGarge
        {
            InRepair = 1,
            Fixed,
            Paid,
        }

        public GarageAccount(string i_OwnerName, string i_OwnerPhoneNumber, Vehicle i_VehicleInfo)
        {
            r_VehicleOwner = new VehicleOwner(i_OwnerName, i_OwnerPhoneNumber);
            r_VehicleInfo = i_VehicleInfo;
            StatusInGarge = eStatusInGarge.InRepair;
        }

        public string OwnerName
        {
            get => r_VehicleOwner.OwnerFullName;
        }

        public Vehicle Vehicle
        {
            get => r_VehicleInfo;
        }

        public string OwnerPhoneNumber
        {
            get => r_VehicleOwner.OwnerPhoneNumber;
        }

        public eStatusInGarge StatusInGarge
        {
            get => m_StatusInGarge;
            set => m_StatusInGarge = value;
        }

        public override string ToString()
        {
            return string.Format(
@"-----Vehicle informations-----
Status in garage is : {0}
{1}.
-----Owner informations-----
Owner name is : {2}
Owner Phone is : {3}",
Enum.GetName(typeof(eStatusInGarge), m_StatusInGarge),
r_VehicleInfo.ToString(),
r_VehicleOwner.OwnerFullName,
r_VehicleOwner.OwnerPhoneNumber);
        }
    }
}
