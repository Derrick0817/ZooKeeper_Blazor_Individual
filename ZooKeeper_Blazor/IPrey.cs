using System;

namespace ZooKeeper_Blazor
{
	public interface IPrey
	{
        // i'm not sure if your Flee() method is necessary for your part,
        // i'll just put my methods about flee below your code in this file
        public bool Flee(Animal prey, int x, int y, string predator, int distance)
        {
            if (Game.Seek(x, y, Direction.up, predator, 1))
            {
                if (Game.Seek(x, y, Direction.up, "null", 1)) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null", 1))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null", 1))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null", 1))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.down, predator, 1))
            {
                if (Game.Seek(x, y, Direction.up, "null", 1)) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null", 1))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null", 1))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null", 1))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.left, predator, 1))
            {
                if (Game.Seek(x, y, Direction.up, "null", 1)) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null", 1))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null", 1))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null", 1))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            if (Game.Seek(x, y, Direction.right, predator, 1))
            {
                if (Game.Seek(x, y, Direction.up, "null", 1)) // check all directions for fleeing
                {
                    if (Game.Move(prey, Direction.up, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.down, "null", 1))
                {
                    if (Game.Move(prey, Direction.down, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.left, "null", 1))
                {
                    if (Game.Move(prey, Direction.left, distance) > 0) return true;
                }
                if (Game.Seek(x, y, Direction.right, "null", 1))
                {
                    if (Game.Move(prey, Direction.right, distance) > 0) return true;
                }
                return false; // can't run
            }
            return false; // nothing to flee
        }
    }
}

