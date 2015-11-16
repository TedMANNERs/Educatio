using System.Windows;

namespace TheNewEra
{
    public interface IMoveableObject
    {
        double X { get; set; }
        double Y { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        double CenterX { get; }
        double CenterY { get; }
        string Sprite { get; set; }
        double Thrust { get; set; }
        double RotationThrust { get; set; }
        Vector ThrustMovement { get; set; }
        Vector SpaceMovement { get; set; }
        double FlightDirectionAngle { get; set; }
        double ViewDirectionAngle { get; set; }

        void Update();
    }
}