using System.Windows;

namespace Educatio
{
    public interface IMoveableObject
    {
        string Sprite { get; set; }
        int FlightDirectionAngle { get; set; }
        Vector AccelerationMovement { get; set; }
        double Acceleration { get; set; }
        int ViewDirectionAngle { get; set; }
        Vector SpaceMovement { get; set; }
        double X { get; set; }
        double Y { get; set; }
        int RotateAcceleration { get; set; }
    }
}