using System.Drawing.Drawing2D;

namespace DrawLib.Shapes
{
    public class PivotArrow : DraggableShape
    {
        public const int _width = 3;
        public const int _magnitude = 35;
        private float    _angle;

        public float Angle
        {
            get { return (-_angle + 90) > 180 ? -_angle + 90 - 360 : -_angle + 90; }
            private set { _angle = value; }
        }

        public PivotArrow(Vector2f pos, float angle, Viewport2D viewport) : base(new List<Vector2f> { pos }, Color.Gainsboro, viewport)
        {
            _angle = (angle - 90) * -1;
        }

        public override void Draw(Graphics g, List<Vector2f> pos)
        {
            float i = _magnitude * (float)Math.Cos(_angle * Math.PI / 180);
            float j = _magnitude * (float)Math.Sin(_angle * Math.PI / 180);

            Pen pen = new Pen(FillColor, _width);
            pen.CustomEndCap = new AdjustableArrowCap(_width, _width);

            g.DrawLine(pen, pos[0].X, pos[0].Y, pos[0].X + i, pos[0].Y + j);
        }

        public override bool Colliding(float x, float y)
        {
            float i = Vertices[0].X + _magnitude * (float)Math.Cos(_angle * Math.PI / 180) / _viewport.GetZoom();
            float j = Vertices[0].Y + _magnitude * (float)Math.Sin(_angle * Math.PI / 180) / _viewport.GetZoom();
            if(x >= (i - (8 / _viewport.GetZoom())) && x <= (i + (8 / _viewport.GetZoom())))
            {
                if(y >= (j - (8 / _viewport.GetZoom())) && y <= (j + (8 / _viewport.GetZoom())))
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
                float x_d = x - Vertices[0].X;
                float y_d = y - Vertices[0].Y;

                _angle = (int)(Math.Atan2(y_d, x_d) * 180 / Math.PI);
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
                FillColor = Color.Gainsboro;
                _viewport.Invalidate();
            }
        }
    }
}
