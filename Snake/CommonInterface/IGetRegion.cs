using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.CommonInterface
{
    public interface IGetRegion
    {
        /// <summary>
        /// Return region of snake
        /// </summary>
        /// <returns></returns>
        Region GetRegion();

    }
}
