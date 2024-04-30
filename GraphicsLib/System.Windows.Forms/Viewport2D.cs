using System.Drawing.Drawing2D;
using GraphicsLib;

namespace System.Windows.Forms
{
    public class Viewport2D : Panel
    {
        private Point zeros = new Point(0, 0);
        private float ZoomRate = 0.05f;

        private bool Panning = false;
        public IDrawable? Dragging = null;

        private Graphics? G;
        public List<IDrawable> Shapes { get; set; }
        public List<IDrawable> Background { get; set; }
        public Vector2f MouseDelta;
        public float Zoom = 1f;
        public Vector2f Offset;

        public Viewport2D() : base()
        {
            this.Shapes = new List<IDrawable>();
            this.Background = new List<IDrawable>();
            this.Offset = new Vector2f();
            this.DoubleBuffered = true;

            this.Paint += this.OnPaint;
            this.MouseDown += this.OnMouseDown;
            this.MouseHover += this.OnMouseHover;
            this.MouseWheel += this.OnMouseWheel;
            this.MouseUp += this.OnMouseUp;
            this.MouseLeave += this.OnMouseLeave;
            this.MouseMove += this.OnMouseMove;
        }

        //----- Public Methods -----

        public void SetShapes(List<IDrawable> shapes)
        {
            this.Shapes = shapes;
            this.Invalidate();
        }

        public void AddShape(IDrawable shape)
        {
            this.Shapes.Add(shape);
            CenterAt(shape.GetPosition());
        }

        public void ClearShapes()
        {
            this.Shapes.Clear();
            this.Invalidate();
        }

        public void AddBackground(IDrawable shape)
        {
            this.Background.Add(shape);
        }

        public void ClearBackground()
        {
            this.Background.Clear();
            this.Invalidate();
        }

        public void CenterAt(Vector2f point)
        {
            this.Offset.X = -point.X + (this.Width / 2);
            this.Offset.Y = -point.Y + (this.Height / 2);
        }

        public Vector2f GetOffset()
        {
            return this.Offset;
        }

        public float GetZoom()
        {
            return this.Zoom;
        }

        public IDrawable? CheckPointCollision()
        {
            Point difference = this.PointToScreen(zeros);

            foreach (IDrawable shape in this.Shapes)
            {
                if (shape.Colliding(Cursor.Position.X - difference.X, Cursor.Position.Y - difference.Y))
                    return shape;
            }

            return null;
        }

        //----- Helper Methods -----

        private void DrawShapes()
        {
            if (this.G == null)
                return;

            for (int i = 0; i < this.Shapes.Count; i++)
            {
                this.Shapes[i].Draw(this.G, this.Offset, this.Zoom);
            }
        }

        private void DrawBackground()
        {
            if (this.G == null)
                return;

            foreach (IDrawable shape in this.Background)
            {
                shape.Draw(this.G, this.Offset, this.Zoom);
            }
        }


        //----- UI Methods -----

        protected void OnPaint(object? sender, PaintEventArgs e)
        {
            this.G = e.Graphics;
            this.G.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            DrawBackground();
            DrawShapes();
        }

        protected void OnMouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control))
            {
                this.MouseDelta.X = Cursor.Position.X - this.Offset.X;
                this.MouseDelta.Y = Cursor.Position.Y - this.Offset.Y;

                if (!this.Panning)
                    this.Panning = true;
            }
            else if (e.Button == MouseButtons.Left)
            {
                IDrawable? collidee = CheckPointCollision();
                if (collidee != null && this.Dragging == null)
                    this.Dragging = CheckPointCollision();
            }
        }

        protected void OnMouseHover(object? sender, EventArgs e)
        {
            this.Focus();
        }

        protected void OnMouseWheel(object? sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
                this.Zoom += ZoomRate;
            else if (e.Delta < 0 && this.Zoom > 0.01f)
                this.Zoom -= ZoomRate;

            this.Invalidate();
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

        protected void OnMouseLeave(object? sender, EventArgs e)
        {
            if (this.Panning)
                this.Panning = false;
        }

        protected void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle || (e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Control))
            {
                if (this.Panning)
                {
                    this.Offset.X = Cursor.Position.X - this.MouseDelta.X;
                    this.Offset.Y = Cursor.Position.Y - this.MouseDelta.Y;

                    this.Invalidate();
                }
            }
            else if (e.Button == MouseButtons.Left)
            {
                if (this.Dragging != null)
                {
                    Point difference = this.PointToScreen(zeros);
                    this.Dragging.SetPosition(Cursor.Position.X - difference.X, Cursor.Position.Y - difference.Y, this.Offset, this.Zoom);

                    this.Invalidate();
                }
            }

            IDrawable? collidee = CheckPointCollision();
            if (collidee != null)
                Cursor.Current = Cursors.SizeAll;
        }
    }
}
