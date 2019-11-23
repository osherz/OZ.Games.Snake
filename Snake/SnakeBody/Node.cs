using SnakeGame.SnakeBody.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SnakeGame.SnakeBody.Enum;
using System.Drawing;
using Shapes;

namespace SnakeGame.SnakeBody
{
    public abstract class Node<T> : INode
        where T: ShapeConst
    {
        #region variable
        private Direction direction;
        protected T shape;
        #endregion

        public Node()
        {
        }

        #region Properties
        /// <summary>
        /// Get/Set Move direction of node
        /// </summary>
        public Direction Direction
        {
            get
            {
                return direction;
            }

            set
            {
                ChangeDirection(value);
            }
        }

        /// <summary>
        /// Get/Set Location of node
        /// </summary>
        public Point Location
        {
            get
            {
                return Point.Round(shape.Location);
            }

            set
            {
                shape.Location = value;
                OnLocationChanged(Location);
            }
        }

        /// <summary>
        /// Get/Set Size of Node
        /// </summary>
        public Size Size
        {
            get
            {
                return shape.Size;
            }

            set
            {
                shape.Size = value;
                OnSizeChanged(Size);
            }
        }

        /// <summary>
        /// Get/Set Color of node
        /// </summary>
        public Color Color
        {
            get
            {
                return shape.Color;
            }

            set
            {
                shape.Color = value;
                OnColorChanged(Color);
            }
        }

        /// <summary>
        /// Get/Set Node style
        /// </summary>
        public ShapeStyle NodeStyle
        {
            get
            {
                return shape.ShapeStyle;
            }

            set
            {
                shape.ShapeStyle = value;
                OnNodeStyleChanged(NodeStyle);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when size of node changed
        /// </summary>
        public event EventHandler<Size> SizeChanged;
        /// <summary>
        /// Occurs when location of node changed
        /// </summary>
        public event EventHandler<Point> LocationChanged;
        /// <summary>
        /// Occurs before direction of node change
        /// </summary>
        public event EventHandler<Direction> DirectionBeforeChanged;
        /// <summary>
        /// Occurs when direction of node changed
        /// </summary>
        public event EventHandler<Direction> DirectionChanged;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        public event EventHandler LookChanged;
        /// <summary>
        /// Occurs when color changed
        /// </summary>
        public event EventHandler<Color> ColorChanged;
        /// <summary>
        /// Occurs when drew
        /// </summary>
        public event EventHandler Drew;
        /// <summary>
        /// Occurs when node style changed
        /// </summary>
        public event EventHandler<ShapeStyle> NodeStyleChanged;
        #endregion

        #region Events methos
        /// <summary>
        /// Occurs before direction changed
        /// </summary>
        /// <param name="direction"></param>
        protected virtual void OnDirectionBeforeChanged(Direction direction)
        {
            DirectionBeforeChanged?.Invoke(this, direction);
        }

        /// <summary>
        /// Occurs when direction changed
        /// </summary>
        /// <param name="direction"></param>
        protected virtual void OnDirectionChanged(Direction direction)
        {

            DirectionChanged?.Invoke(this, direction);
            OnLookChanged(new EventArgs());
        }

        /// <summary>
        /// Occurs when location changed
        /// </summary>
        /// <param name="location"></param>
        protected virtual void OnLocationChanged(Point location)
        {

            LocationChanged?.Invoke(this, location);
            OnLookChanged(new EventArgs());
        }

        /// <summary>
        /// Occurs when size changed
        /// </summary>
        /// <param name="size"></param>
        protected virtual void OnSizeChanged(Size size)
        {

            SizeChanged?.Invoke(this, size);
            OnLookChanged(new EventArgs());
        }

        /// <summary>
        /// Occurs when color changed
        /// </summary>
        /// <param name="color"></param>
        protected virtual void OnColorChanged(Color color)
        {
            ColorChanged?.Invoke(this, color);
            OnLookChanged(new EventArgs());
        }

        /// <summary>
        /// Occurs when look changed
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnLookChanged(EventArgs e)
        {

            LookChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when node drew
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDrew(EventArgs e)
        {

            Drew?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when node style changed
        /// </summary>
        /// <param name="nodeStyle"></param>
        protected virtual void OnNodeStyleChanged(ShapeStyle nodeStyle)
        {

            NodeStyleChanged?.Invoke(this, nodeStyle);
            OnLookChanged(new EventArgs());
        }
        #endregion

        #region Methods
        public void ChangeDirection(Direction direction)
        {
            OnDirectionBeforeChanged(this.direction);
            this.direction = direction;
            OnDirectionChanged(this.direction);
        }

        /// <summary>
        /// Return new copy of Node
        /// </summary>
        /// <returns></returns>
        public abstract object Clone();

        /// <summary>
        /// Draw node
        /// </summary>
        /// <param name="g"></param>
        public virtual void Draw(Graphics g)
        {
            shape.DrawShape(g);
            OnDrew(new EventArgs());
        }

        /// <summary>
        /// Return region of node
        /// </summary>
        /// <returns></returns>
        public Region GetRegion()
        {
            return new Region(new Rectangle(Location, Size));
        }
        #endregion
    }
}
