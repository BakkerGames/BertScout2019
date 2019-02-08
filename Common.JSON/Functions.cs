// Functions.cs - 11/14/2018

using System;
using System.Globalization;
using System.Text;

namespace Common.JSON
{
    static internal class Functions
    {
        internal static int IndentSize = 2;
        internal static CultureInfo decimalCulture = CultureInfo.CreateSpecificCulture("en-US");


        //internal static string GetStringValue(CharReader reader)
        //{
        //    StringBuilder value = new StringBuilder();
        //    char c;
        //    bool lastWasSlash = false;
        //    while (true)
        //    {
        //        c = reader.Read();
        //        if (!lastWasSlash && c == '\\') // slashed char
        //        {
        //            lastWasSlash = true;
        //            continue;
        //        }
        //        if (lastWasSlash) // here is the slashed char
        //        {
        //            lastWasSlash = false;
        //            string escapedChar;
        //            if (c == 'u')
        //            {
        //                escapedChar = FromUnicodeChar($"\\u{reader.Read()}{reader.Read()}{reader.Read()}{reader.Read()}");
        //            }
        //            else
        //            {
        //                escapedChar = FromEscapedChar(c);
        //            }
        //            value.Append(escapedChar);
        //            continue;
        //        }
        //        if (c == '\"') // end of string
        //        {
        //            return value.ToString();
        //        }
        //        // any other char in a string
        //        value.Append(c);
        //    }
        //}

        //internal static string ToJsonString(string input)
        //{
        //    // handle escaping of special chars here
        //    StringBuilder result = new StringBuilder();
        //    int pos = 0;
        //    char c;
        //    while (pos < input.Length)
        //    {
        //        c = input[pos];
        //        pos++;
        //        if (c == '\\')
        //        {
        //            result.Append("\\\\");
        //        }
        //        else if (c == '\"')
        //        {
        //            result.Append("\\\"");
        //        }
        //        else if (c == '\r')
        //        {
        //            result.Append("\\r");
        //        }
        //        else if (c == '\n')
        //        {
        //            result.Append("\\n");
        //        }
        //        else if (c == '\t')
        //        {
        //            result.Append("\\t");
        //        }
        //        else if (c == '\b')
        //        {
        //            result.Append("\\b");
        //        }
        //        else if (c == '\f')
        //        {
        //            result.Append("\\f");
        //        }
        //        else if (c < 32 || c == 127 || c == 129 || c == 141 || c == 143 ||
        //                 c == 144 || c == 157 || c == 160 || c == 173 || c > 255)
        //        {
        //            // ascii control chars, unused chars, or unicode chars
        //            result.Append(string.Format("\\u{0:x4}", (int)c));
        //        }
        //        else
        //        {
        //            result.Append(c);
        //        }
        //    }
        //    return result.ToString();
        //}

        //internal static bool IsNumericType(object value)
        //{
        //    if (value == null)
        //    {
        //        return false;
        //    }
        //    Type t = value.GetType();
        //    switch (Type.GetTypeCode(t))
        //    {
        //        case TypeCode.Byte:
        //        case TypeCode.SByte:
        //        case TypeCode.Int16:
        //        case TypeCode.Int32:
        //        case TypeCode.Int64:
        //        case TypeCode.UInt16:
        //        case TypeCode.UInt32:
        //        case TypeCode.UInt64:
        //        case TypeCode.Single:
        //        case TypeCode.Double:
        //        case TypeCode.Decimal:
        //            return true;
        //        case TypeCode.Object:
        //            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
        //            {
        //                return IsNumericType(Nullable.GetUnderlyingType(t));
        //            }
        //            return false;
        //    }
        //    return false;
        //}

        //internal static bool IsDecimalType(object value)
        //{
        //    if (value == null)
        //    {
        //        return false;
        //    }
        //    Type t = value.GetType();
        //    switch (Type.GetTypeCode(t))
        //    {
        //        case TypeCode.Single:
        //        case TypeCode.Double:
        //        case TypeCode.Decimal:
        //            return true;
        //        case TypeCode.Object:
        //            if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
        //            {
        //                return IsDecimalType(Nullable.GetUnderlyingType(t));
        //            }
        //            return false;
        //    }
        //    return false;
        //}

        //internal static string FromEscapedChar(char c)
        //{
        //    string result;
        //    switch (c)
        //    {
        //        case '"':
        //            result = "\"";
        //            break;
        //        case '\\':
        //            result = "\\";
        //            break;
        //        case '/':
        //            result = "/";
        //            break;
        //        case 'b':
        //            result = "\b";
        //            break;
        //        case 'f':
        //            result = "\f";
        //            break;
        //        case 'n':
        //            result = "\n";
        //            break;
        //        case 'r':
        //            result = "\r";
        //            break;
        //        case 't':
        //            result = "\t";
        //            break;
        //        default:
        //            // escaped unicode (\uXXXX) is handled in FromUnicodeChar()
        //            throw new System.Exception($"Unknown escaped char: \"\\{c}\"");
        //    }
        //    return result;
        //}

        //internal static string FromUnicodeChar(string value)
        //{
        //    string result;
        //    // value should be in the exact format "\u####", where # is a hex digit
        //    if (string.IsNullOrEmpty(value) || value.Length != 6)
        //    {
        //        throw new System.Exception($"Unknown unicode char: \"{value}\"");
        //    }
        //    result = Convert.ToChar(Convert.ToUInt16(value.Substring(2, 4), 16)).ToString();
        //    return result;
        //}

        //internal static string NormalizeDecimal(string value)
        //{
        //    decimal tempValue;
        //    if (string.IsNullOrEmpty(value))
        //    {
        //        return value;
        //    }
        //    if (!decimal.TryParse(value, out tempValue))
        //    {
        //        throw new SystemException($"Cannot parse as decimal: \"{value}\"");
        //    }
        //    return NormalizeDecimal(tempValue);
        //}

        //internal static string NormalizeDecimal(decimal value)
        //{
        //    string result;
        //    result = value.ToString(decimalCulture);
        //    if (result.IndexOf('.') >= 0)
        //    {
        //        while (result.Length > 0)
        //        {
        //            if (result.EndsWith(".")) // remove trailing decimal point
        //            {
        //                result = result.Substring(0, result.Length - 1);
        //                break; // done
        //            }
        //            if (result.EndsWith("0")) // remove trailing zero decimal digits
        //            {
        //                result = result.Substring(0, result.Length - 1);
        //                continue;
        //            }
        //            break;
        //        }
        //    }
        //    return result;
        //}
    }
}
