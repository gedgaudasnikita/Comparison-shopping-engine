using Microsoft.VisualStudio.TestTools.UnitTesting;
using Comparison_shopping_engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Comparison_shopping_engine.Tests
{
    [TestClass()]
    public class ReceiptTranslatorTests
    {
        [TestMethod()]
        public void convertToTextTest_ExtractsText()
        {
            var item = "Nuolaidų kuponas šokoladui";
            var img = new Bitmap("./testdata/receipt.jpg");

            var result = ReceiptTranslator.convertToText(img);

            Assert.IsTrue(result.Contains(item));
        }
    }
}