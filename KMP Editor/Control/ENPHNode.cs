using KMP_Editor.Serial;

namespace KMP_Editor.Control
{
    public class ENPHNode
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

        public List<KMP._ISectionEntry> GetGroup(int index)
        {
            List<KMP._ISectionEntry> result = new List<KMP._ISectionEntry>();
            KMP._ENPH group = (KMP._ENPH)ENPH.GetEntry(index);
            for(int i = group.Start; i < (group.Start + group.Length); i++)
            {
                result.Add(ENPT.GetEntry(i));
            }
            return result;
        }
    }
}
