namespace Educatio
{
    public class Spielfeld
    {
        public Spielfeld()
        {
            Rakete = new Rakete(200, 150);
            Rakete.TankGr�sse = 100;
            Rakete.TreibstoffMenge = 100;
        }

        public Rakete Rakete { get; set; }

        public void Dgedr�ckt()
        {
            Rakete.BlickRichtungsWinkel += 10;
            Rakete.TreibstoffMenge--;
        }

        public void Agedr�ckt()
        {
            Rakete.BlickRichtungsWinkel -= 10;
            Rakete.TreibstoffMenge--;
        }

        public void Sgedr�ckt()
        {
            if (Rakete.TreibstoffMenge > 0)
            {
                Rakete.GeheVorw�rts(-10);
                Rakete.TreibstoffMenge--;
            }
        }

        public void Wgedr�ckt()
        {
            if (Rakete.TreibstoffMenge > 0)
            {
                Rakete.GeheVorw�rts(10);
                Rakete.TreibstoffMenge--;
            }
        }

        public void Leertastegedr�ckt()
        {
            Rakete.Bild = "/Resources/Images/rakete4.png";
        }
    }
}