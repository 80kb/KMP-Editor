using KMP_Editor.Serial;
using static KMP_Editor.Serial.KMP;

namespace KMP_Editor.Control
{
    public class ENPHNode : INode
    {
        public _Section<_ENPH> ENPH { get; private set; }
        public _Section<_ENPT> ENPT { get; private set; }

        public ENPHNode(KMP kmp)
        {
            ENPH = kmp.ENPH;
            ENPT = kmp.ENPT;
        }

        public List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            for(int i = 0; i < ENPH.Length(); i++)
            {
                result.Add(ENPH.GetEntry(i));
            }
            return result;
        }

        public void AddEntry()
        {
            _ENPH lastEntry = (_ENPH)ENPH.GetEntry(ENPH.Length() - 1);
            if(lastEntry.Start == byte.MaxValue)
                return;

            _ENPH newEntry  = (_ENPH)ENPH.AddEntry();
            newEntry.Start = (byte)(lastEntry.Start + lastEntry.Length);
        }

        public void RemoveEntry(int index)
        {
            _ENPH node = (_ENPH)ENPH.GetEntry(index);
            for(int i = node.Start; i < (node.Length + node.Start); i++)
            {
                ENPT.RemoveEntry(node.Start);
            }
            ENPH.RemoveEntry(index);

            byte position = node.Start;
            for(int i = index; i < ENPH.Length(); i++)
            {
                _ENPH current = (_ENPH)ENPH.GetEntry(i);
                current.Start = position;
                position += current.Length;
            }
        }
    }

    public class ENPHGroupNode : INode
    {
        public _Section<_ENPT> ENPT { get; private set; }
        public _ENPH ENPH { get; private set; }

        public ENPHGroupNode(KMP kmp, int index)
        {
            ENPT = kmp.ENPT;
            ENPH = (_ENPH)kmp.ENPH.GetEntry(index);
        }

        public List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
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
