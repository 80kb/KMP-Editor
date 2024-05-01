using KartLib.Serial;
using static KartLib.Serial.KMP;

namespace KMP_Editor.Control
{
    public class AREANode : Node
    {
        public _Section<_AREA> AREA { get; set; }

        public AREANode(KMP kmp)
        {
            AREA = kmp.AREA;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            foreach(_AREA area in AREA.Entries)
                result.Add(area);

            return result;
        }

        public override string GetTitle(int index)
        {
            return "Area " + index;
        }

        public override void AddEntry()
        {
            AREA.AddEntry();
        }

        public override void RemoveEntry(int index)
        {
            AREA.RemoveEntry(index);
        }
    }
}
