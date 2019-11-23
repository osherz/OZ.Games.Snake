using SnakeGame.CommonInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Food.Interface
{
    public interface IFood : ICloneable, IGetRegion, IDraw
    {
        #region Properties
        /// <summary>
        /// Food's location
        /// </summary>
        Point Location { get; set; }
        /// <summary>
        /// Food's size
        /// </summary>
        Size Size { get; set; }
        /// <summary>
        /// Sum of nodes for snake that the food give him
        /// </summary>
        int SumOfNodesForSnake { get; set; }
        #endregion

        #region Behavior
        #endregion

        #region Events
        /// <summary>
        /// Occurs when location changed
        /// </summary>
        event EventHandler<Point> LocationChanged;
        /// <summary>
        /// Occurs when size changed
        /// </summary>
        event EventHandler<Size> SizeChanged;
        /// <summary>
        /// Occurs when sum of nodes for snake changed
        /// </summary>
        event EventHandler<int> SumOfNodesForSnakeChanged;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        event EventHandler LookChanged;
        #endregion
    }
}
