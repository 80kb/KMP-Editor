using KMP_Editor.Serial;

namespace KMP_Editor.Control
{
    public abstract class Node
    {
        public abstract List<KMP._ISectionEntry> GetData();
        public abstract void AddEntry();
        public abstract void RemoveEntry(int index);
        public virtual void Populate(TreeNode node) { node.Tag = this; }
    }
}
