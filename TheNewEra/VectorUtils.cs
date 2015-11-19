using System;
using System.Windows;

namespace TheNewEra
{
    public static class VectorUtils
    {
        public const double ScaleFactor = 10.0;

        public static Vector GetScaledVector(double length, double angle)
        {
            double cos = Math.Cos(AngleUtils.ConvertToRadians(angle));
            double sin = Math.Sin(AngleUtils.ConvertToRadians(angle));
            double x = (length / ScaleFactor) * cos;
            double y = (length / ScaleFactor) * sin;
            return new Vector(x, y);
        }

        public static Point GetCoordinates(double length, double angle)
        {
            double cos = Math.Cos(AngleUtils.ConvertToRadians(angle));
            double sin = Math.Sin(AngleUtils.ConvertToRadians(angle));
            double x = length * cos;
            double y = length * sin;
            return new Point(x, y);
        }
    }
}