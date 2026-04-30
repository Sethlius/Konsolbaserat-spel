

using System;

bool isRunning = true; //cheks if program is running
bool[] whichCoise = [false, false]; //attack option or item option in the menue 
bool enemtTurn = false; 
bool attackTurn = false; //where the damage calculation is done
bool[] whatKindOfItem = [false, false]; //item is a health item or an attack item
bool attackItemActivated = false; //cheks wheter you have chosen an attack item

int moveInMenue = 0; //move in first menue
int attackMenuMove = 0; //move in the attack menue
int enemyAtack = 0; //which attack enemy chose
int level = 1;
int enemylevel = 1;
int attackItemChoise = 0; //move in attack item menue
int healthItemsChoise = 0; //move in health items menue
int choosItem = 0; //move in menue for chosing attack/healt item
int ammountOfAttackItems = 3; //the amount of attackitems you have
int ammountOfHealthItems = 3; //the amount of healt items you have


int[,] attackDamage = { { 110, 80, 90, 60 }, //water type
                        { 90, 55, 130, 120}, //grass type
                        { 90, 75, 85, 60 }, //fire type           
                        { 110, 90, 75, 65}, //electric type
                        { 120, 75, 90, 60 }, //Bug type
                        { 110, 75, 60,  60 }, //flying type 
                        { 80, 120,  90, 65 }, //poison type
                        { 150, 70, 60, 90 }, //normal type
                        { 60, 100, 80, 75 }, //rock typer
                        { 100, 60, 90, 80 }, //ground type
                        { 90, 80, 80, 50 }, //steel type
                        { 110, 60, 90, 75 }, //ice type
                        { 85, 80, 100, 60 }, //dragon type
                        { 80, 95, 90, 80 }, //fairy type
                        { 80, 80, 70, 95 }, //dark type
                        { 80, 90, 70, 100 }, //ghost type
                        { 90, 70, 80, 100 }, //Psychich type
                        { 120, 80, 80, 65 } //Fighting type
                      };
int[] weatherConditions = { 1, 1, 1 };

float damageDelt = 0;
float enemyDamage = 0;
float critDamage = 1.5f;
float[,] typeChart = { { 0.5f, 0.5f, 2, 1, 1, 1, 1, 1, 2, 2, 1, 1, 0.5f, 1, 1, 1, 1, 1 }, //water type (0)
                       { 2, 0.5f, 0.5f, 1, 0.5f, 0.5f, 0.5f, 1, 2, 2, 0.5f, 1, 0.5f, 1, 1, 1, 1, 1 }, //grass type (1)
                       { 0.5f, 2, 0.5f, 1, 2, 1, 1, 1, 0.5f, 1, 2, 2, 0.5f, 1, 1, 1, 1, 1 }, //fire type (2)
                       { 2, 0.5f, 1, 0.5f, 1, 2, 1, 1, 1, 0.25f, 1, 1, 0.5f, 1, 1, 1, 1, 1 }, //electri type (3)
                       { 1, 2, 0.5f, 1, 1, 0.5f, 0.5f, 1, 1, 1, 0.5f, 1, 1, 0.5f, 2, 0.5f, 2, 0.5f }, //bug type (4)
                       { 1, 2, 1, 0.5f, 2, 1, 1, 1, 0.5f, 1, 0.5f, 1, 1, 1, 1, 1, 1, 2 }, //flying type (5)
                       { 1, 2, 1, 1, 1, 1, 0.5f, 1, 0.5f, 0.5f, 0.25f, 1, 1, 2, 1, 0.5f, 1, 1 }, //poison type (6)
                       { 1, 1, 1, 1, 1, 1, 1, 1, 0.5f, 1, 0.5f, 1, 1, 1, 1, 0.25f, 1, 1 }, //normal type (7)
                       { 1, 1, 2, 1, 2, 2, 1, 1, 1, 0.5f, 0.5f, 2, 1, 1, 1, 1, 1, 0.5f }, //rock type (8) 
                       { 1, 0.5f, 2, 2, 0.5f, 0.25f, 2, 1, 2, 1, 2, 1, 1, 1, 1, 1, 1, 1 }, //ground type (9)
                       { 0.5f, 1, 0.5f, 0.5f, 1, 1, 1, 1, 2, 1, 0.5f, 2, 1, 2, 1, 1, 1, 1 }, //steel type (10)
                       { 0.5f, 2, 0.5f, 1, 1, 2, 1, 1, 1, 2, 0.5f, 0.5f, 2, 1, 1, 1, 1, 1 }, //ice type (11)
                       { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.5f, 1, 2, 0.25f, 1, 1, 1, 1 }, //dragon type (12)
                       { 1, 1, 0.5f, 1, 1, 1, 0.5f, 1, 1, 1, 0.5f, 1, 2, 1, 2, 1, 1, 2 }, //fairy type (13)
                       { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 0.5f, 2, 0.5f, 2, 0.5f }, //dark type (14)
                       { 1, 1, 1, 1, 1, 1, 1, 0.25f, 1, 1, 1, 1, 1, 1, 0.5f, 2, 2, 1 }, //ghost type (15)
                       { 1, 1, 1, 1, 1, 1, 2, 1, 1, 1, 0.5f, 1, 1, 1, 0.25f, 1, 0.5f, 2 }, //psychich type (16)
                       { 1, 1, 1, 1, 0.5f, 0.5f, 0.5f, 2, 2, 1, 2, 2, 1, 0.5f, 2, 0.25f, 0.5f, 1 } //fighting type (17)
                    };
