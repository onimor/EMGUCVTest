using Emgu.CV;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Media;

namespace EMGUCVTest
{
    public class CamersImageSource : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));



        private ImageSource _source;
        public ImageSource Source
        {
            get => _source;
            private set
            {
                _source = value;
                OnPropertyChanged();
            }
        }

        /// <summary>Камера</summary>
        public VideoCapture VideoCapture { get; }

        /// <summary>Количество кадров в секунду</summary>
        public int FPS { get; }

        private readonly Timer timer;

        public CamersImageSource(VideoCapture videoCapture, int fps)
        {
            VideoCapture = videoCapture;
            FPS = fps;
            int tik = 1000 / FPS;
            timer = new Timer(Update, null, 0, tik);
        }

        private void Update(object state)
        {
            try
            {
                using var mat = new Mat();
                if (VideoCapture.Retrieve(mat))
                    Source = mat.ToBitmapSource();
            }
            catch
            {

            }

        }
    }
}
