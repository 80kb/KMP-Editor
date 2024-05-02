using DrawLib;
using static KartLib.Serial.KMP;

namespace KMP_Editor.Control
{
    public abstract class Node
    {
        public abstract List<_ISectionEntry> GetData();

        public abstract string GetTitle(int index);
        
        public abstract void AddEntry();

        public abstract void RemoveEntry(int index);

        public virtual void Populate(TreeNode node, Viewport2D viewport)
        {
            node.Tag = this;
        }
    }
}