float yourNomekopHelth;
float enemyNomekopHealt;
float[] yourNomekopStats = { 0, 1 }; //0 is for attackstat and 1 is for defencstat;
float[] enmeyNomekopnStats = { 0, 1 };


string crit = "Critical Hit!";
string superEfective = "Super Effective!";
string notEffective = "Not Very Effective...";
string imune = "This attack has no effect on the oposing Nomekop";

string[] menueChoice = { "Attack", "Items" };

string[,] nomekop = { { "Average water type", "Wekest water type", "fastest water type", "Higest defenc watter type", "Nomekop 5", "Strongest water type" }, //water type Nomekop
                       { "Average grass type ", "wekest grass type", "fastest grass type", "Higest defenc grass type", "Nomekop 5", "Strongest grass type" }, //grass type Nomekop
                       { "Average fire type", "wekest fire type", "fastest grass type", "Higest defenc fire type", "Nomekop 5", "Strongest fire type" }, //fire type Nomekop
                       { "Average electric type", "wekest electric type", "fastet electric type", "Higest defenc electric type", "Nomekop 5", "Strongest electric type" }, //electric type Nomekop
                       { "Average bug type", "wekest bug type", "fastets bug type", "Higest defenc bug type", "Nomekop 5", "Strongest bug type" }, //bug type Nomekop
                       { "Average flying type", "wekest flying type", "fastest flying type", "Higest defenc flying type", "Nomekop 5", "Strongest flying type" }, //flying type Nomekop
                       { "Average poison type", "wekest poision type", "fastest poision type", "Higest defenc poision type", "Nomekop 5", "Strongest poision type" }, //poison type Nomekop
                       { "Average normal type", "wekest normal type", "fastest normal type", "Higest defenc normal type", "Nomekop 5", "strongest normal type" }, //normal type Nomekop
                       { "Average rock type", "wekest rock type", "fastest rcok type", "Higest defenc rock type", "Nomekop 5", "strongest rock type" }, //rock type Nomekop
                       { "Average ground type", "wekest ground type", "fastest ground type", "Higest defenc ground type", "Nomekop 5", "strongest ground type" }, //ground type Nomekop
                       { "Average steel type", "wekest steel type", "fastest steel type", "Higest defenc steel type", "Nomekop 5", "Strongest steel type" }, //steel type Nomekop
                       { "Average ice type", "wekest ice type", "fastest ice type", "Higest defenc ice type", "Nomekop 5", "strongest ice type" }, //ice type Nomekop
                       { "Average dragon type", "wekest dragon type", "fastest dargon type", "Higest defenc dragon type", "Nomekop 5", "strongest dragon type" }, //dragon type Nomekop
                       { "Average fairy type", "wekest fairy type", "fastets fairy type", "Higest defenc fairy type", "Nomekop 5", "strongest fairy type" }, //fairy type Nomekop
                       { "Average dark type", "wekest dark type", "fastest dark type", "Higest defenc dark type", "Nomekop 5", "strongest dark type" }, //dark type Nomekop
                       { "Average ghost type", "wekest ghost type", "fastest ghost type", "Higest defenc ghost type", "Nomekop 5", "strongest ghost type" }, //ghost type Nomekop
                       { "Average psychich type", "wekest psychich type", "fastest psychich type", "Higest defenc pshychich type", "Nomekop 5", "strongest psychich type" }, //psychich type Nomekop
                       { "Average fighting type", "wekest fighting type", "fastest fighting type", "Higest defenc fighting type", "§Nomekop 5", "strongest fighting type" }  //fighting type Nomekop
                       };

