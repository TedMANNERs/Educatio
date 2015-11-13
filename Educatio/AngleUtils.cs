namespace Educatio
{
    public class AngleUtils
    {
        public static int LimitAngle(int angle)
        {
            if (angle < 0)
                return 360 + angle;
            if (angle > 360)
                return angle - 360;
            return angle;
        }
    }
}