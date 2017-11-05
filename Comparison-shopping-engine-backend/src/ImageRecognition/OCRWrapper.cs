using System;
using System.Drawing;
using Tesseract;

namespace Comparison_shopping_engine_backend
{
    /// <summary>
    /// This static class is representing the OCR functionality, contains the Tesseract engine
    /// and makes use of it.
    /// </summary>
    public static class OCRWrapper
    {
        private static TesseractEngine engine = new TesseractEngine(@"./tessdata", "lit", EngineMode.Default);
       
        /// <summary>
        /// Converts a given <see cref="Bitmap"/> to plain text.
        /// </summary>
        /// <param name="receipt">The <see cref="Bitmap"/> to be converted</param>
        /// <returns>The <see cref="string"/> output of the OCR engine</returns>
        public static String ConvertToText(Bitmap receipt)
        {
            Pix convertedImage = PixConverter.ToPix(receipt);
            var resultPage = engine.Process(convertedImage);
            string resultText = resultPage.GetText();
            resultPage.Dispose();
            return resultText;
        }

        public static void Dispose()
        {
            engine.Dispose();
        }
    }
}
