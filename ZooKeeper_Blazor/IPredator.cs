using System;

namespace ZooKeeper_Blazor
{
	public interface IPredator
	{
        public bool Hunt(Animal predator, int x, int y, string prey)
        {
            if (Game.Seek(x, y, Direction.up, prey, 1))
            {
                if (Game.Attack(predator, Direction.up))
                {
                    predator.turnsSinceLastHunt = 0;
                    return true;
                }
                return false;
            }
            else if (Game.Seek(x, y, Direction.down, prey, 1))
            {
                if (Game.Attack(predator, Direction.down))
                {
                    predator.turnsSinceLastHunt = 0;
                    return true;
                }
                return false;
            }
            else if (Game.Seek(x, y, Direction.left, prey, 1))
            {
                if (Game.Attack(predator, Direction.left))
                {
                    predator.turnsSinceLastHunt = 0;
                    return true;
                }
                return false;
            }
            else if (Game.Seek(x, y, Direction.right, prey, 1))
            {
                if (Game.Attack(predator, Direction.right))
                {
                    predator.turnsSinceLastHunt = 0;
                    return true;
                }
                return false;
            }
            return false; // nothing to hunt
        }
    }
}

