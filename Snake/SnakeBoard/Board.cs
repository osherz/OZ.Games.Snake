using SnakeGame.SnakeBoard.Interface;
using SnakeGame.SnakeBody.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SnakeGame.CommonInterface;
using SnakeGame.Food.Interface;
using SnakeGame.SnakeBody.Enum;

namespace SnakeGame.SnakeBoard
{
    public partial class Board : Control
    {
        #region Variables
        /// <summary>
        /// The pototype of border, just Top
        /// </summary>
        private IBorder borderPrototype_Top;
        /// <summary>
        /// Array of all border of board
        /// </summary>
        private Borders borders;
        /// <summary>
        /// The snake
        /// </summary>
        private ISnake snake;
        /// <summary>
        /// The food of snake
        /// </summary>
        private Queue<IFood> foodQueue;
        /// <summary>
        /// Is food was drew. To know check ifsnake hit him.
        /// </summary>
        private bool isFoodDrew = false;
        /// <summary>
        /// The board area
        /// </summary>
        private Region borderArea;
        /// <summary>
        /// How much food snake eated
        /// </summary>
        private int foodEated;
        #endregion

        #region Events
        /// <summary>
        /// Occurs before top border change
        /// </summary>
        public event EventHandler<BorderEventArgs> BorderPototypeBeforeChanged;
        /// <summary>
        /// Occurs when top border changed
        /// </summary>
        public event EventHandler<BorderEventArgs> BorderPototypeChanged;
        /// <summary>
        /// Occurs before snake change
        /// </summary>
        public event EventHandler<ISnake> SnakeBeforeChanged;
        /// <summary>
        /// Occurs when snake changed
        /// </summary>
        public event EventHandler<ISnake> SnakeChanged;
        /// <summary>
        /// Occurs when food added
        /// </summary>
        public event EventHandler<IFood> FoodAdded;
        /// <summary>
        /// Occurs when interval of timer(speed of snake move) changed;
        /// </summary>
        public event EventHandler<int> SpeedChanged;
        /// <summary>
        /// Occurs when game end
        /// </summary>
        public event EventHandler EndGame;
        #endregion

        #region Properties
        /// <summary>
        /// Get/Set border pototype(just Top)
        /// </summary>
        public IBorder BorderPrototype_Top
        {
            get
            {
                return borderPrototype_Top;
            }

            set
            {
                if(borderPrototype_Top!=null) OnBorderPototypeBeforeChange(borderPrototype_Top.GetEventArgs());
                borderPrototype_Top = value;
                OnBorderPototypeChanged(borderPrototype_Top.GetEventArgs());
            }
        }

        /// <summary>
        /// Get/Set snake
        /// </summary>
        public ISnake Snake
        {
            get
            {
                return snake;
            }

            set
            {
                OnSnakeBeforeChange(snake);
                snake = value;
                OnSnakeChanged(snake);

            }
        }

        /// <summary>
        /// Get the first food
        /// Add food to queue food
        /// </summary>
        public IFood Food
        {
            get
            {
                return foodQueue.Peek();
            }
        }

        /// <summary>
        /// Sum of food in queue
        /// </summary>
        public int SumOfFoods
        {
            get
            {
                return foodQueue.Count;
            }
        }

        /// <summary>
        /// Get/Set border area
        /// </summary>
        public Region BorderArea
        {
            get
            {
                return borderArea.Clone();
            }
        }

        /// <summary>
        /// Get/Set start game
        /// </summary>
        public bool IsStartGame
        {
            get
            {
                return timerOfSnakeMove.Enabled;
            }

            set
            {
                if (value) StartGame();
                else PauseGame();
            }
        }

        /// <summary>
        /// After how much time the snake move
        /// </summary>
        public int SpeedOfSnake
        {
            get
            {
                return timerOfSnakeMove.Interval;
            }

            set
            {
                timerOfSnakeMove.Interval = value;
                OnSpeedChanged(timerOfSnakeMove.Interval);
            }
        }

        /// <summary>
        /// How much food snake eated
        /// </summary>
        public int FoodEated
        {
            get
            {
                return foodEated;
            }
        }

