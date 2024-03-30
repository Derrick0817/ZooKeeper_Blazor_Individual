using System;
namespace ZooKeeper_Blazor
{
	public class Vulture: Bird, IPredator
	{
		public Vulture(string name)
		{
            emoji = "🐦‍⬛";
            species = "vulture";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(2, 4);
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a vulture. Fear me!");
            turnsSinceLastHunt++;
            TaskProcess();
        }

        public void TaskProcess()
        {
            TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "corpse");
            if (TaskCheck == false)
            {
                TaskCheck = Fly(this, location.x, location.y, 2);
                if (TaskCheck == false)
                {
                    Walkabout(location.x, location.y);
                }
            }
            TurnCheck = true;
        }
    }
}

