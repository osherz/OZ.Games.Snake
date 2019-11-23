using Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame.Food
{
    class FoodCircle : RegFood<Circle>
    {
        public FoodCircle(): base()
        {
            foodShape = new Circle();
        }
    }
}
