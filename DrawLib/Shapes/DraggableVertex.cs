namespace DrawLib.Shapes
{
    public class DraggableVertex : DraggableShape
    {
        public const int _size = 8;

        public DraggableVertex(Vector2f position, Viewport2D viewport)
            : base(new List<Vector2f> { position }, Color.Black, viewport) { }

        public override void Draw(Graphics g, List<Vector2f> pos)
        {
            g.FillEllipse(new SolidBrush(FillColor), pos[0].X, pos[0].Y, _size, _size);
        }

        public override bool Colliding(float x, float y)
        {
            Vector2f pos = Vertices[0];
            if (x >= pos.X && x <= pos.X + _size / _viewport.GetZoom())
            {
                if (y >= pos.Y && y <= pos.Y + _size / _viewport.GetZoom())
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnMouseDown(object? sender, MouseEventArgs e)
        {
            float x = (e.Location.X - _viewport.GetOffset().X) / _viewport.GetZoom();
            float y = (e.Location.Y - _viewport.GetOffset().Y) / _viewport.GetZoom();
            if (e.Button == MouseButtons.Left && Colliding(x, y))
            {
                _dragging = true;
            }
        }

        protected override void OnMouseUp(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _dragging)
            {
                _dragging = false;
                _viewport.Cursor = Cursors.Default;
            }
        }

        protected override void OnMouseMove(object? sender, MouseEventArgs e)
        {
            float x = (e.Location.X - _viewport.GetOffset().X) / _viewport.GetZoom();
            float y = (e.Location.Y - _viewport.GetOffset().Y) / _viewport.GetZoom();
            if (_dragging)
            {
                Vertices[0] = new Vector2f(x, y);
                _viewport.Cursor = Cursors.Hand;
                _viewport.Invalidate();
            }

            if (Colliding(x, y))
            {
                FillColor = Color.Yellow;
                _viewport.Invalidate();
            }
            else if (!_dragging)
            {
                FillColor = Color.Black;
                _viewport.Invalidate();
            }
        }
    }
}
