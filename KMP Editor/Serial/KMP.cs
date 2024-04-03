using System.Diagnostics;

namespace KMP_Editor.Serial;

public class KMP
{
    public class _Header
    {
        public UInt32   Magic;
        public UInt32   FileLength;
        public UInt16   SectionCount;
        public UInt16   HeaderLength;
        public UInt32   Version;
        public UInt32[] SectionOffsets;

        public _Header()
        {
            Magic           = 0x524B4D44; // RKMD
            FileLength      = 0x00;
            SectionCount    = 0x0F;
            HeaderLength    = 0x4C;
            Version         = 0x9D8;
            SectionOffsets  = new UInt32[SectionCount];
        }

        public _Header(EndianReader reader)
        {
            Magic           = reader.ReadUInt32();
            FileLength      = reader.ReadUInt32();
            SectionCount    = reader.ReadUInt16();
            HeaderLength    = reader.ReadUInt16();
            Version         = reader.ReadUInt32();
            SectionOffsets  = reader.ReadUInt32s(SectionCount);

            for(int i = 0; i < SectionCount; i++)
            {
                Debug.WriteLine(SectionOffsets[i]);
            }
            Debug.WriteLine("}");
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteUInt32(Magic);
            writer.WriteUInt32(FileLength);
            writer.WriteUInt16(SectionCount);
            writer.WriteUInt16(HeaderLength);
            writer.WriteUInt32(Version);
            writer.WriteUInt32s(SectionOffsets);
        }
    }

    public class _SectionHeader
    {
        public UInt32 SectionMagic;
        public UInt16 EntryCount;
        public UInt16 OptionalSetting;

        public _SectionHeader(Type type)
        {
            EntryCount          = 0;
            OptionalSetting     = 0;

            if     (type == typeof(_KTPT)) SectionMagic = 0x4B545054;
            else if(type == typeof(_ENPT)) SectionMagic = 0x454E5054;
            else if(type == typeof(_ENPH)) SectionMagic = 0x454E5048;
            else if(type == typeof(_ITPT)) SectionMagic = 0x49545054;
            else if(type == typeof(_ITPH)) SectionMagic = 0x49545048;
            else if(type == typeof(_CKPT)) SectionMagic = 0x434B5054;
            else if(type == typeof(_CKPH)) SectionMagic = 0x434B5048;
            else if(type == typeof(_GOBJ)) SectionMagic = 0x474F424A;
            else if(type == typeof(_POTI)) SectionMagic = 0x504F5449;
            else if(type == typeof(_AREA)) SectionMagic = 0x41524541;
            else if(type == typeof(_CAME)) SectionMagic = 0x43414D45;
            else if(type == typeof(_JGPT)) SectionMagic = 0x4A475054;
            else if(type == typeof(_CNPT)) SectionMagic = 0x434E5054;
            else if(type == typeof(_MSPT)) SectionMagic = 0x4D535054;
            else if(type == typeof(_STGI)) SectionMagic = 0x53544749;
        }

        public _SectionHeader(EndianReader reader)
        {
            SectionMagic    = reader.ReadUInt32();
            EntryCount      = reader.ReadUInt16();
            OptionalSetting = reader.ReadUInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteUInt32(SectionMagic);
            writer.WriteUInt16(EntryCount);
            writer.WriteUInt16(OptionalSetting);
        }
    }

    public class _Section<T> where T : _ISectionEntry, new()
    {
        public _SectionHeader SectionHeader;
        public List<T> Entries;

        public _Section()
        {
            SectionHeader   = new _SectionHeader(typeof(T));
            Entries         = new List<T>();
        }

        public _Section(EndianReader reader)
        {
            SectionHeader   = new _SectionHeader(reader);
            Entries         = new List<T>();

            for(int i = 0; i <  SectionHeader.EntryCount; i++)
            {
                T entry = new T();
                entry.Read(reader);
                Entries.Add(entry);
            }
        }

        public void Write(EndianWriter writer)
        {
            SectionHeader.Write(writer);

            for(int i = 0; i < SectionHeader.EntryCount; i++)
            {
                Entries[i].Write(writer);
            }
        }

