namespace Educatio
{
    public class GameArea
    {
        public GameArea()
        {
            Rocket = new Rocket(200, 200);
            Rocket.FuelTankSize = 500;
            Rocket.RemainingFuel = 500;
        }

        public Rocket Rocket { get; set; }
    }
}