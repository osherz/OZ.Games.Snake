using SnakeGame.SnakeBoard.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Shapes;

namespace SnakeGame.SnakeBoard
{
    class Border : IBorder
    {
        #region variable
        /// <summary>
        /// Place of birder in client
        /// </summary>
        private Place place;
        private Size size;
        private Point location;
        private Brush brush;
        private ShapeStyle style = ShapeStyle.fill;

        #endregion
        public Border()
        {
        }


        #region Properties
        public Place Place
        {
            get
            {
                return place;
            }

            set
            {
                place = value;
                OnPlaceChanged(Place);
            }
        }

        public Size Size
        {
            get
            {
                return size;
            }

            set
            {
                size = value;
                OnSizeChanged(Size);
            }
        }

        public Point Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                OnLocationChanged(Location);
            }
        }

        public Brush Brush
        {
            get
            {
                return brush;
            }

            set
            {
                brush = value;
                OnBrushChanged(Brush);
            }
        }

        public ShapeStyle Style
        {
            get
            {
                return style;
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Occurs when drew
        /// </summary>
        public event EventHandler Drew;
        /// <summary>
        /// Occurs when border's end location changed
        /// </summary>
        public event EventHandler<Point> LocationChanged;
        /// <summary>
        /// Occurs when border place changed
        /// </summary>
        public event EventHandler<Place> PlaceChanged;
        /// <summary>
        /// Occurs when border's size changed
        /// </summary>
        public event EventHandler<Size> SizeChanged;
        /// <summary>
        /// Occurs on pen changed
        /// </summary>
        public event EventHandler<Brush> BrushChanged;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        public event EventHandler LookChanged;
        #endregion

        #region Events method

        /// <summary>
        /// Occurs when look changed
        /// </summary>
        protected virtual void OnLookChanged(EventArgs e)
        {

            LookChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Rasises the BrushChanged event
        /// </summary>
        /// <param name="brush"></param>
        protected virtual void OnBrushChanged(Brush brush)
        {

            BrushChanged?.Invoke(this, brush);
        }

        /// <summary>
        /// Occurs when border's size changed
        /// </summary>
        protected virtual void OnSizeChanged(SizeF size)
        {

            SizeChanged?.Invoke(this, size.ToSize());
            OnLookChanged(new EventArgs());
        }

        /// <summary>
        /// Occurs when border place changed
        /// </summary>
        protected virtual void OnPlaceChanged(Place place)
        {

            PlaceChanged?.Invoke(this, place);
        }

        /// <summary>
        /// Occurs when border's start location changed
        /// </summary>
        protected virtual void OnLocationChanged(Point startLocation)
        {
            LocationChanged?.Invoke(this, startLocation);
            OnLookChanged(new EventArgs());
        }

        /// <summary>
        /// Occurs when drew
        /// </summary>
        protected virtual void OnDrew(EventArgs e)
        {

            Drew?.Invoke(this, e);
        }
        #endregion

        #region Methods
        public object Clone()
        {
            Border border = new Border();
            border.Brush = (Brush)Brush.Clone();
            border.place = place;
            border.Location = Location;
            border.Size = Size;
            border.style = style;
            return border;
        }

        public void Draw(Graphics g)
        {
            switch(Style)
            {
                case ShapeStyle.fill:
                    g.FillRectangle(Brush, new Rectangle(Location, Size));
                    break;
            }
            OnDrew(new EventArgs());
        }

        public BorderEventArgs GetEventArgs()
        {
            return new BorderEventArgs(this, Place);
        }

        public Region GetRegion()
        {
            return new Region(new RectangleF(Location, Size));
        }

        #endregion
    }
}
