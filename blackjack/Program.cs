using System.Collections;

string Spelen = "j";
int Tokens = 350;
int Bet = 0;
int SoortKaart;
int[] Deck = new int[9] { 2, 3, 4, 5, 6, 7, 8, 9, 10 };
Random random = new Random(); 
List<int> Hand = new List<int>() ;
List<int> HandDealer = new List<int>();
string Keuze;
int Gewonnen = 1; // 1 = gewonnen, 0 = draw, -1 = verloren+
string Bericht = "";

while (Spelen == "j" && Tokens > 5)
{
    Bericht = "";
    Gewonnen = 1;
    HandDealer.Clear();
    Hand.Clear() ;
    do
    {
        Console.Clear() ;
        Console.WriteLine("Tokens: " + Tokens);
        Console.WriteLine("Hoeveel tokens wil je ingeven ?\n10\n50\n100\n1000");
        try
        {
            Bet = Convert.ToInt32(Console.ReadLine());
        }
        catch
        {   }
    } while (Bet != 10 && Bet != 50 && Bet != 100 && Bet != 1000 || (Tokens-Bet) < 0 );
    Tokens -= Bet;
    for (int i = 0; i < 2; i++)
    {
        SoortKaart = random.Next(0, 9);
        Hand.Add(Deck[SoortKaart]);
    }
    for (int i = 0; i < 2; i++)
    {
        SoortKaart = random.Next(0, 9);
        HandDealer.Add(Deck[SoortKaart]);
    }
    Console.Clear();
    Console.WriteLine("Uw kaarten om te beginnen zijn: {0} & {1}\nDus u heeft {2} punten", Hand[0], Hand[1], Hand.Sum());
    Console.WriteLine("de kaarten van de dealer zijn: {0} & {1}\nde dealer heeft {2} punten", HandDealer[0], HandDealer[1], HandDealer.Sum());
    Console.WriteLine("Hit or Stand");
    Keuze = Console.ReadLine();
    while (Keuze == "hit" && Hand.Sum() < 21)
    {
        Console.Clear();
        SoortKaart = random.Next(0, 9);
        Hand.Add(Deck[SoortKaart]);
        Console.WriteLine("U heeft een {0} kaart gekregen, uw som is nu: {1}", Hand[Hand.Count-1], Hand.Sum());
        if (Hand.Sum() > 21)
        {
            Console.Clear() ;
            Bericht = "BUST !!";
            Gewonnen = -1;
        }
        else  
        {
            Console.WriteLine("Hit or Stand");
            Keuze = Console.ReadLine().ToLower();
        }
    }
    //Console.WriteLine(Gewonnen);
    //Console.ReadLine();
    if (Gewonnen == 1)
    {

        Console.Clear();
        Console.WriteLine("Nu zal de dealer spelen\n");
        while (HandDealer.Sum() < 17 && Gewonnen == 1)
        {
            SoortKaart = random.Next(0, 9);
            HandDealer.Add(Deck[SoortKaart]);
        }

        if (HandDealer.Sum() > 21)
        {
            Gewonnen = 1;
            Bericht = "Dealer busted";
        }
        else if (Hand.Sum() > HandDealer.Sum())
        {
            Gewonnen = 1;
            Bericht = "je bent gewonnen !!";
        }
        else if (Hand.Sum() == HandDealer.Sum())
        {
            Gewonnen = 0;
            Bericht = "DRAW";
            Tokens += Bet;
        }
        else
        {
            Gewonnen = -1;
            Bericht = "Je bent verloren :(";
        }
        switch (Gewonnen)
        {
            case 1:
                Tokens += Bet * 2;
                Bericht += ("\nJe hebt " + (Bet * 2) + " Tokens gewonnen");
                break;
        }
        Console.WriteLine("jij: {0}\ndealer: {1}\n", Hand.Sum(), HandDealer.Sum());
    }
    Console.WriteLine(Bericht);
    Console.WriteLine("\nWil je nog eens ? (j/n)");
    Spelen = Console.ReadLine().ToLower();
    
}
Console.Clear();
Console.WriteLine("tot later !!!" + Environment.NewLine + "Punten over: " + Tokens);
Console.ReadLine();
