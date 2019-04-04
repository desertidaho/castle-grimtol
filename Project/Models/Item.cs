using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Item
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public IRoom Room { get; set; }
    public List<Item> Items { get; set; }



    public Item(string name, string description, IRoom room)
    {
      Name = name;
      Description = description;
      Room = room;
    }

  }
}