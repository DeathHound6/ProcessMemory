﻿using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ProcessMemory.Types
{
    [DebuggerDisplay("{Value,nq}")]
    [StructLayout(LayoutKind.Explicit, Pack = 1, Size = 3)]
    public unsafe readonly ref struct UInt24
    {
        public const uint MinValue = unchecked(0x00000000); // 0
        public const uint MaxValue = unchecked(0x00FFFFFF); // 16777215

        [FieldOffset(0x00)]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly Span<byte> _data;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public uint Value => (uint)(_data[0] | _data[1] << 8 | _data[2] << 16);

        public UInt24(byte[] value, int startIndex = 0)
        {
            if (value.Length - startIndex != 3)
                throw new ArgumentOutOfRangeException();

            _data = new byte[3];
            _data[0] = value[startIndex + 2];
            _data[1] = value[startIndex + 1];
            _data[2] = value[startIndex];
        }

        public UInt24(uint value)
        {
            _data = new byte[3];
            _data[0] = (byte)(value & 0xFF);
            _data[1] = (byte)((value >> 8) & 0xFF);
            _data[2] = (byte)((value >> 16) & 0xFF);
        }

        public static implicit operator UInt24(uint v) => new UInt24(v);

        public static bool operator ==(UInt24 value1, UInt24 value2) => value1.Value == value2.Value;

        public static bool operator !=(UInt24 value1, UInt24 value2) => !(value1.Value == value2.Value);

        public bool Equals(UInt24 other) => this == other;

        public bool Equals(UInt24 x, UInt24 y) => x == y;

        public int GetHashCode(UInt24 obj) => obj.GetHashCode();

        public override bool Equals(object obj) => Value.Equals(obj);

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
        public string ToString(IFormatProvider? provider) => Value.ToString(provider);
        public string ToString(string? format) => Value.ToString(format);
        public string ToString(string? format, IFormatProvider? provider) => Value.ToString(format, provider);
        public ReadOnlySpan<byte> GetSpan() => _data;
    }
}
