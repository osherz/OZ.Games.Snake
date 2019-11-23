using SnakeGame.SnakeBody.Enum;
using System;
using System.Drawing;

namespace SnakeGame.SnakeBody.Interface
{
    public class SnakeEventArgs : EventArgs
    {
        /// <summary>
        /// Snake's head location
        /// </summary>
        private Point headLocation;
        /// <summary>
        /// Snake's tail location
        /// </summary>
        private Point tailLocation;
        /// <summary>
        /// Sum of start nodes
        /// </summary>
        private int sumOfNodes;
        /// <summary>
        /// Move direction of snake
        /// </summary>
        private Direction direction;

        /// <summary>
        /// Snake's head location
        /// </summary>
        public Point HeadLocation
        {
            get
            {
                return headLocation;
            }
        }

        /// <summary>
        /// Snake's tail location
        /// </summary>
        public Point TailLocation
        {
            get
            {
                return tailLocation;
            }
        }

        /// <summary>
        /// Sum of start nodes
        /// </summary>
        public int SumOftNodes
        {
            get
            {
                return sumOfNodes;
            }
        }

        /// <summary>
        /// Move direction of snake
        /// </summary>
        public Direction Direction
        {
            get
            {
                return direction;
            }
        }

        public SnakeEventArgs(Point headLocation, Point tailLocation, int sumOfNodes, Direction direction) : base()
        {
            this.headLocation = headLocation;
            this.tailLocation = tailLocation;
            this.sumOfNodes = sumOfNodes;
            this.direction = direction;
        }

    }
}