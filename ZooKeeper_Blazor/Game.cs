using System;
using System.Collections.Generic;
using System.Text;

namespace ZooKeeper_Blazor
{
    public static class Game
    {
        static public int numCellsX = 4;
        static public int numCellsY = 4;

        //Changing to public because ZoneManager needs these propeties
        static public int maxCellsX = 10;
        static public int maxCellsY = 10;


        static public List<List<Zone>> animalZones = new List<List<Zone>>();
        static public Zone holdingPen = new Zone(-1, -1, null);
        static public int totalScore = 0;
        static public List<List<Animal>> activationList = new List<List<Animal>>();
        static public List<Animal> deadAnimal = new List<Animal>();
        static public Animal animalToAdd = new Animal();

        //New attributes, which will be used by ZoneManager
        static public int directionIndex;
        static public string direction = "";


        static public void SetUpGame()
        {
            ZoneManager zoneManager = new ZoneManager();
            for (var y = 0; y < numCellsY; y++)
            {
                List<Zone> rowList = new List<Zone>();
                // Note one-line variation of for loop below!
                for (var x = 0; x < numCellsX; x++) rowList.Add(new Zone(x, y, null));
                animalZones.Add(rowList);
            }
            //At the beginning of the game create a random direction
            direction = zoneManager.CreateRandomDirection();

            for (var i = 0; i < 10; i++)
            {
                activationList.Add(new List<Animal>());//list of List<Animal> with 10 different reaction time
                Console.WriteLine(activationList);
            }
        }

       //Since there is no need to add zones manually, so addzone will be deleted

        static public void ZoneClick(Zone clickedZone)
        {
            ScoreCalculator scoreCalculator = new ScoreCalculator();
            ZoneManager zoneManager = new ZoneManager();
            Console.Write("Got animal ");
            Console.WriteLine(clickedZone.emoji == "" ? "none" : clickedZone.emoji);
            Console.Write("Held animal is ");
            Console.WriteLine(holdingPen.emoji == "" ? "none" : holdingPen.emoji);
            if (clickedZone.occupant != null) clickedZone.occupant.ReportLocation();
            if (holdingPen.occupant == null && clickedZone.occupant != null)
            {
                // take animal from zone to holding pen
                Console.WriteLine("Taking " + clickedZone.emoji);
                holdingPen.occupant = clickedZone.occupant;
                holdingPen.occupant.location.x = -1;
                holdingPen.occupant.location.y = -1;
                clickedZone.occupant = null;
                ActivateAnimals();
                totalScore = scoreCalculator.CalculateTotalScore(animalZones);

                zoneManager.AddZoneWhenFull();//Adding new zone should be executed after all animals finish their actions
                if (zoneManager.IsWin())
                {
                    Console.WriteLine("Player reached the goal");
                    return;
                }
            }
            else if (holdingPen.occupant != null && clickedZone.occupant == null)
            {
                // put animal in zone from holding pen
                Console.WriteLine("Placing " + holdingPen.emoji);
                clickedZone.occupant = holdingPen.occupant;
                clickedZone.occupant.location = clickedZone.location;
                holdingPen.occupant = null;
                Console.WriteLine("Empty spot now holds: " + clickedZone.emoji);
                ActivateAnimals();
                totalScore = scoreCalculator.CalculateTotalScore(animalZones);

                zoneManager.AddZoneWhenFull();//Adding new zone should be excute after all animals finished their actions
                if (zoneManager.IsWin())
                {
                    Console.WriteLine("Player reached the goal");
                    return;
                }
            }
            else if (holdingPen.occupant != null && clickedZone.occupant != null)
            {
                Console.WriteLine("Could not place animal.");
                // Don't activate animals since user didn't get to do anything
            }
        }

