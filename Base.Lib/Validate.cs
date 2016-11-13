using Base.Common;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Base.Lib
{
    public class Validate
    {
        public static Regex RgxInterger = new Regex(Enums.RegexDefine.Interger);
        public static Regex RgxNumber = new Regex(Enums.RegexDefine.Numeric);
        public static Regex RgxDateVn = new Regex(Enums.RegexDefine.DateVN);
        public static Regex RgxDateTimeVn = new Regex(Enums.RegexDefine.DateTimeVN);
        public static Regex RgxDateIso = new Regex(Enums.RegexDefine.DateIso);

        public static bool IsInterger(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            return RgxInterger.IsMatch(value);
        }

        public static int ConvertInt(string value, int defaultValue = 0)
        {
            if (IsInterger(value))
                return Convert.ToInt32(value);
            else
                return defaultValue;
        }

        public static int? ConvertIntAlowNull(string value, int? defaultValue = null)
        {
            if (IsInterger(value))
                return Convert.ToInt32(value);
            else
                return defaultValue;
        }

        public static byte ConvertByte(string value, byte defaultValue = 0)
        {
            if (IsInterger(value))
                return Convert.ToByte(value);
            else
                return defaultValue;
        }

        public static byte? ConvertByteAlowNull(string value, byte? defaultValue = null)
        {
            if (IsInterger(value))
                return Convert.ToByte(value);
            else
                return defaultValue;
        }

        public static short ConvertShort(string value, short defaultValue = 0)
        {
            if (IsInterger(value))
                return Convert.ToInt16(value);
            else
                return defaultValue;
        }

        public static short? ConvertShortAlowNull(string value, short? defaultValue = null)
        {
            if (IsInterger(value))
                return Convert.ToInt16(value);
            else
                return defaultValue;
        }

        public static long ConvertLong(string value, long defaultValue = 0)
        {
            if (IsInterger(value))
                return Convert.ToInt64(value);
            else
                return defaultValue;
        }

        public static long? ConvertLongAlowNull(string value, long? defaultValue = null)
        {
            if (IsInterger(value))
                return Convert.ToInt64(value);
            else
                return defaultValue;
        }

        public static bool IsNumber(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            return RgxNumber.IsMatch(value);
        }

        public static bool IsDecimal(string value, string dot = ".")
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            return Regex.IsMatch(value, @"^\d+(\" + dot + @"\d+)?$");
        }

        public static decimal ConvertDecimal(string value, decimal defaultValue = 0)
        {
            if (IsNumber(value))
                return Convert.ToDecimal(value);
            else
                return defaultValue;
        }

        public static decimal? ConvertDecimalAlowNull(string value, decimal? defaultValue = null)
        {
            if (IsNumber(value))
                return Convert.ToDecimal(value);
            else
                return defaultValue;
        }

        public static double ConvertDouble(string value, double defaultValue = 0)
        {
            if (IsNumber(value))
                return Convert.ToDouble(value);
            else
                return defaultValue;
        }

        public static double? ConvertDoubleAlowNull(string value, double? defaultValue = null)
        {
            if (IsNumber(value))
                return Convert.ToDouble(value);
            else
                return defaultValue;
        }

        public static bool IsDateVn(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            return RgxDateVn.IsMatch(value);
        }
        public static bool IsDateTimeVn(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            return RgxDateTimeVn.IsMatch(value);
        }

        public static DateTime ConvertDateVN(string value, DateTime? defaultValue = null)
        {
            if (IsDateVn(value))
                return DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                return defaultValue.HasValue ? defaultValue.Value : DateTime.Now;
        }

        public static DateTime? ConvertDateVNAlowNull(string value, DateTime? defaultValue = null)
        {
            if (IsDateVn(value))
                return DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            else
                return defaultValue;
        }
        public static DateTime? ConvertDateTimeVNAlowNull(string value, DateTime? defaultValue = null)
        {
            try
            {
                return DateTime.ParseExact(value, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }
        public static bool IsDateIso(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return false;
            return RgxInterger.IsMatch(value);
        }

        public static DateTime ConvertDateIso(string value, DateTime? defaultValue)
        {
            if (IsDateIso(value))
                return Convert.ToDateTime(value);
            else
                return defaultValue.HasValue ? defaultValue.Value : DateTime.Now;
        }

        public static bool CheckCustom(string pattern, string value)
        {
            return Regex.IsMatch(value, pattern);
        }

        public static DateTime ConvertDate(string value, string format = "dd/MM/yyyy", string DateSeparator = "/")
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = format;
            dtfi.DateSeparator = DateSeparator;
            return Convert.ToDateTime(value, dtfi);
        }
    }
}