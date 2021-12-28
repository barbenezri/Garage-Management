namespace Ex03.ConsoleUI
{
    public class Program
    {
        public static void Main()
        {
            GarageUi myGarage = GarageUi.Singelton();
            myGarage.InitiateGarageMenu();
        }
    }
}