string[,] nomekopAttack = { { "Hydro pump", "Watterfall", "Surf", "Water Pulse" }, //watter attacks
                            { "Energy ball", "Razor leaf", "Leaf storm", "Solar beam" }, //grass attacks
                            { "Flamethrower", "Fire Punch", "Blaze Kick", "Incinerate" }, //fire attack 
                            { "Thunder", "Thunderbolt", "Thunder Punch", "Thunder Fang" }, //electric type
                            { "Megahorn", "Signal Beam", "Bug Buzz", "Bug Bite" }, //Bug type
                            { "Hurricane", "Air Slash", "Wing Attack", "Aerial Ace" }, //flying type 
                            { "Poison Jab", "Gunk Shot", "Sludge Bomb", "Sludge" }, //poison type
                            { "Hyper Beam", "Slash", "Swift", "Hyper Voice" }, //normal type
                            { "Ancient Power", "Stone Edge", "Power Gem", "Rock Slide" }, //rock typer
                            { "Earthquake", "Bulldoze", "Earth Power", "Drill Run" }, //ground type
                            { "Meteor Mash", "Flash Cannon", "Iron Head", "Metal Claw" }, //steel type
                            { "Blizzard", "Avalanche", "Ice Beam", "Ice Punch" }, //ice type
                            { "Dragon Pulse", "Dragon Claw", "Dragon Rush", "Dragon Tail" }, //dragon type
                            { "Dazzling Gleam", "Moonblast", "Play Rough", "Alluring Voice" }, //fairy type
                            { "Crunch", "Dark Pulse", "Night Slash", "Foul Play" }, //dark type
                            { "Shadow Ball", "Phantom Force", "Shadow Claw", "Moongeist Beam" }, //ghost type
                            { "Psychic", "Psycho Cut", "Zen Headbutt", "Psystrike" }, //Psychich type
                            { "Close Combat", "Submission", "Aura Sphere", "Low Sweep" } //Fighting type
                          };

ItemsAttack[] items = {
    new ItemsAttack("Item1", 1.25f), //attack item
    new ItemsAttack("Item2", 1.5f) //attack item
};

HealthItems[] healthItems =
{
    new HealthItems("Item1", 50),
    new HealthItems("Item2", 75)
};

