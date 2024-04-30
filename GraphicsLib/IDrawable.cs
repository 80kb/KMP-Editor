namespace GraphicsLib
{
    public interface IDrawable
    {
        void Draw(Graphics g, Vector2f offset, float zoom);

        bool Colliding(float x, float y);

        void SetPosition(int x, int y, Vector2f offset, float zoom);

        void SetColor(Color color);

        Vector2f GetPosition();
    }
}