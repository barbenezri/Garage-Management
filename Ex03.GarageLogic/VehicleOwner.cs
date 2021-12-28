namespace Ex03.GarageLogic
{
    public class VehicleOwner
    {
        private readonly string r_OwnerFullName;
        private readonly string r_OwnerPhoneNumber;

        public VehicleOwner(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            r_OwnerFullName = i_OwnerName;
            r_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        public string OwnerFullName
        {
            get => r_OwnerFullName;
        }

        public string OwnerPhoneNumber
        {
            get => r_OwnerPhoneNumber;
        }
    }
}
