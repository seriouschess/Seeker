using System.Collections.Generic;
using NUnit.Framework;
using Seeker.ServerClasses;
using app.ServerClasses.Interfaces;

namespace app.tests
{
    public class Tests
    {
        public IScanner _scannerModule = new Scanner();

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

        [Test]
        public void TestWordFinderTestOne(){
            string content = "The cat in the hat had sat on a mat.";
            Assert.AreEqual( "the", _scannerModule.ReturnMostFrequentKeyword(content) );
        }

        [Test]
        public void WordFinderTestTwo(){
            string content = "Horse. Plane Car chicken train ;horse;";
            Assert.AreEqual( "horse", _scannerModule.ReturnMostFrequentKeyword(content) );
        }
    }
}