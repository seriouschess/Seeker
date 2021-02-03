using System.Collections.Generic;
using NUnit.Framework;
using Seeker.ServerClasses;

namespace app.tests
{
    public class Tests
    {
        public Scanner _scannerModule = new Scanner();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            List<string> keywords = new List<string>(){
                "cat"
            };
            string content = "The cat in the hat had a mat where he sat.";
            double one_eleventh = (double)1/11;
            Assert.AreEqual(one_eleventh, _scannerModule.ScanString( content, keywords ));
        }

        [Test]
        public void Test2()
        {
            List<string> keywords = new List<string>(){
                "thrill",
                "mill"
            };
            string content = "Bill got his thrill at the mill getting his fill";
            double two_elevenths = (double)2/10;
            Assert.AreEqual(two_elevenths, _scannerModule.ScanString( content, keywords ));
        }
    }
}