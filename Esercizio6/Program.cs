internal class Program
{
    public int turn;
    public int maxTurn;
    public bool end;
    public GameElement[] gameElements;
    public Character player;

    public void CheckTurn()
    {
        foreach (GameElement obj in gameElements)
        {
            float distance = player.pos.Distance(obj.pos);
            if(distance <= 2 && distance > 0)
            {
                Console.WriteLine($"Be careful!! {obj.name} is distant {Math.Round(distance)} meter.");
            }
            else if(distance < 1) 
            {

                if(obj.name == "Trap")
                {
                    Console.WriteLine("Have you find a Trap and are you DEAD!");
                    player.isDeath = true;
                }
                else if (obj.name == "Gem")
                {
                    Console.WriteLine("Have u find the Gam! YOU WIN!");
                }
                end = true;
            }
        }
    }

    public class Vector2
    {
        public int[] vector;
        public int X { get; set; }
        public int Y { get; set; }

        public Vector2(int x,int y)
        {
            vector = new int[] {x,y};
        }
        public float Distance(Vector2 secondVector)
        {
            float distanceX = (float)Math.Pow(secondVector.vector[0] - vector[0],2);
            float distanceY = (float)Math.Pow(secondVector.vector[1] - vector[1],2);

            return (float)Math.Sqrt(distanceX + distanceY);
        }
    }
    public class Character
    {
        public Vector2 pos;
        public bool isDeath;

        public Character(Vector2 _pos)
        {
            pos = _pos;
            isDeath = false;
        }
        public Vector2 SumPos(Character char1,Character char2)
        {
            int sommaX = char1.pos.X + char2.pos.X;
            int sommaY = char2.pos.Y + char1.pos.Y;

            return new Vector2(sommaX, sommaY);
        }
    }
    public class GameElement
    {
        public Vector2 pos;
        public string name;


        public Vector2 PosCasual()
        {
            Random random = new Random();
            int randomX = random.Next(1, 6);
            int randomY = random.Next(1, 6);

            return new Vector2(randomX, randomY);
        }

    }
    public class Trap : GameElement
    {
        public Trap(Vector2 _pos)
        {
            this.name = "Trap";
            this.pos = _pos;
        }
    }
    public class Gem : GameElement
    {
        public Gem(Vector2 _pos)
        {
            this.name = "Gem";
            this.pos = _pos;
        }
    }

    private static void Main(string[] args)
    {
        Program p = new Program();
        Program.GameElement gameElement = new GameElement();

        p.turn = 1;
        p.maxTurn = 10;
        p.end = false;

        p.player = new Character(new Vector2(0, 0));

        p.gameElements = new GameElement[3];

        Console.WriteLine("You're playing in a 5x5 table");

        for(int i = 0; i < p.gameElements.Length;i++)
        {
            if(i < p.gameElements.Length - 1)
            {
                p.gameElements[i] = new Trap(gameElement.PosCasual());
            }
            else
            {
                p.gameElements[i] = new Gem(gameElement.PosCasual());
                float distance = p.player.pos.Distance(p.gameElements[i].pos);

                Random random = new Random();
                int rand = random.Next(1,3 );

                if (random.Next(0,2) == 1)
                {
                    Console.WriteLine($"The Gem is located about {Math.Round(distance) - rand} unit");
                }
                else
                {
                    Console.WriteLine($"The Gem is located about  {Math.Round(distance) + rand} unit");

                }
            }
        }
        
        //inizio del gioco
        for(int i = 0;i < p.maxTurn; i++)
        {
            Console.WriteLine("You are in position 0,0");
            Console.WriteLine("Where do you want to move? Enter the X-axis,press \"Enter\" and after that the Y-axis and press \"Enter\"");

            p.player.pos.vector[0] = int.Parse(Console.ReadLine());
            p.player.pos.vector[1] = int.Parse(Console.ReadLine());

            p.CheckTurn();
            
            if (p.end)
            {
                break;
            }

        }


    }
}