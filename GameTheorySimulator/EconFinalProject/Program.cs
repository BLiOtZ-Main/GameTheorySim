using System.IO.Pipes;

namespace EconSim
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Calls the Menu Method to Print and manage the menu;
            MenuScreen();
        }

        static void MenuScreen()
        {
            int userInput;
            bool quit = false;

            //Prints intro prompt
            Console.WriteLine("Jett Moreno | Game Theory Simulator");
            Console.WriteLine();

            while (!quit)
            {
                Console.WriteLine();

                Console.WriteLine("Which game would you like to play?");
                Console.WriteLine();

                //Prints the menu options
                Console.WriteLine("1.> The Ultimatum Game");
                Console.WriteLine();

                Console.WriteLine("2.> The Prisioner's Dilemma");
                Console.WriteLine();

                Console.WriteLine("3.> Quit");
                Console.WriteLine();

                //Gets the user's menu option
                userInput = SmartConsole.GetValidNumericInput("(1-3)>", 1, 3);

                //Calls a method depending on the menu choice
                switch (userInput)
                {

                    case 1:
                        Console.WriteLine();
                        Ultimatum();
                        break;

                    //If user selects 2 call the Prisioner's Dilemma method
                    case 2:
                        Prisoner();
                        break;

                    //If user selects 3 end game
                    case 3:
                        quit = true;
                        break;
                }

            }


        }

        static void Ultimatum()
        {
            //Needed variables for the Ultimatum game
            Random rng = new Random();
            string userInput;
            bool validOption = false;
            int userSplit;
            int cpuSplit;
            int acceptChance;

            //Creates a list of Cpu split options and rates
            int[] cpuRates = { 0, 1, 1, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 6, 6, 6, 7, 7, 8, 8, 9, 10 };

            //Prints a summary of the ultimatum game
            Console.WriteLine(" The Ultimatum Game is a game where one player (The Ultimater) is given a sum of money to split." +
                "\n The Other Player (The Receiver) may accept or Reject the Ultimater's split. If rejected no one gets money. ");
            Console.WriteLine();

            //Loops until the user inputs a valid answer
            while (!validOption)
            {
                //Asks the user if they would like to go back to the menu
                userInput = SmartConsole.GetPromptedInput("Do you still want to play the Ultiamtum game?(Yes/No)").ToLower();

                switch (userInput)
                {
                    case "yes":
                        validOption = true;
                        break;

                    case "no":
                        validOption = true;
                        return;

                    default:
                        Console.WriteLine("Please input a valid option");
                        break;

                }
            }

            //resets vallid option
            validOption = false;

            //Loops until the user inputs a valid answer
            while (!validOption)
            {
                Console.WriteLine();

                //Asks the user if they would like to be the ultimater
                userInput = SmartConsole.GetPromptedInput("Would you like to be the Ultimater?(Yes/No)").ToLower();
                Console.WriteLine();

                switch (userInput)
                {
                    case "yes":
                        validOption = true;

                        //Prints the situation
                        Console.WriteLine("You are given $10 to Split between you and another person");

                        //Gets the user's desired split amount
                        userSplit = SmartConsole.GetValidNumericInput("How much will you give to the other Receiver?(0-10)", 0, 10);

                        //Based on Split amount the other player decides to accept or reject;

                        //If offered 10-6 instantly accept
                        if (userSplit <= 10 && userSplit >= 6)
                        {
                            SmartConsole.PrintSuccess(String.Format("The Receiver accepted your offer!!! Your decision was altruistic. You Made {0:c}", 10 - userSplit));
                        }

                        //If offered 5-3 accept 80% of the time
                        else if (userSplit <= 5 && userSplit >= 3)
                        {
                            acceptChance = rng.Next(100);

                            //80% chance the cpu accepts a rationa offer.
                            if (acceptChance <= 80)
                            {
                                SmartConsole.PrintSuccess(String.Format("The Receiver accepted your offer!!! You made a fair and rational decision. You Made {0:c}", 10 - userSplit));
                            }
                            else
                            {
                                SmartConsole.PrintError("The Receiver Rejected your offer! But You made a fair and rational decision. You Made $0");
                            }
                        }

                        //If offered 2-1 accept 80% of the time
                        else if (userSplit <= 2 && userSplit >= 1)
                        {
                            acceptChance = rng.Next(100);

                            //30% chance the cpu accepts a rationa offer.
                            if (acceptChance <= 30)
                            {
                                SmartConsole.PrintSuccess(String.Format("The Receiver accepted your offer!!! You made the most economically rational decision. You Made {0:c}", 10 - userSplit));
                            }
                            else
                            {
                                SmartConsole.PrintError("The Receiver rejected your offer! Your decision was too economically rational. You Made $0");
                            }
                        }

                        //If 0 reject instantly
                        else
                        {
                            SmartConsole.PrintError("The Receiver rejected your offer! Your Decision was irrational. You Made $0");
                        }

                        break;

                    // If no, A randomizer will pick from a list of number holding rates 
                    case "no":
                        validOption = true;
                        //Randomizes how much they decide to Split. Realisitacally never the full 10.
                        cpuSplit = rng.Next(39);

                        Console.WriteLine("The Ultimater was given $10 to Split between you and them.");

                        Console.WriteLine("They decided to give you {0:c}", cpuRates[cpuSplit]);

                        //Based on Split amount prints a message saying how rational or irrational it was;
                        if (cpuRates[cpuSplit] <= 10 && cpuRates[cpuSplit] >= 6)
                        {
                            SmartConsole.PrintSuccess("Their Decision was Altruistic");
                        }

                        else if (cpuRates[cpuSplit] <= 5 && cpuRates[cpuSplit] >= 3)
                        {
                            SmartConsole.PrintSuccess("Their Decision was Fair and Rational");
                        }

                        else if (cpuRates[cpuSplit] <= 2 && cpuRates[cpuSplit] >= 1)
                        {
                            SmartConsole.PrintWarning("Their Decision was economically rational");
                        }

                        else if (cpuRates[cpuSplit] == 0)
                        {
                            SmartConsole.PrintError("Their Decision was irrational");
                        }

                        validOption = false;

                        Console.WriteLine();

                        //Propmts the user for their accpetance choice and tells them how much they made
                        while (!validOption)
                        {
                            userInput = SmartConsole.GetPromptedInput("Do you accept?(Yes/No)").ToLower();

                            if (userInput == "yes")
                            {
                                validOption = true;
                                SmartConsole.PrintSuccess(string.Format("You made {0:c}", cpuRates[cpuSplit]));
                            }
                            else if (userInput == "no")
                            {
                                validOption = true;
                                SmartConsole.PrintError("You both made $0");
                            }
                            else
                            {
                                Console.WriteLine("Please input a valid option");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Please input a valid option");
                        break;

                }

            }
        }

        static void Prisoner()
        {
            Random rng = new Random();
            bool validOption = false;
            string userInput;
            int cpuChoice;
            
            Console.WriteLine();
            //Prints a summary of the Prisoner's dilemma
            Console.WriteLine(" The Prisioner's delemma presents two people the option to either confesss to a crime/ rat out their partner or to stay quiet." +
                "\nEach Player can either betray their partner for their own best interest, or both stay silent and receive little punishment." +
                "\n IF both confess, 5 Years, if one confess and not the other the one who confessed goes free and the other goes to prison for life" +
                "\n IF both stay silent then they both only go to prision for one year");
            Console.WriteLine();

            //Loops until the user inputs a valid answer
            while (!validOption)
            {
                //Asks the user if they would like to go back to the menu
                userInput = SmartConsole.GetPromptedInput("Do you still want to go through the Prisoner's Dilemma?(Yes/No)").ToLower();

                switch (userInput)
                {
                    case "yes":
                        validOption = true;
                        break;

                    case "no":
                        validOption = true;
                        return;

                    default:
                        Console.WriteLine("Please input a valid option");
                        break;

                }
            }

            //Resets valid option
            validOption = false;

            //Generates the cpu's choice
            cpuChoice = rng.Next(100);

           
            //Loops until the user inputs a valid answer
            while (!validOption)
            {
                Console.WriteLine();

                //Asks the user if they want to confess
                userInput = SmartConsole.GetPromptedInput("Do you Confess?(Yes/No)").ToLower();
                Console.WriteLine();

                switch (userInput)
                {
                    case "yes":
                        validOption = true;

                        //There is an 80% chance the cpu selects confess as that is the rational decision
                        if (cpuChoice <= 80)
                        {
                            SmartConsole.PrintSuccess("You Both Confessed!!! You both get 5 years in Prision");
                        }
                        //otherwise they snitch
                        else
                        {
                            SmartConsole.PrintSuccess("Your Partner didnt Confess.You Betrayed your Partner and got off scott free");
                        }
                        break;

                    case "no":
                        validOption = true;

                        //There is an 80% chance the cpu selects confess as that is the rational decision
                        if (cpuChoice <= 80)
                        {
                            SmartConsole.PrintError("Your Partner Confessed. You are going away for a long time.");
                        }
                        //otherwise they snitch
                        else
                        {
                            SmartConsole.PrintSuccess("You and your Partner Stayed Silent. You both only get 1 year in prison!!!");
                        }
                        break;

                    default:
                        Console.WriteLine("Please input a valid option");
                        break;
                }
            }
        }
    }
}