NomekopType[] types = {
    new NomekopType("Water type",    ConsoleColor.DarkCyan, /*Health*/ [71.1f, 20f, 61f, 50f, 1f, 100f], /*Attackstat*/ [76.5f, 10f, 123f, 95f, 1, 160f], /*DefenceStat*/ [74.7f, 55f, 60f, 180f, 1, 97f]), //0
    new NomekopType("Grass type",    ConsoleColor.DarkGreen, /*Health*/ [72.9f, 40f, 60f, 74f, 1, 59f], /*Attackstat*/ [77, 27f, 50f, 94f, 1, 181f], /*DefenceStat*/ [73.8f, 60f, 70f, 131f, 1, 131f]), //1
    new NomekopType("Fire type",     ConsoleColor.Red, /*Health*/ [71.3f, 50f, 105f, 70f, 1, 100f], /*Attackstat*/ [84.8f, 30f, 160f, 85f, 1, 180f], /*DefenceStat*/ [71.4f, 55f, 55f, 140f, 1, 160f]), //2
    new NomekopType("Electric type", ConsoleColor.Yellow, /*Health*/ [66.9f, 40f, 80f, 80f, 1, 100f], /*Attackstat*/ [75.5f, 30f, 100f, 120f, 1, 150f], /*DefenceStat*/ [69.3f, 50f, 50f, 130f, 1, 120f]), //3
    new NomekopType("Bug type",      ConsoleColor.Green, /*Health*/ [56.9f, 20f, 61f, 30f, 1, 80f], /*Attackstat*/ [71, 10f, 90f, 30f, 1, 185f], /*DefenceStat*/ [70.7f, 230f, 45f, 200f, 1, 115f]), //4
    new NomekopType("Flying type",   ConsoleColor.Blue, /*Health*/ [72.4f, 40f, 80f, 65f, 1, 105f], /*Attackstat*/ [81.6f, 20f, 135f, 80f, 1, 180f], /*DefenceStat*/ [68.4f, 30f, 85f, 140f, 1, 100f]), //5
    new NomekopType("Poison type",   ConsoleColor.DarkMagenta, /*Health*/ [69.1f, 45f, 65f, 88f, 1, 65f], /*Attackstat*/ [74.4f, 25f, 150f, 88f, 1, 150f], /*DefenceStat*/ [69.5f, 50f, 40f, 160f, 1, 40f]), //6
    new NomekopType("Normal type",   ConsoleColor.White, /*Health*/ [77.5f, 100f, 65f, 103f, 1, 150f], /*Attackstat*/ [75.6f, 5f, 136f, 60f, 1, 160], /*DefenceStat*/ [62.4f, 5f, 94f, 126f, 1, 100f]), //7
    new NomekopType("Rock type",     ConsoleColor.Gray, /*Health*/ [69.3f, 48f, 90f, 61f, 1, 97f], /*Attackstat*/ [91.4f, 35f, 120f, 131f, 1, 165f], /*DefenceStat*/ [102.1f, 42f, 80f, 211f, 1, 60]), //8
    new NomekopType("Ground type",   ConsoleColor.DarkGray, /*Health*/ [77.9f, 40f, 35f, 75f, 1, 108f], /*Attackstat*/ [91.2f, 40f, 100, 125f, 1, 170f], /*DefenceStat*/ [88.3f, 55f, 50f, 230f, 1, 115f]), //9
    new NomekopType("Steel type",    ConsoleColor.DarkYellow, /*Health*/ [70.5f, 57f, 92f, 70f, 1, 97f], /*Attackstat*/ [95.2f, 24f, 150, 140f, 1, 157f], /*DefenceStat*/ [108.9f, 86f, 115f, 230f, 1, 127f]), //10
    new NomekopType("Ice type",      ConsoleColor.Cyan, /*Health*/ [80.3f, 30f, 56f, 95f, 1, 125f], /*Attackstat*/ [88.2f, 25f, 80f, 127f, 1, 170f], /*DefenceStat*/ [79, 35f, 114f, 184f, 1, 100f]), //11
    new NomekopType("Dragon type",   ConsoleColor.DarkBlue, /*Health*/ [88.7f, 40f, 70f, 225f, 1, 90f], /*Attackstat*/ [99.2f, 30f, 110f, 115f, 1, 110f], /*DefenceStat*/ [88.3f, 35f, 75f, 250f, 1, 150f]), //12
    new NomekopType("Fairy type",    ConsoleColor.Magenta, /*Health*/ [67.8f, 50f, 92f, 50f, 1, 50f], /*Attackstat*/ [70, 20f, 132f, 100f, 1, 160f], /*DefenceStat*/ [74.5f, 40f, 100f, 150f, 1, 110f]), //13
    new NomekopType("Dark type",     ConsoleColor.DarkBlue, /*Health*/ [75.1f, 40f, 80f, 100f, 1, 100f], /*Attackstat*/ [94.8f, 28f, 120f, 164f, 1, 164f], /*DefenceStat*/ [71.9f, 28f, 80f, 150f, 1, 150f]), //14
    new NomekopType("Ghost type",    ConsoleColor.DarkMagenta, /*Health*/ [66.5f, 50f, 100f, 59f, 1, 64f], /*Attackstat*/ [78.9f, 30f, 85f, 110f, 1, 165f], /*DefenceStat*/ [78.9f, 53f, 80f, 150f, 1, 75f]), //15
    new NomekopType("Psychich type", ConsoleColor.Magenta, /*Health*/ [75.2f, 25f, 50f, 95f, 1, 106f], /*Attackstat*/ [76.7f, 20f, 95f, 75f, 1, 190f], /*DefenceStat*/ [76.7f, 15f, 90f, 180f, 1, 100f]), //16
    new NomekopType("Fighting type", ConsoleColor.DarkRed, /*Health*/ [77.9f, 35f, 71f, 92f, 1, 68f], /*Attackstat*/ [108.8f, 30f, 137f, 120f, 1, 165f], /*DefenceStat*/ [79.6f, 35f, 37f, 1140f, 1, 95f]) //17
};

