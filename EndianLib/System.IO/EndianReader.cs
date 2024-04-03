using System.Text;

namespace System.IO
{
    public sealed class EndianReader : IDisposable
    {
        private byte[] Buffer;
        private Stream m;
        private Endianness _Endian;
        private int _Position;
        private Stack<int> PositionStack = new Stack<int>();
        public bool Disposed { get; private set; }

        public Stream Stream
        {
            get
            {
                if (m == null)
                {
                    throw new NullReferenceException();
                }

                return m;
            }
        }

        public Endianness Endian
        {
            get
            {
                return _Endian;
            }
            set
            {
                _Endian = value;
            }
        }

        public int Position
        {
            get
            {
                if (m == null)
                {
                    throw new NullReferenceException();
                }

                return _Position;
            }
            set
            {
                if (m == null)
                {
                    throw new NullReferenceException();
                }

                if (value < 0)
                {
                    throw new ArgumentException("The position can't be negative");
                }

                m.Position = value;
                _Position = value;
            }
        }

        public long StreamLength
        {
            get
            {
                if (m == null)
                {
                    throw new NullReferenceException();
                }

                return m.Length;
            }
        }

        public EndianReader(Stream Stream, Endianness Endian)
        {
            if (Stream == null)
            {
                throw new NullReferenceException();
            }

            Disposed = false;
            _Position = 0;
            _Endian = Endian;
            m = Stream;
        }

        public EndianReader(byte[] Data, Endianness Endian)
        {
            if (Data.Length < 0)
            {
                throw new Exception("Array size cannot be less than or equal to 0");
            }

            Disposed = false;
            _Position = 0;
            _Endian = Endian;
            m = new MemoryStream(Data);
        }

        public void Close()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                GC.SuppressFinalize(this);
                if (m != null)
                {
                    m.Close();
                    m = null;
                }

