using KartLib.Serial;
using static KartLib.Serial.KMP;

namespace KMP_Editor.Control.Nodes
{
    public class KTPTNode : Node
    {
        public _Section<_KTPT> KTPT { get; private set; }

        public KTPTNode(KMP kmp)
        {
            KTPT = kmp.KTPT;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            for (int i = 0; i < KTPT.Length(); i++)
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
