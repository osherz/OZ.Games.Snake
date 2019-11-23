using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.CommonInterface
{
    public interface IDraw
    {
        /// <summary>
        /// Draw node
        /// </summary>
        /// <param name="g"></param>
        void Draw(Graphics g);

        /// <summary>
        /// Occurs after node drew
        /// </summary>
        event EventHandler Drew;
    }
}
