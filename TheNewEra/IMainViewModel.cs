using System.ComponentModel;

namespace TheNewEra
{
    public interface IMainViewModel
    {
        Universe Universe { get; set; }

        void Close(object sender, CancelEventArgs e);
    }
}