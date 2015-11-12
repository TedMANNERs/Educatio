namespace Educatio
{
    public class Spielfeld
    {
        public Spielfeld()
        {
            Rakete = new Rakete(200, 150);
            Rakete.TankGrösse = 100;
            Rakete.TreibstoffMenge = 100;
        }

        public Rakete Rakete { get; set; }

        public void Dgedrückt()
        {
            Rakete.BlickRichtungsWinkel += 10;
            Rakete.TreibstoffMenge--;
        }

        public void Agedrückt()
        {
            Rakete.BlickRichtungsWinkel -= 10;
            Rakete.TreibstoffMenge--;
        }

        public void Sgedrückt()
        {
            if (Rakete.TreibstoffMenge > 0)
            {
                Rakete.GeheVorwärts(-10);
                Rakete.TreibstoffMenge--;
            }
        }

        public void Wgedrückt()
        {
            if (Rakete.TreibstoffMenge > 0)
            {
                Rakete.GeheVorwärts(10);
                Rakete.TreibstoffMenge--;
            }
        }

        public void Leertastegedrückt()
        {
            Rakete.Bild = "/Resources/Images/rakete4.png";
        }
    }
}