        #endregion

        public Board()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
    //        this.SetStyle(
    //System.Windows.Forms.ControlStyles.UserPaint |
    //System.Windows.Forms.ControlStyles.AllPaintingInWmPaint |
    //System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer,
    //true);
            borders = new SnakeBoard.Borders();
            borderArea = new Region();
            foodQueue = new Queue<Food.Interface.IFood>();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            Food.Draw(pe.Graphics);
            snake.Draw(pe.Graphics);
            PaintBorder(pe.Graphics);
        }

        /// <summary>
        /// Draw the borders
        /// </summary>
        /// <param name="g"></param>
        private void PaintBorder(Graphics g)
        {
            borders.Draw(g);
        }

        /// <summary>
        /// Active by timer.
        /// snakeMove
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerOfSnakeMove_Tick(object sender, EventArgs e)
        {
            snake.Move();
            //Invalidate();
        }

        #region Events updates
        /// <summary>
        /// Reset the border events
        /// </summary>
        /// <param name="border"></param>
        private void ResetBorderEvents(IBorder border)
        {
            if (border != null)
            {
                border.LocationChanged -= OnLocationOfBorderChanged;
                border.SizeChanged -= OnSizeBorderChanged;
            }
        }

        /// <summary>
        /// Restart the border events
        /// </summary>
        /// <param name="border"></param>
        private void RestartBorderEvents(IBorder border)
        {
            if (border != null)
            {
                border.LocationChanged += OnLocationOfBorderChanged;
                border.SizeChanged += OnSizeBorderChanged;
            }
        }
        
        /// <summary>
        /// Reset the snake events
        /// </summary>
        private void ResetSnakeEvents()
        {
            if (Snake != null)
            {
                snake.Birthed -= OnSnakeBirthed;
                snake.HeadChanged -= OnSnakeHeadChanged;
                snake.HitSomething -= OnSnakeHitSomething;
                snake.Moved -= OnSnakeMoved;
                snake.NodePrototypeChanged -= OnSnakeNodePototypeChanged;
                snake.TailChanged -= OnSnakeTailChanged;
                snake.LookChanged -= OnSnakeLookChanged;
            }
        }

        /// <summary>
        /// Restart the snake events
        /// </summary>
        private void RestartSnakeEvents()
        {
            if (Snake != null)
            {
                snake.Birthed += OnSnakeBirthed;
                snake.HeadChanged += OnSnakeHeadChanged;
                snake.HitSomething += OnSnakeHitSomething;
                snake.Moved += OnSnakeMoved;
                snake.NodePrototypeChanged += OnSnakeNodePototypeChanged;
                snake.TailChanged += OnSnakeTailChanged;
                snake.LookChanged += OnSnakeLookChanged;
            }
        }

        /// <summary>
        /// Reset the food events
        /// </summary>
        private void ResetFoodEvents()
        {
            if (Food != null)
            {
                Food.Drew -= OnFoodDrew;
                Food.LocationChanged -= OnFoodLocationChanged;
                Food.SizeChanged -= OnSizeOfFoodChanged;
                Food.SumOfNodesForSnakeChanged -= OnSumOfNodesForSnakeChanged;
            }
        }

        /// <summary>
        /// Restart the food events
        /// </summary>
        private void RestartFoodEvents()
        {
            if (Food != null)
            {
                Food.Drew += OnFoodDrew;
                Food.LocationChanged += OnFoodLocationChanged;
                Food.SizeChanged += OnSizeOfFoodChanged;
                Food.SumOfNodesForSnakeChanged += OnSumOfNodesForSnakeChanged;
            }
        }
        #endregion

        #region Events method

        #region Snake events
        /// <summary>
        /// Occurs before snake change
        /// </summary>
        /// <param name="snake"></param>
        protected virtual void OnSnakeBeforeChange(ISnake snake)
        {
            ResetSnakeEvents();
            SnakeBeforeChanged?.Invoke(this, snake);
        }

        /// <summary>
        /// Occurs when snake changed
        /// </summary>
        /// <param name="snake"></param>
        protected virtual void OnSnakeChanged(ISnake snake)
        {
            RestartSnakeEvents();
            SnakeChanged?.Invoke(this, snake);
        }

