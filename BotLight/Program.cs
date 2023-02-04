#region Assistents
using System.Linq.Expressions;

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

# endregion
#region Field
char[,] FieldCreation(ref int XPlayer, ref int YPlayer, ref int XPortal, ref int YPortal, string nameFile)
{
    StreamReader reader = new StreamReader(nameFile);

    int cols = int.Parse(reader.ReadLine());
    int rows = int.Parse(reader.ReadLine());

    char[,] fieldLVL1 = new char[cols, rows];
    for (int i = 0; i < fieldLVL1.GetLength(0); i++)
    {
        char[] strfield = reader.ReadLine().ToCharArray();
        for (int j = 0; j < strfield.Length; j++)
        {
            if (strfield[j] == 'X')
            {
                YPlayer = i;
                XPlayer = j;
                fieldLVL1[i, j] = 'a';
            }
            else if (strfield[j] == '0')
            {
                YPortal = i;
                XPortal = j;
            }
            else
            {
                fieldLVL1[i, j] = strfield[j];
            }
        }
    }

    reader.Close();

    return fieldLVL1;
}

void CheckVin(int XPlayer, int YPlayer, int XPortal, int YPortal)
{
    if (XPlayer == XPortal && YPlayer == YPortal)
    {
        Console.WriteLine("Вы смогли пройти уровень");
    }
    else
    {
        Console.WriteLine("Вы проиграли, попробуйте изменить ходы");
    }
}

void PrintField(char[,] field, int YPlayer, int XPlayer, int XPortal, int YPortal)
{
    for (int i = 0; i < field.GetLength(0); i++)
    {
        for (int j = 0; j < field.GetLength(1); j++)
        {

            if ((YPlayer == i) && (XPlayer == j))
            {
                Console.Write('X');
            }
            else if (YPortal == i && XPortal == j)
            {
                Console.Write('0');
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
    StreamReader reader = new StreamReader(@"C:\Users\user\Desktop\move.txt");

    char[] moves = reader.ReadLine().ToCharArray();

    reader.Close();

    return moves;
}

bool CheckMove(int YPlayer, int XPlayer, char[,] field)
{
    try
    {
        return field[YPlayer, XPlayer] == '#' ? true : false;
    }
    catch 
    {
        return true;
    }
}

void Move(ref int XPlayer, ref int YPlayer, char move, char[,] field, ref bool checkLoss)
{
    switch (move)
    {
        case 'w':
            {
                YPlayer--;
                if (CheckMove(YPlayer, XPlayer, field) && YPlayer < 1)
                {
                    YPlayer++;

                    checkLoss = false;
                }
                break;
            }
        case 's':
            {
                YPlayer++;
                if (CheckMove(YPlayer, XPlayer, field) || YPlayer > field.GetLength(0) -1)
                {
                    YPlayer--;

                    checkLoss = false;
                }

                break;
            }
        case 'a':
            {
                XPlayer--;
                if (CheckMove(YPlayer, XPlayer, field) || XPlayer < 1)
                {
                    XPlayer++;

                    checkLoss = false;
                }

                break;
            }
        case 'd':
            {
                XPlayer++;
                if (CheckMove(YPlayer, XPlayer, field) || XPlayer > field.GetLength(1) -1)
                {
                    YPlayer--;

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
    int XPlayer = 0;
    int YPlayer = 0;
    int XPortal = 0;
    int YPortal = 0;

    Console.WriteLine("Уровень 1: light \nУровень 2: middle\nУровень 3: harde");
    int level = InputInt("Введите уровень: ");

    char[,] field = FieldCreation(ref XPlayer, ref YPlayer, ref XPortal, ref YPortal, $@"C:\Users\user\Desktop\{level}level.txt");
    char[] moves = ReadMoves();
    bool chekLoss = true;
    int count = 0;

    Console.ReadLine();
    Console.Clear();

    while (count < moves.Length && chekLoss)
    {
        Move(ref XPlayer, ref YPlayer, moves[count], field, ref chekLoss);

        PrintField(field, YPlayer, XPlayer, XPortal, YPortal);

        Thread.Sleep(300);

        Console.Clear();

        count++;
    }

    CheckVin(XPlayer, YPlayer, XPortal, YPortal);

    Console.ReadLine();
}