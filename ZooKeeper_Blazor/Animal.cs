using System;

namespace ZooKeeper_Blazor
{
    public class Animal : Occupant
    {
        public string name;
        public int reactionTime = 5; // default reaction time for animals (1 - 10)
        public bool TaskCheck;
        public bool TurnCheck;

        public int turnsSinceLastHunt { get; set; } = 0;//track the non-eating turn

        virtual public void Activate()
        {
            Console.WriteLine($"Animal {name} at {location.x},{location.y} activated");
        }

        public bool Walkabout(int x, int y)
        {
            Random random = new Random();
            List<Direction> possibleDirections = new List<Direction> { Direction.up, Direction.down, Direction.left, Direction.right };

            if (!Game.Seek(x, y, Direction.up, "null", 1)) // check all directions for ability to wander in
            {
                possibleDirections.Remove(Direction.up);
            }
            if (!Game.Seek(x, y, Direction.down, "null", 1))
            {
                possibleDirections.Remove(Direction.down);
            }
            if (!Game.Seek(x, y, Direction.left, "null", 1))
            {
                possibleDirections.Remove(Direction.right);
            }
            if (!Game.Seek(x, y, Direction.right, "null", 1))
            {
                possibleDirections.Remove(Direction.left);
            }

            if (possibleDirections.Count > 0)
            {
                Direction moveDirection = possibleDirections[random.Next(possibleDirections.Count)];

                if (Game.Move(this, moveDirection, 1) > 0) return true;
            }
            return false;
        }
    }
}