        static public void AddToHolding(string occupantType)
        {
            ZoneManager zoneManager = new ZoneManager();
            if (holdingPen.occupant != null) return;
            if (occupantType == "cat")
            {
                animalToAdd = new Cat("Fluffy");
                holdingPen.occupant = animalToAdd;
                activationList[animalToAdd.reactionTime - 1].Add(animalToAdd);
                Console.WriteLine(animalToAdd.emoji + "added to list" + (animalToAdd.reactionTime - 1));
                animalToAdd = null;
            }
            if (occupantType == "mouse")
            {
                animalToAdd = new Mouse("Squeaky");
                holdingPen.occupant = animalToAdd;
                activationList[animalToAdd.reactionTime - 1].Add(animalToAdd);
                Console.WriteLine(animalToAdd.emoji + "added to list" + (animalToAdd.reactionTime - 1));
                animalToAdd = null;
            }
            if (occupantType == "raptor")
            {
                animalToAdd = new Raptor("Chance the Raptor");
                holdingPen.occupant = animalToAdd;
                activationList[animalToAdd.reactionTime - 1].Add(animalToAdd);
                Console.WriteLine(animalToAdd.emoji + "added to list" + (animalToAdd.reactionTime - 1));
                animalToAdd = null;
            } 
            if (occupantType == "chick")
            {
                animalToAdd = new Chick("Tweety (uncopyrighted)");
                holdingPen.occupant = animalToAdd;
                activationList[animalToAdd.reactionTime - 1].Add(animalToAdd);
                Console.WriteLine(animalToAdd.emoji + "added to list" + (animalToAdd.reactionTime - 1));
                animalToAdd = null;

            }
            if (occupantType == "rooster")
            {
                animalToAdd = new Rooster("Earl Wings");
                holdingPen.occupant = animalToAdd;
                activationList[animalToAdd.reactionTime - 1].Add(animalToAdd);
                Console.WriteLine(animalToAdd.emoji + "added to list" + (animalToAdd.reactionTime - 1));
                animalToAdd = null;
            }
            if (occupantType == "vulture")
            {
                holdingPen.occupant = new Vulture("Van Helswing");
                holdingPen.occupant = animalToAdd;
                activationList[animalToAdd.reactionTime - 1].Add(animalToAdd);
                Console.WriteLine(animalToAdd.emoji + "added to list" + (animalToAdd.reactionTime - 1));
                animalToAdd = null;
            }
            if (occupantType == "corpse") holdingPen.occupant = new Corpse();



            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
            Console.WriteLine($"Holding pen occupant at {holdingPen.occupant.location.x},{holdingPen.occupant.location.y}");
            //ActivateAnimals(); turns only occur when placed on the board now
            zoneManager.AddZoneWhenFull();//Keeping watching whether current is full and then adding new zone
        }

        static public void ActivateAnimals()
        {
            for (var r = 0; r < 10; r++)
            {
                foreach (Animal a in activationList[r])
                {                   
                    a.Activate(); //activate animal
                    if (a.turnsSinceLastHunt >5) //check for turn into corpse
                    {                       
                        deadAnimal.Add(a);  //add to deadAnimal to remove later
                        turnCorpse(a);     
                    }
                    mature(a); //if satisfy condition, mature         

                }

                foreach (Animal b in deadAnimal)
                {
                    activationList[b.reactionTime - 1].Remove(b);
                }

                activationList[r].Reverse();
            }
        }

        static public void turnCorpse(Animal a) //turn an animal into corpse
        {
            int x = a.location.x;
            int y = a.location.y;
            animalZones[y][x].occupant = new Corpse();
        }

        static public void mature(Animal a) //chick mature after 3 turns on the filed
        {
            Chick chick = a as Chick;

            if (chick != null && chick.totalTurns > 3) //grow up!!! and add new bird to the activationList
            {
                deadAnimal.Add(a);//remove old chick from activation list by add it to deadAnimal for removal
                Random random = new Random();
                int choice = random.Next(10);
                if (choice < 2)
                {
                    int x = a.location.x;
                    int y = a.location.y;
                    Raptor raptor = new Raptor("raptor");
                    animalZones[y][x].occupant = raptor;
                    activationList[raptor.reactionTime - 1].Add(raptor);
                }
                else if (choice < 7) // The probability of a rooster is 1 in 2
                {
                    int x = a.location.x;
                    int y = a.location.y;
                    Rooster rooster = new Rooster("rooster");
                    animalZones[y][x].occupant = rooster;
                    activationList[rooster.reactionTime - 1].Add(rooster);
                }
                else // The remaining 1/3 probability is allocated to Vultures
                {
                    int x = a.location.x;
                    int y = a.location.y;
                    Vulture vulture = new Vulture("vulture");
                    animalZones[y][x].occupant = vulture;
                    activationList[vulture.reactionTime - 1].Add(vulture);
                }
            }
        }

