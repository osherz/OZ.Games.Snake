using SnakeGame.CommonInterface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.SnakeBoard.Interface
{
    public interface IBorder : ICloneable, IGetRegion, IDraw
    {
        #region Properties
        /// <summary>
        /// Start location of border
        /// </summary>
        Point Location { get; set; }
        /// <summary>
        /// Size of Border(Instead of EndLocation and Width)
        /// </summary>
        Size Size { get; set; }
        /// <summary>
        /// The place of border on client
        /// </summary>
        Place Place { get; set; }
        #endregion

        #region Behavior
        BorderEventArgs GetEventArgs();
        #endregion

        #region Events
        /// <summary>
        /// Occurs when border's start location changed
        /// </summary>
        event EventHandler<Point> LocationChanged;
        /// <summary>
        /// Occurs when border's end location changed
        /// </summary>
        event EventHandler<Size> SizeChanged;
        /// <summary>
        /// Occurs when border place changed
        /// </summary>
        event EventHandler<Place> PlaceChanged;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        event EventHandler LookChanged;
        #endregion
    }
}
