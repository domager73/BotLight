using XY;

#region Assistents
int InputInt(string massage)
{
    bool check = false;
    int number;
    Console.Write(massage);

    do
    {
        check = int.TryParse(Console.ReadLine(), out number);
    } while (!check);

    return number;
}

int SearchOfY(char[,] field, char elem)
{
    int y = 0;

    for (int i = 0; i < field.GetLength(0); i++)
    {
        for (int j = 0; j < field.GetLength(1); j++)
        {
            if (field[i, j] == elem)
            {
                y = i;
            }
        }
    }

    return y;
}
int SearchOfX(char[,] field, char elem)
{
    int x = 0;

    for (int i = 0; i < field.GetLength(0); i++)
    {
        for (int j = 0; j < field.GetLength(1); j++)
        {
            if (field[i, j] == elem)
            {
                x = j;
            }
        }
    }

    return x;
}

void PrintAction() 
{
    Console.WriteLine("1: Играть в уровни игры");
    Console.WriteLine("2: Создать свой уровень");
}
# endregion

#region Field
char[,] FieldCreation(string nameFile)
{
    StreamReader reader = new StreamReader(nameFile);

    int cols = int.Parse(reader.ReadLine());
    int rows = int.Parse(reader.ReadLine());

    char[,] field = new char[cols, rows];
    for (int i = 0; i < field.GetLength(0); i++)
    {
        char[] str = reader.ReadLine().ToCharArray();
        for (int j = 0; j < field.GetLength(1); j++)
        {
            if (str[j] == 'X')
            {
                field[i, j] = 'a';
            }
            else
            {
                field[i, j] = str[j];
            }
        }
    }

    reader.Close();

    return field;
}

void CheckVin(bool chekLoss, char[,] field, Coordinate Player)
{
    if (chekLoss && field[Player.Y, Player.X] == '0')
    {
        Console.WriteLine("Вы смогли пройти уровень");
    }
    else
    {
        Console.WriteLine("Вы проиграли, попробуйте изменить ходы");
    }

}

void PrintField(char[,] field, Coordinate Player)
{
    for (int i = 0; i < field.GetLength(0); i++)
    {
        for (int j = 0; j < field.GetLength(1); j++)
        {
            if ((Player.Y == i) && (Player.X == j))
            {
                Console.Write('X');
            }
            else
            {
                Console.Write(field[i, j]);
            }
        }
        Console.WriteLine();
    }
}
# endregion

#region Move
char[] ReadMoves()
{
    StreamReader reader = new StreamReader(@"move.txt");

    char[] moves = reader.ReadLine().ToCharArray();

    reader.Close();

    return moves;
}

bool CheckMove(Coordinate Player, char[,] field)
{
    try
    {
        return field[Player.Y, Player.X] == '#' ? true : false;
    }
    catch
    {
        return true;
    }
}

void Move(ref Coordinate Player, char move, char[,] field, ref bool checkLoss)
{
    switch (move)
    {
        case 'w':
            {
                Player.Y--;
                if (CheckMove(Player, field) && Player.Y < 1)
                {
                    Player.Y++;

                    checkLoss = false;
                }
                break;
            }
        case 's':
            {
                Player.Y++;
                if (CheckMove(Player, field) || Player.Y > field.GetLength(0) - 1)
                {
                    Player.Y--;

                    checkLoss = false;
                }

                break;
            }
        case 'a':
            {
                Player.X--;
                if (CheckMove(Player, field) || Player.X < 1)
                {
                    Player.X++;

                    checkLoss = false;
                }

                break;
            }
        case 'd':
            {
                Player.X++;
                if (CheckMove(Player, field) || Player.X > field.GetLength(1) - 1)
                {
                    Player.X--;

                    checkLoss = false;
                }

                break;
            }
        default:
            {
                Console.WriteLine("Ходы введены не правильно");

                break;
            }
    }
}
#endregion

bool stopGame = true;

while (stopGame)
{
    PrintAction();
    int action = InputInt("Введите действие: ");
    string level = " ";

    switch (action) 
    {
        case 1: 
            {
                Console.WriteLine("Уровень 1: light \nУровень 2: middle\nУровень 3: harde");

                int numberLevel = InputInt("Введите уровень: ");
                level = @$"C:\Users\user\source\repos\BotLigh\BotLight\Новая папка\{numberLevel}level.txt";
                break;
            } 
        case 2:
            {
                Console.WriteLine("Введите ");

                Console.WriteLine("Укажите место на диске: ");

                level = Console.ReadLine();

                break;
            }
    }
    Console.Clear();

    Console.WriteLine("Уровень 1: light \nУровень 2: middle\nУровень 3: harde");

    char[,] field = FieldCreation(level);
    char[] moves = ReadMoves();
    bool chekLoss = true;

    Coordinate Player = new Coordinate();
    Player.X = SearchOfX(field, 'X');
    Player.X = SearchOfY(field, 'X');

    Console.Clear();

    Console.WriteLine("Вам нужно пройти этот уровень");
    PrintField(field, Player);

    Console.ReadLine();
    Console.Clear();

    int count = 0;

    while (chekLoss && count < moves.Length)
    {
        Move(ref Player, moves[count], field, ref chekLoss);

        PrintField(field, Player);

        Thread.Sleep(300);

        Console.Clear();
        count++;
    }

    CheckVin(chekLoss, field, Player);

    Console.ReadLine();
}