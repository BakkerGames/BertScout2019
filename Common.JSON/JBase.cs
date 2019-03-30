// JBase.cs - 03/29/2019

using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Common.JSON
{
    public abstract class JBase
    {
        protected const string _dateOnlyFormat = "yyyy-MM-dd";
        protected const string _dateTimeFormat = "O";

        protected int IndentSize = 2;
        protected CultureInfo decimalCulture = CultureInfo.CreateSpecificCulture("en-US");

        public static object Parse(string value)
        {
            return Parse(new CharReader(value));
        }

        public static object Parse(TextReader value)
        {
            return Parse(new CharReader(value));
        }

        public static object Parse(CharReader reader)
        {
            _SkipWhitespace(reader);
            char startChar;
            startChar = reader.Peek();
            if (startChar == '{')
            {
                return _ParseJObject(reader);
            }
            else if (startChar == '[')
            {
                return _ParseJArray(reader);
            }
            else
            {
                throw new ArgumentException("Invalid JSON input");
            }
        }

        protected static JObject _ParseJObject(CharReader reader)
        {
            JObject result = new JObject();
            char c;
            try
            {
                _SkipWhitespace(reader);
                if (reader.Read() != '{')
                {
                    throw new SystemException("Begining brace expected");
                }
                string key;
                object value;
                _SkipWhitespace(reader);
                while (reader.Peek() != '}')
                {
                    _SkipWhitespace(reader);
                    if (reader.Peek() == ',')
                    {
                        // allow extraneous commas
                        reader.Read(); // gobble char
                        _SkipWhitespace(reader);
                        continue;
                    }
                    if (reader.Peek() != '\"')
                    {
                        throw new SystemException("Quote char expected");
                    }
                    key = _GetStringValue(reader);
                    _SkipWhitespace(reader);
                    if (reader.Read() != ':')
                    {
                        throw new SystemException("Colon char expected");
                    }
                    _SkipWhitespace(reader);
                    value = _GetValue(reader);
                    result.Add(key, value);
                    _SkipWhitespace(reader);
                    c = reader.Peek();
                    if (c != ',' && c != '}')
                    {
                        throw new SystemException("Comma or end brace expected");
                    }
                    if (c == ',')
                    {
                        reader.Read(); // gobble comma char
                    }
                }
                reader.Read(); // gobble } char
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Invalid JObject input\r\n{ex.Message}");
            }
            return result;
        }

        protected static JArray _ParseJArray(CharReader reader)
        {
            JArray result = new JArray();
            char c;
            try
            {
                _SkipWhitespace(reader);
                c = reader.Read();
                if (c != '[')
                {
                    throw new SystemException("Begining bracket expected");
                }
                object value;
                _SkipWhitespace(reader);
                while (reader.Peek() != ']')
                {
                    _SkipWhitespace(reader);
                    if (reader.Peek() == ',')
                    {
                        // allow extraneous commas
                        reader.Read(); // gobble char
                        _SkipWhitespace(reader);
                        continue;
                    }
                    value = _GetValue(reader);
                    result.Add(value);
                    _SkipWhitespace(reader);
                    c = reader.Peek();
                    if (c != ',' && c != ']')
                    {
                        throw new SystemException("Comma or end bracket expected");
                    }
                    if (c == ',')
                    {
                        reader.Read(); // gobble comma char
                        _SkipWhitespace(reader);
                    }
                }
                reader.Read(); // gobble end bracket char
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Invalid JArray input\r\n{ex.Message}");
            }
            return result;
        }

        protected static void _SkipWhitespace(CharReader reader)
        {
            char currChar;
            while (true)
            {
                if (reader.EOF())
                {
                    break;
                }
                currChar = reader.Peek();
                // use c# definition of whitespace
                if (char.IsWhiteSpace(currChar))
                {
                    reader.Read(); // gobble char
                    continue;
                }
                // check for comments
                if (currChar == '/')
                {
                    reader.Read(); // gobble char
                    char nextChar = reader.Read();
                    if (nextChar == '/') // to EOF
                    {
                        while (true)
                        {
                            // not error if EOF before CR-LF
                            if (reader.EOF())
                            {
                                break;
                            }
                            currChar = reader.Read();
                            if (currChar == '\r' || currChar == '\n')
                            {
                                break;
                            }
                        }
                        continue;
                    }
                    else if (nextChar == '*') /* to */
                    {
                        nextChar = ' '; // clear for comparison
                        while (true)
                        {
                            // will throw an error if EOF before '*/' is found
                            currChar = nextChar;
                            nextChar = reader.Read();
                            if (currChar == '*' && nextChar == '/')
                            {
                                break;
                            }
                        }
                        continue;
                    }
                    else // not a comment
                    {
                        // push back in reverse order
                        reader.Push(nextChar);
                        reader.Push(currChar);
                        break;
                    }
                }
                break;
            }
        }

        private static object _GetValue(CharReader reader)
        {
            char c;
            string token;
            c = reader.Peek();
            switch (c)
            {
                case 'n':
                    token = _GetToken(reader);
                    if (token != "null")
                    {
                        throw new ArgumentException($"Invalid token: {token}");
                    }
                    return null;
                case 'f':
                    token = _GetToken(reader);
                    if (token != "false")
                    {
                        throw new ArgumentException($"Invalid token: {token}");
                    }
                    return false;
                case 't':
                    token = _GetToken(reader);
                    if (token != "true")
                    {
                        throw new ArgumentException($"Invalid token: {token}");
                    }
                    return true;
                case '\"':
                    return _GetStringValue(reader);
                case '[':
                    return _ParseJArray(reader);
                case '{':
                    return _ParseJObject(reader);
                case '-':
                case '+':
                case '.':
                case 'e':
                case 'E':
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return _GetNumeric(reader);
                default:
                    throw new ArgumentException($"Unexpected character: {c}");
            }
        }

        private static string _GetToken(CharReader reader)
        {
            StringBuilder result = new StringBuilder();
            char c;
            while (true)
            {
                c = reader.Read();
                if (char.IsLetterOrDigit(c) || c == '-' || c == '+' || c == '.')
                {
                    result.Append(c);
                    continue;
                }
                reader.Push(c);
                break;
            }
            return result.ToString();
        }

        private static string _GetStringValue(CharReader reader)
        {
            // reader must be on starting quote char
            if (reader.Read() != '\"')
            {
                throw new ArgumentException("Invalid JSON string");
            }
            StringBuilder result = new StringBuilder();
            bool escaped = false;
            char c;
            while (true)
            {
                c = reader.Read();
                if (escaped)
                {
                    switch (c)
                    {
                        case '\"':
                        case '\\':
                        case '/':
                            result.Append(c);
                            break;
                        case 'b':
                            result.Append('\b');
                            break;
                        case 'f':
                            result.Append('\f');
                            break;
                        case 'n':
                            result.Append('\n');
                            break;
                        case 'r':
                            result.Append('\r');
                            break;
                        case 't':
                            result.Append('\t');
                            break;
                        case 'u':
                            string unicodeValue = $"{reader.Read()}{reader.Read()}{reader.Read()}{reader.Read()}";
                            result.Append(Convert.ToChar(Convert.ToUInt16(unicodeValue, 16)));
                            break;
                        default:
                            throw new ArgumentException($"Invalid escaped char in string: {c}");
                    }
                    escaped = false;
                    continue;
                }
                // escape next char
                if (c == '\\')
                {
                    escaped = true;
                    continue;
                }
                // ending quote, done
                if (c == '\"')
                {
                    break;
                }
                // normal char
                result.Append(c);
            }
            return result.ToString();
        }

        private static object _GetNumeric(CharReader reader)
        {
            string value = _GetToken(reader);
            if (value.Contains("e") || value.Contains("E"))
            {
                return double.Parse(value);
            }
            if (value.Contains("."))
            {
                return decimal.Parse(value);
            }
            int intResult;
            if (int.TryParse(value, out intResult))
            {
                return intResult;
            }
            long longResult;
            if (long.TryParse(value, out longResult))
            {
                return longResult;
            }
            throw new ArgumentException($"Unable to parse numeric: {value}");
        }

        protected string _ToJsonString(string input)
        {
            // handle escaping of special chars here
            StringBuilder result = new StringBuilder();
            int pos = 0;
            char c;
            while (pos < input.Length)
            {
                c = input[pos];
                pos++;
                if (c == '\\')
                {
                    result.Append("\\\\");
                }
                else if (c == '\"')
                {
                    result.Append("\\\"");
                }
                else if (c == '\r')
                {
                    result.Append("\\r");
                }
                else if (c == '\n')
                {
                    result.Append("\\n");
                }
                else if (c == '\t')
                {
                    result.Append("\\t");
                }
                else if (c == '\b')
                {
                    result.Append("\\b");
                }
                else if (c == '\f')
                {
                    result.Append("\\f");
                }
                else if (c < 32 || c == 127 || c == 129 || c == 141 || c == 143 ||
                         c == 144 || c == 157 || c == 160 || c == 173 || c > 255)
                {
                    // ascii control chars, unused chars, or unicode chars
                    result.Append(string.Format("\\u{0:x4}", (int)c));
                }
                else
                {
                    result.Append(c);
                }
            }
            return result.ToString();
        }

        protected bool _IsNumericType(object value)
        {
            if (value == null)
            {
                return false;
            }
            Type t = value.GetType();
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                case TypeCode.Object:
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return _IsNumericType(Nullable.GetUnderlyingType(t));
                    }
                    return false;
            }
            return false;
        }

        protected bool _IsFloatType(object value)
        {
            if (value == null)
            {
                return false;
            }
            Type t = value.GetType();
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Single:
                case TypeCode.Double:
                    return true;
                case TypeCode.Object:
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return _IsFloatType(Nullable.GetUnderlyingType(t));
                    }
                    return false;
            }
            return false;
        }

        protected bool _IsDecimalType(object value)
        {
            if (value == null)
            {
                return false;
            }
            Type t = value.GetType();
            switch (Type.GetTypeCode(t))
            {
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return true;
                case TypeCode.Object:
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        return _IsDecimalType(Nullable.GetUnderlyingType(t));
                    }
                    return false;
            }
            return false;
        }

        protected string _NormalizeDecimal(string value)
        {
            decimal tempValue;
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            if (!decimal.TryParse(value, out tempValue))
            {
                throw new SystemException($"Cannot parse as decimal: \"{value}\"");
            }
            return _NormalizeDecimal(tempValue);
        }

        protected string _NormalizeDecimal(decimal value)
        {
            string result;
            result = value.ToString(decimalCulture);
            if (result.IndexOf('.') >= 0)
            {
                while (result.Length > 0)
                {
                    if (result.EndsWith(".")) // remove trailing decimal point
                    {
                        result = result.Substring(0, result.Length - 1);
                        break; // done
                    }
                    if (result.EndsWith("0")) // remove trailing zero decimal digits
                    {
                        result = result.Substring(0, result.Length - 1);
                        continue;
                    }
                    break;
                }
            }
            return result;
        }
    }
}
