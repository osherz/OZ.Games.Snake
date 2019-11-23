using Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Food
{
    class FoodSeq : RegFood<Sequre>
    {
        public FoodSeq(): base()
        {
            foodShape = new Sequre();
        }
    }
}
