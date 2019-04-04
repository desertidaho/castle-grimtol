using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }
    public void PrintInventory(List<Item> items)
    {
      for (int i = 0; i < items.Count; i++)
      {
        Console.WriteLine(value: $"{i + 1}. {items[i].Name} by {items[i].Description}");
      }
    }
  }

}