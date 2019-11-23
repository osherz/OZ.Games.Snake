using Shapes;
using SnakeGame.Food;
using SnakeGame.Food.Interface;
using SnakeGame.Properties;
using SnakeGame.SnakeBoard;
using SnakeGame.SnakeBody;
using SnakeGame.SnakeBody.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGame
{
    public partial class MainGame : Form
    {
        Board board;
        Snake snake;
        NodeCircle head;
        NodeCircle tail;
        NodeCircle nodePrototype;
        FoodSeq food;
        Border border;
        Size nodeSize = new Size(15, 15);
        Rectangle scoreRec = new Rectangle();
        int score = 0;

        public MainGame()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            board = new SnakeBoard.Board();
            snake = new SnakeBody.Snake();
            head = new SnakeBody.NodeCircle();
            tail = new NodeCircle();
            nodePrototype = new NodeCircle();
            food = new FoodSeq();
            border = new Border();

            Controls.Add(board);
            board.Parent = this;
            snake.Eated += new EventHandler<IFood>(Snake_OnEated);
            board.EndGame += new EventHandler(Bourd_OnEndGame);
        }

        private void Snake_OnEated(object sender, IFood e)
        {
            score += e.SumOfNodesForSnake * 10 * (int)speedNum.Value;
            Size size = TextRenderer.MeasureText(CreateGraphics(), score.ToString(), Font);
            scoreRec.Size = size;
            scoreRec.Location = new Point((board.Width - size.Width) / 2 + board.Left, board.Top + board.Height - 1);
            Invalidate(scoreRec);
        }

        private void MainGame_Load(object sender, EventArgs e)
        {
            buttonPause.Text = "השהה";

            board.Size = new Size(nodeSize.Width * 30, nodeSize.Height * 30);
            Size size = SizeOfWindow();
            Width = size.Width;
            Height = size.Height;
            board.Left = (Width - board.Width) / 2  - 7;
            board.Top = 30;
            this.MinimumSize = size;
            this.MaximumSize = size;
            head.Color = Color.Black;
            head.Size = nodeSize;
            head.NodeStyle = ShapeStyle.fill;

            tail.Color = Color.DarkGray;
            tail.Size = nodeSize;
            tail.NodeStyle = ShapeStyle.fill;

            nodePrototype.Color = Color.DarkSlateGray;
            nodePrototype.Size = nodeSize;
            nodePrototype.NodeStyle = ShapeStyle.fill;
            //nodePrototype.PointsColor = Color.Blue;

            snake.Head = head;
            snake.Tail = tail;
            snake.NodePrototype = nodePrototype;

            border.Brush = new HatchBrush(HatchStyle.DarkVertical, Color.Blue);
            border.Size = new Size(board.Width, nodeSize.Height);

            food.Color = Color.Purple;
            food.Size = new Size(10, 10);
            food.SumOfNodesForSnake = 1;
            while(board.SumOfFoods<5) board.AddFood((IFood)food.Clone());
            food.Color = Color.Red;
            food.SumOfNodesForSnake = 5;
            board.AddFood((IFood)food.Clone());

            board.Snake = snake;
            board.BorderPrototype_Top = border;
            board.BackColor = Color.Black;

            //snake.Birth(1, nodeSize.Width * 15, nodeSize.Height * 15);
            head.Location = new Point(nodeSize.Width * 15, nodeSize.Height * 15);
            head.Draw(CreateGraphics());
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Size size = TextRenderer.MeasureText(e.Graphics, score.ToString(), Font);
            TextRenderer.DrawText(e.Graphics, score.ToString(), Font,
                scoreRec, SystemColors.ActiveBorder);
            base.OnPaint(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            score = 0;
            Invalidate(true);
            board.Focus();
            board.SpeedOfSnake = 500/(int)speedNum.Value;
            board.RestartGame(1, new Point(nodeSize.Width * 15, nodeSize.Height * 15));
            buttonPause.Enabled = true;
            labelEndMessage.Visible = false;
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (board.IsStartGame)
            {
                board.PauseGame();
                buttonPause.Text = "המשך";
            }
            else
            {
                board.ContinueGame();
                buttonPause.Text = "השהה";
                board.Focus();
            }
        }

        private void Bourd_OnEndGame(object sender, EventArgs e)
        {
            buttonPause.Enabled = false;
            buttonPause.Text = "השהה";
            StringBuilder str = new StringBuilder();
            str.AppendFormat("הניקוד שלך הוא {0}", score);
            str.AppendLine();
            str.Append("בהצלחה בפעם הבאה :)");
            labelEndMessage.Text = str.ToString();
            labelEndMessage.Left = (Width - labelEndMessage.Width) / 2;
            labelEndMessage.Top = (Height - labelEndMessage.Height) / 2;
            labelEndMessage.Visible = true;
        }

        private void ButtonsToMiddle()
        {
            int buttonsWidth = buttonPause.Width + buttonPause.Margin.Left +
                speedNum.Margin.Right + speedNum.Width + speedNum.Margin.Left +
                buttonStartGame.Margin.Right + buttonStartGame.Width;
            int x = (Width - buttonsWidth) / 2;
            buttonPause.Left = x;
            //buttonPause.Top += 2;
            x += buttonPause.Width + buttonPause.Margin.Right + speedNum.Margin.Right;
            speedNum.Left = x;
            //speedNum.Top += 2;
            x += speedNum.Width + speedNum.Margin.Right + buttonStartGame.Margin.Left;
            buttonStartGame.Left = x;
            //buttonStartGame.Top += 2;
        }

        protected override void OnResize(EventArgs e)
        {
            ButtonsToMiddle();
            base.OnResize(e);
        }

        private Size SizeOfWindow()
        {
            int width = board.Width + 60;
            int height = board.Height + 150;
            return new Size(width, height);
        }

        private void MainGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space) buttonPause.PerformClick();
        }
    }
}
