using Emgu.CV;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EMGUCVTest
{
    public class EmguModel
    {
        public async Task<List<CamersImageSource>> StartCamersAsync(List<CamersDto> camersDto)
        {
            return await Task.Run(() => StartCamers(camersDto));
        }


        public List<CamersImageSource> StartCamers(List<CamersDto> camersDto)
        {
            List<CamersImageSource> videoCapturesCollection = new();

            foreach (var item in camersDto)
            {
                videoCapturesCollection.Add(new CamersImageSource(new VideoCapture(item.Url), item.FPS));
            }

            foreach (var item in videoCapturesCollection)
            {
                //item.VideoCapture.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.Fps, 15);
                item.VideoCapture.Start();
            }
            return videoCapturesCollection;
        }
    }
}
