using SnakeGame.CommonInterface;
using SnakeGame.SnakeBoard.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SnakeGame.SnakeBoard
{
    public class Borders : IDraw, IGetRegion
    {
        IBorder[] bordersArry;

        public event EventHandler Drew;

        public Borders()
        {
            bordersArry = new IBorder[4];
        }

        public IBorder TopBorder
        {
            get
            {
                return bordersArry[0];
            }

            set
            {
                bordersArry[0] = value;
            }
        }

        public IBorder BottomBorder
        {
            get
            {
                return bordersArry[1];
            }

            set
            {
                bordersArry[1] = value;
            }
        }

        public IBorder LeftBorder
        {
            get
            {
                return bordersArry[2];
            }

            set
            {
                bordersArry[2] = value;
            }
        }

        public IBorder RightBorder
        {
            get
            {
                return bordersArry[3];
            }

            set
            {
                bordersArry[3] = value;
            }
        }

        public IBorder this[int index]
        {
            get
            {
                return bordersArry[index];
            }

            set
            {
                bordersArry[index] = value;
            }
        }

        public void Draw(Graphics g)
        {
            foreach(IBorder border in bordersArry)
            {
                border.Draw(g);
            }
            OnDrew(new EventArgs());
        }

        protected virtual void OnDrew(EventArgs e)
        {
            Drew?.Invoke(this, e);
        }

        public Region GetRegion()
        {
            Region reg = new Region();
            reg.MakeEmpty();
            foreach (IBorder border in bordersArry) reg.Union(border.GetRegion());
            return reg;
        }
    }
}
