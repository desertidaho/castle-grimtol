using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }

    public Dictionary<Direction, IRoom> NearbyRooms { get; set; }


    public void AddNearbyRoom(Direction direction, IRoom room)
    {
      NearbyRooms.Add(direction, room);
    }

    public IRoom MoveToRoom(Direction direction)
    {
      if (NearbyRooms.ContainsKey(direction))
      {
        return NearbyRooms[direction];

      }
      Console.WriteLine("You cannot go that way! \n ");
      return (IRoom)this;
    }


    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      NearbyRooms = new Dictionary<Direction, IRoom>();
      Items = new List<Item>();
    }
  }

  public enum Direction
  {
    forward,
    aft,
    port,
    starboard
  }
}