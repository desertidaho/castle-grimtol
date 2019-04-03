using System;
using System.Collections.Generic;
// using CastleGrimtol.Interfaces;
// using CastleGrimtol.Models;

namespace CastleGrimtol
{
  class App
  {
    public void Run()
    {
      Intro();
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

    public void Initialize()
    {

    }
  }
}