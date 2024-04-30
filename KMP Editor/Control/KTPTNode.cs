using KMP_Editor.Serial;

namespace KMP_Editor.Control
{
    public class KTPTNode : Node
    {
        public KMP._Section<KMP._KTPT> KTPT { get; private set; }

        public KTPTNode(KMP kmp)
        {
            KTPT = kmp.KTPT;
        }

        public override List<KMP._ISectionEntry> GetData()
        {
            List<KMP._ISectionEntry> result = new List<KMP._ISectionEntry>();
            for(int i = 0; i < KTPT.Length(); i++)
            {
                result.Add(KTPT.GetEntry(i));
            }
            return result;
        }

        public override string GetTitle(int index)
        {
            return "Start Point " + index;
        }

        public override void AddEntry()
        {
            KTPT.AddEntry();
        }

        public override void RemoveEntry(int index)
        {
            KTPT.RemoveEntry(index);
        }
    }
}
