using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// Overload for BitConverter that supports big endian
namespace Utils
{
    /// <summary>
    ///     Converts base data types to an array of bytes, and an array of bytes to base
    ///     data types.
    ///     All info taken from the meta data of System.BitConverter. This implementation
    ///     allows for Endianness consideration.
    ///</summary>

    public static class BigBitConverter
    {
        /// <summary>
        ///     Indicates the byte order ("endianess") in which data is stored in this computer
        ///     architecture.
        ///</summary>
        public static bool IsLittleEndian { get; set; } // should default to false, which is what we want for Empire
        ///
        /// <summary>
        ///     Returns the specified Boolean value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     A Boolean value.
        ///
        /// Returns:
        ///     An array of bytes with length 1.
        ///</summary>
        public static byte[] GetBytes(bool value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns the specified Unicode character value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     A character to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 2.
        ///</summary>
        public static byte[] GetBytes(char value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns the specified double-precision floating point value as an array of
        ///     bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 8.
        ///</summary>
        public static byte[] GetBytes(double value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns the specified single-precision floating point value as an array of
        ///     bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 4.
        ///</summary>
        public static byte[] GetBytes(float value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns the specified 32-bit signed integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 4.
        ///</summary>
        public static byte[] GetBytes(int value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns the specified 64-bit signed integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 8.
        ///</summary>
        public static byte[] GetBytes(long value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns the specified 16-bit signed integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 2.
        ///</summary>
        public static byte[] GetBytes(short value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns the specified 32-bit unsigned integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 4.
        ///</summary>
        [CLSCompliant(false)]
        public static byte[] GetBytes(uint value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns the specified 64-bit unsigned integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 8.
        ///</summary>
        [CLSCompliant(false)]
        public static byte[] GetBytes(ulong value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns the specified 16-bit unsigned integer value as an array of bytes.
        ///
        /// Parameters:
        ///   value:
        ///     The number to convert.
        ///
        /// Returns:
        ///     An array of bytes with length 2.
        ///</summary>
        public static byte[] GetBytes(ushort value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.GetBytes(value);
            }
            else
            {
                return System.BitConverter.GetBytes(value).Reverse().ToArray();
            }
        }
        ///
        /// <summary>
        ///     Returns a 16-bit signed integer converted from two bytes at a specified position
        ///     in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 16-bit signed integer formed by two bytes beginning at startIndex.
        ///</summary>
        public static short ToInt16(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToInt16(value, startIndex);
            }
            else
            {
                return System.BitConverter.ToInt16(value.Reverse().ToArray(), value.Length - sizeof(Int16) - startIndex);
            }
        }
        ///
        /// <summary>
        ///     Returns a 32-bit signed integer converted from four bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 32-bit signed integer formed by four bytes beginning at startIndex.
        ///</summary>
        public static int ToInt32(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToInt32(value, startIndex);
            }
            else
            {
                return System.BitConverter.ToInt32(value.Reverse().ToArray(), value.Length - sizeof(Int32) - startIndex);
            }
        }
        ///
        /// <summary>
        ///     Returns a 64-bit signed integer converted from eight bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 64-bit signed integer formed by eight bytes beginning at startIndex.
        ///</summary>
        public static long ToInt64(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToInt64(value, startIndex);
            }
            else
            {
                return System.BitConverter.ToInt64(value.Reverse().ToArray(), value.Length - sizeof(Int64) - startIndex);
            }
        }
        ///
        /// <summary>
        ///     Returns a single-precision floating point number converted from four bytes
        ///     at a specified position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A single-precision floating point number formed by four bytes beginning at
        ///     startIndex.
        ///</summary>
        public static float ToSingle(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToSingle(value, startIndex);
            }
            else
            {
                return System.BitConverter.ToSingle(value.Reverse().ToArray(), value.Length - sizeof(Single) - startIndex);
            }
        }
        ///
        /// <summary>
        ///     Converts the numeric value of each element of a specified array of bytes
        ///     to its equivalent hexadecimal string representation.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        /// Returns:
        ///     A System.String of hexadecimal pairs separated by hyphens, where each pair
        ///     represents the corresponding element in value; for example, "7F-2C-4A".
        ///</summary>
        public static string ToString(byte[] value)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToString(value);
            }
            else
            {
                return System.BitConverter.ToString(value.Reverse().ToArray());
            }
        }
        ///
        /// <summary>
        ///     Converts the numeric value of each element of a specified subarray of bytes
        ///     to its equivalent hexadecimal string representation.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A System.String of hexadecimal pairs separated by hyphens, where each pair
        ///     represents the corresponding element in a subarray of value; for example,
        ///     "7F-2C-4A".
        ///</summary>
        public static string ToString(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToString(value, startIndex);
            }
            else
            {
                return System.BitConverter.ToString(value.Reverse().ToArray(), startIndex);
            }
        }
        ///
        /// <summary>
        ///     Converts the numeric value of each element of a specified subarray of bytes
        ///     to its equivalent hexadecimal string representation.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        ///   length:
        ///     The number of array elements in value to convert.
        ///
        /// Returns:
        ///     A System.String of hexadecimal pairs separated by hyphens, where each pair
        ///     represents the corresponding element in a subarray of value; for example,
        ///     "7F-2C-4A".
        ///</summary>
        public static string ToString(byte[] value, int startIndex, int length)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToString(value, startIndex, length);
            }
            else
            {
                return System.BitConverter.ToString(value.Reverse().ToArray(), startIndex, length);
            }
        }
        ///
        /// <summary>
        ///     Returns a 16-bit unsigned integer converted from two bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     The array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 16-bit unsigned integer formed by two bytes beginning at startIndex.
        ///</summary>
        public static ushort ToUInt16(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToUInt16(value, startIndex);
            }
            else
            {
                return System.BitConverter.ToUInt16(value.Reverse().ToArray(), value.Length - sizeof(UInt16) - startIndex);
            }
        }
        ///
        /// <summary>
        ///     Returns a 32-bit unsigned integer converted from four bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 32-bit unsigned integer formed by four bytes beginning at startIndex.
        ///</summary>
        public static uint ToUInt32(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToUInt32(value, startIndex);
            }
            else
            {
                return System.BitConverter.ToUInt32(value.Reverse().ToArray(), value.Length - sizeof(UInt32) - startIndex);
            }
        }
        ///
        /// <summary>
        ///     Returns a 64-bit unsigned integer converted from eight bytes at a specified
        ///     position in a byte array.
        ///
        /// Parameters:
        ///   value:
        ///     An array of bytes.
        ///
        ///   startIndex:
        ///     The starting position within value.
        ///
        /// Returns:
        ///     A 64-bit unsigned integer formed by the eight bytes beginning at startIndex.
        ///</summary>
        public static ulong ToUInt64(byte[] value, int startIndex)
        {
            if (IsLittleEndian)
            {
                return System.BitConverter.ToUInt64(value, startIndex);
            }
            else
            {
                return System.BitConverter.ToUInt64(value.Reverse().ToArray(), value.Length - sizeof(UInt64) - startIndex);
            }
        }
    }
}