                _Position = 0;
                Disposed = true;
            }
        }

        private void Try(int Length)
        {
            if (m == null)
            {
                throw new NullReferenceException();
            }

            if (_Position + Length > m.Length)
            {
                throw new EndOfStreamException();
            }
        }

        private void FillBuffer(int Count, int Stride)
        {
            Buffer = new byte[Count];
            m.Read(Buffer, 0, Count);
            _Position += Count;
            if (Endian == Endianness.LittleEndian)
            {
                for (int i = 0; i < Count; i += Stride)
                {
                    Array.Reverse((Array)Buffer, i, Stride);
                }
            }
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
            PositionStack.Push(Position);
        }

        public int PeekPosition()
        {
            return PositionStack.Peek();
        }

        public int PopPosition()
        {
            return PositionStack.Pop();
        }

        public void Align(int Alignment)
        {
            while (Position % Alignment != 0)
            {
                Position++;
            }
        }

        public byte ReadByte()
        {
            Try(1);
            FillBuffer(1, 1);
            return Buffer[0];
        }

        public byte[] ReadBytes(int Count)
        {
            Try(Count);
            FillBuffer(Count, 1);
            return Buffer;
        }

        public sbyte ReadSByte()
        {
            Try(1);
            FillBuffer(1, 1);
            return (sbyte)Buffer[0];
        }

        public sbyte[] ReadSBytes(int Count)
        {
            Try(Count);
            FillBuffer(Count, 1);
            sbyte[] array = new sbyte[Count];
            for (int i = 0; i < Count; i++)
            {
                array[i] = (sbyte)Buffer[i];
            }

            return array;
        }

        public ushort ReadUInt16()
        {
            Try(2);
            FillBuffer(2, 2);
            Array.Reverse((Array)Buffer);
            return BitConverter.ToUInt16(Buffer, 0);
        }

        public ushort[] ReadUInt16s(int Count)
        {
            Try(2 * Count);
            FillBuffer(2 * Count, 2);
            ushort[] array = new ushort[Count];
            for (int i = 0; i < Count; i++)
            {
                byte[] array2 = new byte[2];
                Array.Copy(Buffer, i * 2, array2, 0, 2);
                Array.Reverse((Array)array2);
                array[i] = BitConverter.ToUInt16(array2, 0);
            }

            return array;
        }

        public short ReadInt16()
        {
            Try(2);
            FillBuffer(2, 2);
            Array.Reverse((Array)Buffer);
            return BitConverter.ToInt16(Buffer, 0);
        }

        public short[] ReadInt16s(int Count)
        {
            Try(2 * Count);
            FillBuffer(2 * Count, 2);
            short[] array = new short[Count];
            for (int i = 0; i < Count; i++)
            {
                byte[] array2 = new byte[2];
                Array.Copy(Buffer, i * 2, array2, 0, 2);
                Array.Reverse((Array)array2);
                array[i] = BitConverter.ToInt16(array2, 0);
            }

            return array;
        }

        public uint ReadUInt24()
        {
            Try(3);
            FillBuffer(3, 3);
            return (uint)((Buffer[0] << 16) | (Buffer[1] << 8) | Buffer[2]);
        }

        public int ReadInt24()
        {
            Try(3);
            FillBuffer(3, 3);
            return (Buffer[0] << 16) | (Buffer[1] << 8) | Buffer[2];
        }

        public uint ReadUInt32()
        {
            Try(4);
            FillBuffer(4, 4);
            Array.Reverse((Array)Buffer);
            return BitConverter.ToUInt32(Buffer, 0);
        }

        public uint[] ReadUInt32s(int Count)
        {
            Try(4 * Count);
            FillBuffer(4 * Count, 4);
            uint[] array = new uint[Count];
            for (int i = 0; i < Count; i++)
            {
                byte[] array2 = new byte[4];
                Array.Copy(Buffer, i * 4, array2, 0, 4);
                Array.Reverse((Array)array2);
                array[i] = BitConverter.ToUInt32(array2, 0);
            }

            return array;
        }

        public int ReadInt32()
        {
            Try(4);
            FillBuffer(4, 4);
            Array.Reverse((Array)Buffer);
            return BitConverter.ToInt32(Buffer, 0);
        }

        public int[] ReadInt32s(int Count)
        {
            Try(4 * Count);
            FillBuffer(4 * Count, 4);
            int[] array = new int[Count];
            for (int i = 0; i < Count; i++)
            {
                byte[] array2 = new byte[4];
                Array.Copy(Buffer, i * 4, array2, 0, 4);
                Array.Reverse((Array)array2);
                array[i] = BitConverter.ToInt32(array2, 0);
            }

            return array;
        }

        public ulong ReadUInt64()
        {
            Try(8);
            FillBuffer(8, 8);
            Array.Reverse((Array)Buffer);
            return BitConverter.ToUInt64(Buffer, 0);
        }

        public ulong[] ReadUInt64s(int Count)
        {
            Try(8 * Count);
            FillBuffer(8 * Count, 8);
            ulong[] array = new ulong[Count];
            for (int i = 0; i < Count; i++)
            {
                byte[] array2 = new byte[8];
                Array.Copy(Buffer, i * 8, array2, 0, 8);
                Array.Reverse((Array)array2);
                array[i] = BitConverter.ToUInt64(array2, 0);
            }

            return array;
        }

        public long ReadInt64()
        {
            Try(8);
            FillBuffer(8, 8);
            Array.Reverse((Array)Buffer);
            return BitConverter.ToInt64(Buffer, 0);
        }

        public long[] ReadInt64s(int Count)
        {
            Try(8 * Count);
            FillBuffer(8 * Count, 8);
            long[] array = new long[Count];
            for (int i = 0; i < Count; i++)
            {
                byte[] array2 = new byte[8];
                Array.Copy(Buffer, i * 8, array2, 0, 8);
                Array.Reverse((Array)array2);
                array[i] = BitConverter.ToInt64(array2, 0);
            }

            return array;
        }

        public float ReadSingle()
        {
            Try(4);
            FillBuffer(4, 4);
            Array.Reverse((Array)Buffer);
            return BitConverter.ToSingle(Buffer, 0);
        }

        public float ReadFloat()
        {
            return ReadSingle();
        }

        public float[] ReadSingles(int Count)
        {
            Try(4 * Count);
            FillBuffer(4 * Count, 4);
            float[] array = new float[Count];
            for (int i = 0; i < Count; i++)
            {
                byte[] array2 = new byte[4];
                Array.Copy(Buffer, i * 4, array2, 0, 4);
                Array.Reverse((Array)array2);
                array[i] = BitConverter.ToSingle(array2, 0);
            }

            return array;
        }

        public float[] ReadFloats(int Count)
        {
            return ReadSingles(Count);
        }

        public double ReadDouble()
        {
            Try(8);
            FillBuffer(8, 8);
            Array.Reverse((Array)Buffer);
            return BitConverter.ToDouble(Buffer, 0);
        }

        public double[] ReadDouble(int Count)
        {
            Try(8);
            FillBuffer(8 * Count, 8);
            double[] array = new double[Count];
            for (int i = 0; i < Count; i++)
            {
                byte[] array2 = new byte[8];
                Array.Copy(Buffer, i * 8, array2, 0, 8);
                array[i] = BitConverter.ToSingle(array2, 0);
            }

            return array;
        }

        public string ReadString(int Count)
        {
            Try(Count);
            FillBuffer(Count, 1);
            return Encoding.Default.GetString(Buffer);
        }

        public string ReadString(int Count, Encoding En)
        {
            Try(Count * GetEncodingSize(En));
            FillBuffer(Count * GetEncodingSize(En), GetEncodingSize(En));
            return En.GetString(Buffer);
        }

        public string ReadStringNT()
        {
            List<byte> list = new List<byte>();
            do
            {
                FillBuffer(1, 1);
                list.Add(Buffer[0]);
            }
            while (Buffer[0] != 0);
            list.RemoveAt(list.Count - 1);
            return Encoding.Default.GetString(list.ToArray());
        }

        public string ReadStringNT(Encoding En)
        {
            int encodingSize = GetEncodingSize(En);
            List<byte> list = new List<byte>();
            while (true)
            {
                FillBuffer(encodingSize, encodingSize);
                bool flag = true;
                for (int i = 0; i < encodingSize; i++)
                {
                    if (Buffer[i] != 0)
                    {
                        flag = false;
                        break;
                    }
                }

                if (flag)
                {
                    break;
                }

                list.AddRange(Buffer);
            }

            return En.GetString(list.ToArray());
        }

        public char ReadChar()
        {
            Try(1);
            FillBuffer(1, 1);
            return Encoding.Default.GetChars(Buffer)[0];
        }

        public char ReadChar(Encoding En)
        {
            Try(GetEncodingSize(En));
            FillBuffer(GetEncodingSize(En), GetEncodingSize(En));
            return En.GetString(Buffer)[0];
        }

        public char[] ReadChars(int Count)
        {
            Try(Count);
            FillBuffer(Count, 1);
            return Encoding.Default.GetChars(Buffer);
        }

        public char[] ReadChars(int Count, Encoding En)
        {
            Try(Count * GetEncodingSize(En));
            FillBuffer(Count * GetEncodingSize(En), GetEncodingSize(En));
            return En.GetChars(Buffer);
        }

        public string ReadName(int TotalLength)
        {
            Try(TotalLength);
            FillBuffer(TotalLength, 1);
            return Encoding.Default.GetString(Buffer).Replace("\0", "");
        }

        public string ReadName(int TotalLength, Encoding En)
        {
            Try(TotalLength);
            FillBuffer(TotalLength, 1);
            return En.GetString(Buffer).Replace("\0", "");
        }

        public int ReadVLQ()
        {
            try
            {
                FillBuffer(1, 1);
                byte b = Buffer[0];
                if (((b >> 7) & 1) == 0)
                {
                    return b & 0x7F;
                }

                FillBuffer(1, 1);
                byte b2 = Buffer[0];
                if (((b2 >> 7) & 1) == 0)
                {
                    return ((b & 0x7F) << 7) | (b2 & 0x7F);
                }

                FillBuffer(1, 1);
                byte b3 = Buffer[0];
                if (((b3 >> 7) & 1) == 0)
                {
                    return ((b & 0x7F) << 14) | ((b2 & 0x7F) << 7) | (b3 & 0x7F);
                }

                FillBuffer(1, 1);
                byte b4 = Buffer[0];
                if (((b4 >> 7) & 1) == 0)
                {
                    return ((b & 0x7F) << 21) | ((b2 & 0x7F) << 14) | ((b3 & 0x7F) << 7) | (b4 & 0x7F);
                }
            }
            catch (IndexOutOfRangeException)
            {
                throw new EndOfStreamException();
            }

            throw new Exception("Error at " + Position.ToString("X4") + ". Invalid VLQ.");
        }

        public int ReadSROffset()
        {
            int num = ReadInt32();
            if (num != 0)
            {
                num += Position - 4;
            }

            return num;
        }

        public ushort ReadBOM()
        {
            switch (ReadUInt16())
            {
                case 65279:
                    return 65279;
                case 65534:
                    Endian = ((Endian == Endianness.BigEndian) ? Endianness.LittleEndian : Endianness.BigEndian);
                    return 65279;
                default:
                    throw new BOMException();
            }
        }

        public float ReadFx16()
        {
            return (float)ReadInt16() / 4096f;
        }

        public float ReadFx32()
        {
            return (float)ReadInt32() / 4096f;
        }
    }
}