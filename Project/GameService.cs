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
    public bool Playing { get; set; }
    public int count { get; set; } = 0;
    public bool StoppedFlooding { get; set; } = false;

    //Start the game
    public void Run()
    {
      CurrentPlayer = new Player("Sailor");
      count = 0;
      Intro();
      while (Playing)
      {
        if (count >= 12)
        {
          Console.Clear();
          Console.WriteLine("You wasted too much time. Your boat is underwater. Abandon ship and swim to shore. \nYou lose. Better luck next time. \n \nDo you want to play again?");
          string answer = Console.ReadLine().ToLower();
          if (answer[0] == 'y')
          {
            Reset();
          }
          else
          {
            Quit();
            break;
          }
        }
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


      SetUp();
    }

    //Initialize rooms, their exits, and add items to rooms
    public void SetUp()
    {
      Room aft = new Room("aft cabin", "You're in the aft cabin, this is where you were sleeping. The only thing here is your bed, clothes, and personal items. There's a small window to port, but too small for you to squeeze out of. The only door leads forward.");
      Room passageway = new Room("passageway", "You're in the passageway, which runs forward and aft. There's water on the deck and the water level is rising quickly. \nFrom the passageway you can enter the aft cabin, forward berth, port side, starboard side, \nor go up to the cockpit outside.");
      Room forward = new Room("forward berth", "As you enter the forward berth you feel a rush of water on your feet and legs and you see a large hole the size of a \nsoccerball on the forward-most starboard side, water is rushing into your boat.");
      Room starboard = new Room("starboard", "The starboard side of the boat has a Coast Guard radio and a galley with food and dishes. \nYou're feeling hungry, maybe you should make a sandwich, afterall the water is only up to your ankles.");
      Room port = new Room("port", "The port side of the boat has nautical gear scattered about, several piles of books and charts, and a \nsoccerball that never gets used (because you live on a boat).");
      Room outside = new Room("outside", "You're outside in the cockpit of your boat. The weather is sunny and warm.");

      passageway.AddNearbyRoom(Direction.aft, aft);
      passageway.AddNearbyRoom(Direction.forward, forward);
      passageway.AddNearbyRoom(Direction.port, port);
      passageway.AddNearbyRoom(Direction.starboard, starboard);
      passageway.AddNearbyRoom(Direction.up, outside);

      aft.AddNearbyRoom(Direction.forward, passageway);
      forward.AddNearbyRoom(Direction.aft, passageway);
      port.AddNearbyRoom(Direction.starboard, passageway);
      starboard.AddNearbyRoom(Direction.port, passageway);
      outside.AddNearbyRoom(Direction.down, passageway);

      Item soccerball = new Item("soccerball", "a white, black, and yellow, size 5, rarely used Adidas soccerball.");
      Item radio = new Item("radio", "a nautical radio for communicating with other boats and monitored by the Coast Guard.");
      port.Items.Add(soccerball);
      starboard.Items.Add(radio);

      CurrentRoom = aft;
      Playing = true;

      StartGame();
    }

    //Setup and Starts the Game loop
    public void StartGame()
    {
      Console.Clear();
      Console.WriteLine("Your goal is to stop the water from flooding your boat, then make a mayday call for help. \nHurry, if you waste too much time your boat will sink and you'll lose the game.");
      Console.WriteLine("\nIf you're not familiar with boats you can move: \nforward to go toward the front of the boat, \naft to go toward the back of the boat, \nport to go to the left side of the boat, \nstarboard to go to the right side of the boat, \nup to go outside to the cockpit of the boat, and \ndown to go back down into the boat.");
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
      count += 1;
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
      else if (letter == 'u')
      {
        Console.Clear();
        CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.up);
      }
      else if (letter == 'd')
      {
        Console.Clear();
        CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.down);
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

Go + a direction (ex. go forward, aft, port, starboard, up, or down),
Use + the item name (ex. use screwdriver), 
Take + the item name (ex. take peanut butter), 
Look (to get a description of the room), 
Inventory (to see what items you have available), 
Reset (to restart the game),
Quit (to end the game).

Press any key to continue.");
      Console.ReadKey();
      Console.Clear();

    }

    //Print the list of items in the players inventory to the console
    public void Inventory()
    {
      Console.Clear();
      if (CurrentPlayer.Inventory.Count > 0)
      {
        for (int i = 0; i < CurrentPlayer.Inventory.Count; i++)
        {
          Console.WriteLine(value: $"{i + 1}. {CurrentPlayer.Inventory[i].Name.ToUpper()[0] + CurrentPlayer.Inventory[i].Name.Substring(1)}: {CurrentPlayer.Inventory[i].Description}");
        }
      }
      else
      {
        Console.WriteLine("You don't have any items in your inventory. Go find some. \n");
      }
      Console.WriteLine("\nWhat would you like to do?");
      string response = Console.ReadLine().ToLower();
      GetUserInput(response);
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
      Playing = false;
    }

    //Restarts the game 
    public void Reset()
    {
      Playing = false;
      Console.Clear();
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
      if (name == "soccerball")
      {
        CurrentRoom.Description = "The port side of the boat has nautical gear scattered about, and several piles of books and charts.";
        Console.Clear();
        Console.WriteLine("You have successfully taken the soccerball. \n");
      }
      else if (name == "radio")
      {
        CurrentRoom.Description = "The starboard side of the boat has a galley with food and dishes. \nYou're feeling hungry, maybe you should make a sandwich, afterall the water is only up to your ankles.";
        Console.Clear();
        Console.WriteLine("You have successfully taken the radio. \n");
      }
      else
      {
        Console.Clear();
        Console.WriteLine("You cannot take that item. \n");
        Console.WriteLine("What would you like to do?");
        string answer = Console.ReadLine().ToLower();
        GetUserInput(answer);
      }
    }

    //No need to Pass a room since Items can only be used in the CurrentRoom
    //Make sure you validate the item is in the room or player inventory before
    //being able to use the item
    public void UseItem(string itemName)
    {
      string name = itemName.Split(" ")[1].ToLower();
      Item item = CurrentPlayer.Inventory.Find(i =>
      {
        return i.Name.ToLower() == name;
      });
      if (CurrentRoom.Description[0] == 'A' && name == "soccerball")
      {
        CurrentPlayer.Inventory.Remove(item);
        StoppedFlooding = true;
        CurrentRoom.Description = "You're in the forward berth, it's a mess and there's water everywhere but the soccerball is plugging the hole \nand there's no water leaking in.";
        Console.Clear();
        Console.WriteLine("Congratulations! You saved your boat from sinking. It's a good thing you had that soccerball. \nNow hurry and go up to the cockpit outside to make a mayday call. \n ");
      }
      else if (CurrentRoom.Description[7] == 'o' && name == "radio" && StoppedFlooding)
      {
        CurrentPlayer.Inventory.Remove(item);
        Console.Clear();
        Console.WriteLine("Great, you're trying to use the radio to make a mayday call, but the radio is on the wrong frequency. \nTo go to the correct frequency you first have to answer a complicated radio question. \nBe careful, if you answer incorrectly the radio will short circut and you'll be stranded with no help on the way, \nwhich means you will lose the game. \n");
        Console.WriteLine(@"Does the following return true or false?

  Given two strings does str2 contain all of the letters of str1 in the order they occur in str1?
  let str1 = 'cat'
  let str2 = 'rclnaztsw'

  function sameCharacters(str1, str2) {
  let count = 0
  let length = str1.length
  for (let i = 0; i < str2.length; i++) {
    if (str2[i] == str1[count]){
      count++
      if (count == length){
        return true
      }
    }
  }
  return false
  }");
        Console.WriteLine("\n");
        string answer = Console.ReadLine().ToLower();
        if (answer == "true")
        {
          Console.Clear();
          Console.WriteLine("Congratulations you won the game! \nYou answered correctly and your mayday call will now go through. Help is on the way.\nWell done captain, you're a worthy sailor. Cheers. \nWant to play again? Yes/No");
          string play = Console.ReadLine().ToLower();
          if (play[0] == 'y')
          {
            Reset();
          }
          else
          {
            Console.Clear();
            Console.WriteLine("Goodbye.");
            Playing = false;
          }
        }
        else
        {
          Console.Clear();
          Console.WriteLine("Oh no! You answered incorrectly. \nThe radio has short-circuted and you have no way to call for help (except maybe your computer and cellphone dut disregard those items). \n \nYou lose. Game over. \nStudy up buttercup, better luck next time. \nWant to play again? Yes/No");
          string play = Console.ReadLine().ToLower();
          if (play[0] == 'y')
          {
            Reset();
          }
          else
          {
            Console.Clear();
            Console.WriteLine("Goodbye.");
            Playing = false;
          }
        }
      }
      else
      {
        Console.Clear();
        Console.WriteLine("You cannot use that item here. \n ");
        Console.WriteLine("What would you like to do?");
        string answer = Console.ReadLine().ToLower();
        GetUserInput(answer);
      }
    }
  }
}