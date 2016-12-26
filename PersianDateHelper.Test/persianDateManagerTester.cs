using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  NUnit.Framework;
using PerisanDateHelper;
using StructureMap;

namespace PersianDateHelper.Test
{
    [TestFixture]
    public class persianDateManagerTester
    {
        private IPersianDateManager persianDateManager;
        public persianDateManagerTester()
        {
            var container = new Container(x =>
            {
                x.For<IDateBase>().Use<DateBase>();
                x.For<IPersianDateManager>().Use<PersianDateManager>();
            });

            persianDateManager = container.GetInstance<IPersianDateManager>();
        }
        [Test]
        public void TestDateDifferenceRate()
        {
            //int dateDifference= persianDateManager.GetDateDifference("950202", "951002");
            //Assert.That(dateDifference,Is.EqualTo(32));

            var dateRate = persianDateManager.GetDateDifferenceRate("1395/01/01", "1395/12/29", "95/04/22", "95/07/22");
            //Assert.That(dateRate,Is.EqualTo(24));
        }
        //
        [Test]
        public void TestState1()
        {
            var diff = persianDateManager.GetPartialDateCount("1395/01/01", "1395/12/29", "95/04/22", "95/07/22");
            Assert.That(diff,Is.EqualTo(93));
        }
        //
        //
        [Test]
        public void TestState2()
        {
            var diff = persianDateManager.GetPartialDateCount("1395/05/01", "1395/12/29", "95/04/22", "95/07/22");
            Assert.That(diff, Is.EqualTo(83));
        }
        //
        [Test]
        public void TestState3()
        {
            var diff = persianDateManager.GetPartialDateCount("1395/05/01", "1395/06/29", "95/04/22", "95/07/22");
            Assert.That(diff, Is.EqualTo(59));
        }
        //
        [Test]
        public void TestState4()
        {
            var diff = persianDateManager.GetPartialDateCount("1395/02/01", "1395/03/29", "95/04/22", "95/07/22");
            Assert.That(diff, Is.EqualTo(0));
        }
        //
        [Test]
        public void TestState5()
        {
            var diff = persianDateManager.GetPartialDateCount("1395/05/01", "1395/06/29", "95/03/22", "95/04/22");
            Assert.That(diff, Is.EqualTo(0));
        }
        //
        [Test]
        public void TestState6()
        {
            Assert.Throws<NotSupportedException>(
                () => persianDateManager.GetPartialDateCount("1395/07/01", "1395/06/29", "95/03/22", "95/04/22"));
        }
    }
}
