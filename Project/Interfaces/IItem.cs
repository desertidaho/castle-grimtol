using System.Collections.Generic;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project.Interfaces
{
  public interface IItem
  {
    string Name { get; set; }
    string Description { get; set; }
    List<Item> Items { get; set; }
  }
}