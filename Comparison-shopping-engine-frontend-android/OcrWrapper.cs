using Android.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Tesseract;
using Tesseract.Droid;

namespace Comparison_shopping_engine_frontend_android
{
    /// <summary>
    /// This class is representing the OCR functionality, contains the Tesseract engine
    /// and makes use of it.
    /// </summary>
    public class OcrWrapper
    {
        private TesseractApi engine;

        public OcrWrapper(Android.Content.Context context)
        {
            engine = new TesseractApi(context, AssetsDeployment.OncePerVersion);
        }

        /// <summary>
        /// Initializes the Ocr engine
        /// </summary>
        /// <returns>The result of initialization</returns>
        public async Task<bool> Initialize()
        {
            return await engine.Init("lit", OcrEngineMode.TesseractOnly);
        }

        /// <summary>
        /// Converts a specified image to plain text.
        /// </summary>
        /// <param name="path">The <see cref="Bitmap"/> of the image to be converted</param>
        /// <returns>The <see cref="string"/> output of the OCR engine</returns>
        public async Task<string> ConvertToText(Bitmap receipt, IEnumerable<EventHandler<ProgressEventArgs>> progressListeners = null)
        {
            if (progressListeners != null)
            {
                foreach (var handler in progressListeners)
                {
                    engine.Progress += handler;
                }
            }

            engine.Clear();
            
            bool recognised = await engine.Recognise(receipt);

            var result = "";

            if (recognised)
            {
                result = engine.Text;
            }

            if (progressListeners != null)
            {
                foreach (var handler in progressListeners)
                {
                    engine.Progress -= handler;
                }
            }

            return result;
        }

        public void Dispose()
        {
            engine.Dispose();
        }
    }
}
