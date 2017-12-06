using NUnit.Framework;
using Comparison_shopping_engine_backend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Configuration;
using System.IO;

namespace Comparison_shopping_engine_backend.Tests
{
    [TestFixture]
    public class NormalizationEngineTests
    {
        [Test]
        public void GetInstanceTest_isSingleton()
        {
            NormalizationEngine m1 = NormalizationEngine.GetInstance();
            NormalizationEngine m2 = NormalizationEngine.GetInstance();
            Assert.AreEqual(m1, m2);
        }

        [Test]
        public void GetClosestTest_getsClosest()
        {
            NormalizationEngine m1 = NormalizationEngine.GetInstance();
            m1.AddName("very unlikely to get matched with");
            m1.AddName("pretty likely");

            var result = m1.GetClosest("pretty Lily");

            Assert.AreEqual("pretty likely", result);
        }

        [Test]
        public void GetClosestTest_returnsEmptyStringIfNoMatch()
        {
            NormalizationEngine m1 = NormalizationEngine.GetInstance();
            m1.ClearList();

            var result = m1.GetClosest("pretty Lily");

            Assert.AreEqual("", result);
        }

        [Test]
        public void GetClosestListTest_getsClosest()
        {
            NormalizationEngine m1 = NormalizationEngine.GetInstance();
            m1.AddName("very unlikely to get matched with");
            m1.AddName("pretty likely");

            var result = m1.GetClosestList("pretty Lily", 2);

            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("pretty likely", result.ElementAt(0));
            Assert.AreEqual("very unlikely to get matched with", result.ElementAt(1));
        }

        [Test]
        public void GetClosestListTest_returnsEmptyListIfNoMatch()
        {
            NormalizationEngine m1 = NormalizationEngine.GetInstance();
            m1.ClearList();

            var result = m1.GetClosestList("pretty Lily", 2);

            Assert.AreEqual(0, result.Count());
        }


        [Test]
        public void PersistTest_savesNames()
        {
            NormalizationEngine m1 = NormalizationEngine.GetInstance();
            string normalizationDir = ConfigurationManager.AppSettings["normalizationDir"];
            m1.AddName("name");

            m1.Persist();

            m1.ClearList();
            DirectoryInfo normalizationDirInfo = new DirectoryInfo(normalizationDir);
            Assert.AreEqual(1, normalizationDirInfo.GetFiles("names.list").Length);
        }

        [Test]
        public void LoadAllTest_loadsNames()
        {
            NormalizationEngine m1 = NormalizationEngine.GetInstance();
            string normalizationDir = ConfigurationManager.AppSettings["normalizationDir"];
            m1.AddName("name");

            m1.Persist();
            m1.ClearList();
            Assert.IsFalse(m1.Exists("name"));
            m1.LoadAll();

            Assert.IsTrue(m1.Exists("name"));
        }

        [Test]
        public void LoadAllTest_createsDirectory()
        {
            NormalizationEngine m1 = NormalizationEngine.GetInstance();
            string normalizationDir = ConfigurationManager.AppSettings["normalizationDir"];
            DirectoryInfo normalizationDirInfo = new DirectoryInfo(normalizationDir);
            foreach (FileInfo file in normalizationDirInfo.GetFiles())
            {
                file.Delete();
            }
            normalizationDirInfo.Delete();

            m1.LoadAll();

            DirectoryInfo newInfo = new DirectoryInfo(normalizationDir);

            Assert.IsTrue(newInfo.Exists);
        }

        [Test]
        public void PersistTest_createsDirectory()
        {
            NormalizationEngine m1 = NormalizationEngine.GetInstance();
            string normalizationDir = ConfigurationManager.AppSettings["normalizationDir"];
            DirectoryInfo normalizationDirInfo = new DirectoryInfo(normalizationDir);
            foreach (FileInfo file in normalizationDirInfo.GetFiles())
            {
                file.Delete();
            }
            normalizationDirInfo.Delete();

            m1.Persist();
            DirectoryInfo normalizationDirInfoNew = new DirectoryInfo(normalizationDir);
            Assert.IsTrue(normalizationDirInfoNew.Exists);
        }
    }
}