        /// <summary>
        /// Occurs when snake birthed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSnakeBirthed(object sender, SnakeEventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Occurs when snake head changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSnakeHeadChanged(object sender, INode e)
        {
            Invalidate();
        }

        /// <summary>
        /// Occurs when snake hit something
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSnakeHitSomething(object sender, Region e)
        {
            
        }

        /// <summary>
        /// Occurs when snake is moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSnakeMoved(object sender, Region e)
        {
            //Invalidate(e);
            if (snake.IsHitSomething(borderArea) || snake.IsHitHimself())
            {
                OnEndGame();
            }
            else if (isFoodDrew && snake.IsHitSomething(Food))
            {
                snake.Eat(Food);
                foodEated++;
                isFoodDrew = false;
                GenerateNewFood();
            }
            Invalidate();
        }

        /// <summary>
        /// Occurs when snake's node pototype changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSnakeNodePototypeChanged(object sender, INode e)
        {
            Invalidate();
        }

        /// <summary>
        /// Occurs when snake's tail changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSnakeTailChanged(object sender, INode e)
        {
            Invalidate();
        }

        /// <summary>
        /// Occurs when look of snake changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSnakeLookChanged(object sender, EventArgs e)
        {
            //Invalidate(snake.GetRegion());
        }

