namespace TheNewEra
{
    public interface IFuelTank
    {
        int Size { get; }
        int ScaleFactor { get; }
        double RemainingFuel { get; set; }
        double RemainingFuelScaled { get; }
    }
}