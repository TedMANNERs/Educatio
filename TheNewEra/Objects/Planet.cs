using System.Windows;

namespace TheNewEra.Objects
{
    public class Planet : MoveableObjectBase
    {
        public Planet(Vector position, int height, int width)
        {
            Position = position;
            Height = height;
            Width = width;
            Sprite = "Resources/Images/Planet1.png";
            Init();
            CollisionRadius = Height > Width ? Height / 2 : Width / 2;
            RotationSpeed = -0.005;
        }
    }
}