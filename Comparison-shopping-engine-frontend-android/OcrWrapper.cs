using System;
using System.Drawing;
using System.Threading.Tasks;
using Tesseract;
using Tesseract.Droid;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This class is representing the OCR functionality, contains the Tesseract engine
    /// and makes use of it.
    /// </summary>
    public class OcrWrapper
    {
        private TesseractApi engine;
        private bool initialized = false;

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
            return await engine.Init("lit");
        }

        /// <summary>
        /// Converts a specified image to plain text.
        /// </summary>
        /// <param name="path">The path of the image to be converted</param>
        /// <returns>The <see cref="string"/> output of the OCR engine</returns>
        public async Task<string> ConvertToText(string path)
        {
            await engine.SetImage(path);
            return engine.Text;
        }

        public void Dispose()
        {
            engine.Dispose();
        }
    }
}
