using System;
using System.Windows;

namespace TheNewEra
{
    public static class VectorUtils
    {
        public static Vector GetVector(double length, double angle)
        {
            double cos = Math.Cos(angle);
            double sin = Math.Sin(angle);
            double x = length * cos;
            double y = length * sin;
            return new Vector(x, y);
        }

        public static double GetDistance(IMoveableObject objectA, IMoveableObject objectB)
        {
            double distanceX = objectA.Position.X - objectB.Position.X;
            double distanceY = objectA.Position.Y - objectB.Position.Y;
            double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            return distance;
        }
    }
}