using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms
{
    public partial class Viewport2D : Panel
    {
        public Viewport2D() : base()
        {
            this.DoubleBuffered = true;
        }
    }
}
