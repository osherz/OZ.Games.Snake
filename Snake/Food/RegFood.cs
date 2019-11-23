using SnakeGame.Food.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Shapes;

namespace SnakeGame.Food
{
    public class RegFood<T> : IFood
        where T: ShapeConst
    {
        #region Variable
        private int sumOfNodesForSnake;
        protected T foodShape;
        #endregion

        public RegFood()
        {

        }

        #region Properties
        public Point Location
        {
            get
            {
                return Point.Round(foodShape.Location);
            }

            set
            {
                foodShape.Location = value;
                OnLocationChanged(Location);
            }
        }

        public Size Size
        {
            get
            {
                return foodShape.Size;
            }

            set
            {
                foodShape.Size = value;
                OnSizeChanged(Size);
            }
        }

        public int SumOfNodesForSnake
        {
            get
            {
                return sumOfNodesForSnake;
            }

            set
            {
                sumOfNodesForSnake = value;
                OnSumOfNodesForSnakeChanged(SumOfNodesForSnake);
            }
        }

        public Color Color
        {
            get
            {
                return foodShape.Color;
            }

            set
            {
                foodShape.Color = value;
                OnColorChanged(Color);
            }
        }

        public ShapeStyle FoodShapeStyle
        {
            get
            {
                return foodShape.ShapeStyle;
            }

            set
            {
                foodShape.ShapeStyle = value;
                OnFoodShapeStyleChanged(FoodShapeStyle);
            }
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when drew
        /// </summary>
        /// <param name="e"></param>
        public event EventHandler Drew;
        /// <summary>
        /// Occurs when location changed
        /// </summary>
        public event EventHandler<Point> LocationChanged;
        /// <summary>
        /// Occurs when size changed
        /// </summary>
        public event EventHandler<Size> SizeChanged;
        /// <summary>
        /// Occurs when sum of nodes for snake changed
        /// </summary>
        public event EventHandler<int> SumOfNodesForSnakeChanged;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        public event EventHandler LookChanged;
        /// <summary>
        /// Occurs when color changed
        /// </summary>
        public event EventHandler<Color> ColorChanged;
        /// <summary>
        /// Occurs when style of food's shape changed
        /// </summary>
        public event EventHandler<ShapeStyle> FoodShapeStyleChanged;
        #endregion

        #region Events method
        /// <summary>
        /// Occurs when drew
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnDrew(EventArgs e)
        {

            Drew?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when look changed
        /// </summary>
        protected virtual void OnLookChanged(EventArgs e)
        {

            LookChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when location changed
        /// </summary>
        protected virtual void OnLocationChanged(Point location)
        {

            LocationChanged?.Invoke(this, location);
        }

        /// <summary>
        /// Occurs when size changed
        /// </summary>
        protected virtual void OnSizeChanged(Size size)
        {

            SizeChanged?.Invoke(this, size);
            OnLookChanged(new EventArgs());
        }

        /// <summary>
        /// Occurs when sum of nodes for snake changed
        /// </summary>
        protected virtual void OnSumOfNodesForSnakeChanged(int sum)
        {

            SumOfNodesForSnakeChanged?.Invoke(this, sum);
        }

        /// <summary>
        /// Occurs when color changed
        /// </summary>
        /// <param name="color"></param>
        private void OnColorChanged(Color color)
        {

            ColorChanged?.Invoke(this, color);
            OnLookChanged(new EventArgs());
        }

        /// <summary>
        /// Occurs when shape style of food changed
        /// </summary>
        /// <param name="foodShapeStyle"></param>
        private void OnFoodShapeStyleChanged(ShapeStyle foodShapeStyle)
        {
            FoodShapeStyleChanged?.Invoke(this, foodShapeStyle);
            OnLookChanged(new EventArgs());
        }

        #endregion

        #region Methods
        public object Clone()
        {
            RegFood<T> food = new Food.RegFood<T>();
            food.foodShape = (T)foodShape.CreateNewShape();
            food.sumOfNodesForSnake = sumOfNodesForSnake;
            return food;
        }

        public void Draw(Graphics g)
        {
            foodShape.DrawShape(g);
            OnDrew(new EventArgs());
        }

        public Region GetRegion()
        {
            return new Region(foodShape.Rect);
        }
        #endregion
    }
}
