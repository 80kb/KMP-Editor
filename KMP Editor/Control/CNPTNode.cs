using KMP_Editor.Serial;
using static KMP_Editor.Serial.KMP;

namespace KMP_Editor.Control
{
    public class CNPTNode : Node
    {
        public _Section<_CNPT> CNPT { get; set; }

        public CNPTNode(KMP kmp)
        {
            CNPT = kmp.CNPT;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            foreach(_CNPT cnpt in CNPT.Entries)
                result.Add(cnpt);

            return result;
        }

        public override string GetTitle(int index) { return "Cannon " + index; }

        public override void AddEntry() { CNPT.AddEntry(); }

        public override void RemoveEntry(int index) { CNPT.RemoveEntry(index); }
    }
}
