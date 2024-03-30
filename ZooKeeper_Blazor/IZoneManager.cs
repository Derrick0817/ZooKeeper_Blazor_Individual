using System;
using ZooKeeper_Blazor;

public interface IZoneManager
{
    void AddZoneWhenFull();
    void AddZones(Direction d);
    string CreateRandomDirection();
    bool IsWin();
}
