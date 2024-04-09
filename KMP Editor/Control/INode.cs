using KMP_Editor.Serial;

namespace KMP_Editor.Control
{
    public interface INode
    {
        public List<KMP._ISectionEntry> GetData();
        public void AddEntry();
        public void RemoveEntry(int index);
    }
}
