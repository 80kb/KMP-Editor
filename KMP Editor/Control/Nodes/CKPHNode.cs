using KartLib.Serial;
using static KartLib.Serial.KMP;

namespace KMP_Editor.Control.Nodes
{
    public class CKPHNode : Node
    {
        public KMP KMP { get; set; }
        public _Section<_CKPH> CKPH { get; set; }
        public _Section<_CKPT> CKPT { get; set; }

        public CKPHNode(KMP kmp)
        {
            KMP = kmp;
            CKPH = kmp.CKPH;
            CKPT = kmp.CKPT;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            for (int i = 0; i < CKPH.Length(); i++)
            {
                result.Add(CKPH.GetEntry(i));
            }
            return result;
        }

        public override string GetTitle(int index)
        {
            return "Group " + index;
        }

        public override void AddEntry()
        {
            if (CKPH.Length() <= 0)
            {
                CKPH.AddEntry();
                return;
            }

            _CKPH lastEntry = (_CKPH)CKPH.GetEntry(CKPH.Length() - 1);
            if (lastEntry.Start == byte.MaxValue)
                return;

            _CKPH newEntry = (_CKPH)CKPH.AddEntry();
            newEntry.Start = (byte)(lastEntry.Start + lastEntry.Length);
        }

        public override void RemoveEntry(int index)
        {
            _CKPH node = (_CKPH)CKPH.GetEntry(index);
            for (int i = node.Start; i < node.Length + node.Start; i++)
            {
                CKPT.RemoveEntry(node.Start);
            }
            CKPH.RemoveEntry(index);

            byte position = node.Start;
            for (int i = index; i < CKPH.Length(); i++)
            {
                _CKPH current = (_CKPH)CKPH.GetEntry(i);
                current.Start = position;
                position += current.Length;
            }
        }

        public override void Populate(TreeNode node, Viewport2D viewport)
        {
            List<_ISectionEntry> ckphData = GetData();

            node.Nodes.Clear();
            node.Tag = this;
            for (int i = 0; i < ckphData.Count; i++)
            {
                CKPHGroupNode ckphGroupNode = new CKPHGroupNode(KMP, i);

                TreeNode treeNode = new TreeNode("Group " + i);
                treeNode.Tag = ckphGroupNode;
                treeNode.ImageIndex = 3;
                treeNode.SelectedImageIndex = 3;

                node.Nodes.Add(treeNode);
            }
        }
    }

    public class CKPHGroupNode : Node
    {
        public _Section<_CKPT> CKPT { get; private set; }
        public _CKPH CKPH { get; private set; }

        public CKPHGroupNode(KMP kmp, int index)
        {
            CKPT = kmp.CKPT;
            CKPH = (_CKPH)kmp.CKPH.GetEntry(index);
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            for (int i = CKPH.Start; i < CKPH.Start + CKPH.Length; i++)
            {
                result.Add(CKPT.GetEntry(i));
            }
            return result;
        }

        public override string GetTitle(int index)
        {
            return "Checkpoint " + index;
        }

        public override void AddEntry()
        {
            if (CKPH.Length + 1 == byte.MaxValue)
                return;

            CKPT.AddEntry(CKPH.Start + CKPH.Length);
            CKPH.Length++;
        }

        public override void RemoveEntry(int index)
        {
            CKPT.RemoveEntry(CKPH.Start + index);
            CKPH.Length--;
        }
    }
}
