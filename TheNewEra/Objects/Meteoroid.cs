using System.Windows;
using TheNewEra.Util;

namespace TheNewEra.Objects
{
    public class Meteoroid : MoveableObjectBase
    {
        public Meteoroid(Vector position, int height, int width, Vector movement, int rotationSpeed)
        {
            Position = position;
            RotationSpeed = AngleUtils.ConvertToRadians(rotationSpeed);
            Velocity = movement;
            Height = height;
            Width = width;
            Sprite = "Resources/Images/asteroid.png";
            Init();
        }

        public Meteoroid(Vector position, int height, int width)
        {
            Position = position;
            Height = height;
            Width = width;
        }
    }
}