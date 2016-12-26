using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PerisanDateHelper;

namespace PersianDateHelper.Test
{
    [TestFixture]
    public class TestDateBase
    {
        [Test]
        public void TestSixDigit()
        {
            var dateBase = new DateBase();
            var res = dateBase.GetYearMonthDay("950202");
            var monthDay = new YearMonthDay(1395, 2, 2);
            Assert.That(monthDay.Year, Is.EqualTo(res.Year));
            Assert.That(monthDay.Month, Is.EqualTo(res.Month));
            Assert.That(monthDay.Day, Is.EqualTo(res.Day));
        }
        //
        [Test]
        public void TestEightDigitWithoutSlash()
        {
            var dateBase = new DateBase();
            var res = dateBase.GetYearMonthDay("13950202");
            var monthDay = new YearMonthDay(1395, 2, 2);
            Assert.That(monthDay.Year, Is.EqualTo(res.Year));
            Assert.That(monthDay.Month, Is.EqualTo(res.Month));
            Assert.That(monthDay.Day, Is.EqualTo(res.Day));
        }
        //
        [Test]
        public void TestEightDigitWithSlash()
        {
            var dateBase = new DateBase();
            var res = dateBase.GetYearMonthDay("95/02/02");
            var monthDay = new YearMonthDay(1395, 2, 2);
            Assert.That(monthDay.Year, Is.EqualTo(res.Year));
            Assert.That(monthDay.Month, Is.EqualTo(res.Month));
            Assert.That(monthDay.Day, Is.EqualTo(res.Day));
        }
        //
        [Test]
        public void TestTenDigit()
        {
            var dateBase=new DateBase();
            var res = dateBase.GetYearMonthDay("1395/02/02");
            var monthDay = new YearMonthDay(1395, 2, 2);
            Assert.That(monthDay.Year,Is.EqualTo(res.Year));
            Assert.That(monthDay.Month, Is.EqualTo(res.Month));
            Assert.That(monthDay.Day, Is.EqualTo(res.Day));
        }
        //
      

    }
}
