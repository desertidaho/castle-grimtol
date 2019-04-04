using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project.Models
{
  public class Player : IPlayer
  {
    public string Name { get; set; }
    public List<Item> Inventory { get; set; }



    public void PrintInventory(List<Item> inventory)
    {
      for (int i = 0; i < inventory.Count; i++)
      {
        Console.WriteLine(value: $"{i + 1}. {inventory[i].Name} by {inventory[i].Description}");
      }
    }

    public void AddItem(Item item)
    {
      Inventory.Add(item);
    }


    public Player(string name)
    {
      Name = name;
      Inventory = new List<Item>();

    }

  }

}