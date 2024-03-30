using System;

namespace ZooKeeper_Blazor
{
    public class Mouse : Animal, IPrey, IPredator
    {
        public Mouse(string name)
        {
            emoji = "🐭";
            species = "mouse";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(1, 4); // reaction time of 1 (fast) to 3
            /* Note that Mouse reactionTime range is smaller than Cat reactionTime,
             * so mice are more likely to react to their surroundings faster than cats!
             Mouse*/
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a mouse. Squeak.");
            turnsSinceLastHunt++;
            TaskProcess();
            // in my part, my method to let the mouse flee two squares 
            // and can move different way in the second step is TotalFlee(), 
            // so i just put 
            // (this as IPrey).TotalFlee(this, location.x, location.y, "cat");
            // here instead of other things
        }

        public void TaskProcess() //To update
        {
            TaskCheck = TotalFlee(location.x, location.y, "raptor");
            if (TaskCheck == false)
            {
                TaskCheck = TotalFlee(location.x, location.y, "cat");
                if (TaskCheck == false)
                {
                    TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "grass");
                    if (TaskCheck == false)
                    {
                        Walkabout(location.x, location.y);
                    }
                }
            }
            TurnCheck = true;
        }

        public Direction Flee(int x, int y, string predator)
        {
            Random random = new Random();
            List<Direction> possibleDirections = new List<Direction> { Direction.up, Direction.down, Direction.left, Direction.right };
            int predators = 0;

            if (Game.SeekForMouse(x, y, Direction.up, predator, 1) > 0)
            {
                possibleDirections.Remove(Direction.up);
                predators++;
            }
            if (Game.SeekForMouse(x, y, Direction.down, predator, 1) > 0)
            {
                possibleDirections.Remove(Direction.down);
                predators++;
            }
            if (Game.SeekForMouse(x, y, Direction.left, predator, 1) > 0)
            {
                possibleDirections.Remove(Direction.left);
                predators++;
            }
            if (Game.SeekForMouse(x, y, Direction.right, predator, 1) > 0)
            {
                possibleDirections.Remove(Direction.right);
                predators++;
            }

            if (possibleDirections.Count > 0 && predators > 0)
            {
                Direction moveDirection = possibleDirections[random.Next(possibleDirections.Count)];
                return moveDirection;
            }
            else
            {
                return Direction.stay;
            }
        }

        public bool TotalFlee(int x, int y, string predator)
        {
            // make the first move
            Random random = new Random();
            Direction move = Flee(x, y, predator);
            if (Game.SeekForMouse(x, y, move, predator, 1) == 0)
            {
                Game.Move(this, move, 1);
            }

            // make sure the prey will not flee back to the original suqare
            List<Direction> possibleDirections = new List<Direction> { Direction.up, Direction.down, Direction.left, Direction.right };
            if (move == Direction.up)
            {
                possibleDirections.Remove(Direction.down);
            }
            else if (move == Direction.down)
            {
                possibleDirections.Remove(Direction.up);
            }
            else if (move == Direction.left)
            {
                possibleDirections.Remove(Direction.right);
            }
            else if (move == Direction.right)
            {
                possibleDirections.Remove(Direction.left);
            }

            // if the mouse can countinue moving, the direction will not change,
            // if cannot, it will choose a possible direction randomly.
            if (Game.SeekForMouse(x, y, move, predator, 2) == 0)
            {
                if (Game.Move(this, move, 1) > 0) return true;
            }
            else if (possibleDirections.Count > 0)
            {
                Direction moveDirection = possibleDirections[random.Next(possibleDirections.Count)];
                if (Game.Move(this, moveDirection, 1) > 0) return true;
            }
            return false;
        }
    }
}