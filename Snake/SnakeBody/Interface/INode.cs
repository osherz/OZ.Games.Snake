using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SnakeGame.SnakeBody.Enum;
using SnakeGame.CommonInterface;

namespace SnakeGame.SnakeBody.Interface
{
    public interface INode : ICloneable, IGetRegion, IDraw
    {
        #region Properties
        /// <summary>
        /// Size of Node
        /// </summary>
        Size Size { get; set; }
        /// <summary>
        /// Location of node
        /// </summary>
        Point Location { get; set; }
        /// <summary>
        /// Move direction of node
        /// </summary>
        Direction Direction { get; set; }
        #endregion

        #region Behavior
        /// <summary>
        /// Change move Direction
        /// </summary>
        /// <param name="direction"></param>
        void ChangeDirection(Direction direction);
        #endregion

        #region Events
        /// <summary>
        /// Occurs when size of node changed
        /// </summary>
        event EventHandler<Size> SizeChanged;
        /// <summary>
        /// Occurs when location of node changed
        /// </summary>
        event EventHandler<Point> LocationChanged;
        /// <summary>
        /// Occurs before direction of node change
        /// </summary>
        event EventHandler<Direction> DirectionBeforeChanged;
        /// <summary>
        /// Occurs when direction of node changed
        /// </summary>
        event EventHandler<Direction> DirectionChanged;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        event EventHandler LookChanged;
        #endregion
    }
}
