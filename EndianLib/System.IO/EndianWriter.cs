using System.Text;

namespace System.IO;

public sealed class EndianWriter : IDisposable
{
    private Stack<int> PositionStack = new Stack<int>();

    private byte[] Buffer { get; set; }

    private Stream m { get; set; }

    private Endianness _Endian { get; set; }

    private long _Position { get; set; }

    public bool Disposed { get; private set; }

    public Stream Stream
    {
        get
        {
            if (this.m == null)
            {
                throw new NullReferenceException();
            }
            return this.m;
        }
    }

    public Endianness Endian
    {
        get
        {
            return this._Endian;
        }
        set
        {
            this._Endian = value;
        }
    }

    public int Position
    {
        get
        {
            if (this.m == null)
            {
                throw new NullReferenceException();
            }
            return (int)this._Position;
        }
        set
        {
            if (this.m == null)
            {
                throw new NullReferenceException();
            }
            if (value < 0)
            {
                throw new Exception("The position can't be negative");
            }
            this.m.Position = value;
            this._Position = value;
        }
    }

    public long StreamLength
    {
        get
        {
            if (this.m == null)
            {
                throw new NullReferenceException();
            }
            return this.m.Length;
        }
    }

    public EndianWriter(Stream Stream, Endianness Endian)
    {
        if (Stream == null)
        {
            throw new NullReferenceException();
        }
        this.Disposed = false;
        this._Position = 0L;
        this.m = Stream;
        this._Endian = Endian;
    }

    public void Close()
    {
        this.Dispose();
    }

    public void Dispose()
    {
        if (!this.Disposed)
        {
            GC.SuppressFinalize(this);
            if (this.m != null)
            {
                this.m.Close();
                this.m = null;
            }
            this._Position = 0L;
            this.Disposed = true;
        }
    }

    private void CreateBuffer(int Size)
    {
        this.Buffer = new byte[Size];
    }

    private void WriteBuffer(int Count, int Stride)
    {
        this._Position += Count;
        if (this._Endian == Endianness.LittleEndian)
        {
            for (int i = 0; i < Count; i += Stride)
            {
                Array.Reverse(this.Buffer, i, Stride);
            }
        }
        this.m.Write(this.Buffer, 0, Count);
    }

    private static int GetEncodingSize(Encoding En)
    {
        if (En == Encoding.Unicode || En == Encoding.BigEndianUnicode)
        {
            return 2;
        }
        if (En == Encoding.UTF32)
        {
            return 4;
        }
        return 1;
    }

    public void PushPosition()
    {
        this.PositionStack.Push(this.Position);
    }

    public int PeekPosition()
    {
        return this.PositionStack.Peek();
    }

    public int PopPosition()
    {
        return this.PositionStack.Pop();
    }

    public void Align(int Alignment)
    {
        while (this.Position % Alignment != 0)
        {
            this.Position++;
        }
    }

    public void HardAlign(int Alignment)
    {
        int i;
        for (i = this.Position; i % Alignment != 0; i++)
        {
        }
        this.WriteBytes(new byte[i - this.Position]);
    }

    public void WriteByte(byte Data)
    {
        this.CreateBuffer(1);
        this.Buffer[0] = Data;
        this.WriteBuffer(1, 1);
    }

    public void WriteBytes(byte[] Data)
    {
        this.CreateBuffer(Data.Length);
        this.Buffer = Data;
        this.WriteBuffer(Data.Length, 1);
    }

    public void WriteSByte(sbyte Data)
    {
        this.CreateBuffer(1);
        this.Buffer[0] = (byte)Data;
        this.WriteBuffer(1, 1);
    }

    public void WriteSBytes(sbyte[] Data)
    {
        this.CreateBuffer(Data.Length);
        for (int i = 0; i < Data.Length; i++)
        {
            this.Buffer[i] = (byte)Data[i];
        }
        this.WriteBuffer(Data.Length, 1);
    }

    public void WriteUInt16(ushort Data)
    {
        this.CreateBuffer(2);
        byte[] bytes = BitConverter.GetBytes(Data);
        Array.Reverse(bytes);
        this.Buffer = bytes;
        this.WriteBuffer(2, 2);
    }

