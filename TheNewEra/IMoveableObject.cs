using System.Windows;

namespace TheNewEra
{
    public interface IMoveableObject
    {
        Point Position { get; set; }
        int Height { get; set; }
        int Width { get; set; }
        Point RelativeCenter { get; }
        Vector Center { get; }
        string Sprite { get; set; }
        double Thrust { get; set; }
        double RotationSpeed { get; set; }
        Vector ThrustMovement { get; set; }
        Vector Velocity { get; set; }
        double FlightDirectionAngle { get; set; }
        double ViewDirectionAngle { get; set; }
        double CollisionRadius { get; set; }

        void Update();
    }
}