using System.Windows;

namespace Educatio
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
        double Acceleration { get; set; }
        double RotateAcceleration { get; set; }
        Vector AccelerationMovement { get; set; }
        Vector SpaceMovement { get; set; }
        double FlightDirectionAngle { get; set; }
        double ViewDirectionAngle { get; set; }
    }
}