        /// <summary>
        /// Occurs when key preesed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (IsStartGame)
            {
                switch(e.KeyCode)
                {
                    case Keys.W:
                        snake.GoUp();
                        break;
                    case Keys.S:
                        snake.GoDown();
                        break;
                    case Keys.D:
                        snake.GoRight();
                        break;
                    case Keys.A:
                        snake.GoLeft();
                        break;
                }
            }
            base.OnKeyDown(e);
        }


        #endregion

        #region Food method
        #region Food events
        /// <summary>
        /// Occurs when food changed
        /// </summary>
        /// <param name="food"></param>
        protected virtual void OnFoodAdded(IFood food)
        {
            if(foodQueue.Count == 1)
            {
                ResetFoodEvents();
                RestartFoodEvents();
            }
            FoodAdded?.Invoke(this, food);
        }

        /// <summary>
        /// Occurs when food was drew
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnFoodDrew(object sender, EventArgs e)
        {
            isFoodDrew = true;
        }

        /// <summary>
        /// Occurs when location of food changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnFoodLocationChanged(object sender, Point e)
        {
            Invalidate(Food.GetRegion());
        }

        /// <summary>
        /// Occurs when size of food changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSizeOfFoodChanged(object sender, Size e)
        {
            Invalidate();
        }

        /// <summary>
        /// Occurs when sum of nodes for snake changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSumOfNodesForSnakeChanged(object sender, int e)
        {
        }
        #endregion

        /// <summary>
        /// Add food to queue
        /// </summary>
        /// <param name="food"></param>
        public void AddFood(IFood food)
        {
            foodQueue.Enqueue(food);
            OnFoodAdded(food);
        }

        /// <summary>
        /// Generate new location for food
        /// </summary>
        private Region GenerateNewFood()
        {
            Region regToRe = Food.GetRegion();
            
            if (foodQueue.Count > 1)
            {
                if (foodEated > 0) foodQueue.Enqueue(foodQueue.Dequeue());
                ResetFoodEvents();
                RestartFoodEvents();
            }
            Point newLocation = new Point();
            IFood food = (IFood)Food.Clone();
            Random rnd = new Random();
            Rectangle foodRect;
            do
            {
                newLocation.X = rnd.Next(borders.LeftBorder.Size.Width + borders.LeftBorder.Location.X, borders.RightBorder.Location.X);
                newLocation.Y = rnd.Next(borders.TopBorder.Size.Height + borders.TopBorder.Location.Y, borders.BottomBorder.Location.Y);
                food.Location = newLocation;
                foodRect = new Rectangle(food.Location, food.Size);

            } while (snake.GetRegion().IsVisible(foodRect) || borderArea.IsVisible(foodRect));
            this.Food.Location = newLocation;
            return regToRe;
        }
        #endregion

        #region Border method
        #region Border events
        /// <summary>
        /// Occurs before border change
        /// </summary>
        /// <param name="eBorderPototype"></param>
        protected virtual void OnBorderPototypeBeforeChange(BorderEventArgs eBorderPototype)
        {
            ResetBorderEvents(eBorderPototype.Border);
            BorderPototypeBeforeChanged?.Invoke(this, eBorderPototype);
        }

        /// <summary>
        /// Occurs when border changed
        /// </summary>
        /// <param name="borderPototype"></param>
        protected virtual void OnBorderPototypeChanged(BorderEventArgs borderPototype)
        {
            UpdateBorders();
            BorderPototypeChanged?.Invoke(this, borderPototype);
        }

        /// <summary>
        /// Occurs when end location of border changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnEndLocationOfBorderChanged(object sender, Point e)
        {
            Invalidate();
        }

        /// <summary>
        /// Occurs when start location of border changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnLocationOfBorderChanged(object sender, Point e)
        {
            Invalidate();
        }

        /// <summary>
        /// Occurs when size of border changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSizeBorderChanged(object sender, Size e)
        {
            Invalidate();
        }

        /// <summary>
        /// Occurs when width of border changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnWidthOfBorderChanged(object sender, float e)
        {
            Invalidate();
        }
        #endregion

        private void CopyBorderPototypeToAll()
        {
            borderArea.MakeEmpty();
            IBorder border = (IBorder)borderPrototype_Top.Clone();
            Point Location = new Point();
            Size size = border.Size;

            //TOP BORDER
            Location = new Point(0, 0);
            border.Location = Location;
            border.Size = size;
            border.Place = Place.Top;
            borders.TopBorder = (IBorder)border.Clone();

            //BOTTOM BORDER
            Location.Y = Height - size.Height;
            border.Location = Location;
            border.Size = size;
            border.Place = Place.Bottom;
            borders.BottomBorder = (IBorder)border.Clone();

            //LEFT BORDER
            size.Width = border.Size.Height;
            size.Height = border.Size.Width;
            border.Size = size;
            Location = new Point(0, 0);
            border.Location = Location;
            border.Place = Place.Left;
            borders.LeftBorder = (IBorder)border.Clone();

            //RIGHT BORDER
            Location.X = Width - border.Size.Width;
            border.Size = size;
            border.Location = Location;
            border.Place = Place.Right;
            borders.RightBorder = (IBorder)border.Clone();

            borderArea = borders.GetRegion();
        }

        private void UpdateBorders()
        {
            CopyBorderPototypeToAll();
            RestartBorderEvents(borderPrototype_Top);
            Invalidate();
        }
        #endregion

        protected virtual void OnSpeedChanged(int speed)
        {
            SpeedChanged?.Invoke(this, speed);
        }
        #endregion

        #region Game method
        /// <summary>
        /// Begin the game
        /// </summary>
        public void StartGame()
        {
            snake.ChangeMoveDirection(Direction.Left);
            GenerateNewFood();
            timerOfSnakeMove.Start();
        }

        /// <summary>
        /// Reset and start the game
        /// </summary>
        /// <param name="sumOfNodesOfSnake"></param>
        /// <param name="headLocation">The loaction of snake's head</param>
        public void RestartGame(int sumOfNodesOfSnake, Point headLocation)
        {
            snake.KillSnake();
            snake.Birth(sumOfNodesOfSnake, headLocation);
            StartGame();
        }

        /// <summary>
        /// Stop move of snake
        /// </summary>
        public void PauseGame()
        {
            timerOfSnakeMove.Stop();
        }

        /// <summary>
        /// Continue game
        /// </summary>
        public void ContinueGame()
        {
            timerOfSnakeMove.Start();
        }


        /// <summary>
        /// Occurs when the game end
        /// </summary>
        protected virtual void OnEndGame()
        {
            PauseGame();
            EndGame?.Invoke(this, new EventArgs());
        }
        #endregion
    }
}
