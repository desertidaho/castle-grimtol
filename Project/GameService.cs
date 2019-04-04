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
      Intro();
      while (Alive)
      {
        Console.WriteLine($"{CurrentRoom.Description}");
        Console.WriteLine($"\nWhat would you like to do? Or type help for help.");
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

      Console.Clear();
      Console.WriteLine("Your goal is to go from the aft cabin to the forward berth and plug the hole to stop water from flooding your boat.");
      Console.WriteLine("\nIf you're not familiar with boats your directions will be: \n(F)orward to go toward the front of the ship, \n(A)ft to go towards the back of the ship, \n(P)ort to go to the left side of the ship, and \n(S)tarboard to go to the right side of the ship.");
      Console.WriteLine("\nPress any key to begin. Good luck.");
      Initialize();
    }

    //Initialize rooms, their exits, and add items to rooms
    public void Initialize()
    {
      Room aft = new Room("aft cabin", "You're in the aft cabin, this is where you were sleeping. The only thing here is your bed, clothes, and personal items. There's a small window to port, but too small for you to squeeze out of.");
      Room passageway = new Room("passageway", "You're in the passageway, which runs forward and aft. There's water on the floor and the water level is rising quickly. From the passageway you can enter the aft cabin, forward berth, port side, or starboard side of the ship.");
      Room forward = new Room("forward berth", "As you enter the forward berth you feel a rush of water on your feet and legs and you see a large hole the size of a soccerball on the forward-most starboard side, water is rushing into your boat.");
      Room port = new Room("port", "The port side of the ship has scattered miscellaneous nautical gear, books, and some sports equipment including a soccerball.");
      Room starboard = new Room("starboard", "The starboard side of the ship has a galley with food and dishes. You're feeling hungry, maybe you should make a sandwich.");

      passageway.AddNearbyRoom(Direction.aft, aft);
      passageway.AddNearbyRoom(Direction.forward, forward);
      passageway.AddNearbyRoom(Direction.port, port);
      passageway.AddNearbyRoom(Direction.starboard, starboard);

      aft.AddNearbyRoom(Direction.forward, passageway);
      forward.AddNearbyRoom(Direction.aft, passageway);
      port.AddNearbyRoom(Direction.starboard, passageway);
      starboard.AddNearbyRoom(Direction.port, passageway);

      CurrentRoom = aft;
      Alive = true;
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
    }

    //Validate CurrentRoom.Exits contains the desired direction
    //if it does change the CurrentRoom
    public void Go(string response)
    {
      char letter = response[3];
      if (CurrentRoom is Room aft && letter == 'f')
      {
        CurrentRoom.MoveToRoom(Direction.forward);
      }
    }

    //Should display a list of commands to the console
    public void Help()
    {
      Console.Clear();
      Console.WriteLine(@"When prompted you can respond with following commands:

Go + a direction (ex. go forward),
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

    }

    //Display the CurrentRoom Description, Exits, and Items
    public void Look()
    {

    }

    //Stops the application
    public void Quit()
    {

    }

    //Restarts the game 
    public void Reset()
    {

    }

    //Setup and Starts the Game loop
    public void StartGame()
    {

    }

    //When taking an item be sure the item is in the current room 
    //before adding it to the player inventory, Also don't forget to 
    //remove the item from the room it was picked up in
    public void TakeItem(string itemName)
    {

    }

    //No need to Pass a room since Items can only be used in the CurrentRoom
    //Make sure you validate the item is in the room or player inventory before
    //being able to use the item
    public void UseItem(string itemName)
    {

    }
  }
}