    public void WriteUInt16s(ushort[] Data)
    {
        this.CreateBuffer(2 * Data.Length);
        for (int i = 0; i < Data.Length; i++)
        {
            byte[] bytes = BitConverter.GetBytes(Data[i]);
            Array.Reverse(bytes);
            Array.Copy(bytes, 0, this.Buffer, i * 2, 2);
        }
        this.WriteBuffer(2 * Data.Length, 2);
    }

    public void WriteUInt16s(IEnumerable<ushort> Data)
    {
        this.WriteUInt16s(Data.ToArray());
    }

    public void WriteInt16(short Data)
    {
        this.CreateBuffer(2);
        byte[] bytes = BitConverter.GetBytes(Data);
        Array.Reverse(bytes);
        this.Buffer = bytes;
        this.WriteBuffer(2, 2);
    }

    public void WriteInt16s(short[] Data)
    {
        this.CreateBuffer(2 * Data.Length);
        for (int i = 0; i < Data.Length; i++)
        {
            byte[] bytes = BitConverter.GetBytes(Data[i]);
            Array.Reverse(bytes);
            Array.Copy(bytes, 0, this.Buffer, i * 2, 2);
        }
        this.WriteBuffer(2 * Data.Length, 2);
    }

    public void WriteInt16s(IEnumerable<short> Data)
    {
        this.WriteInt16s(Data.ToArray());
    }

    public void WriteUInt24(uint Data)
    {
        this.CreateBuffer(3);
        byte[] buffer = new byte[3]
        {
            (byte)((Data >> 16) & 0xFFu),
            (byte)((Data >> 8) & 0xFFu),
            (byte)(Data & 0xFFu)
        };
        this.Buffer = buffer;
        this.WriteBuffer(3, 3);
    }

    public void WriteInt24(int Data)
    {
        this.CreateBuffer(3);
        byte[] buffer = new byte[3]
        {
            (byte)((uint)(Data >> 16) & 0xFFu),
            (byte)((uint)(Data >> 8) & 0xFFu),
            (byte)((uint)Data & 0xFFu)
        };
        this.Buffer = buffer;
        this.WriteBuffer(3, 3);
    }

    public void WriteUInt32(uint Data)
    {
        this.CreateBuffer(4);
        byte[] bytes = BitConverter.GetBytes(Data);
        Array.Reverse(bytes);
        this.Buffer = bytes;
        this.WriteBuffer(4, 4);
    }

    public void WriteUInt32s(uint[] Data)
    {
        this.CreateBuffer(4 * Data.Length);
        for (int i = 0; i < Data.Length; i++)
        {
            byte[] bytes = BitConverter.GetBytes(Data[i]);
            Array.Reverse(bytes);
            Array.Copy(bytes, 0, this.Buffer, i * 4, 4);
        }
        this.WriteBuffer(4 * Data.Length, 4);
    }

    public void WriteUInt32s(IEnumerable<uint> Data)
    {
        this.WriteUInt32s(Data.ToArray());
    }

    public void WriteInt32(int Data)
    {
        this.CreateBuffer(4);
        byte[] bytes = BitConverter.GetBytes(Data);
        Array.Reverse(bytes);
        this.Buffer = bytes;
        this.WriteBuffer(4, 4);
    }

    public void WriteInt32s(int[] Data)
    {
        this.CreateBuffer(4 * Data.Length);
        for (int i = 0; i < Data.Length; i++)
        {
            byte[] bytes = BitConverter.GetBytes(Data[i]);
            Array.Reverse(bytes);
            Array.Copy(bytes, 0, this.Buffer, i * 4, 4);
        }
        this.WriteBuffer(4 * Data.Length, 4);
    }

    public void WriteInt32s(IEnumerable<int> Data)
    {
        this.WriteInt32s(Data.ToArray());
    }

    public void WriteUInt64(ulong Data)
    {
        this.CreateBuffer(8);
        byte[] bytes = BitConverter.GetBytes(Data);
        Array.Reverse(bytes);
        this.Buffer = bytes;
        this.WriteBuffer(8, 8);
    }

    public void WriteUInt64s(ulong[] Data)
    {
        this.CreateBuffer(8 * Data.Length);
        for (int i = 0; i < Data.Length; i++)
        {
            byte[] bytes = BitConverter.GetBytes(Data[i]);
            Array.Reverse(bytes);
            Array.Copy(bytes, 0, this.Buffer, i * 8, 8);
        }
        this.WriteBuffer(8 * Data.Length, 8);
    }

