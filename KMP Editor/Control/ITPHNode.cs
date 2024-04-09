using KMP_Editor.Serial;
using static KMP_Editor.Serial.KMP;

namespace KMP_Editor.Control
{
    public class ITPHNode : INode
    {
        public _Section<_ITPH> ITPH;
        public _Section<_ITPT> ITPT;

        public ITPHNode(KMP kmp)
        {
            ITPH = kmp.ITPH;
            ITPT = kmp.ITPT;
        }

        public List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            for (int i = 0; i < ITPH.Length(); i++)
            {
                result.Add(ITPH.GetEntry(i));
            }
            return result;
        }

        public void AddEntry()
        {
            if (ITPH.Length() <= 0)
            {
                ITPH.AddEntry();
                return;
            }

            _ITPH lastEntry = (_ITPH)ITPH.GetEntry(ITPH.Length() - 1);
            if (lastEntry.Start == byte.MaxValue)
                return;

            _ITPH newEntry = (_ITPH)ITPH.AddEntry();
            newEntry.Start = (byte)(lastEntry.Start + lastEntry.Length);
        }

        public void RemoveEntry(int index)
        {
            _ITPH node = (_ITPH)ITPH.GetEntry(index);
            for (int i = node.Start; i < (node.Length + node.Start); i++)
            {
                ITPT.RemoveEntry(node.Start);
            }
            ITPH.RemoveEntry(index);

            byte position = node.Start;
            for (int i = index; i < ITPH.Length(); i++)
            {
                _ITPH current = (_ITPH)ITPH.GetEntry(i);
                current.Start = position;
                position += current.Length;
            }
        }
    }

    public class ITPHGroupNode : INode
    {
        public _Section<_ITPT> ITPT { get; private set; }
        public _ITPH ITPH { get; private set; }

        public ITPHGroupNode(KMP kmp, int index)
        {
            ITPT = kmp.ITPT;
            ITPH = (_ITPH)kmp.ITPH.GetEntry(index);
        }

        public List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            for (int i = ITPH.Start; i < (ITPH.Start + ITPH.Length); i++)
            {
                result.Add(ITPT.GetEntry(i));
            }
            return result;
        }

        public void AddEntry()
        {
            if (ITPH.Length + 1 == byte.MaxValue)
                return;

            ITPT.AddEntry(ITPH.Start + ITPH.Length);
            ITPH.Length++;
        }

        public void RemoveEntry(int index)
        {
            ITPT.RemoveEntry(ITPH.Start + index);
            ITPH.Length--;
        }
    }
}
