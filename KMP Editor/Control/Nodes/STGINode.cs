﻿using KartLib.Serial;
using static KartLib.Serial.KMP;

namespace KMP_Editor.Control.Nodes
{
    public class STGINode : Node
    {
        public _Section<_STGI> STGI { get; set; }

        public STGINode(KMP kmp)
        {
            STGI = kmp.STGI;
        }

        public override List<_ISectionEntry> GetData() { return new List<_ISectionEntry>() { STGI.Entries[0] }; }

        public override string GetTitle(int index) { return "Stage Info"; }

        public override void AddEntry() { return; }

        public override void RemoveEntry(int index) { return; }
    }
}
