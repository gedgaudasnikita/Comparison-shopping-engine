using System;
using System.Drawing;
using Tesseract;

namespace Comparison_shopping_engine
{
    public static class OCRWrapper
    {
        private static TesseractEngine engine = new TesseractEngine(@"./tessdata", "lit", EngineMode.Default);

        public static String ConvertToText(Bitmap receipt)
        {
            PixConverter.ToPix(receipt);
            var text = engine.Process(receipt).GetText();
            return text;
        }
    }
}
