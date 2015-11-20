using System;
using System.Windows;

namespace TheNewEra
{
    public static class VectorUtils
    {
        public static Vector GetVector(double length, double angle)
        {
            double cos = Math.Cos(AngleUtils.ConvertToRadians(angle));
            double sin = Math.Sin(AngleUtils.ConvertToRadians(angle));
            double x = length * cos;
            double y = length * sin;
            return new Vector(x, y);
        }
        public static double GetDistance(IMoveableObject objectA, IMoveableObject objectB)
        {
            double distanceX = objectA.Center.X - objectB.Center.X;
            double distanceY = objectA.Center.Y - objectB.Center.Y;
            double distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
            return distance;
        }
    }
}