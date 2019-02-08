// CharReader.cs - 11/14/2018

using System;
using System.Collections.Generic;
using System.IO;

namespace Common.JSON
{
    public class CharReader
    {
        private const int TYPE_STRING = 0;
        private const int TYPE_STREAM = 1;
        private readonly string _inputString = null;
        private readonly TextReader _inputStream = null;
        private readonly int _typeValue = -1;
        private int _charPos = 0;
        private Stack<char> _charStack = new Stack<char>();

        public CharReader(string value)
        {
            _typeValue = TYPE_STRING;
            _inputString = value;
        }

        public CharReader(TextReader value)
        {
            _typeValue = TYPE_STREAM;
            _inputStream = value;
        }

        public char Peek()
        {
            if (_charStack.Count > 0)
            {
                return _charStack.Peek();
            }
            switch (_typeValue)
            {
                case TYPE_STRING:
                    if (_charPos >= _inputString.Length)
                    {
                        throw new EndOfStreamException();
                    }
                    return _inputString[_charPos];
                case TYPE_STREAM:
                    int result = _inputStream.Peek();
                    if (result < 0)
                    {
                        throw new EndOfStreamException();
                    }
                    return (char)result;
                default:
                    throw new ArgumentException();
            }
        }

        public char Read()
        {
            if (_charStack.Count > 0)
            {
                return _charStack.Pop();
            }
            switch (_typeValue)
            {
                case TYPE_STRING:
                    if (_charPos >= _inputString.Length)
                    {
                        throw new EndOfStreamException();
                    }
                    return _inputString[_charPos++];
                case TYPE_STREAM:
                    int result = _inputStream.Read();
                    if (result < 0)
                    {
                        throw new EndOfStreamException();
                    }
                    return (char)result;
                default:
                    throw new ArgumentException();
            }
        }

        public bool EOF()
        {
            if (_charStack.Count > 0)
            {
                return false;
            }
            switch (_typeValue)
            {
                case TYPE_STRING:
                    if (_charPos >= _inputString.Length)
                    {
                        return true;
                    }
                    return false;
                case TYPE_STREAM:
                    int result = _inputStream.Peek();
                    if (result < 0)
                    {
                        return true;
                    }
                    return false;
                default:
                    throw new ArgumentException();
            }
        }

        public void Push(char c)
        {
            _charStack.Push(c);
        }
    }
}
