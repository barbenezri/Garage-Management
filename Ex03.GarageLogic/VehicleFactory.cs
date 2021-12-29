namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public enum eVehicleType
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck,
        }

        public static Vehicle MakeVehicle(eVehicleType i_VehicleType)
        {
            Vehicle vehicleToReturn;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    vehicleToReturn = new Car();
                    vehicleToReturn.VehicleEngine = new CombustionEngine(CombustionEngine.eFuelKind.Octan95);
                    break;
                case eVehicleType.ElectricCar:
                    vehicleToReturn = new Car();
                    vehicleToReturn.VehicleEngine = new ElectricEngine();
                    break;
                case eVehicleType.FuelMotorcycle:
                    vehicleToReturn = new Motorcycle();
                    vehicleToReturn.VehicleEngine = new CombustionEngine(CombustionEngine.eFuelKind.Octan98);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    vehicleToReturn = new Motorcycle();
                    vehicleToReturn.VehicleEngine = new ElectricEngine();
                    break;
                case eVehicleType.Truck:
                    vehicleToReturn = new Truck();
                    vehicleToReturn.VehicleEngine = new CombustionEngine(CombustionEngine.eFuelKind.Soler);
                    break;
                default:
                    vehicleToReturn = null;
                    break;
            }

            return vehicleToReturn;
        }
    }
}
