using KartLib.Serial;
using static KartLib.Serial.KMP;

namespace KMP_Editor.Control
{
    public class POTINode : Node
    {
        public _Section<_POTI> POTI { get; set; }

        public POTINode(KMP kmp)
        {
            POTI = kmp.POTI;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            for(int i = 0; i < POTI.Entries.Count; i++)
                result.Add(POTI.Entries[i]); 

            return result;
        }

        public override string GetTitle(int index)
        {
            return "Route " + index;
        }

        public override void AddEntry()
        {
            POTI.AddEntry();
        }

        public override void RemoveEntry(int index)
        {
            POTI.RemoveEntry(index);
        }

        public override void Populate(TreeNode node)
        {
            List<_ISectionEntry> data = GetData();

            node.Nodes.Clear();
            node.Tag = this;
            for(int i = 0; i < data.Count; i++)
            {
                POTIPointNode point = new POTIPointNode((_POTI)data[i]);
                TreeNode treeNode = new TreeNode("Group " + i);
                treeNode.Tag = point;
                treeNode.ImageIndex = 5;
                treeNode.SelectedImageIndex = 5;
                node.Nodes.Add(treeNode);
            }
        }
    }

    public class POTIPointNode : Node
    {
        private _POTI POTI;

        public POTIPointNode(_POTI poti)
        {
            POTI = poti;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry> ();
            foreach(_POTI._Point point in POTI.Points)
                result.Add(point);

            return result;
        }

        public override string GetTitle(int index)
        {
            return "Point " + index;
        }

        public override void AddEntry()
        {
            POTI.PointCount++;
            POTI.Points.Add(new _POTI._Point());
        }

        public override void RemoveEntry(int index)
        {
            POTI.PointCount--;
            POTI.Points.RemoveAt(index);
        }
    }
}
