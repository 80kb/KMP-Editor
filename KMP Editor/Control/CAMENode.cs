using KartLib.Serial;
using static KartLib.Serial.KMP;

namespace KMP_Editor.Control
{
    public class CAMENode : Node
    {
        public _Section<_CAME> CAME { get; set; }

        public CAMENode(KMP kmp)
        {
            CAME = kmp.CAME;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            foreach(_CAME came in CAME.Entries)
                result.Add(came);

            return result;
        }

        public override string GetTitle(int index)
        {
            return "Camera " + index;
        }

        public override void AddEntry()
        {
            CAME.AddEntry();
        }

        public override void RemoveEntry(int index)
        {
            CAME.RemoveEntry(index);
        }
    }
}
