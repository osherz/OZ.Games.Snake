using Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.SnakeBody
{
    public class NodeCircle : Node<Circle>
    {
        public NodeCircle() : base()
        {
            shape = new Circle();
        }

        public override object Clone()
        {
            NodeCircle node = new NodeCircle();
            node.Direction = Direction;
            node.shape = (Circle)shape.CreateNewShape();
            return node;
        }
    }

}