        public int Length()
        {
            return SectionHeader.EntryCount;
        }

        public void AddEntry()
        {
            SectionHeader.EntryCount++;
            Entries.Add(new T());
        }

        public void RemoveEntry(int index)
        {
            SectionHeader.EntryCount--;
            Entries.RemoveAt(index);
        }

        public T GetEntry(int index)
        {
            return Entries[index];
        }
    }

    public interface _ISectionEntry
    {
        public void Read(EndianReader reader);
        public void Write(EndianWriter writer);
    }

    public class _KTPT : _ISectionEntry
    {
        public float[]  StartPosition;
        public float[]  StartRotation;
        public Int16    PlayerIndex;
        public UInt16   Padding;

        public _KTPT()
        {
            StartPosition   = new float[3];
            StartRotation   = new float[3];
            PlayerIndex     = -1;
            Padding         = 0;
        }

        public _KTPT(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            StartPosition   = reader.ReadFloats(3);
            StartRotation   = reader.ReadFloats(3);
            PlayerIndex     = reader.ReadInt16();
            Padding         = reader.ReadUInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteSingles(StartPosition);
            writer.WriteSingles(StartRotation);
            writer.WriteInt16(PlayerIndex);
            writer.WriteUInt16(Padding);
        }
    }

    public class _ENPT : _ISectionEntry
    {
        public float[]  Position;
        public float    Scale;
        public UInt16   Setting1;
        public Byte     Setting2;
        public Byte     Setting3;

        public _ENPT()
        {
            Position    = new float[3];
            Scale       = 1f;
            Setting1    = 0; 
            Setting2    = 0;
            Setting3    = 0;
        }

        public _ENPT(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Position    = reader.ReadFloats(3);
            Scale       = reader.ReadFloat();
            Setting1    = reader.ReadUInt16();
            Setting2    = reader.ReadByte();
            Setting3    = reader.ReadByte();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteSingles(Position);
            writer.WriteSingle(Scale);
            writer.WriteUInt16(Setting1);
            writer.WriteByte(Setting2);
            writer.WriteByte(Setting3);
        }
    }

    public class _ENPH : _ISectionEntry
    {
        public Byte     Start;
        public Byte     Length;
        public Byte[]   Previous;
        public Byte[]   Next;
        public UInt16   Padding;

