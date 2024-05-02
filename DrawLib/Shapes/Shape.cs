namespace DrawLib.Shapes
{
    public abstract class Shape
    {
        protected Vector2f Position;

        public Shape()
        {
            Position = new Vector2f();
        }

        public abstract void Draw(Graphics g);

        public abstract bool Colliding(float x, float y);

        public abstract void SetPosition(float x, float y);

        public abstract Vector2f GetPosition();

        public abstract void SetColor(Color color);

        public virtual void SetStaticPosition(float x, float y)
        {
            Position.X = x;
            Position.Y = y;
        }

        public virtual Vector2f GetStaticPosition()
        {
            return Position;
        }

    }
}
