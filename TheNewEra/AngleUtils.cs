using System;

namespace TheNewEra
{
    public class AngleUtils
    {
        public static double LimitAngle(double angle)
        {
            if (angle < 0)
                return 2 * Math.PI + angle;
            if (angle > 2 * Math.PI)
                return angle - 2 * Math.PI;
            return angle;
        }

        public static double ConvertToRadians(double angle)
        {
            return angle * (Math.PI / 180);
        }

        public static double ConvertToDegrees(double angle)
        {
            return angle * (180 / Math.PI);
        }

        public static double GetAngle(IMoveableObject objectA, IMoveableObject objectB)
        {
            double distance = VectorUtils.GetDistance(objectA, objectB);
            double distanceY = objectA.Position.Y - objectB.Position.Y;
            return Math.Asin(distanceY / distance);
        }
    }
}