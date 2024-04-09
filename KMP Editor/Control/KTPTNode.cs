using KMP_Editor.Serial;

namespace KMP_Editor.Control
{
    public class KTPTNode : INode
    {
        public KMP._Section<KMP._KTPT> KTPT { get; private set; }

        public KTPTNode(KMP kmp)
        {
            KTPT = kmp.KTPT;
        }

        public List<KMP._ISectionEntry> GetData()
        {
            List<KMP._ISectionEntry> result = new List<KMP._ISectionEntry>();
            for(int i = 0; i < KTPT.Length(); i++)
            {
                result.Add(KTPT.GetEntry(i));
            }
            return result;
        }

        public void AddEntry()
        {
            KTPT.AddEntry();
        }

        public void RemoveEntry(int index)
        {
            KTPT.RemoveEntry(index);
        }
    }
}
