using System.Windows;

namespace TheNewEra.Objects
{
    public interface IMoveableObject
    {
        Vector TranslatedPosition { get; }
        int Height { get; set; }
        int Width { get; set; }
        Point RelativeCenter { get; }
        Vector Position { get; set; }
        string Sprite { get; set; }
        double Thrust { get; set; }
        double RotationSpeed { get; set; }
        Vector ThrustMovement { get; set; }
        Vector Velocity { get; set; }
        double FlightDirectionAngle { get; set; }
        double ViewDirectionAngle { get; set; }
        double CollisionRadius { get; set; }
        double Mass { get; set; }

        void Update();
    }
}