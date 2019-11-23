using SnakeGame.CommonInterface;
using SnakeGame.Food.Interface;
using SnakeGame.SnakeBody.Enum;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.SnakeBody.Interface
{
    public interface ISnake : ICloneable, IGetRegion, IDraw
    {
        #region Properties
        /// <summary>
        /// The head of snake
        /// </summary>
        INode Head { get; set; }
        /// <summary>
        /// The prototype node of snake
        /// </summary>
        INode NodePrototype { get; set; }
        /// <summary>
        /// The tail of snake
        /// </summary>
        INode Tail { get; set; }
        /// <summary>
        /// Start location of snake
        /// </summary>
        Point HeadLocation { get; }
        /// <summary>
        /// End location of snake
        /// </summary>
        Point TailLocation { get; }
        /// <summary>
        /// Sum of nodes of snake
        /// </summary>
        int SumOfNodes { get; }
        /// <summary>
        /// Move direction of head
        /// </summary>
        Direction Direction { get; }
        /// <summary>
        /// Is snake birthed
        /// </summary>
        bool IsBirthed { get; }
        #endregion

        #region Behavior
        /// <summary>
        /// Birth the snake. before the snake can move he must to birth.
        /// </summary>
        /// <param name="sumOfNodes"></param>
        /// <param name="startX">X of start location</param>
        /// <param name="startY">Y of start location</param>
        void Birth(int sumOfNodes, int headX, int headY);
        /// <summary>
        /// Birth the snake. before the snake can move he must to birth.
        /// </summary>
        /// <param name="sumOfNodes"></param>
        /// <param name="startLocation"></param>
        void Birth(int sumOfNodes, Point headLocation);

        #region Move methods
        /// <summary>
        /// Change move direction of snake.
        /// Parallel to GoXX() methods.
        /// </summary>
        /// <param name="direction"></param>
        bool ChangeMoveDirection(Direction direction);
        /// <summary>
        /// Change the move direction of snake to up.
        /// Parallel to ChangeMoveDirection(Direction.Up).
        /// </summary>
        void GoUp();
        /// <summary>
        /// Change the move direction of snake to down.
        /// Parallel to ChangeMoveDirection(Direction.Down).
        /// </summary>
        void GoDown();
        /// <summary>
        /// Change the move direction of snake to left.
        /// Parallel to ChangeMoveDirection(Direction.Left).
        /// </summary>
        void GoLeft();
        /// <summary>
        /// Change the move direction of snake to right.
        /// Parallel to ChangeMoveDirection(Direction.Right).
        /// </summary>
        void GoRight();
        /// <summary>
        /// Moving the snake forwards by his diection.
        /// </summary>
        void Move();
        #endregion

        /// <summary>
        /// Eat the snake with food.
        /// The snake will grow.
        /// </summary>
        /// <param name="food"></param>
        void Eat(IFood food);
        /// <summary>
        /// Kill the snake.
        /// convet IsBirthed to false too.
        /// </summary>
        void KillSnake();

        /// <summary>
        /// Check if the snake hit something
        /// </summary>
        /// <param name="rect">Rectangle of the item that maybe the snake hit</param>
        /// <returns></returns>
        bool IsHitSomething(Rectangle rect);
        /// <summary>
        /// Check if the snake hit something
        /// </summary>
        /// <param name="reg">Region of the item that maybe the snake hit</param>
        /// <returns></returns>
        bool IsHitSomething(Region reg);
        /// <summary>
        /// Check if the snake hit something
        /// </summary>
        /// <param name="reg">The reg that maybe the snake hit</param>
        /// <returns></returns>
        bool IsHitSomething(IGetRegion reg);
        /// <summary>
        /// Check if the snake hit himself
        /// </summary>
        /// <returns></returns>
        bool IsHitHimself();

        /// <summary>
        /// Return event args of snake
        /// </summary>
        /// <returns></returns>
        SnakeEventArgs GetEventArgs();
        #endregion

        #region Events
        /// <summary>
        /// Occures before head change.
        /// </summary>
        event EventHandler<INode> HeadBeforeChange;
        /// <summary>
        /// Occures when head changed.
        /// </summary>
        event EventHandler<INode> HeadChanged;
        /// <summary>
        /// Occures before node prototype change.
        /// </summary>
        event EventHandler<INode> NodePrototypeBeforeChanged;
        /// <summary>
        /// Occures when NodePrototype changed.
        /// </summary>
        event EventHandler<INode> NodePrototypeChanged;
        /// <summary>
        /// Occures before tail change.
        /// </summary>
        event EventHandler<INode> TailBeforeChanged;
        /// <summary>
        /// Occures when tail changed.
        /// </summary>
        event EventHandler<INode> TailChanged;
        /// <summary>
        /// Occures when snake eated.
        /// </summary>
        event EventHandler<IFood> Eated;
        /// <summary>
        /// Occures when drew
        /// </summary>
        event EventHandler<SnakeEventArgs> Birthed;
        /// <summary>
        /// Occurs when direction of node changed
        /// </summary>
        event EventHandler<Direction> DirectionChanged;
        /// <summary>
        /// Occures when snake moved
        /// </summary>
        event EventHandler<Region> Moved;
        /// <summary>
        /// Occures when snake hit something
        /// </summary>
        event EventHandler<Region> HitSomething;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        event EventHandler LookChanged;
        /// <summary>
        /// Occurs when snake killed
        /// </summary>
        event EventHandler<SnakeEventArgs> Killed;
        #endregion

    }
}
