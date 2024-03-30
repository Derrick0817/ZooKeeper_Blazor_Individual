using System;

namespace ZooKeeper_Blazor
{
	public class Chick : Bird, IPrey, IPredator 
	{
        public Chick(string name)
        {
            emoji = "🐥";
            species = "chick";
            this.name = name; // "this" to clarify instance vs. method parameter
            reactionTime = new Random().Next(6, 10);
        }

        public int totalTurns { get; private set; } = 0;//track chick's total turns 

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a chick. Beepbeep.");
            turnsSinceLastHunt++;
            totalTurns++;
            TaskProcess();
        }

        public void TaskProcess()
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

