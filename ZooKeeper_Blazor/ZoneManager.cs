using System;
using ZooKeeper_Blazor;

public class ZoneManager :  IZoneManager
{
    
    public void AddZoneWhenFull()
    {
        //recurring the current zone, if any of the zone is empty, which means that it is not full of animal, return
        for (int i = 0; i < Game.animalZones.Count; i++)
        {
            for (int j = 0; j < Game.animalZones[i].Count; j++)
            {
                //if any zone is empety, 
                if (Game.animalZones[i][j].occupant == null)
                {
                    Console.WriteLine("Still has some zones are empty");
                    return;
                }
            }
        }

        switch (Game.directionIndex)
        {
            case 1:
                AddZones(Direction.up);
                CreateRandomDirection();
                Console.WriteLine("up,yep");
                break;
            case 2:
                AddZones(Direction.down);
                CreateRandomDirection();
                Console.WriteLine("down,yep");
                break;
            case 3:
                AddZones(Direction.left);
                CreateRandomDirection();
                Console.WriteLine("left,yep");
                break;
            case 4:
                AddZones(Direction.right);
                CreateRandomDirection();
                Console.WriteLine("right,yep");
                break;
            default:
                break;
        }
    }

    public void AddZones(Direction d)
    {
        if (d == Direction.down || d == Direction.up)
        {
            if (Game.numCellsY >= Game.maxCellsY) return; // hit maximum height!
            List<Zone> rowList = new List<Zone>();//Creating row list
            for (var x = 0; x < Game.numCellsX; x++)
            {
                rowList.Add(new Zone(x, Game.numCellsY, null));
            }
            Game.numCellsY++;
            if (d == Direction.down) Game.animalZones.Add(rowList);
            if (d == Direction.up) Game.animalZones.Insert(0, rowList);
        }
        else // must be left or right...
        {
            if (Game.numCellsX >= Game.maxCellsX) return; // hit maximum width!
            for (var y = 0; y < Game.numCellsY; y++)
            {
                var rowList = Game.animalZones[y];
                if (d == Direction.left) rowList.Insert(0, new Zone(Game.numCellsX, y, null));
                if (d == Direction.right) rowList.Add(new Zone(Game.numCellsX, y, null));
            }
            Game.numCellsX++;
        }
    }

    public string CreateRandomDirection()
    {
        Random randomIndex = new Random();
        string direction;
        //X left and right Y up and down
        if (Game.numCellsY < Game.maxCellsY && Game.numCellsX < Game.maxCellsX)
        {
            Game.directionIndex = randomIndex.Next(1, 4);
        }
        else if (Game.numCellsX >= Game.maxCellsX && Game.numCellsY <= Game.maxCellsY)
        {
            Game.directionIndex = randomIndex.Next(1, 2);
        }
        else if (Game.numCellsX < Game.maxCellsX && Game.numCellsY >= Game.maxCellsY)
        {
            Game.directionIndex = randomIndex.Next(3, 4);
        }


        switch (Game.directionIndex)
        {
            case 1:
                direction = "UP";
                return direction;
                
            case 2:
                direction = "DOWN";
                return direction;
                
            case 3:
                direction = "LEFT";
                return direction;
                
            case 4:
                direction = "RIGHT";
                return direction;
                
            default:
                direction = "NULL(Reached Maximum)";
                return direction;
                
        }
    }

    public bool IsWin()
    {
        if (Game.numCellsX >= Game.maxCellsX && Game.numCellsY >= Game.maxCellsY)
        {
            //recurring the current zone, if any of the zone is empty, which means that it is not full of animal, return
            for (int i = 0; i < Game.animalZones.Count; i++)
            {
                for (int j = 0; j < Game.animalZones[i].Count; j++)
                {
                    //if any zone is empety, 
                    if (Game.animalZones[i][j].occupant == null)
                    {
                        Console.WriteLine("Still has some zones are empty");
                        return false;
                    }
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}
