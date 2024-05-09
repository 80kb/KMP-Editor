using System.CodeDom;

namespace KMP_Editor.Viewport
{
    public abstract class Shape
    {
        public List<Vector2f> Vertices { get; set; }
        public Color FillColor         { get; set; }

        internal Viewport2D _viewport;
        internal bool       _dragging;

        public Shape(List<Vector2f> vertices, Color color, Viewport2D viewport)
        {
            Vertices  = vertices;
            FillColor = color;
            _viewport = viewport;

            _viewport.MouseDown += OnMouseDown;
            _viewport.MouseUp   += OnMouseUp;
            _viewport.MouseMove += OnMouseMove;
        }

        public abstract void Draw(Graphics g, List<Vector2f> pos);

        public abstract bool Colliding(float x, float y);

        protected virtual void OnMouseDown(object? sender, MouseEventArgs e) { }

        protected virtual void OnMouseUp(object? sender, MouseEventArgs e) { }

        protected virtual void OnMouseMove(object? sender, MouseEventArgs e) { }
    }
}
