using System;
using System.Windows;

namespace TheNewEra
{
    public static class VectorUtils
    {

        public static Vector GetVector(double length, double angle)
        {
            double cos = Math.Cos(angle * (Math.PI / 180));
            double sin = Math.Sin(angle * (Math.PI / 180));
            double x = (length / 10.0) * cos;
            double y = (length / 10.0) * sin;
            return new Vector(x, y);
        }

        public static Point GetCoordinates(double length, double angle)
        {
            double cos = Math.Cos(angle * (Math.PI / 180));
            double sin = Math.Sin(angle * (Math.PI / 180));
            double x = length * cos;
            double y = length * sin;
            return new Point(x, y);
        }
    }
}