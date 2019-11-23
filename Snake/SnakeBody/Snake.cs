using SnakeGame.CommonInterface;
using SnakeGame.Food.Interface;
using SnakeGame.SnakeBody.Enum;
using SnakeGame.SnakeBody.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.SnakeBody
{
    public class Snake : ISnake
    {
        #region Variable
        /// <summary>
        /// Head of snake
        /// </summary>
        private INode head;
        /// <summary>
        /// Node pototype of snake
        /// </summary>
        private INode nodePrototype;
        /// <summary>
        /// Tail of snake
        /// </summary>
        private INode tail;
        /// <summary>
        /// Size of node
        /// </summary>
        private Size sizeOfNode;
        /// <summary>
        /// Array of snake's location of nodes.
        /// </summary>
        private List<INode> nodeList;
        /// <summary>
        /// If the snake was birthed
        /// </summary>
        private bool isBirthed = false;
        /// <summary>
        /// Using for snake nove.
        /// X and Y are 0 or 1 or -1;
        /// </summary>
        private Point movePoint;
        /// <summary>
        /// Queue of direction.
        /// </summary>
        private Queue<Direction> directionQueue;
        #endregion

        public Snake()
        {
            nodeList = new List<Interface.INode>();
            directionQueue = new Queue<Enum.Direction>();
        }


        #region Properties
        /// <summary>
        /// The head of snake
        /// </summary>
        public INode Head
        {
            get
            {
                return head;
            }

            set
            {
                OnHeadBeforeChange(Head);
                Direction direction = head != null ? Direction : Direction.Left;
                head = value;
                OnHeadChanged(Head);
                head.Direction = Direction.None;
                ChangeMoveDirection(direction);
            }
        }

        /// <summary>
        /// The prototype node of snake
        /// </summary>
        public INode NodePrototype
        {
            get
            {
                return nodePrototype;
            }

            set
            {
                OnNodePrototypeBeforeChanged(NodePrototype);
                nodePrototype = value;
                OnNodePrototypeChanged(NodePrototype);
            }
        }

        /// <summary>
        /// The tail of snake
        /// </summary>
        public INode Tail
        {
            get
            {
                return tail;
            }

            set
            {
                OnTailBeforeChanged(Tail);
                tail = value;
                OnTailChanged(Tail);
            }
        }

        /// <summary>
        /// Get/Set size of node
        /// </summary>
        public Size SizeOfNode
        {
            get
            {
                return sizeOfNode;
            }

            set
            {
                sizeOfNode = value;
                OnSizeOfNodeChanged(SizeOfNode);               
            }
        }

        /// <summary>
        /// Start location of snake
        /// </summary>
        public Point HeadLocation
        {
            get
            {
                return head.Location;
            }
        }

        /// <summary>
        /// End location of snake
        /// </summary>
        public Point TailLocation
        {
            get
            {
                return tail.Location;
            }
        }

        /// <summary>
        /// Sum of nodes of snake
        /// </summary>
        public int SumOfNodes
        {
            get
            {
                return nodeList.Count;
            }
        }

        /// <summary>
        /// Get/Set Move direction of head
        /// </summary>
        public Direction Direction
        {
            get
            {
                return head.Direction;
            }
        }

        /// <summary>
        /// Get if the snake was birthed.
        /// </summary>
        public bool IsBirthed
        {
            get
            {
                return isBirthed;
            }
        }
        #endregion

        #region Behavior
        /// <summary>
        /// Birth the snake. before the snake can move he must to birth.
        /// </summary>
        /// <param name="sumOfNodes"></param>
        /// <param name="startX">X of start location</param>
        /// <param name="startY">Y of start location</param>
        public void Birth(int sumOfNodes, int headX, int headY)
        {
            Birth(sumOfNodes, new Point(headX, headY));
        }
        /// <summary>
        /// Birth the snake. before the snake can move he must to birth.
        /// </summary>
        /// <param name="sumOfNodes"></param>
        /// <param name="startLocation"></param>
        public void Birth(int sumOfNodes, Point headLocation)
        {
            Point location = new Point();
            nodeList.Clear();
            location = headLocation;
            ///HEAD
            head.Direction = Direction.None;
            ChangeMoveDirection(Direction.Left);
            head.Direction = Direction.Left;
            head.Location = location;

            ///NODES
            INode node;
            for(int i = 1; i<=sumOfNodes;i++)
            {
                node = (INode)nodePrototype.Clone();
                location.X += nodePrototype.Size.Width;
                node.Location = location;
                node.Direction = Direction.Left;
                nodeList.Add(node);                
            }

            ///TAIL
            location.X += nodeList[nodeList.Count - 1].Size.Width;
            tail.Location = location;

            ChangeMoveDirection(Direction.Left);
            isBirthed = true;
        }

        #region Move methods
        /// <summary>
        /// Change move direction of snake.
        /// Parallel to GoXX() methods.
        /// </summary>
        /// <param name="direction"></param>
        public bool ChangeMoveDirection(Direction direction)
        {
            bool isChangeDirection = true;
            if(Direction != Direction.Right && Direction!=Direction.Left)
            {
                if (direction == Direction.Right)
                {
                    movePoint.X = 1;
                    movePoint.Y = 0;
                }
                else if (direction == Direction.Left)
                {
                    movePoint.X = -1;
                    movePoint.Y = 0;
                }
                else isChangeDirection = false;
            }
            else
            {
                if (direction == Direction.Up)
                {
                    movePoint.X = 0;
                    movePoint.Y = -1;
                }
                else if (direction == Direction.Down)
                {
                    movePoint.X = 0;
                    movePoint.Y = 1;
                }
                else isChangeDirection = false;
            }
            if(isChangeDirection)
            {
                OnDirectionChanged(Direction);
            }
            return isChangeDirection;
        }
        
        /// <summary>
        /// Change the move direction of snake to up.
        /// Parallel to ChangeMoveDirection(Direction.Up).
        /// </summary>
        public void GoUp()
        {
            directionQueue.Enqueue(Direction.Up);
        }

        /// <summary>
        /// Change the move direction of snake to down.
        /// Parallel to ChangeMoveDirection(Direction.Down).
        /// </summary>
        public void GoDown()
        {
            directionQueue.Enqueue(Direction.Down);
        }
        
        /// <summary>
        /// Change the move direction of snake to left.
        /// Parallel to ChangeMoveDirection(Direction.Left).
        /// </summary>
        public void GoLeft()
        {
            directionQueue.Enqueue(Direction.Left);
        }

        /// <summary>
        /// Change the move direction of snake to right.
        /// Parallel to ChangeMoveDirection(Direction.Right).
        /// </summary>
        public void GoRight()
        {
            directionQueue.Enqueue(Direction.Right);
        }

        /// <summary>
        /// Moving the snake forwards by his diection.
        /// </summary>
        public void Move()
        {
            Region changedReg = new Region();
            changedReg.MakeEmpty();
            Direction direction = Head.Direction;
            bool isChangeDirection = false;
            if (directionQueue.Count>0)
            {
                direction = directionQueue.Dequeue();
                isChangeDirection = ChangeMoveDirection(direction);
            }
            int lastNodeIndex = nodeList.Count - 1;
            ///MOVE THE LAST NODE TO LOCATION OF HEAD
            INode node = nodeList[lastNodeIndex];
            nodeList.RemoveAt(lastNodeIndex);
            node.Location = HeadLocation;
            node.ChangeDirection(head.Direction);
            nodeList.Insert(0, node);
            changedReg.Union(head.GetRegion());
            ///HEAD
            Point location = HeadLocation;
            location.X += NodePrototype.Size.Width * movePoint.X;
            location.Y += NodePrototype.Size.Height * movePoint.Y;
            head.Location = location;
            if(isChangeDirection) head.Direction = direction;
            changedReg.Union(head.GetRegion());

            ///TAIL
            node = nodeList[lastNodeIndex];
            location = node.Location;
            switch(node.Direction)
            {
                case Direction.Up:
                    location.Y += node.Size.Height;
                    break;
                case Direction.Down:
                    location.Y -= node.Size.Height;
                    break;
                case Direction.Right:
                    location.X -= node.Size.Width;
                    break;
                case Direction.Left:
                    location.X += node.Size.Width;
                    break;
            }
            if (!tail.Location.Equals(location))
            {
                changedReg.Union(tail.GetRegion());
                tail.Location = location;
                tail.ChangeDirection(node.Direction);
                changedReg.Union(tail.GetRegion());
            }
            OnMoved(changedReg);
        }
        #endregion

        /// <summary>
        /// Eat the snake with food.
        /// The snake will grow.
        /// </summary>
        /// <param name="food"></param>
        public void Eat(IFood food)
        {
            AddNodes(food.SumOfNodesForSnake);
            OnEated(food);
        }

        /// <summary>
        /// Add nodes to snake
        /// </summary>
        /// <param name="sum"></param>
        private void AddNodes(int sum)
        {
            INode node;
            Point location;
            if (nodeList.Count > 0) location = nodeList[nodeList.Count-1].Location;
            else location = head.Location;
            for (int i = 0; i<sum; i++)
            {
                node = (INode)nodePrototype.Clone();
                node.Location = location;
                nodeList.Add(node);
            }
        }
       
        /// <summary>
        /// Kill the snake.
        /// convet IsBirthed to false too.
        /// </summary>
        public void KillSnake()
        {
            isBirthed = false;
            directionQueue.Clear();
            head.Direction = Direction.None;
            OnKilled(GetEventArgs());
        }

        /// <summary>
        /// Check if the snake hit himself
        /// </summary>
        /// <returns></returns>
        public bool IsHitHimself()
        {
            if (IsHitSomething(tail)) return true;
            foreach (INode node in nodeList)
            {
                if (IsHitSomething(node)) return true;
            }
            return false;
        }


        /// <summary>
        /// Check if the snake hit something
        /// </summary>
        /// <param name="rect">Rectangle of the item that maybe the snake hit</param>
        /// <returns></returns>
        public bool IsHitSomething(Rectangle rect)
        {
            return head.GetRegion().IsVisible(rect);
        }
       
        /// <summary>
        /// Check if the snake hit something
        /// </summary>
        /// <param name="reg">Region of the item that maybe the snake hit</param>
        /// <returns></returns>
        public bool IsHitSomething(Region reg)
        {
            return reg.IsVisible(new Rectangle(head.Location, head.Size));
        }
        
        /// <summary>
        /// Check if the snake hit something
        /// </summary>
        /// <param name="reg">The reg that maybe the snake hit</param>
        /// <returns></returns>
        public bool IsHitSomething(IGetRegion reg)
        {
            return IsHitSomething(reg.GetRegion());
        }
       
        /// <summary>
        /// Return event args of snake
        /// </summary>
        /// <returns></returns>
        public SnakeEventArgs GetEventArgs()
        {
            return new SnakeEventArgs(HeadLocation, TailLocation, SumOfNodes, Direction);
        }

        public object Clone()
        {
            Snake snake = new SnakeBody.Snake();
            snake.head = (INode)Head.Clone();
            snake.tail = (INode)tail.Clone();
            snake.nodePrototype = (INode)nodePrototype.Clone();
            snake.nodeList = nodeList.ToList();
            snake.ChangeMoveDirection(Direction);
            snake.isBirthed = isBirthed;
            return snake;
        }

        public Region GetRegion()
        {
            Region reg = new Region();
            reg.MakeEmpty();
            foreach (INode node in nodeList) reg.Union(node.GetRegion());
            reg.Union(head.GetRegion());
            reg.Union(tail.GetRegion());
            return reg;
        }

        public void Draw(Graphics g)
        {
            head.Draw(g);
            foreach (INode node in nodeList) node.Draw(g);
            tail.Draw(g);
        }
        #endregion

        #region Events method
        /// <summary>
        /// Occurs when direction changed
        /// </summary>
        /// <param name="direction"></param>
        protected virtual void OnDirectionChanged(Direction direction)
        {

            DirectionChanged?.Invoke(this, direction);
            OnLookChanged(this, new EventArgs());
        }

        /// <summary>
        /// Occures before head change.
        /// </summary>
        protected virtual void OnHeadBeforeChange(INode head)
        {
            ResetHeadEvents();
            HeadBeforeChange?.Invoke(this, head);
        }

        /// <summary>
        /// Occures when head changed.
        /// </summary>
        protected virtual void OnHeadChanged(INode head)
        {
            RestartHeadEvents();
            HeadChanged?.Invoke(this, head);
        }

        /// <summary>
        /// Occures before node prototype change.
        /// </summary>
        protected virtual void OnNodePrototypeBeforeChanged(INode node)
        {
            ResetNodePrototypeEvents();
            NodePrototypeBeforeChanged?.Invoke(this, head);
        }

        /// <summary>
        /// Occures when NodePrototype changed.
        /// </summary>
        protected virtual void OnNodePrototypeChanged(INode node)
        {
            RestartNodePrototypeEvents();
            NodePrototypeChanged?.Invoke(this, node);
        }

        /// <summary>
        /// Occures before tail change.
        /// </summary>
        protected virtual void OnTailBeforeChanged(INode tail)
        {
            ResetTailEvents();
            TailBeforeChanged?.Invoke(this, tail);
        }

        /// <summary>
        /// Occures when tail changed.
        /// </summary>
        protected virtual void OnTailChanged(INode tail)
        {
            RestartTailEvents();
            TailChanged?.Invoke(this, tail);
        }

        /// <summary>
        /// Occures when snake eated.
        /// </summary>
        protected virtual void OnEated(IFood food)
        {

            Eated?.Invoke(this, food);
        }

        /// <summary>
        /// Occures when drew
        /// </summary>
        protected virtual void OnBirthed(SnakeEventArgs e)
        {

            Birthed?.Invoke(this, GetEventArgs());
        }

        /// <summary>
        /// Occures when snake moved
        /// </summary>
        protected virtual void OnMoved(Region regMoved)
        {

            Moved?.Invoke(this, regMoved);
        }

        /// <summary>
        /// Occures when snake hit something
        /// </summary>
        protected virtual void OnHitSomething(Region reg)
        {

            HitSomething?.Invoke(this, reg);
        }

        /// <summary>
        /// Occurs when look changed
        /// </summary>
        protected virtual void OnLookChanged(object sender, EventArgs e)
        {

            LookChanged?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when drew
        /// </summary>
        protected virtual void OnDrew(EventArgs e)
        {

            Drew?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when snake killed
        /// </summary>
        protected virtual void OnKilled(SnakeEventArgs e)
        {

            Killed?.Invoke(this, e);
        }

        /// <summary>
        /// Occurs when size of node changed
        /// </summary>
        /// <param name="size"></param>
        protected virtual void OnSizeOfNodeChanged(Size size)
        {
            nodePrototype.Size = size;
            head.Size = size;
            tail.Size = size;
            SizeOfNodeChanged?.Invoke(this, size);
            OnLookChanged(this, new EventArgs());
        }

        /// <summary>
        /// Occurs when nod prototype look changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void NodePrototype_OnLookChanged(object sender, EventArgs e)
        {
            INode node = (INode)nodePrototype.Clone();
            for (int i = 0; i < nodeList.Count; i++)
            {
                node.Location = nodeList[i].Location;
                node.ChangeDirection(nodeList[i].Direction);
                nodeList[i] = (INode)node.Clone();
            }
        }
        #endregion

        #region Restarts event
        private void ResetHeadEvents()
        {
            if(Head !=null)
            {
                head.LookChanged -= OnLookChanged;
            }
        }

        private void RestartHeadEvents()
        {
            if (Head != null)
            {
                head.LookChanged += OnLookChanged;
            }
        }

        private void ResetNodePrototypeEvents()
        {
            if (NodePrototype != null)
            {
                nodePrototype.LookChanged -= NodePrototype_OnLookChanged;
            }
        }

        private void RestartNodePrototypeEvents()
        {
            if (NodePrototype != null)
            {
                nodePrototype.LookChanged += NodePrototype_OnLookChanged;
            }
        }

        private void ResetTailEvents()
        {
            if (Tail != null)
            {
                tail.LookChanged -= OnLookChanged;
            }
        }

        private void RestartTailEvents()
        {
            if (Tail != null)
            {
                tail.LookChanged += OnLookChanged;
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Occures before head change.
        /// </summary>
        public event EventHandler<INode> HeadBeforeChange;
        /// <summary>
        /// Occures when head changed.
        /// </summary>
        public event EventHandler<INode> HeadChanged;
        /// <summary>
        /// Occures before node prototype change.
        /// </summary>
        public event EventHandler<INode> NodePrototypeBeforeChanged;
        /// <summary>
        /// Occures when NodePrototype changed.
        /// </summary>
        public event EventHandler<INode> NodePrototypeChanged;
        /// <summary>
        /// Occures before tail change.
        /// </summary>
        public event EventHandler<INode> TailBeforeChanged;
        /// <summary>
        /// Occures when tail changed.
        /// </summary>
        public event EventHandler<INode> TailChanged;
        /// <summary>
        /// Occures when snake eated.
        /// </summary>
        public event EventHandler<IFood> Eated;
        /// <summary>
        /// Occures when drew
        /// </summary>
        public event EventHandler<SnakeEventArgs> Birthed;
        /// <summary>
        /// Occurs when direction of node changed
        /// </summary>
        public event EventHandler<Direction> DirectionChanged;
        /// <summary>
        /// Occures when snake moved
        /// </summary>
        public event EventHandler<Region> Moved;
        /// <summary>
        /// Occures when snake hit something
        /// </summary>
        public event EventHandler<Region> HitSomething;
        /// <summary>
        /// Occurs when look changed
        /// </summary>
        public event EventHandler LookChanged;
        /// <summary>
        /// Occurs when drew
        /// </summary>
        public event EventHandler Drew;
        /// <summary>
        /// Occurs when snake killed
        /// </summary>
        public event EventHandler<SnakeEventArgs> Killed;
        /// <summary>
        /// Occurs when size of node changed
        /// </summary>
        public event EventHandler<Size> SizeOfNodeChanged;
        #endregion

    }
}
