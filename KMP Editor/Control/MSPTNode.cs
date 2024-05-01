using KartLib.Serial;
using static KartLib.Serial.KMP;

namespace KMP_Editor.Control
{
    public class MSPTNode : Node
    {
        public _Section<_MSPT> MSPT { get; set; }

        public MSPTNode(KMP kmp)
        {
            MSPT = kmp.MSPT;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            foreach(_MSPT mspt in MSPT.Entries)
                result.Add(mspt);

            return result;
        }

        public override string GetTitle(int index) { return "Point " + index; }

        public override void AddEntry() { MSPT.AddEntry(); }

        public override void RemoveEntry(int index) { MSPT.RemoveEntry(index); }
    }
}
