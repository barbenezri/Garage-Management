namespace Ex03.GarageLogic
{
    public class PersonInformation
    {
        private readonly string r_PersonFullName;
        private readonly string r_PersonPhoneNumber;

        public PersonInformation(string i_PersonName, string i_PersonPhoneNumber)
        {
            r_PersonFullName = i_PersonName;
            r_PersonPhoneNumber = i_PersonPhoneNumber;
        }

        public string PersonFullName
        {
            get => r_PersonFullName;
        }

        public string PersonPhoneNumber
        {
            get => r_PersonPhoneNumber;
        }
    }
}
