using EMGUCVTest.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace EMGUCVTest
{
    public class EmguVM:ViewModelBase
    {
        private List<CamersImageSource> _camersImageSourceCollection;
        ///<summary>Камеры</summary>
        public List<CamersImageSource> CamersImageSourceCollection
        {
            get => _camersImageSourceCollection;
            set => Set(ref _camersImageSourceCollection, value);
        }

        
        private EmguModel camersModel;
        public EmguVM()
        {
            camersModel = new();
        }

        private async void RunCamers()
        {
            List<CamersDto> camers = new();
            //camers.Add(new CamersDto { Id = 6, PostId = 3, Url = @"rtsp://192.168.1.10/user=admin_password=tlJwpbo6_channel=1_stream=0.sdp?real_stream", FPS = 10 });
            //camers.Add(new CamersDto { Id = 7, PostId = 3, Url = @"rtsp://192.168.1.10/user=admin_password=tlJwpbo6_channel=1_stream=0.sdp?real_stream", FPS = 10 });
            camers.Add(new CamersDto { Id = 2, PostId = 2, Url = @"rtsp://wowzaec2demo.streamlock.net/vod/mp4:BigBuckBunny_115k.mov", FPS = 30 });
            camers.Add(new CamersDto { Id = 3, PostId = 2, Url = @"rtsp://wowzaec2demo.streamlock.net/vod/mp4:BigBuckBunny_115k.mov", FPS = 30 });
            camers.Add(new CamersDto { Id = 4, PostId = 2, Url = @"rtsp://wowzaec2demo.streamlock.net/vod/mp4:BigBuckBunny_115k.mov", FPS = 30 });
            camers.Add(new CamersDto { Id = 1, PostId = 2, Url = @"rtsp://wowzaec2demo.streamlock.net/vod/mp4:BigBuckBunny_115k.mov", FPS = 30 });
            CamersImageSourceCollection = await camersModel.StartCamersAsync(camers);
        }

        private ICommand _runCamersCommand;
        public ICommand RunCamersCommand => _runCamersCommand ??= new RelayCommand(RunCamers);
    }
}
