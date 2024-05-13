﻿using System.Drawing.Drawing2D;

namespace DrawLib.Shapes
{
    public class Arrow : Shape
    {
        public const int _width = 4;

        public Arrow(Vector2f pos1, Vector2f pos2) : base(new List<Vector2f> { pos1, pos2 }, Color.Black) { }

        public override void Draw(Graphics g, List<Vector2f> pos)
        {
            Pen pen = new Pen(Color.Black, _width);
            pen.CustomEndCap = new AdjustableArrowCap(_width - 1, _width - 1);

            float[] line = new float[4]
            {
                pos[0].X, pos[0].Y,
                pos[1].X, pos[1].Y,
            };
            g.DrawLine(pen, line[0], line[1], line[2], line[3]);

        }
    }
}
