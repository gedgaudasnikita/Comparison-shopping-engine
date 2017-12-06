using NUnit.Framework;
using Comparison_shopping_engine_backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Comparison_shopping_engine_core_entities;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestFixture]
    public class ItemListTests
    {
        [Test]
        public void ParseTest_parsesItemList()
        {
            string receipt = @"MAXIMA L UAB

                ,: szepavvmau= pr Ga Kaunas, Kasa m— 4
                PVM nokstow kodas L1730335113

                500460756

   

                Mitas 455/578

                VamHnti ledai ""Maxima

                Favorit“ 1 29 A
                KELMĖS menas, 2,5X Men 1,19 A
                Cvgarete$ ""Partner Red"" 4,79 C
                cvgaretes “Partner Red"" 4,89 0
                Malta kava ""Pamig Extra“ 8 99 A




                Be PVM PVM Su PVM
                1 „99




                Maksu 20. 95
                Mama(Kov - (des)20, 95
                „mmmnsss 93, 95 „

                Nemokamas kokybes sk m 8 500 20050
                Ačiū, KAD PIRKOTE

                Kasvnvnkesbe) 15 4
                MWMU 2009 - 0917 17:29 53

                "; ;

            ItemListParser sp = new ItemListParser();

            List<Item> itemList = sp.Parse(receipt);
            Assert.AreEqual(3, itemList.Count);
            Assert.AreEqual(129, itemList[0].Price);
            Assert.AreEqual("KELMĖS menas, 2,5X Men", itemList[1].Name);
        }
    }
}