namespace Educatio
{
    public class Spielfeld
    {
        public Spielfeld()
        {
            Rakete = new Rakete(200, 200);
            Rakete.TankGr�sse = 500;
            Rakete.TreibstoffMenge = 500;
        }

        public Rakete Rakete { get; set; }
    }
}