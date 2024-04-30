using KMP_Editor.Serial;
using static KMP_Editor.Serial.KMP;

namespace KMP_Editor.Control
{
    public class GOBJNode : Node
    {
        public _Section<_GOBJ> GOBJ { get; set; }

        public GOBJNode(KMP kmp)
        {
            GOBJ = kmp.GOBJ;
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            List<UInt16> uniqueIds = new List<UInt16>();
            foreach(_GOBJ obj in GOBJ.Entries)
            {
                if(!uniqueIds.Contains(obj.ID)) uniqueIds.Add(obj.ID);
            } 

            foreach(UInt16 id in uniqueIds) result.Add( new GOBJGroupNode(GOBJ, id) );
            return result;
        }

        public override void AddEntry()
        {
            // open window to determine which object

            // add new group if object is unused

            // add new instancee of object if group exists

            throw new NotImplementedException();
        }

        public override void RemoveEntry(int index)
        {
            UInt16 id = ((GOBJGroupNode)GetData()[index]).ID;
            for(int i = 0; i < GOBJ.Entries.Count; i++)
            {
                if (GOBJ.Entries[i].ID == id) GOBJ.RemoveEntry(i);
            }
        }

        public override void Populate(TreeNode node)
        {
            List<_ISectionEntry> data = GetData();

            node.Nodes.Clear();
            node.Tag = this;
            for(int i = 0; i < data.Count; i++)
            {
                TreeNode treeNode = new TreeNode(((GOBJGroupNode)data[i]).ID.ToString());
                treeNode.Tag = (GOBJGroupNode)data[i];
                treeNode.ImageIndex = 4;
                treeNode.SelectedImageIndex = 4;

                node.Nodes.Add(treeNode);
            }
        }
    }

    public class GOBJGroupNode : Node, _ISectionEntry
    {
        public UInt16 ID { get; set; }

        private List<_ISectionEntry> Objects;
        private _Section<_GOBJ> GOBJ;

        public GOBJGroupNode(_Section<_GOBJ> gobj, UInt16 id)
        {
            ID = id;
            Objects = new List<_ISectionEntry>();
            GOBJ = gobj;

            foreach (_GOBJ obj in GOBJ.Entries)
            {
                if (obj.ID == ID) Objects.Add(obj);
            }
        }

        public void Read(EndianReader reader)
        {
            throw new NotImplementedException();
        }

        public void Write(EndianWriter writer)
        {
            throw new NotImplementedException();
        }

        public override List<_ISectionEntry> GetData()
        {
            List<_ISectionEntry> result = new List<_ISectionEntry>();
            foreach (_GOBJ obj in GOBJ.Entries)
            {
                if (obj.ID == ID) result.Add(obj);
            }
            return result;
        }

        public override void AddEntry()
        {
            _GOBJ entry = (_GOBJ)GOBJ.AddEntry();
            entry.ID = ID;
        }

        public override void RemoveEntry(int index)
        {
            GOBJ.RemoveEntry(index);
        }
    }
}
