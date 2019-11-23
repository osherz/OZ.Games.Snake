using Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeGame.SnakeBody
{
    public class NodeSeq : Node<Sequre>
    {
        private Color pointsColor;
        private RectangleF[] rectF;

        public Color PointsColor
        {
            get
            {
                return pointsColor;
            }

            set
            {
                pointsColor = value;
            }
        }

        public NodeSeq():base()
        {
            shape = new Sequre();
            rectF = new RectangleF[0];
            UpdatePoints();
        }

        protected virtual void UpdatePoints()
        {
            Random rnd = new Random();
            PointF location = new PointF();
            SizeF size = new SizeF();

            for (int i = 0; i < rectF.Length; i++)
            {
                size.Width = (float)Size.Width / 10;
                size.Height = (float)Size.Height / 10;
                location.X = (float)rnd.NextDouble() * (Size.Width - size.Width);
                location.Y = (float)rnd.NextDouble() * (Size.Height - size.Height);
                rectF[i] = new RectangleF(location, size);
            }
        }

        protected override void OnSizeChanged(Size size)
        {
            base.OnSizeChanged(size);
            UpdatePoints();
        }

        public override void Draw(Graphics g)
        {
            base.Draw(g);
            PointF location = new Point();
            foreach(RectangleF rec in rectF)
            {
                location.X = rec.X + Location.X;
                location.Y = rec.Y + Location.Y;
                g.FillEllipse(new SolidBrush(PointsColor), location.X, location.Y, rec.Width, rec.Height);
            }
        }

        public override object Clone()
        {
            NodeSeq node = new NodeSeq();
            node.Direction = Direction;
            node.shape = (Sequre)shape.CreateNewShape();
            node.pointsColor = pointsColor;
            rectF.CopyTo(node.rectF, 0);
            return node;
        }
    }
}
