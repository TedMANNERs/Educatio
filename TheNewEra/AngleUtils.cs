using System;

namespace TheNewEra
{
    public class AngleUtils
    {
        public static double LimitAngle(double angle)
        {
            if (angle < 0)
                return 360 + angle;
            if (angle > 360)
                return angle - 360;
            return angle;
        }

        public static double ConvertToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }
    }
}