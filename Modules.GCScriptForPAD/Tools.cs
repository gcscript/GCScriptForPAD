using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Modules.GCScriptForPAD
{
    public enum ETextCase { None, ToLower, ToUpper, ToTitleCase }
    public enum ETextType { None, OnlyLetters, OnlyNumbers, OnlyLettersNumbers, OnlyLettersNumbersSpaces }
    public enum ETextTrim { None, Trim, TrimStart, TrimEnd }
    public enum ETextRemoveSpaces { None, Duplicate, All }

    public static class Tools
    {
        public static string ProcessText(string text, bool removeAccents, ETextTrim textTrim, ETextCase textCase, ETextType textType, ETextRemoveSpaces removeSpaces)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }

            if (removeAccents) { text = RemoveAccents(text); }

            switch (textTrim)
            {
                case ETextTrim.Trim: { text = text.Trim(); break; }
                case ETextTrim.TrimStart: { text = text.TrimStart(); break; }
                case ETextTrim.TrimEnd: { text = text.TrimEnd(); break; }
            }

            switch (textCase)
            {
                case ETextCase.ToLower: { text = text.ToLower(); break; }
                case ETextCase.ToUpper: { text = text.ToUpper(); break; }
                case ETextCase.ToTitleCase: { text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower()); break; }
            }

            switch (removeSpaces)
            {
                case ETextRemoveSpaces.Duplicate: { text = RemoveDuplicateSpaces(text); break; }
                case ETextRemoveSpaces.All: { text = RemoveSpaces(text); break; }
            }

            switch (textType)
            {
                case ETextType.OnlyLetters: { text = OnlyLetters(text); break; }
                case ETextType.OnlyNumbers: { text = OnlyNumbers(text); break; }
                case ETextType.OnlyLettersNumbers: { text = OnlyLettersNumbers(text); break; }
                case ETextType.OnlyLettersNumbersSpaces: { text = OnlyLettersNumbersSpaces(text); break; }
            }

            return text;
        }

        public static string RemoveAccents(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            StringBuilder sbReturn = new StringBuilder();
            foreach (char letter in text.Normalize(NormalizationForm.FormD))
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static string RemoveSpaces(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            text = Regex.Replace(text, @"\s", "");
            return text;
        }

        public static string RemoveDuplicateSpaces(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            text = Regex.Replace(text, @"\s+", " ");
            return text;
        }

        public static string OnlyLetters(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            text = Regex.Replace(text, @"[^a-zA-Z]", "");
            return text;
        }

        public static string OnlyNumbers(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            text = Regex.Replace(text, @"[^0-9]", "");
            return text;
        }


        public static string OnlyLettersNumbers(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            text = Regex.Replace(text, @"[^a-zA-Z0-9]", "");
            return text;
        }

        public static string OnlyLettersNumbersSpaces(string text)
        {
            if (string.IsNullOrEmpty(text)) { return ""; }
            text = Regex.Replace(text, @"[^a-zA-Z0-9\s]", "");
            return text;
        }

        public static DateTime DateParser(string input)
        {
            if (input is null) { return DateTime.MinValue; }

            try
            {
                //var formats = DateTimeFormatInfo.InvariantInfo.GetAllDateTimePatterns();

                string[] formats = { "dd/MM/yyyy HH:mm:ss:fff",
                                 "dd/MM/yyyy HH:mm:ss",
                                 "dd/MM/yyyy HH:mm",
                                 "dd/MM/yyyy",
                                 "d/MM/yyyy HH:mm:ss:fff",
                                 "d/MM/yyyy HH:mm:ss",
                                 "d/MM/yyyy HH:mm",
                                 "d/MM/yyyy",
                                 "d/M/yyyy HH:mm:ss:fff",
                                 "d/M/yyyy HH:mm:ss",
                                 "d/M/yyyy HH:mm",
                                 "d/M/yyyy",
                                 "d/M/yy HH:mm:ss:fff",
                                 "d/M/yy HH:mm:ss",
                                 "d/M/yy HH:mm",
                                 "d/M/yy" };

                if (DateTime.TryParseExact(input, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    return date;
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }

        public static int GetDifferenceInDays(DateTime date1, DateTime date2)
        {
            return (int)(date2 - date1).TotalDays;
        }
    }
}
