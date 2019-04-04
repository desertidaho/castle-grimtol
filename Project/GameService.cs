using System;
using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {

    public Room CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Alive { get; set; }


    //Start the game
    public void Run()
    {
      CurrentPlayer = new Player("Sailor");
      Intro();
      while (Alive)
      {
        Console.WriteLine($"{CurrentRoom.Description}");
        Console.WriteLine($"\nWhat would you like to do? Type help for help.");
        string response = Console.ReadLine().ToLower();
        GetUserInput(response);
      }
    }

    private void Intro()
    {
      Console.WriteLine("Welcome to Tropical Tuesday.");
      Console.WriteLine(@"
         
                      |
                     /|\
                    / | \
                   /  |  \
                  /   |   \
                 /    |    \
                /     |     \
               /      |      \
              /       |       \
             /________|        \
                 _____|__    ___\_
          ______/ = = = =\__/__/_/
         /                      /
        /\_____________________/
       / /        /  /
      /_/        /__/");
      Console.WriteLine("\nYou're a successful freelance software developer and you live on a sailboat. \nYou spend your days cruising tropical waters, swimming, and writing eloquent JavaScript applications. \n  ");
      Console.WriteLine("Press any key to continue.");
      Console.ReadKey();
      Console.Clear();
      Console.WriteLine("It's a typical tropical Tuesday, so you set your autopilot and go below-deck to take a quick nap.");
      Console.WriteLine(@"
                                        
                                                      |
                                                     /|\
                                                    / | \
                                                   /  |  \
                                                  /   |   \
                                                 /    |    \
                                                /     |     \
                                               /      |      \
                                              /       |       \
                                             /________|        \
                                                 _____|__    ___\_
                                          ______/ = = = =\__/__/_/
                                         /                      /
                                        /\_____________________/
                                       / /        /  /
                                      /_/        /__/");

      Console.WriteLine("\nPress any key to continue.");
      Console.ReadKey();

      Console.Clear();
      Console.WriteLine("You're dreaming of code when you hear a loud crashing noise that wakes you abruptly.");
      Console.WriteLine(@"
                                        
                                                                              |
                                                                             /|\
                                                                            / | \
                                                                           /  |  \
                                                                          /   |   \
                                                                         /    |    \
                                                                        /     |     \
                                                                       /      |      \
                                                                      /       |       \
                                                                     /________|        \
                                                                         _____|__    ___\_
                                                                  ______/ = = = =\__/__/_/
                                                                 /                      /
                                                                /\__________________************            
                                                               / /        /  /      ************
                                                              /_/        /__/");

      Console.WriteLine("\nYou wake up confused but quickly realize something has hit the bow or your boat and there's water rushing in.");
      Console.WriteLine("\nPress any key to continue.");
      Console.ReadKey();


      Initialize();
    }

    //Initialize rooms, their exits, and add items to rooms
    public void Initialize()
    {
      Room aft = new Room("aft cabin", "You're in the aft cabin, this is where you were sleeping. The only thing here is your bed, clothes, and personal items. There's a small window to port, but too small for you to squeeze out of. The only door leads forward.");
      Room passageway = new Room("passageway", "You're in the passageway, which runs forward and aft. There's water on the deck and the water level is rising quickly. \nFrom the passageway you can enter the aft cabin, forward berth, port side, or starboard side of the boat.");
      Room forward = new Room("forward berth", "As you enter the forward berth you feel a rush of water on your feet and legs and you see a large hole the size of a \nsoccerball on the forward-most starboard side, water is rushing into your boat.");
      Room starboard = new Room("starboard", "The starboard side of the boat has a galley with food and dishes. You're feeling hungry, maybe you should make a \nsandwich, afterall the water on the deck is only up to your ankles.");
      Room port = new Room("port", "The port side of the boat has nautical gear scattered about, several piles of books and charts, and a \nsoccerball that never gets used (because you live on a boat).");

      passageway.AddNearbyRoom(Direction.aft, aft);
      passageway.AddNearbyRoom(Direction.forward, forward);
      passageway.AddNearbyRoom(Direction.port, port);
      passageway.AddNearbyRoom(Direction.starboard, starboard);

      aft.AddNearbyRoom(Direction.forward, passageway);
      forward.AddNearbyRoom(Direction.aft, passageway);
      port.AddNearbyRoom(Direction.starboard, passageway);
      starboard.AddNearbyRoom(Direction.port, passageway);

      Item soccerball = new Item("soccerball", "a white, black, and yellow, size 5, rarely used Adidas soccerball.");
      port.Items.Add(soccerball);

      CurrentRoom = aft;
      Alive = true;

      StartGame();
    }

    //Setup and Starts the Game loop
    public void StartGame()
    {
      Console.Clear();
      Console.WriteLine("Your goal is to stop the water from flooding and sinking your boat.");
      Console.WriteLine("\nIf you're not familiar with boats you can move: \nforward to go toward the front of the boat, \naft to go towards the back of the boat, \nport to go to the left side of the boat, and \nstarboard to go to the right side of the boat.");
      Console.WriteLine("\nPress any key to begin. Good luck.");
      Console.ReadKey();
      Console.Clear();
    }

    //Gets the user input and calls the appropriate command
    public void GetUserInput(string response)
    {
      if (response[0] == 'g')
      {
        Go(response);
      }
      else if (response[0] == 'u')
      {
        UseItem(response);
      }
      else if (response[0] == 't')
      {
        TakeItem(response);
      }
      else if (response[0] == 'l')
      {
        Look();
      }
      else if (response[0] == 'i')
      {
        Inventory();
      }
      else if (response[0] == 'h')
      {
        Help();
      }
      else if (response[0] == 'q')
      {
        Quit();
      }
      else if (response[0] == 'r')
      {
        Reset();
      }
      else
      {
        Console.Clear();
        Console.WriteLine($"Invalid command! Try again or ask for help. \n");
      }
    }

    //Validate CurrentRoom.Exits contains the desired direction
    //if it does change the CurrentRoom
    public void Go(string response)
    {
      char letter = response[3];
      if (letter == 'f')
      {
        Console.Clear();
        CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.forward);
      }
      else if (letter == 'a')
      {
        Console.Clear();
        CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.aft);
      }
      else if (letter == 'p')
      {
        Console.Clear();
        CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.port);
      }
      else if (letter == 's')
      {
        Console.Clear();
        CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.starboard);
      }
      else
      {
        Console.Clear();
        Console.WriteLine($"Invalid command! Try again or ask for help. \n");
      }

    }

    //Should display a list of commands to the console
    public void Help()
    {
      Console.Clear();
      Console.WriteLine(@"When prompted you can respond with following commands:

Go + a direction (ex. go forward, aft, port, or starboard),
Use + the item name (ex. use screwdriver), 
Take + the item name (ex. take peanut butter), 
Look (to get a description of the room), 
Inventory (to see what items you have available), 
Quit (to end the game).

Press any key to continue.");
      Console.ReadKey();
      Console.Clear();

    }

    //Print the list of items in the players inventory to the console
    public void Inventory()
    {
      for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
      {
        Console.Clear();
        Console.WriteLine(value: $"{i + 1}. {CurrentPlayer.Inventory[i].Name.ToUpper()[0] + CurrentPlayer.Inventory[i].Name.Substring(1)}: {CurrentPlayer.Inventory[i].Description} \n");
        Console.WriteLine("What would you like to do?");
        string response = Console.ReadLine().ToLower();
        GetUserInput(response);
      }
    }

    //Display the CurrentRoom Description, Exits, and Items
    public void Look()
    {
      Console.Clear();
      Console.WriteLine("Okay, have another look around. Here's what you see: \n");
    }

    //Stops the application
    public void Quit()
    {
      Console.Clear();
      System.Console.WriteLine($"Sorry to see you go. Guess we can't all be winners. \n \nGoodbye.");
      Alive = false;
    }

    //Restarts the game 
    public void Reset()
    {
      Alive = false;
      Run();
    }


    //When taking an item be sure the item is in the current room 
    //before adding it to the player inventory, Also don't forget to 
    //remove the item from the room it was picked up in
    public void TakeItem(string response)
    {
      string name = response.Split(" ")[1];
      Item item = CurrentRoom.Items.Find(i =>
      {
        return i.Name.ToLower() == name;
      });
      CurrentRoom.Items.Remove(item);
      CurrentPlayer.AddItem(item);
      CurrentRoom.Description = "The port side of the boat has nautical gear scattered about, and several piles of books and charts.";

      Console.Clear();
      Console.WriteLine("You have successfully taken the soccerball. \n");
    }

    //No need to Pass a room since Items can only be used in the CurrentRoom
    //Make sure you validate the item is in the room or player inventory before
    //being able to use the item
    public void UseItem(string itemName)
    {

    }
  }
}