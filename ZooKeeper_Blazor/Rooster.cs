using System;
namespace ZooKeeper_Blazor
{
	public class Rooster : Bird, IPredator, IPrey
    {
		public Rooster(string name)
        {
            emoji = "🐓";
            species = "rooster";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(3, 6);
        }

        public override void Activate()
        {
            base.Activate();
            turnsSinceLastHunt++;
            TaskProcess();
            Console.WriteLine("I am a rooster. Bacaw!");
        }

        public void TaskProcess() // Priority is to flee over hunt
        {
            TaskCheck = (this as IPrey).Flee(this, location.x, location.y, "cat", 1);
            if (TaskCheck == false)
            {
                TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "grass");
                if (TaskCheck == false)
                {
                    Walkabout(location.x, location.y);
                }
            }
            TurnCheck = true;
        }
    }
}