    public void WriteUInt64s(IEnumerable<ulong> Data)
    {
        this.WriteUInt64s(Data.ToArray());
    }

    public void WriteInt64(long Data)
    {
        this.CreateBuffer(8);
        byte[] bytes = BitConverter.GetBytes(Data);
        Array.Reverse(bytes);
        this.Buffer = bytes;
        this.WriteBuffer(8, 8);
    }

    public void WriteInt64s(long[] Data)
    {
        this.CreateBuffer(8 * Data.Length);
        for (int i = 0; i < Data.Length; i++)
        {
            byte[] bytes = BitConverter.GetBytes(Data[i]);
            Array.Reverse(bytes);
            Array.Copy(bytes, 0, this.Buffer, i * 8, 8);
        }
        this.WriteBuffer(8 * Data.Length, 8);
    }

    public void WriteInt64s(IEnumerable<long> Data)
    {
        this.WriteInt64s(Data.ToArray());
    }

    public void WriteSingle(float Data)
    {
        this.CreateBuffer(4);
        byte[] bytes = BitConverter.GetBytes(Data);
        Array.Reverse(bytes);
        this.Buffer = bytes;
        this.WriteBuffer(4, 4);
    }

    public void WriteSingles(float[] Data)
    {
        this.CreateBuffer(4 * Data.Length);
        for (int i = 0; i < Data.Length; i++)
        {
            byte[] bytes = BitConverter.GetBytes(Data[i]);
            Array.Reverse(bytes);
            Array.Copy(bytes, 0, this.Buffer, i * 4, 4);
        }
        this.WriteBuffer(4 * Data.Length, 4);
    }

    public void WriteSingles(IEnumerable<float> Data)
    {
        this.WriteSingles(Data.ToArray());
    }

    public void WriteDouble(double Data)
    {
        this.CreateBuffer(8);
        byte[] bytes = BitConverter.GetBytes(Data);
        Array.Reverse(bytes);
        this.Buffer = bytes;
        this.WriteBuffer(8, 8);
    }

    public void WriteDoubles(double[] Data)
    {
        this.CreateBuffer(8 * Data.Length);
        for (int i = 0; i < Data.Length; i++)
        {
            byte[] bytes = BitConverter.GetBytes(Data[i]);
            Array.Reverse(bytes);
            Array.Copy(bytes, 0, this.Buffer, i * 8, 8);
        }
        this.WriteBuffer(8 * Data.Length, 8);
    }

    public void WriteDoubles(IEnumerable<double> Data)
    {
        this.WriteDoubles(Data.ToArray());
    }

    public void WriteString(string Data)
    {
        this.CreateBuffer(Data.Length * EndianWriter.GetEncodingSize(Encoding.Default));
        this.Buffer = Encoding.Default.GetBytes(Data.ToCharArray());
        this.WriteBuffer(Data.Length, EndianWriter.GetEncodingSize(Encoding.Default));
    }

    public void WriteString(string Data, Encoding En)
    {
        this.CreateBuffer(Data.Length * EndianWriter.GetEncodingSize(En));
        this.Buffer = En.GetBytes(Data.ToCharArray());
        this.WriteBuffer(Data.Length * EndianWriter.GetEncodingSize(En), EndianWriter.GetEncodingSize(En));
    }

    public void WriteString(string Data, int TotalSize, Encoding En)
    {
        Data = Data.Replace("\0", "");
        if (Data.Length == 0)
        {
            this.CreateBuffer(TotalSize);
            this.WriteBuffer(TotalSize, 1);
            return;
        }
        int encodingSize = EndianWriter.GetEncodingSize(En);
        if (TotalSize < encodingSize * Data.Length)
        {
            throw new Exception("Error while writing. Invalid parameter (TotalSize) in method WriteString().");
        }
        this.CreateBuffer(TotalSize);
        byte[] bytes = En.GetBytes(Data.ToCharArray());
        Array.Copy(bytes, this.Buffer, bytes.Length);
        this.WriteBuffer(TotalSize, encodingSize);
    }

    public void WriteStringNT(string Data)
    {
        this.WriteString(Data);
        this.Buffer = new byte[1];
        this.WriteBuffer(1, 1);
    }