Random enemyType = new Random(); //gives the enemy a pokemon
int typeEnemy = enemyType.Next(0, 17);
int enemyRandomStat = enemyType.Next(0, 5);
enemyNomekopHealt = types[typeEnemy].health[enemyRandomStat];


Console.WriteLine("Pick the type off Nomekop you want, write the number 0-17 for the type");

for (int i = 0; i < types.Length; i++)
{
    types[i].Print(i);
}

string userChoice = Console.ReadLine();

int choiceUser = int.Parse(userChoice);
Console.Clear();
Console.WriteLine("You have chosen:");
types[choiceUser].Print(choiceUser);

for (int i = 0; i <= 5 ; i++)
{
    Console.WriteLine($"{nomekop[choiceUser, i]}: " +
        $"\n attack stat: {types[choiceUser].statAttack[i]}" +
        $" \n defence stat: {types[choiceUser].statDefence[i]} " +
        $"\n hp: {types[choiceUser].health[i]} \n");
}

string nomekopChoicse = Console.ReadLine(); //for getting stats
int chosenNomekop = int.Parse(nomekopChoicse);

yourNomekopStats[0] = types[choiceUser].statAttack[chosenNomekop];
yourNomekopStats[1] = types[choiceUser].statDefence[chosenNomekop];
yourNomekopHelth = types[choiceUser].health[chosenNomekop];

enmeyNomekopnStats[0] = types[typeEnemy].statAttack[enemyRandomStat];
enmeyNomekopnStats[1] = types[typeEnemy].statDefence[enemyRandomStat];

Console.WriteLine("Press enter to start");
userChoice = Console.ReadLine();


