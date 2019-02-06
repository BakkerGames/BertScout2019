// JArray.cs - 11/14/2018

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Common.JSON
{
    sealed public partial class JArray : JBase, IEnumerable<object>
    {
        private List<object> _data = new List<object>();

        public IEnumerator<object> GetEnumerator()
        {
            return ((IEnumerable<object>)_data).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<object>)_data).GetEnumerator();
        }

        public JArray()
        {
        }

        public JArray(JArray values)
        {
            Append(values);
        }

        public int Count
        {
            get
            {
                return _data.Count;
            }
        }

        public void Clear()
        {
            _data.Clear();
        }

        public void Add(object value)
        {
            _data.Add(value);
        }

        public void Append(JArray jarray)
        {
            if (jarray == null)
            {
                return;
            }
            foreach (object value in jarray)
            {
                _data.Add(value);
            }
        }

        public void Remove(int index)
        {
            _data.RemoveAt(index);
        }

        public object GetValue(int index)
        {
            return _data[index];
        }

        public void SetValue(int index, object value)
        {
            _data[index] = value;
        }

        public override string ToString()
        {
            return _ToString(JsonFormat.None, 0);
        }

        public string ToString(JsonFormat format)
        {
            return _ToString(format, 0);
        }

        internal string _ToString(JsonFormat format, int level)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            level++;
            bool addComma = false;
            foreach (object obj in this)
            {
                if (addComma)
                {
                    sb.Append(",");
                    if (format == JsonFormat.Indent)
                    {
                        sb.AppendLine();
                        sb.Append(new string(' ', level * IndentSize));
                    }
                    else if (format == JsonFormat.Tabs)
                    {
                        sb.AppendLine();
                        sb.Append(new string('\t', level));
                    }
                }
                else
                {
                    if (format == JsonFormat.Indent)
                    {
                        sb.AppendLine();
                        sb.Append(new string(' ', level * IndentSize));
                    }
                    else if (format == JsonFormat.Tabs)
                    {
                        sb.AppendLine();
                        sb.Append(new string('\t', level));
                    }
                    addComma = true;
                }
                if (obj == null)
                {
                    sb.Append("null"); // must be lowercase
                }
                else if (obj.GetType() == typeof(bool))
                {
                    sb.Append((bool)obj ? "true" : "false"); // must be lowercase
                }
                else if (_IsFloatType(obj))
                {
                    // don't try to normalize
                    sb.Append(obj.ToString());
                }
                else if (_IsDecimalType(obj))
                {
                    // normalize decimal places
                    sb.Append(_NormalizeDecimal(obj.ToString()));
                }
                else if (_IsNumericType(obj))
                {
                    // number with no quotes
                    sb.Append(obj.ToString());
                }
                else if (obj.GetType() == typeof(DateTime))
                {
                    // datetime converted to string format
                    sb.Append("\"");
                    DateTime tempDT = (DateTime)obj;
                    if (tempDT.Hour + tempDT.Minute + tempDT.Second + tempDT.Millisecond == 0)
                    {
                        sb.Append(tempDT.ToString(_dateOnlyFormat));
                    }
                    else
                    {
                        sb.Append(tempDT.ToString(_dateTimeFormat));
                    }
                    sb.Append("\"");
                }
                else if (obj.GetType() == typeof(JObject))
                {
                    sb.Append(((JObject)obj)._ToString(format, level));
                }
                else if (obj.GetType() == typeof(JArray))
                {
                    sb.Append(((JArray)obj)._ToString(format, level));
                }
                else if (obj.GetType().IsArray)
                {
                    JArray tempArray = new JArray();
                    for (int i = ((Array)obj).GetLowerBound(0); i <= ((Array)obj).GetUpperBound(0); i++)
                    {
                        if (((Array)obj).Rank == 1)
                        {
                            tempArray.Add(((Array)obj).GetValue(i));
                        }
                        else
                        {
                            JArray tempArray2 = new JArray();
                            for (int j = ((Array)obj).GetLowerBound(1); j <= ((Array)obj).GetUpperBound(1); j++)
                            {
                                if (((Array)obj).Rank == 2)
                                {
                                    tempArray2.Add(((Array)obj).GetValue(i, j));
                                }
                                else
                                {
                                    JArray tempArray3 = new JArray();
                                    for (int k = ((Array)obj).GetLowerBound(2); k <= ((Array)obj).GetUpperBound(2); k++)
                                    {
                                        tempArray3.Add(((Array)obj).GetValue(i, j, k));
                                    }
                                    tempArray2.Add(tempArray3);
                                }
                            }
                            tempArray.Add(tempArray2);
                        }
                    }
                    sb.Append(tempArray.ToString());
                }
                else if (obj.GetType().IsGenericType && obj is IEnumerable)
                {
                    JArray tempArray = new JArray();
                    foreach (object o in (IEnumerable)obj)
                    {
                        tempArray.Add(o);
                    }
                    sb.Append(tempArray.ToString());
                }
                else // string or other type which needs quotes
                {
                    sb.Append("\"");
                    sb.Append(_ToJsonString(obj.ToString()));
                    sb.Append("\"");
                }
            }
            level--;
            if (addComma)
            {
                if (format == JsonFormat.Indent)
                {
                    sb.AppendLine();
                    sb.Append(new string(' ', level * IndentSize));
                }
                else if (format == JsonFormat.Tabs)
                {
                    sb.AppendLine();
                    sb.Append(new string('\t', level));
                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static void _SaveValue(ref JArray obj, string value, bool inStringValue)
        {
            if (!inStringValue)
            {
                value = value.TrimEnd(); // helps with parsing
            }
            if (inStringValue)
            {
                // see if the string is a datetime format
                if (DateTime.TryParse(value, CultureInfo.InvariantCulture,
                                      DateTimeStyles.RoundtripKind, out DateTime datetimeValue))
                {
                    obj.Add(datetimeValue);
                }
                else
                {
                    obj.Add(value);
                }
            }
            else if (value == "null")
            {
                obj.Add(null);
            }
            else if (value == "true")
            {
                obj.Add(true);
            }
            else if (value == "false")
            {
                obj.Add(false);
            }
            else if (int.TryParse(value, out int intValue))
            {
                obj.Add(intValue); // default to int for anything smaller
            }
            else if (long.TryParse(value, out long longValue))
            {
                obj.Add(longValue);
            }
            else if (decimal.TryParse(value, out decimal decimalValue))
            {
                obj.Add(decimalValue);
            }
            else if (double.TryParse(value, out double doubleValue))
            {
                obj.Add(doubleValue);
            }
            else // unknown or non-numeric value
            {
                throw new SystemException($"Invalid value = '{value}'");
            }
        }

        public new static JArray Parse(string value)
        {
            return (JArray)JBase.Parse(value);
        }

        public new static JArray Parse(TextReader value)
        {
            return (JArray)JBase.Parse(value);
        }

        public new static JArray Parse(CharReader value)
        {
            return (JArray)JBase.Parse(value);
        }

        public JArray Clone()
        {
            // returns a new JArray with no references to any existing objects in memory
            return Parse(ToString());
        }
    }
}
