using System;

namespace PerisanDateHelper
{
    public interface IDateBase
    {
        YearMonthDay GetYearMonthDay(string persianDate);
    }
    public class DateBase:IDateBase
    {
        public YearMonthDay GetYearMonthDay(string persianDate)
        {
            var persianDateType = GetPersianDateType(persianDate);
            var yearMonthDay = GetYearMonthDay(persianDate, persianDateType);
            return yearMonthDay;
        }
        //
        private PersianDateType GetPersianDateType(string persianDate)
        {
            const int SIX = 6;
            const int Eight = 8;
            const int TEN = 10;
            var length = persianDate.Length;
            switch (length)
            {
                case SIX:
                    return PersianDateType.SixChar;
                case Eight:
                    bool containsSlash = ContainsSlash(persianDate);
                    return containsSlash ? PersianDateType.EithtCharWithSlash : PersianDateType.EithtCharWithoutSlash;
                case TEN:
                    return PersianDateType.TenChar;
                default:
                    throw new NotSupportedException();
            }
        }
        //
        private bool ContainsSlash(string persianDate)
        {
            bool containsSlash ;
            containsSlash = persianDate.Contains("/") ? true : false;
            return containsSlash;
        }
        //
        private YearMonthDay GetYearMonthDay(string persianDate, PersianDateType persianDateType)
        {
            switch (persianDateType)
            {
                case PersianDateType.SixChar:
                    return GetYearMonthDay6Digit(persianDate);

                case PersianDateType.EithtCharWithoutSlash:
                    return GetYearMonthDay8DigitWithoutSlash(persianDate);

                case PersianDateType.EithtCharWithSlash:
                    return GetYearMonthDay8DigitWithSlash(persianDate);
                case PersianDateType.TenChar:
                    return GetYearMonthDay10(persianDate);
                default:
                    throw new NotSupportedException();
            }
        }
        //
        private YearMonthDay GetYearMonthDay6Digit(string sixDigitDate)
        {
            var yearMonthDay = ToYearMonthDay(sixDigitDate, 0, 2, 2, 2, 4, 2);
            return yearMonthDay;
        }
        //
        private YearMonthDay GetYearMonthDay8DigitWithoutSlash(string eightDigitWithoutSlash)
        {
            var yearMonthDay = ToYearMonthDay(eightDigitWithoutSlash, 0, 4, 4, 2, 6, 2);
            return yearMonthDay;
        }
        //
        private YearMonthDay GetYearMonthDay8DigitWithSlash(string eightDigitWithSlash)
        {
            var yearMonthDay = ToYearMonthDay(eightDigitWithSlash, 0, 2, 3, 2, 6, 2);
            return yearMonthDay;
        }
        //
        private YearMonthDay GetYearMonthDay10(string tenDigitDate)
        {
            var yearMonthDay = ToYearMonthDay(tenDigitDate, 0, 4, 5, 2, 8, 2);
            return yearMonthDay;
        }
        //
        private YearMonthDay ToYearMonthDay(string fullString, int yearIndex, int yearLength,
            int monthIndex,int monthLength,int dayIndex,int dayLength)
        {
            var year = Int32.Parse(fullString.Substring(yearIndex, yearLength));
            year = GetStandartYear(year);
            var month = Int32.Parse(fullString.Substring(monthIndex, monthLength));
            var day = Int32.Parse(fullString.Substring(dayIndex, dayLength));
            return new YearMonthDay(year,month,day);
        }
        //
        private int GetStandartYear(int year)
        {
            if (year < 1300)
            {
                return year > 90 ? 1300 + year : 1400 + year;//90 chon emsal 95 ast !!! 
            }
            return year;
        }
    }
    //
   
}
