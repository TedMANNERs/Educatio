using System.Windows;
using System.Windows.Controls;
using TheNewEra.Objects.Rocket;

namespace TheNewEra
{
    /// <summary>
    /// Interaction logic for FuelGauge.xaml
    /// </summary>
    public partial class FuelGauge : UserControl
    {
        public FuelGauge()
        {
            InitializeComponent();
            RootCanvas.DataContext = this;
        }

        public Rocket Rocket
        {
            get { return (Rocket)GetValue(RocketProperty); }
            set { SetValue(RocketProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Rocket.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RocketProperty =
            DependencyProperty.Register("Rocket", typeof(Rocket), typeof(FuelGauge), new PropertyMetadata(null));
    }
}