        static public bool Seek(int x, int y, Direction d, string target, int distance)
        {
            if (target == "null") // Searching for an empty spot
            {
                switch (d)
                {
                    case Direction.up:
                        y = y - distance;
                        break;
                    case Direction.down:
                        y = y + distance;
                        break;
                    case Direction.left:
                        x = x - distance;
                        break;
                    case Direction.right:
                        x = x + distance;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return true;
            }
            else
            {
                switch (d)
                {
                    case Direction.up:
                        y = y - distance;
                        break;
                    case Direction.down:
                        y = y + distance;
                        break;
                    case Direction.left:
                        x = x - distance;
                        break;
                    case Direction.right:
                        x = x + distance;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return false;
                if (animalZones[y][x].occupant == null) return false;
                if (animalZones[y][x].occupant.species == target)
                {
                    return true;
                }
            }
            return false;
        }

        static public int Move(Animal animal, Direction d, int distance)
        {
            int movedDistance = 0;
            int x = animal.location.x;
            int y = animal.location.y;

            for (int i = 0; i < distance; i++)
            {
                switch (d)
                {
                    case Direction.up:
                        y--;
                        break;
                    case Direction.down:
                        y++;
                        break;
                    case Direction.left:
                        x--;
                        break;
                    case Direction.right:
                        x++;
                        break;
                }
                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) break;
                if (animalZones[y][x].occupant == null)
                {
                    animalZones[animal.location.y][animal.location.x].occupant = null;
                    animalZones[y][x].occupant = animal;
                    movedDistance++;
                }
                else
                {
                    break;
                }
            }
            return movedDistance;
        }

        static public bool Attack(Animal attacker, Direction d)
        {
            Console.WriteLine($"{attacker.name} is attacking {d.ToString()}");
            int x = attacker.location.x;
            int y = attacker.location.y;

            switch (d)
            {
                case Direction.up:
                    if (animalZones[y - 1][x].occupant != null)
                    {
                        animalZones[y - 1][x].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.down:
                    if (animalZones[y + 1][x].occupant != null)
                    {
                        animalZones[y + 1][x].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.left:
                    if (animalZones[y][x - 1].occupant != null)
                    {
                        animalZones[y][x - 1].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
                case Direction.right:
                    if (animalZones[y][x + 1].occupant != null)
                    {
                        animalZones[y][x + 1].occupant = attacker;
                        animalZones[y][x].occupant = null;
                        return true; // hunt successful
                    }
                    return false;
            }
            return false; // nothing to hunt
        }

        static public bool Retreat(Animal runner, Direction d, int distance)
        {
            Console.WriteLine($"{runner.name} is retreating {d.ToString()}");
            int x = runner.location.x;
            int y = runner.location.y;

            switch (d)
            {
                case Direction.up:
                    if (y > 0 && animalZones[y - distance][x].occupant == null)
                    {
                        animalZones[y - distance][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false; // retreat was not successful
                case Direction.down:
                    if (y < numCellsY && animalZones[y + distance][x].occupant == null)
                    {
                        animalZones[y + distance][x].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
                case Direction.left:
                    if (x > 0 && animalZones[y][x - distance].occupant == null)
                    {
                        animalZones[y][x - distance].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
                case Direction.right:
                    if (x < numCellsX && animalZones[y][x + distance].occupant == null)
                    {
                        animalZones[y][x + distance].occupant = runner;
                        animalZones[y][x].occupant = null;
                        return true; // retreat was successful
                    }
                    return false;
            }
            return false; // cannot retreat
        }

        static public int SeekForMouse(int x, int y, Direction d, string target, int distance)
        {
            int squaresToNearest = 0;
            for (int i = 1; i <= distance; i++)
            {
                switch (d)
                {
                    case Direction.up:
                        y--;
                        break;
                    case Direction.down:
                        y++;
                        break;
                    case Direction.left:
                        x--;
                        break;
                    case Direction.right:
                        x++;
                        break;
                }

                if (y < 0 || x < 0 || y > numCellsY - 1 || x > numCellsX - 1) return 0;
                if (animalZones[y][x].occupant == null) return 0;
                if (animalZones[y][x].occupant.species == target)
                {
                    squaresToNearest = i;
                    return squaresToNearest;
                }
            }
            return 0;
        }
    }
}

