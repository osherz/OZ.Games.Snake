using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.SnakeBoard.Interface
{
    public class BorderEventArgs : EventArgs
    {
        private IBorder border;
        private Place place;

        public IBorder Border
        {
            get
            {
                return border;
            }
        }

        public Place Place
        {
            get
            {
                return place;
            }
        }

        public BorderEventArgs(IBorder border, Place place)
        {
            this.border = border;
            this.place = place;
        }
    }
}