while (isRunning)
{
    Random criticalChans = new Random();
    Random wheter = new Random();
    int criticalHit = criticalChans.Next(1, 10);
    int enemyCritHit = criticalChans.Next(1, 10);
    int weatherConditionRandom = wheter.Next(0, 2); 

    switch (moveInMenue)
    {
        case 0:
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"{menueChoice[0]} \t");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(menueChoice[1]);
            Console.WriteLine();
            break;
        case 1: 
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"{menueChoice[0]} \t");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(menueChoice[1]);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            break;

    }

    Console.WriteLine($"Your nomekop helth {yourNomekopHelth}");
    Console.WriteLine($"Enemy nomekop helth {enemyNomekopHealt}");

    Console.WriteLine($"attack items left {ammountOfAttackItems}");
    Console.WriteLine($"health items left {ammountOfHealthItems}");

    if (attackItemActivated == true)
    {
        Console.WriteLine($"{items[attackItemChoise].itemName} has been used, your multiplier is: {items[attackItemChoise].attackItemMultipliers}");
    }


    ConsoleKey consolekey = Console.ReadKey().Key;
    if (consolekey == ConsoleKey.A || consolekey == ConsoleKey.LeftArrow)
    {
        moveInMenue--;
    }
    else if (consolekey == ConsoleKey.D || consolekey == ConsoleKey.RightArrow)
    {
        moveInMenue++;
    }
    if (moveInMenue > 1)
    {
        moveInMenue = 0;
    }
    else if (moveInMenue < 0)
    {
        moveInMenue = 1;
    }
    if (moveInMenue == 0 && consolekey == ConsoleKey.Enter)
    {
        whichCoise[0] = true;
    }
    else if (moveInMenue == 1 && consolekey == ConsoleKey.Enter)
    {
        whichCoise[1] = true;
    }
    
    while (whichCoise[0]) // attack menu
    {
        

        switch (attackMenuMove)
        {
            case 0: //first attack
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{nomekopAttack[choiceUser,0]}\t");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{nomekopAttack[choiceUser, 1]}\t");
                Console.Write($"{nomekopAttack[choiceUser, 2]}\t");
                Console.Write(nomekopAttack[choiceUser, 3]);
                break;
            case 1: //second attack 
                Console.Clear();
                Console.Write($"{nomekopAttack[choiceUser, 0]}\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{nomekopAttack[choiceUser, 1]}\t");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{nomekopAttack[choiceUser, 2]}\t");
                Console.Write(nomekopAttack[choiceUser, 3]);
                break;
            case 2: //third attack
                Console.Clear();
                Console.Write($"{nomekopAttack[choiceUser, 0]}\t");
                Console.Write($"{nomekopAttack[choiceUser, 1]}\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{nomekopAttack[choiceUser, 2]}\t");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(nomekopAttack[choiceUser, 3]);
                break;
            case 3: //fourth attack
                Console.Clear();
                Console.Write($"{nomekopAttack[choiceUser, 0]}\t");
                Console.Write($"{nomekopAttack[choiceUser, 1]}\t");
                Console.Write($"{nomekopAttack[choiceUser, 2]}\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(nomekopAttack[choiceUser, 3]);
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }

        ConsoleKey attckMover = Console.ReadKey(true).Key;

        if (attckMover == ConsoleKey.A || attckMover == ConsoleKey.LeftArrow)
        {
            attackMenuMove--;
        }
        else if (attckMover == ConsoleKey.D || attckMover == ConsoleKey.RightArrow)
        {
            attackMenuMove++;
        }
        if (attackMenuMove < 0)
        {
            attackMenuMove = 3;
        }
        else if (attackMenuMove > 3)
        {
            attackMenuMove = 0;
        }

        if (attckMover == ConsoleKey.Enter) 
        {
            whichCoise[0] = false;
            enemtTurn = true;
        }
    }

    while (enemtTurn == true) // enemy attack
    {
        Random whatMove = new Random();
        enemyAtack = whatMove.Next(0, 3);
        attackTurn = true;
        enemtTurn = false;
    }

    while (attackTurn == true)
    {
        Random randomDamageRange = new Random();
        int randomMultiplier = randomDamageRange.Next(75, 125);
        float multiplierDamageRandom = randomMultiplier / 100;
        Console.Clear();
        //damage calculation (based on the one actualy used in Nomekop, just simplified)
        damageDelt = 2 * level;
        damageDelt = damageDelt / 5 + 2;
        damageDelt = damageDelt * attackDamage[choiceUser, attackMenuMove];
        damageDelt = damageDelt * yourNomekopStats[0] / enmeyNomekopnStats[1];
        damageDelt = damageDelt / 50;
        damageDelt = 2 + damageDelt * weatherConditions[weatherConditionRandom];
        Console.WriteLine($"You used {nomekopAttack[choiceUser, attackMenuMove]}");
        if (criticalHit > 8)
        {
            damageDelt *= critDamage;
            Console.WriteLine(crit);
        }
        damageDelt = damageDelt * multiplierDamageRandom;
        if (attackItemActivated == true)
        {
            damageDelt = damageDelt * items[attackItemChoise].attackItemMultipliers;
        }
        damageDelt = damageDelt * typeChart[choiceUser, typeEnemy];
        if (typeChart[choiceUser, typeEnemy] == 0.5)
        {
            Console.WriteLine(notEffective);
        }
        else if (typeChart[choiceUser, typeEnemy] == 2)
        {
            Console.WriteLine(superEfective);
        }
        else if (typeChart[choiceUser, typeEnemy] == 0)
        {
            Console.WriteLine(imune);
        }

        Console.WriteLine(" ");

        enemyNomekopHealt -= damageDelt;

        if (enemyNomekopHealt > 0)
        {
            randomMultiplier = randomDamageRange.Next(75, 125);
            multiplierDamageRandom = randomMultiplier / 100;
            //enemys damage calculation
            enemyDamage = 2 * enemylevel;
            enemyDamage = enemyDamage / 5 + 2;
            enemyDamage = enemyDamage * attackDamage[typeEnemy, enemyAtack];
            enemyDamage = enemyDamage * enmeyNomekopnStats[0] / yourNomekopStats[1];
            enemyDamage = enemyDamage / 50 + 2;
            enemyDamage = enemyDamage * weatherConditions[weatherConditionRandom] * items[attackItemChoise].attackItemMultipliers;
            Console.WriteLine($"The enemy used {nomekopAttack[typeEnemy, enemyAtack]}");
            if (enemyCritHit > 8)
            {
                enemyDamage *= enemyCritHit;
                Console.WriteLine(crit);
            }
            enemyDamage = enemyDamage * multiplierDamageRandom;
            enemyDamage = enemyDamage * typeChart[typeEnemy, choiceUser];
            if (typeChart[typeEnemy, choiceUser] == 0.5)
            {
                Console.WriteLine(notEffective);
            }
            else if (typeChart[typeEnemy, choiceUser] == 2)
            {
                Console.WriteLine(superEfective);
            }
            else if (typeChart[typeEnemy, choiceUser] == 0)
            {
                Console.WriteLine(imune);
            }
            yourNomekopHelth -= enemyDamage;
        }
        

        if (yourNomekopHelth <= 0) // stops code if your Nomekop faints
        {
            isRunning = false;
            attackTurn = false;
            break;
        }
        else if (enemyNomekopHealt <= 0) 
        {
            Console.WriteLine("enemy Nomekop fainted");
            Console.WriteLine($"Your Nomekop health: {yourNomekopHelth}");

            level++;
            enemylevel++;
            //levle upp
            yourNomekopHelth = types[choiceUser].health[chosenNomekop] + level;
            yourNomekopStats[0] = types[choiceUser].statAttack[chosenNomekop] + level;
            yourNomekopStats[1] = types[choiceUser].statDefence[chosenNomekop] + level;
            for (int i = 0; i < 17; i++)
            {
                attackDamage[i, 0] += level;
                attackDamage[i, 1] += level;
                attackDamage[i, 2] += level;
                attackDamage[i, 3] += level;
            }
            //gives enemy new Nomekop if it faints
            typeEnemy = enemyType.Next(0, 17); 
            enemyRandomStat = enemyType.Next(0, 5);
            enemyNomekopHealt = types[typeEnemy].health[enemyRandomStat] + enemylevel;
            enmeyNomekopnStats[0] = types[typeEnemy].statAttack[enemyRandomStat] + enemylevel;
            enmeyNomekopnStats[1] = types[typeEnemy].statDefence[enemyRandomStat] + enemylevel;
        }
        else //no pokemon faints
        {
            Console.WriteLine($"Your nomekop health: {yourNomekopHelth}");
            Console.WriteLine($"Enemy nomekop health: {enemyNomekopHealt}");
        }

        ConsoleKey noUse = Console.ReadKey(true).Key;
        damageDelt = 0;
        enemyDamage = 0;
        attackItemActivated = false;
        attackTurn = false;
    }

    

    while (whichCoise[1]) //itesm menu 
    {
        switch (choosItem)
        {
            case 0:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Attack items");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Helth items");
                break;
            case 1:
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Attack items");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Health items");
                break;
        }

        if (ammountOfHealthItems <= 0 && ammountOfAttackItems <= 0) //makse sure you actualy have items
        {
            whichCoise[1] = false;
            break;
        }

        ConsoleKey itemsMenuMove = Console.ReadKey(true).Key;

        if (itemsMenuMove == ConsoleKey.UpArrow)
        {
            choosItem--;
        }
        if (itemsMenuMove == ConsoleKey.DownArrow)
        {
            choosItem++;
        }

        Console.WriteLine(choosItem.ToString());

        if (choosItem < 0)
        {
            choosItem = 1;
        }
        else if (choosItem > 1)
        {
            choosItem = 0;
        }

        if (itemsMenuMove == ConsoleKey.Enter)
        {
            if (ammountOfAttackItems > 0 && choosItem == 0) //makse sure you can only get attakcitems if you actualy have them
            {
                whatKindOfItem[0] = true;
            }
            if (ammountOfHealthItems > 0 && choosItem == 1) // maske sure that you can only get health items if you actualy have them.
            {
                whatKindOfItem[1] = true;
            }

                whichCoise[1] = false;
        }
    }

    while (whatKindOfItem[0] == true) 
    {
        Console.Clear();
        for (int i = 0; i < 2; i++)
        {
            if (i == attackItemChoise)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine($"{items[i].itemName}, multiplier {items[i].attackItemMultipliers}");
            Console.ForegroundColor = ConsoleColor.White;
        }

        ConsoleKey itemAttackMove = Console.ReadKey(true).Key;

        if (itemAttackMove == ConsoleKey.UpArrow)
        {
            attackItemChoise--;
        }
        else if (itemAttackMove == ConsoleKey.DownArrow)
        {
            attackItemChoise++;
        }
        if (attackItemChoise < 0)
        {
            attackItemChoise = 1;
        }
        else if (attackItemChoise > 1)
        {
            attackItemChoise = 0;
        }

        if (itemAttackMove == ConsoleKey.Enter)
        {
            ammountOfAttackItems--;
            whatKindOfItem[0] = false;
            attackItemActivated = true;
        }

    }

    while (whatKindOfItem[1] == true) 
    {
        Console.Clear();

        for (int i = 0; i < 2;i++)
        {
            if (i == healthItemsChoise)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine($"{healthItems[i].NameItems}, + {healthItems[i].HealthRegained} hp");
            Console.ForegroundColor = ConsoleColor.White;
        }

        ConsoleKey itemHealthMove = Console.ReadKey(true).Key;

        if (itemHealthMove == ConsoleKey.UpArrow)
        {
            healthItemsChoise--;
        }
        else if (itemHealthMove == ConsoleKey.DownArrow)
        {
            healthItemsChoise++;
        }
        if (healthItemsChoise < 0)
        {
            healthItemsChoise = 1;
        }
        else if (healthItemsChoise > 1)
        {
            healthItemsChoise = 0;
        }

        if (itemHealthMove == ConsoleKey.Enter)
        {
            ammountOfHealthItems--;
            yourNomekopHelth += healthItems[healthItemsChoise].HealthRegained;
            whatKindOfItem[1] = false;
        }
        
    }

}

class NomekopType(string Name, ConsoleColor Color, float[] Health, float[] StatAttack, float[] StatDefence)
{
    // GNU GPL 3.0 LICENCE https://www.gnu.org/licenses/gpl-3.0.en.html
    // MADE BY HARRY MÅRTENSSON, THIS COMMENT MAY NOT BE REMOVED

    string name = Name;
    ConsoleColor color = Color;

    public float[] health = Health;
    public float[] statAttack = StatAttack;
    public float[] statDefence = StatDefence;

    public void Print(int index = 0)
    {
        ConsoleColor prevColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine($"{index}: {name}");
        Console.ForegroundColor = prevColor;
    }
}

class ItemsAttack(string ItemName, float AttackItemMultipliers)
{
    public string itemName = ItemName;
    public float attackItemMultipliers = AttackItemMultipliers;
}

class HealthItems(string NameItems, float HealthRegained)
{
    public string NameItems = NameItems;
    public float HealthRegained = HealthRegained;
}