    public void WriteStringNT(string Data, Encoding En)
    {
        this.WriteString(Data, En);
        this.Buffer = new byte[EndianWriter.GetEncodingSize(En)];
        this.WriteBuffer(EndianWriter.GetEncodingSize(En), 1);
    }

    public void WriteStrings(string[] Data)
    {
        this.WriteStrings(Data, Encoding.ASCII);
    }

    public void WriteStrings(IEnumerable<string> Data)
    {
        this.WriteStrings(Data.ToArray(), Encoding.ASCII);
    }

    public void WriteStrings(string[] Data, Encoding En)
    {
        for (int i = 0; i < Data.Length; i++)
        {
            this.WriteString(Data[i], En);
        }
    }

    public void WriteStrings(IEnumerable<string> Data, Encoding En)
    {
        this.WriteStrings(Data.ToArray(), En);
    }

    public void WriteChar(char Data)
    {
        this.CreateBuffer(EndianWriter.GetEncodingSize(Encoding.Default));
        this.Buffer = Encoding.Default.GetBytes(new char[1] { Data });
        this.WriteBuffer(EndianWriter.GetEncodingSize(Encoding.Default), EndianWriter.GetEncodingSize(Encoding.Default));
    }

    public void WriteChar(char Data, Encoding En)
    {
        this.CreateBuffer(EndianWriter.GetEncodingSize(En));
        this.Buffer = Encoding.Default.GetBytes(new char[1] { Data });
        this.WriteBuffer(EndianWriter.GetEncodingSize(En), EndianWriter.GetEncodingSize(En));
    }

    public void WriteChars(char[] Data)
    {
        this.CreateBuffer(Data.Length * EndianWriter.GetEncodingSize(Encoding.Default));
        this.Buffer = Encoding.Default.GetBytes(Data);
        this.WriteBuffer(Data.Length, EndianWriter.GetEncodingSize(Encoding.Default));
    }

    public void WriteChars(char[] Data, Encoding En)
    {
        this.CreateBuffer(Data.Length * EndianWriter.GetEncodingSize(En));
        this.Buffer = En.GetBytes(Data);
        this.WriteBuffer(Data.Length * EndianWriter.GetEncodingSize(En), EndianWriter.GetEncodingSize(En));
    }

    public void WriteChars(IEnumerable<char> Data)
    {
        this.WriteChars(Data.ToArray());
    }

    public void WriteChars(IEnumerable<char> Data, Encoding En)
    {
        this.WriteChars(Data.ToArray(), En);
    }

    public void WriteBOM()
    {
        this.WriteUInt16(65279);
    }

    public void WriteVLQ(int Data)
    {
        if (Data < 0 || Data > 268435455)
        {
            throw new Exception("An error occured.");
        }
        if (Data <= 127)
        {
            this.CreateBuffer(1);
            this.Buffer = new byte[1] { (byte)((uint)Data & 0x7Fu) };
        }
        else if (Data <= 16383)
        {
            this.CreateBuffer(2);
            this.Buffer = new byte[2]
            {
                (byte)((uint)(Data >> 7) & 0x7Fu),
                (byte)(((uint)Data & 0x7Fu) | 0x100u)
            };
        }
        else if (Data <= 2097151)
        {
            this.CreateBuffer(3);
            this.Buffer = new byte[3]
            {
                (byte)((uint)(Data >> 14) & 0x7Fu),
                (byte)(((uint)(Data >> 7) & 0x7Fu) | 0x80u),
                (byte)(((uint)Data & 0x7Fu) | 0x80u)
            };
        }
        else if (Data <= 268435455)
        {
            this.CreateBuffer(4);
            this.Buffer = new byte[4]
            {
                (byte)((uint)(Data >> 21) & 0x7Fu),
                (byte)(((uint)(Data >> 14) & 0x7Fu) | 0x80u),
                (byte)(((uint)(Data >> 7) & 0x7Fu) | 0x80u),
                (byte)(((uint)Data & 0x7Fu) | 0x80u)
            };
        }
    }

    public void WriteSROffset(int Data)
    {
        this.WriteInt32((Data == 0) ? Data : (Data - this.Position));
    }

    public void WriteSROffset(uint Data)
    {
        this.WriteUInt32((uint)((Data == 0) ? Data : (Data - this.Position)));
    }
}
