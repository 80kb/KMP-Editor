using DrawLib;
using DrawLib.Shapes;

namespace System.Windows.Forms
{
    public partial class Viewport2D : Panel
    {
        private Graphics? Graphics;
        private List<Shape> Shapes;

        public const float ZoomRate = 0.05f;
        public float Zoom = 1f;
        private Vector2f Offset;
        private Vector2f MouseDelta;

        private Shape? Dragging = null;
        private bool Panning        = false;

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
            CenterAt(shape.GetPosition());
        }

        public Vector2f GetOffset() { return this.Offset; }

        // Private methods
        
        private void CenterAt(Vector2f point)
        {
            this.Offset.X = (this.Width / 2) - point.X;
            this.Offset.Y = (this.Height / 2) - point.Y;
        }

        private void DrawShapes()
        {
            if(this.Graphics == null)
                return;

            foreach(Shape shape in this.Shapes)
            {
                Vector2f pos = shape.GetStaticPosition();
                shape.SetPosition((pos.X + Offset.X) * Zoom, (pos.Y + Offset.Y) * Zoom);
                shape.Draw(this.Graphics);
            }
        }

        private Shape? GetCurrentCollider(float MouseX, float MouseY)
        {
            foreach (Shape shape in this.Shapes)
            {
                if (shape.Colliding(MouseX, MouseY)) 
                    return shape;
            }

            return null;
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
            else if (e.Button == MouseButtons.Left)
            {
                Shape? collider = GetCurrentCollider(e.Location.X, e.Location.Y);
                if (collider != null && this.Dragging == null)
                    this.Dragging = collider;
            }
        }

        protected void OnMouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control))
            {
                this.Panning = false;
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (this.Dragging != null)
                    this.Dragging = null;
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
            else if (e.Button == MouseButtons.Left)
            {
                if (this.Dragging != null)
                {
                    this.Dragging.SetStaticPosition((e.Location.X / Zoom) - Offset.X, (e.Location.Y / Zoom) - Offset.Y);
                    this.Invalidate();
                }
            }

            Shape? collidee = GetCurrentCollider(e.Location.X, e.Location.Y);
            if (collidee != null)
                Cursor.Current = Cursors.SizeAll;
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
