using KMP_Editor.Serial;
using System.Diagnostics;

namespace KMP_Editor.Control
{
    public class ENPHNode : INode
    {
        public KMP._Section<KMP._ENPH> ENPH { get; private set; }
        public KMP._Section<KMP._ENPT> ENPT { get; private set; }

        public ENPHNode(KMP kmp)
        {
            ENPH = kmp.ENPH;
            ENPT = kmp.ENPT;
        }

        public List<KMP._ISectionEntry> GetData()
        {
            List<KMP._ISectionEntry> result = new List<KMP._ISectionEntry>();
            for(int i = 0; i < ENPH.Length(); i++)
            {
                result.Add(ENPH.GetEntry(i));
            }
            return result;
        }

        public void AddEntry()
        {
            KMP._ENPH lastEntry = (KMP._ENPH)ENPH.GetEntry(ENPH.Length() - 1);
            if(lastEntry.Start == byte.MaxValue)
                return;

            KMP._ENPH newEntry  = (KMP._ENPH)ENPH.AddEntry();
            newEntry.Start = (byte)(lastEntry.Start + lastEntry.Length);
        }

        public void RemoveEntry(int index)
        {
            KMP._ENPH node = (KMP._ENPH)ENPH.GetEntry(index);
            for(int i = node.Start; i < (node.Length + node.Start); i++)
            {
                ENPT.RemoveEntry(node.Start);
            }
            ENPH.RemoveEntry(index);

            byte position = node.Start;
            for(int i = index; i < ENPH.Length(); i++)
            {
                KMP._ENPH current = (KMP._ENPH)ENPH.GetEntry(i);
                current.Start = position;
                position += current.Length;
            }
        }
    }

    public class ENPHGroupNode : INode
    {
        public KMP._Section<KMP._ENPT> ENPT { get; private set; }
        public KMP._ENPH ENPH { get; private set; }

        public ENPHGroupNode(KMP kmp, int index)
        {
            ENPT = kmp.ENPT;
            ENPH = (KMP._ENPH)kmp.ENPH.GetEntry(index);
        }

        public List<KMP._ISectionEntry> GetData()
        {
            List<KMP._ISectionEntry> result = new List<KMP._ISectionEntry>();
            for(int i = ENPH.Start; i < (ENPH.Start + ENPH.Length); i++)
            {
                result.Add(ENPT.GetEntry(i));
            }
            return result;
        }

        public void AddEntry()
        {
            if(ENPH.Length + 1 == byte.MaxValue)
                return;

            ENPT.AddEntry(ENPH.Start + ENPH.Length);
            ENPH.Length++;
        }

        public void RemoveEntry(int index)
        {
            ENPT.RemoveEntry(ENPH.Start + index);
            ENPH.Length--;
        }
    }
}
