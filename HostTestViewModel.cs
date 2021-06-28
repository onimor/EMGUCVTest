using Emgu.CV;
using Emgu.CV.Structure;

namespace EMGUCVTest
{
    public class HostTestViewModel
    {
        public IInputArray ImageArray { get; set; }

        public HostTestViewModel()
        {
            ImageArray = new Image<Bgr, byte>("Images/virus.jpg");
        }

        public static HostTestViewModel Instance { get; } = new HostTestViewModel();
    }
}
