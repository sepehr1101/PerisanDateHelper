using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Globalization;

namespace PerisanDateHelper

{
    public interface IPersianDateManager
    {
        int GetDateDifference(string fromDate, string toDate);

        decimal GetDateDifferenceRate(string fromOverallDate, string toOverallDate, string fromPartialDate,
            string toPartialDate);

        int GetPartialDateCount(string fromOverallDate, string toOverallDate, string fromPartialDate,
            string toPartialDate);
    }
    public class PersianDateManager:IPersianDateManager
    {
        private readonly IDateBase _dateBase;

        public PersianDateManager(IDateBase dateBase)
        {
            _dateBase = dateBase;
        }
        //
        /// <summary>
        /// اختلاف بین دو تاریخ شمسی را به تعداد روز محاسبه میکند
        /// </summary>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <returns>اختلاف به تعداد روز</returns>
        public int GetDateDifference(string fromDate, string toDate)
        {
            var fromYearMonthDay = _dateBase.GetYearMonthDay(fromDate);
            var toYearMonthDay = _dateBase.GetYearMonthDay(toDate);
            var fromMiladidate = GetDateTime(fromYearMonthDay);
            var toMiladiDate = GetDateTime(toYearMonthDay);
            int dateDifference = (int) (toMiladiDate - fromMiladidate).TotalDays;
            return dateDifference;
        }
        //
        public decimal GetDateDifferenceRate(string fromOverallDate, string toOverallDate, string fromPartialDate,
            string toPartialDate)
        {
            var overalDateDifference = GetDateDifference(fromOverallDate, toOverallDate);
            var partialDateDifference = GetDateDifference(fromPartialDate, toPartialDate);
            decimal dateDivisionRate =Decimal.Divide(partialDateDifference,overalDateDifference);
            return dateDivisionRate;
        }
        //
        /// <summary>
        /// تاریخ استاندارد میلادی برمیگرداند
        /// </summary>
        /// <param name="yearMonthDay"></param>
        /// <returns>DateTime (miladi)</returns>
        private DateTime GetDateTime(YearMonthDay yearMonthDay)
        {
            DateTime dateMiladi = new PersianCalendar().ToDateTime(yearMonthDay.Year, yearMonthDay.Month, yearMonthDay.Day, 0, 0, 0, 0);
            return dateMiladi;
        }
        //
        public int GetPartialDateCount(string fromOverallDate, string toOverallDate, string fromPartialDate,
            string toPartialDate)
        {
            string date1, date2;
            const int ZERO = 0;

            var fromOverallMiladi = GetDateTime(_dateBase.GetYearMonthDay(fromOverallDate));
            var toOverallMiladi = GetDateTime(_dateBase.GetYearMonthDay(toOverallDate));
            var fromPartialMiladi = GetDateTime(_dateBase.GetYearMonthDay(fromPartialDate));
            var toPartialMiladi = GetDateTime(_dateBase.GetYearMonthDay(toPartialDate));

            ValidateDates(fromOverallMiladi,toOverallMiladi);
            ValidateDates(fromPartialMiladi,toPartialMiladi);
            //
            // <--------------->p
            //          <--------------------------->o
            if (fromPartialMiladi <= fromOverallMiladi && toPartialMiladi <= toOverallMiladi && fromOverallMiladi<=toPartialMiladi)
            {
                date1 = fromOverallDate;
                date2 = toPartialDate;
            }
            //
            //  <----------->p
            //<---------------->o
            else if (fromOverallMiladi <= fromPartialMiladi && toPartialMiladi <= toOverallMiladi && fromOverallMiladi<=toPartialMiladi)
            {
                date1 = fromPartialDate;
                date2 = toPartialDate;
            }
            //
            //             <------>p
            // <-------------->o
            else if (fromOverallMiladi <= fromPartialMiladi && toOverallMiladi <= toPartialMiladi && fromPartialMiladi<=toOverallMiladi)
            {
                date1 = fromPartialDate;
                date2 = toOverallDate;
            }
            //
            // <----------------------------->p
            //    <------------------->o
            else if (fromPartialMiladi <= fromOverallMiladi && toOverallMiladi <= toPartialMiladi && fromOverallMiladi<=toPartialMiladi)
            {
                date1 = fromOverallDate;
                date2 = toOverallDate;
            }
            else
            {
                return ZERO;
            }
            return GetDateDifference(date1, date2);
        }
        //
        public void ValidateDates(DateTime date1, DateTime date2)
        {
            if (date2 < date1)
            {
                throw new NotSupportedException();
            }
        }
    }
}
