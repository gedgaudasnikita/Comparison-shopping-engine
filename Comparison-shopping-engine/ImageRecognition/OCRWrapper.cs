using System;
using System.Drawing;
using Tesseract;

namespace Comparison_shopping_engine
{
    /// <summary>
    /// This static class is representing the OCR functionality, contains the Tesseract engine
    /// and makes use of it
    /// </summary>
    public static class OCRWrapper
    {
        private static TesseractEngine engine = new TesseractEngine(@"./tessdata", "lit", EngineMode.Default);

        /// <summary>
        /// Converts a given <see langword="Bitmap"/> to plain text.
        /// </summary>
        /// <param name="receipt">The <see langword="Bitmap"/> to be converted</param>
        /// <returns>The <see langword="string"/> output of the OCR engine</returns>
        public static String ConvertToText(Bitmap receipt)
        {
            PixConverter.ToPix(receipt);
            var text = engine.Process(receipt).GetText();
            return text;
        }
    }
}
