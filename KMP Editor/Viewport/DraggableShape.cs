namespace KMP_Editor.Viewport
{
    public abstract class DraggableShape : Shape
    {
        internal Viewport2D _viewport;
        internal bool       _dragging;

        public DraggableShape(List<Vector2f> vertices, Color color, Viewport2D viewport) : base(vertices, color)
        {
            _viewport = viewport;
            _viewport.MouseDown += OnMouseDown;
            _viewport.MouseUp   += OnMouseUp;
            _viewport.MouseMove += OnMouseMove;
        }

        public abstract bool Colliding(float x, float y);

        protected virtual void OnMouseDown(object? sender, MouseEventArgs e) { }

        protected virtual void OnMouseUp(object? sender, MouseEventArgs e) { }

        protected virtual void OnMouseMove(object? sender, MouseEventArgs e) { }
    }
}
