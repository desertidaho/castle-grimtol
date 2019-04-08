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
        if (CurrentRoom.Name == "passageway" && StoppedFlooding)
        {
          Console.WriteLine("You're in the passageway, which runs forward and aft. There's water on the deck but the water level isn't rising. \nFrom the passageway you can enter the aft cabin, forward berth, port side, starboard side, \nor go up to the cockpit outside.");
        }
        else
        {
          Console.WriteLine($"{CurrentRoom.Description}");
        }
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

    public void StartGame()
    {
      Console.Clear();
      Console.WriteLine("Your goal is to stop the water from flooding your boat, then make a radio mayday call for help from the cockpit. \nHurry, if you waste too much time your boat will sink and you'll lose the game.");
      Console.WriteLine("\nIf you're not familiar with boats you can move: \nforward to go toward the front of the boat, \naft to go toward the back of the boat, \nport to go to the left side of the boat, \nstarboard to go to the right side of the boat, \nup to go outside to the cockpit of the boat, and \ndown to go back down into the boat.");
      Console.WriteLine("\nPress any key to begin. Good luck.");
      Console.ReadKey();
      Console.Clear();
    }

    public void GetUserInput(string response)
    {
      string[] inputArr = response.Split(" ");
      string command = inputArr[0].ToLower();
      string option = "";
      if (inputArr.Length > 1)
      {
        option = inputArr[1];
      }
      switch (command)
      {
        case "go":
          Go(option);
          break;
        case "use":
          UseItem(option);
          break;
        case "take":
          TakeItem(option);
          break;
        case "look":
          Look();
          break;
        case "inventory":
          Inventory();
          break;
        case "help":
          Help();
          break;
        case "quit":
          Quit();
          break;
        case "reset":
          Reset();
          break;
        default:
          Console.Clear();
          Console.WriteLine($"Invalid command! Try again or ask for help. \n");
          break;
      }
    }

    public void Go(string response)
    {
      count += 1;
      Console.Clear();
      switch (response)
      {
        case "forward":
          CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.forward);
          break;
        case "aft":
          CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.aft);
          break;
        case "port":
          CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.port);
          break;
        case "starboard":
          CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.starboard);
          break;
        case "up":
          CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.up);
          break;
        case "down":
          CurrentRoom = (Room)CurrentRoom.MoveToRoom(Direction.down);
          break;
        default:
          Console.WriteLine($"Invalid command! Try again or ask for help. \n");
          break;
      }
    }

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

Your goal is to plug the hole in the boat with something, then make a radio mayday call from the cockpit outside.

Press any key to continue.");
      Console.ReadKey();
      Console.Clear();
    }

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

    public void Look()
    {
      Console.Clear();
      Console.WriteLine("Okay, have another look around. Here's what you see: \n");
    }

    public void Quit()
    {
      Console.Clear();
      System.Console.WriteLine($"Sorry to see you go. Guess we can't all be winners. \n \nGoodbye.");
      Playing = false;
    }

    public void Reset()
    {
      Playing = false;
      Console.Clear();
      Run();
    }

    public void TakeItem(string response)
    {
      Item item = CurrentRoom.Items.Find(i =>
      {
        return i.Name.ToLower() == response;
      });
      if (item == null)
      {
        Console.WriteLine("You cannot take that item. \n");
        Console.WriteLine("What would you like to do?");
        string answer = Console.ReadLine().ToLower();
        GetUserInput(answer);
      }
      else
      {

        CurrentRoom.Items.Remove(item);
        CurrentPlayer.AddItem(item);
        if (response == "soccerball")
        {
          CurrentRoom.Description = "The port side of the boat has nautical gear scattered about, and several piles of books and charts.";
          Console.Clear();
          Console.WriteLine("You have successfully taken the soccerball. \n");
        }
        else if (response == "radio")
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
    }

    public void UseItem(string itemName)
    {
      Item item = CurrentPlayer.Inventory.Find(i =>
      {
        return i.Name.ToLower() == itemName;
      });
      if (item == null)
      {
        Console.WriteLine("You cannot use that item. \n");
        Console.WriteLine("What would you like to do?");
        string answer = Console.ReadLine().ToLower();
        GetUserInput(answer);
      }
      if (CurrentRoom.Description[0] == 'A' && itemName == "soccerball")
      {
        CurrentPlayer.Inventory.Remove(item);
        StoppedFlooding = true;
        CurrentRoom.Description = "You're in the forward berth, it's a mess and there's water everywhere but the soccerball is plugging the hole \nand there's no water leaking in.";
        Console.Clear();
        Console.WriteLine("You saved your boat from sinking. It's a good thing you had that soccerball. \nNow hurry and go up to the cockpit outside to make a mayday call. \n ");
      }
      else if (CurrentRoom.Description[7] == 'o' && itemName == "radio" && StoppedFlooding)
      {
        CurrentPlayer.Inventory.Remove(item);
        Console.Clear();
        Console.WriteLine("Great, you're trying to use the radio to make a mayday call, but the radio is on the wrong frequency. \nTo go to the correct frequency you first have to answer a complicated radio question. \nBe careful, if you answer incorrectly the radio will short circut and you'll be stranded with no help on the way, \nwhich means you will lose the game. \n");
        Console.WriteLine(@"Does the following return true or false?

  Given two strings does str2 contain all of the letters of str1 in the order they occur in str1?
  
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
  }
  
  sameCharacters('cat', 'rclnaztsw')");
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
          Console.WriteLine("Oh no! You answered incorrectly. \nThe radio has short-circuted and you have no way to call for help (except maybe your computer and cellphone \nbut disregard those items for now). \n \nYou lose. Game over. \nStudy up buttercup, better luck next time. \nWant to play again? Yes/No");
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