using KMP_Editor.Viewport;

namespace System.Windows.Forms
{
    public partial class Viewport2D : Panel
    {
        private Graphics?   Graphics;
        private List<Shape> Shapes;

        private const float ZoomRate = 0.05f;
        private float       Zoom = 1f;
        private bool        Panning  = false;
        private Vector2f    Offset;
        private Vector2f    MouseDelta;

        public Viewport2D() : base()
        {
            this.DoubleBuffered = true;
            this.Shapes         = new List<Shape>();
            this.Offset         = new Vector2f();
            this.MouseDelta     = new Vector2f();

            this.Paint      += this.OnPaint;
            this.MouseDown  += this.OnMouseDown;
            this.MouseUp    += this.OnMouseUp;
            this.MouseMove  += this.OnMouseMove;
            this.MouseWheel += this.OnMouseWheel;
        }

        // Public methods

        public void AddShape(Shape shape)
        {
            this.Shapes.Add(shape);
            Shape current = Shapes[Shapes.Count - 1];
            CenterAt(current.Vertices[0].X, current.Vertices[0].Y);
        }

        public void ClearShapes()
        {
            this.Shapes.Clear();
        }

        public Vector2f GetOffset() { return this.Offset; }

        public float GetZoom() { return this.Zoom; }

        // Private methods
        
        private void CenterAt(float x, float y)
        {
            this.Offset.X = (this.Width / 2) - x;
            this.Offset.Y = (this.Height / 2) - y;
        }

        private void DrawShapes()
        {
            if(this.Graphics == null)
                return;

            foreach(Shape shape in this.Shapes)
            {
                List<Vector2f> transPos = new List<Vector2f>();
                for(int i = 0; i < shape.Vertices.Count; i++)
                    transPos.Add((shape.Vertices[i] * Zoom) + Offset);
                shape.Draw(Graphics, transPos);
            }
        }

        // Event handlers

        protected void OnPaint(object? sender, PaintEventArgs e)
        {
            this.Graphics = e.Graphics;
            this.Graphics.PixelOffsetMode = Drawing.Drawing2D.PixelOffsetMode.HighSpeed;

            DrawShapes();
        }

        protected void OnMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control))
            {
                this.MouseDelta.X = e.Location.X - Offset.X;
                this.MouseDelta.Y = e.Location.Y - Offset.Y;

                if (!this.Panning)
                    this.Panning = true;
            }
        }

        protected void OnMouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control))
            {
                this.Panning = false;
            }
        }

        protected void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control))
            {
                if (this.Panning)
                {
                    this.Offset.X = e.Location.X - this.MouseDelta.X;
                    this.Offset.Y = e.Location.Y - this.MouseDelta.Y;

                    this.Invalidate();
                }
            }
        }

        protected void OnMouseWheel(object? sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                this.Zoom += ZoomRate;
            else if (e.Delta < 0 && this.Zoom > 0.01f)
                this.Zoom -= ZoomRate;

            this.Invalidate();
        }
    }
}
