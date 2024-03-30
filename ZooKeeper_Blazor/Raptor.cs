using System;

namespace ZooKeeper_Blazor
{
    public class Raptor : Bird, IPredator
    {
        public Raptor(string name)
        {
            emoji = "🦅";
            species = "raptor";
            this.name = name;
            reactionTime = 1; // reaction time 1 (fast)
        }

        public override void Activate()
        {
            base.Activate();
            Console.WriteLine("I am a raptor. RAAAAAAAAA 'Murica RAAAAAAA ");
            turnsSinceLastHunt++;
            TaskProcess();
        }

        public void TaskProcess()
        {
            TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "cat");
            if (TaskCheck == false)
            {
                TaskCheck = (this as IPredator).Hunt(this, location.x, location.y, "mouse");
                if (TaskCheck == false)
                {
                    TaskCheck = Fly(this, location.x, location.y, 2);
                    if (TaskCheck == false)
                    {
                        Walkabout(location.x, location.y);
                    }
                }
            }
            TurnCheck = true;
        }
    }
}