        public _ENPH()
        {
            Start       = 0;
            Length      = 0;
            Previous    = new Byte[6] { 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            Next        = new Byte[6] { 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            Padding     = 0;
        }
        
        public _ENPH(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Start       = reader.ReadByte();
            Length      = reader.ReadByte();
            Previous    = reader.ReadBytes(6);
            Next        = reader.ReadBytes(6);
            Padding     = reader.ReadUInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteByte(Start);
            writer.WriteByte(Length);
            writer.WriteBytes(Previous);
            writer.WriteBytes(Next);
            writer.WriteUInt16(Padding);
        }
    }

    public class _ITPT : _ISectionEntry
    {
        public float[]  Position;
        public float    Scale;
        public UInt16   Setting1;
        public UInt16   Setting2;

        public _ITPT()
        {
            Position = new float[3];
            Scale    = 1f;
            Setting1 = 0;
            Setting2 = 0;
        }

        public _ITPT(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Position = reader.ReadFloats(3);
            Scale    = reader.ReadSingle();
            Setting1 = reader.ReadUInt16();
            Setting2 = reader.ReadUInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteSingles(Position);
            writer.WriteSingle(Scale);
            writer.WriteUInt16(Setting1);
            writer.WriteUInt16(Setting2);
        }
    }

    public class _ITPH : _ISectionEntry
    {
        public Byte     Start;
        public Byte     Length;
        public Byte[]   Previous;
        public Byte[]   Next;
        public UInt16   Padding;

        public _ITPH()
        {
            Start       = 0;
            Length      = 0;
            Previous    = new Byte[6] { 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            Next        = new Byte[6] { 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            Padding     = 0;
        }

        public _ITPH(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Start       = reader.ReadByte();
            Length      = reader.ReadByte();
            Previous    = reader.ReadBytes(6);
            Next        = reader.ReadBytes(6);
            Padding     = reader.ReadUInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteByte(Start);
            writer.WriteByte(Length);
            writer.WriteBytes(Previous);
            writer.WriteBytes(Next);
            writer.WriteUInt16(Padding);
        }
    }

    public class _CKPT : _ISectionEntry
    {
        public float[]  PositionL;
        public float[]  PositionR;
        public Byte     RespawnID;
        public SByte    Type;
        public Byte     Previous;
        public Byte     Next;

        public _CKPT()
        {
            PositionL   = new float[2];
            PositionR   = new float[2];
            RespawnID   = 0;
            Type        = 0x7F;
            Previous    = 0xFF;
            Next        = 0xFF;
        }

        public _CKPT(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            PositionL   = reader.ReadFloats(2);
            PositionR   = reader.ReadFloats(2);
            RespawnID   = reader.ReadByte();
            Type        = reader.ReadSByte();
            Previous    = reader.ReadByte();
            Next        = reader.ReadByte();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteSingles(PositionL);
            writer.WriteSingles(PositionR);
            writer.WriteByte(RespawnID);
            writer.WriteSByte(Type);
            writer.WriteByte(Previous);
            writer.WriteByte(Next);
        }
    }

    public class _CKPH : _ISectionEntry
    {
        public Byte     Start;
        public Byte     Length;
        public Byte[]   Previous;
        public Byte[]   Next;
        public UInt16   Padding;

        public _CKPH()
        {
            Start       = 0;
            Length      = 0;
            Previous    = new byte[6] { 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            Next        = new byte[6] { 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            Padding     = 0;
        }

        public _CKPH(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Start       = reader.ReadByte();
            Length      = reader.ReadByte();
            Previous    = reader.ReadBytes(6);
            Next        = reader.ReadBytes(6);
            Padding     = reader.ReadUInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteByte(Start);
            writer.WriteByte(Length);
            writer.WriteBytes(Previous);
            writer.WriteBytes(Next);
            writer.WriteUInt16(Padding);
        }
    }

    public class _GOBJ : _ISectionEntry
    {
        public UInt16   ID;
        public UInt16   Padding;
        public float[]  Position;
        public float[]  Rotation;
        public float[]  Scale;
        public UInt16   RouteID;
        public UInt16[] Settings;
        public UInt16   Flag;

        public _GOBJ()
        {
            ID          = 0;
            Padding     = 0;
            Position    = new float[3];
            Rotation    = new float[3];
            Scale       = new float[3] { 1f, 1f, 1f };
            RouteID     = 0xFFFF;
            Settings    = new ushort[8];
            Flag        = 0x003F;
        }

        public _GOBJ(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            ID          = reader.ReadUInt16();
            Padding     = reader.ReadUInt16();
            Position    = reader.ReadFloats(3);
            Rotation    = reader.ReadFloats(3);
            Scale       = reader.ReadFloats(3);
            RouteID     = reader.ReadUInt16();
            Settings    = reader.ReadUInt16s(8);
            Flag        = reader.ReadUInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteUInt16(ID);
            writer.WriteUInt16(Padding);
            writer.WriteSingles(Position);
            writer.WriteSingles(Rotation);
            writer.WriteSingles(Scale);
            writer.WriteUInt16(RouteID);
            writer.WriteUInt16s(Settings);
            writer.WriteUInt16(Flag);
        }
    }

    public class _POTI : _ISectionEntry
    {
        public class _Point
        {
            public float[]  Position;
            public UInt16   Setting1;
            public UInt16   Setting2;

            public _Point()
            {
                Position = new float[3];
                Setting1 = 0xFFFF;
                Setting2 = 0xFFFF;
            }

            public _Point(EndianReader reader)
            {
                Position = reader.ReadFloats(3);
                Setting1 = reader.ReadUInt16();
                Setting2 = reader.ReadUInt16();
            }

            public void Write(EndianWriter writer)
            {
                writer.WriteSingles(Position);
                writer.WriteUInt16(Setting1);
                writer.WriteUInt16(Setting2);
            }
        }

        public UInt16   PointCount;
        public Byte     Setting1;
        public Byte     Setting2;
        public _Point[] Points;

        public _POTI()
        {
            PointCount  = 0;
            Setting1    = 0;
            Setting2    = 0;
            Points      = new _Point[PointCount];
        }

        public _POTI(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            PointCount  = reader.ReadUInt16();
            Setting1    = reader.ReadByte();
            Setting2    = reader.ReadByte();
            Points      = new _Point[PointCount];

            for(int i = 0; i < PointCount; i++) 
            {
                Points[i] = new _Point(reader);
            }
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteUInt16(PointCount);
            writer.WriteByte(Setting1);
            writer.WriteByte(Setting2);

            for(int i = 0; i < PointCount; i++)
            {
                Points[i].Write(writer);
            }
        }
    }

    public class _AREA : _ISectionEntry
    {
        public Byte     Shape;          // 0 = Box, 1 = Cylinder
        public Byte     Type;           // https://wiki.tockdom.com/wiki/AREA_type
        public Byte     CameraID;
        public Byte     Priority;
        public float[]  Position;
        public float[]  Rotation;
        public float[]  Scale;
        public UInt16   Setting1;
        public UInt16   Setting2;
        public Byte     RouteID;
        public Byte     EnemyPointID;
        public UInt16   Padding;

        public _AREA()
        {
            Shape           = 0;
            Type            = 0;
            CameraID        = 0;
            Priority        = 0;
            Position        = new float[3];
            Rotation        = new float[3];
            Scale           = new float[3] { 1f, 1f, 1f };
            Setting1        = 0;
            Setting2        = 0;
            RouteID         = 0;
            EnemyPointID    = 0;
            Padding         = 0;
        }

        public _AREA(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Shape           = reader.ReadByte();
            Type            = reader.ReadByte();
            CameraID        = reader.ReadByte();
            Priority        = reader.ReadByte();
            Position        = reader.ReadFloats(3);
            Rotation        = reader.ReadFloats(3);
            Scale           = reader.ReadFloats(3);
            Setting1        = reader.ReadUInt16();
            Setting2        = reader.ReadUInt16();
            RouteID         = reader.ReadByte();
            EnemyPointID    = reader.ReadByte();
            Padding         = reader.ReadUInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteByte(Shape);
            writer.WriteByte(Type);
            writer.WriteByte(CameraID);
            writer.WriteByte(Priority);
            writer.WriteSingles(Position);
            writer.WriteSingles(Rotation);
            writer.WriteSingles(Scale);
            writer.WriteUInt16(Setting1);
            writer.WriteUInt16(Setting2);
            writer.WriteByte(RouteID);
            writer.WriteByte(EnemyPointID);
            writer.WriteUInt16(Padding);
        }
    }

    public class _CAME : _ISectionEntry
    {
        public Byte     Type;
        public Byte     Next;
        public Byte     Shake;
        public Byte     RouteID;
        public UInt16   MoveSpeed;
        public UInt16   ZoomSpeed;
        public UInt16   ViewSpeed;
        public Byte     Start;
        public Byte     Movie;
        public float[]  Position;
        public float[]  Rotation;
        public float    ZoomStart;
        public float    ZoomEnd;
        public float[]  ViewStart;
        public float[]  ViewEnd;
        public float    TimeActive;

        public _CAME()
        {
            Type        = 0;
            Next        = 0;
            Shake       = 0;
            RouteID     = 0;
            MoveSpeed   = 0;
            ZoomSpeed   = 0;
            ViewSpeed   = 0;
            Start       = 0;
            Movie       = 0;
            Position    = new float[3];
            Rotation    = new float[3];
            ZoomStart   = 0;
            ZoomEnd     = 0;
            ViewStart   = new float[3];
            ViewEnd     = new float[3];
            TimeActive  = 0;
        }

        public _CAME(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Type        = reader.ReadByte();
            Next        = reader.ReadByte();
            Shake       = reader.ReadByte();
            RouteID     = reader.ReadByte();
            MoveSpeed   = reader.ReadUInt16();
            ZoomSpeed   = reader.ReadUInt16();
            ViewSpeed   = reader.ReadUInt16();
            Start       = reader.ReadByte();
            Movie       = reader.ReadByte();
            Position    = reader.ReadFloats(3);
            Rotation    = reader.ReadFloats(3);
            ZoomStart   = reader.ReadFloat();
            ZoomEnd     = reader.ReadFloat();
            ViewStart   = reader.ReadFloats(3);
            ViewEnd     = reader.ReadFloats(3);
            TimeActive  = reader.ReadFloat();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteByte(Type);
            writer.WriteByte(Next);
            writer.WriteByte(Shake);
            writer.WriteByte(RouteID);
            writer.WriteUInt16(MoveSpeed);
            writer.WriteUInt16(ZoomSpeed);
            writer.WriteUInt16(ViewSpeed);
            writer.WriteByte(Start);
            writer.WriteByte(Movie);
            writer.WriteSingles(Position);
            writer.WriteSingles(Rotation);
            writer.WriteSingle(ZoomStart);
            writer.WriteSingle(ZoomEnd);
            writer.WriteSingles(ViewStart);
            writer.WriteSingles(ViewEnd);
            writer.WriteSingle(TimeActive);
        }
    }

    public class _JGPT : _ISectionEntry
    {
        public float[]  Position;
        public float[]  Rotation;
        public UInt16   ID;
        public Int16    Setting;

        public _JGPT()
        {
            Position    = new float[3];
            Rotation    = new float[3];
            ID          = 0;
            Setting     = 0;
        }

        public _JGPT(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Position    = reader.ReadFloats(3);
            Rotation    = reader.ReadFloats(3);
            ID          = reader.ReadUInt16();
            Setting     = reader.ReadInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteSingles(Position);
            writer.WriteSingles(Rotation);
            writer.WriteUInt16(ID);
            writer.WriteInt16(Setting);
        }
    }

    public class _CNPT : _ISectionEntry
    {
        public float[] Position;
        public float[] Rotation;
        public UInt16 ID;
        public Int16 Setting;

        public _CNPT()
        {
            Position    = new float[3];
            Rotation    = new float[3];
            ID          = 0;
            Setting     = 0;
        }

        public _CNPT(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Position    = reader.ReadFloats(3);
            Rotation    = reader.ReadFloats(3);
            ID          = reader.ReadUInt16();
            Setting     = reader.ReadInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteSingles(Position);
            writer.WriteSingles(Rotation);
            writer.WriteUInt16(ID);
            writer.WriteInt16(Setting);
        }
    }

    public class _MSPT : _ISectionEntry
    {
        public float[] Position;
        public float[] Rotation;
        public UInt16 ID;
        public Int16 Setting;

        public _MSPT()
        {
            Position = new float[3];
            Rotation = new float[3];
            ID = 0;
            Setting = 0;
        }

        public _MSPT(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            Position = reader.ReadFloats(3);
            Rotation = reader.ReadFloats(3);
            ID = reader.ReadUInt16();
            Setting = reader.ReadInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteSingles(Position);
            writer.WriteSingles(Rotation);
            writer.WriteUInt16(ID);
            writer.WriteInt16(Setting);
        }
    }

    public class _STGI : _ISectionEntry
    {
        public Byte     LapCount;
        public Byte     PolePosition;
        public Byte     NarrowMode;
        public Byte     LensFlare;
        public UInt32   FlareColor;
        public Byte     FlareTransparency;
        public Byte     Padding;
        public UInt16   Speed;              // for use with speed modifier code

        public _STGI()
        {
            LapCount            = 3;
            PolePosition        = 0;
            NarrowMode          = 0;
            LensFlare           = 0;
            FlareColor          = 0xFFFFFF;
            FlareTransparency   = 0x4B;
            Padding             = 0;
            Speed               = 1;
        }

        public _STGI(EndianReader reader)
        {
            Read(reader);
        }

        public void Read(EndianReader reader)
        {
            LapCount            = reader.ReadByte();
            PolePosition        = reader.ReadByte();
            NarrowMode          = reader.ReadByte();
            LensFlare           = reader.ReadByte();
            FlareColor          = reader.ReadUInt32();
            FlareTransparency   = reader.ReadByte();
            Padding             = reader.ReadByte();
            Speed               = reader.ReadUInt16();
        }

        public void Write(EndianWriter writer)
        {
            writer.WriteByte(LapCount);
            writer.WriteByte(PolePosition);
            writer.WriteByte(NarrowMode);
            writer.WriteByte(LensFlare);
            writer.WriteUInt32(FlareColor);
            writer.WriteByte(FlareTransparency);
            writer.WriteByte(Padding);
            writer.WriteUInt16(Speed);
        }
    }

    public _Header Header;
    public string  Filename;

    public _Section<_KTPT> KTPT;
    public _Section<_ENPT> ENPT;
    public _Section<_ENPH> ENPH;
    public _Section<_ITPT> ITPT;
    public _Section<_ITPH> ITPH;
    public _Section<_CKPT> CKPT;
    public _Section<_CKPH> CKPH;
    public _Section<_GOBJ> GOBJ;
    public _Section<_POTI> POTI;
    public _Section<_AREA> AREA;
    public _Section<_CAME> CAME;
    public _Section<_JGPT> JGPT;
    public _Section<_CNPT> CNPT;
    public _Section<_MSPT> MSPT;
    public _Section<_STGI> STGI;

    public KMP()
    {
        Header      = new _Header();
        Filename    = "Untitled.kmp";

        KTPT = new _Section<_KTPT> ();
        ENPT = new _Section<_ENPT> ();
        ENPH = new _Section<_ENPH> ();
        ITPT = new _Section<_ITPT> ();
        ITPH = new _Section<_ITPH> ();
        CKPT = new _Section<_CKPT> ();
        CKPH = new _Section<_CKPH> ();
        GOBJ = new _Section<_GOBJ> ();
        POTI = new _Section<_POTI> ();
        AREA = new _Section<_AREA> ();
        CAME = new _Section<_CAME> ();
        JGPT = new _Section<_JGPT> ();
        CNPT = new _Section<_CNPT> ();
        MSPT = new _Section<_MSPT> ();
        STGI = new _Section<_STGI> ();
    }

    public KMP(byte[] Data, string Filename)
    {
        this.Filename       = Filename;
        EndianReader reader = new EndianReader(Data, Endianness.BigEndian);
        Header              = new _Header(reader);

        Debug.WriteLine("Header.FileLength: " + Header.FileLength);

        KTPT = new _Section<_KTPT> (reader);
        ENPT = new _Section<_ENPT> (reader);
        ENPH = new _Section<_ENPH> (reader);
        ITPT = new _Section<_ITPT> (reader);
        ITPH = new _Section<_ITPH> (reader);
        CKPT = new _Section<_CKPT> (reader);
        CKPH = new _Section<_CKPH> (reader);
        GOBJ = new _Section<_GOBJ> (reader);
        POTI = new _Section<_POTI> (reader);
        AREA = new _Section<_AREA> (reader);
        CAME = new _Section<_CAME> (reader);
        JGPT = new _Section<_JGPT> (reader);
        CNPT = new _Section<_CNPT> (reader);
        MSPT = new _Section<_MSPT> (reader);
        STGI = new _Section<_STGI> (reader);
    }

    public byte[] Write()
    {
        MemoryStream stream = new MemoryStream();
        EndianWriter writer = new EndianWriter(stream, Endianness.BigEndian);

        List<uint> headerOffsets = new List<uint>();
        uint       fileLength;
        try
        {
            // Write header first to get position aligned
            // Subtract length of header (0x4C) from positions
            //   * Offsets start from end of header

            Header.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            KTPT.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);
            
            ENPT.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            ENPH.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            ITPT.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            ITPH.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            CKPT.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            CKPH.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            GOBJ.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            POTI.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            AREA.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            CAME.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            JGPT.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            CNPT.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            MSPT.Write(writer);
            headerOffsets.Add((uint)writer.Position - 0x4C);

            STGI.Write(writer);
            fileLength = (uint)writer.Position;

            // Update header data and write
            // Moves writer back to position 0 to overwrite placeholder header

            Header.FileLength = fileLength;
            Header.SectionOffsets = headerOffsets.ToArray();

            writer.Position = 0x00;
            Header.Write(writer);
        }
        finally
        {
            writer.Close();
            stream.Close();
        }

        return stream.ToArray();
    }
}
