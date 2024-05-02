namespace DrawLib.Shapes
{
    public class Point : Shape
    {
        private const int Size = 8;
        private RectangleF Rect;
        private SolidBrush Brush;

        public Point(Vector2f position, Color color)
        {
            this.Position   = position;
            this.Brush      = new SolidBrush(color);
            this.Rect       = new RectangleF(position.X - (Size / 2), position.Y - (Size / 2), Size, Size);
        }

        public override void Draw(Graphics g)
        {
            if(this.Rect.X > 0 && this.Rect.Y > 0)
                g.FillEllipse(this.Brush, this.Rect);
        }

        public override bool Colliding(float x, float y)
        {
            if (x > this.Rect.X && x < this.Rect.X + Size)
            {
                if (y > this.Rect.Y && y < this.Rect.Y + Size)
                {
                    return true;
                }
            }
            return false;
        }

        public override void SetPosition(float x, float y)
        {
            this.Rect.X = x;
            this.Rect.Y = y;
        }

        public override Vector2f GetPosition()
        {
            return new Vector2f(Rect.X, Rect.Y);
        }

        public override void SetColor(Color color)
        {
            this.Brush.Color = color;
        }
    }
}
