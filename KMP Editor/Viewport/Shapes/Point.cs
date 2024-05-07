namespace KMP_Editor.Viewport.Shapes
{
    public class Point
    {
        private RectangleF _rect;
        private SolidBrush _brush;
        private Vector2f _position;

        public const int Size = 10;

        public Vector2f Position
        {
            get { return new Vector2f(X, Y); }
            set { X = value.X; Y = value.Y; }
        }
        public float X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }
        public float Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        public Vector2f RectPosition
        {
            get { return new Vector2f(RectX, RectY); }
            set { RectX = value.X; RectY = value.Y; }
        }
        public float RectX
        {
            get { return _rect.X + Size / 2; }
            set { _rect.X = value - Size / 2; }
        }
        public float RectY
        {
            get { return _rect.Y + Size / 2; }
            set { _rect.Y = value - Size / 2; }
        }

        public Point()
        {
            X = 0;
            Y = 0;
            _rect = new RectangleF(X - Size / 2, Y - Size / 2, Size, Size);
            _brush = new SolidBrush(Color.Blue);
        }

        public Point(Vector2f position)
        {
            X = position.X;
            Y = position.Y;
            _rect = new RectangleF(position.X - Size / 2, position.Y - Size / 2, Size, Size);
            _brush = new SolidBrush(Color.Blue);
        }

        public Point(Vector2f position, Color color)
        {
            X = position.X;
            Y = position.Y;
            _rect = new RectangleF(position.X - Size / 2, position.Y - Size / 2, Size, Size);
            _brush = new SolidBrush(color);
        }

        public void Draw(Graphics g)
        {
            if (RectX > 0 && RectY > 0)
                g.FillEllipse(_brush, _rect);
        }

        public void Drag(float x, float y)
        {
            X = x;
            Y = y;
        }

        public bool Colliding(float x, float y)
        {
            if (x > _rect.X && x < _rect.X + Size)
            {
                if (y > _rect.Y && y < _rect.Y + Size)
                {
                    return true;
                }
            }
            return false;
        }

        public void Translate(float x, float y)
        {
            RectX = x;
            RectY = y;
        }

        public void SetColor(Color color)
        {
            _brush.Color = color;
        }
    }
}
