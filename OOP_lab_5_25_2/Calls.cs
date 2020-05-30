using System;
using System.IO;

namespace OOP_lab_5_25_1
{
    class Calls : TelephoneNumber
    {
        private DateTime _date;
        private double _minutesCount;
        private double _spentMoney;
        private const string _format = "{0, -20} {1, -15} {2, -15} {3, -30} {4, -20}";


        public DateTime Date
        {
            get => _date;
            set => _date = value;
        }

        public double MinutesCount
        {
            get => _minutesCount;
            set => _minutesCount = value;
        }

        public double SpentMoney
        {
            get => _spentMoney;
            set => _spentMoney = value;
        }

        public virtual double MinuteValue()
        {
            return _spentMoney / _minutesCount;
        }

        public Calls()
        {
            base.Number = "Не вказано.";
            base.Operator = "Не вказано.";

            _date = DateTime.Parse("01.01.01");
            _minutesCount = 0;
            _spentMoney = 0;
        }

        public Calls(string Number, string Operator, DateTime Date, double MinutesCount, double SpentMoney)
        {
            base.Number = UkrainianI(Number); ;
            base.Operator = UkrainianI(Operator);

            _date = Date;
            _minutesCount = MinutesCount;
            _spentMoney = SpentMoney;
        }

        public static void Read()
        {
            ReadBase();
            ReadKey();
        }

        public static void ReadBase()
        {
            StreamReader file = new StreamReader("base.txt");

            string[] tempStr = file.ReadToEnd().Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            InitialiseBase(tempStr.Length / 5);

            for (int i = 0; i < tempStr.Length; i += 5)
            {
                Program.abonents[i / 5] = new Calls(tempStr[i], tempStr[i + 1], DateTime.Parse(tempStr[i + 2]), int.Parse(tempStr[i + 3]), int.Parse(tempStr[i + 4]));
            }

            file.Close();
        }

        private static void ReadKey()
        {

        Start:

            Console.WriteLine("Додавання записiв: +");
            Console.WriteLine("Редагування записiв: E");
            Console.WriteLine("Знищення записiв: -");
            Console.WriteLine("Виведення записiв: Enter");
            Console.WriteLine("Середня платня в день; : A");
            Console.WriteLine("Кiлькiсть днiв, коли вартiсть хвилини розмови перевищувала задане значення: B");
            Console.WriteLine("Днi, коли кiлькiсть хвилин розмов була парна: O");
            Console.WriteLine("Вихiд: Esc");

            ConsoleKey key = Console.ReadKey().Key;

            switch (key)
            {
                case ConsoleKey.OemPlus:
                    Add();
                    goto Start;

                case ConsoleKey.E:
                    Edit();
                    goto Start;

                case ConsoleKey.A:
                    Average();
                    goto Start;

                case ConsoleKey.O:
                    Odd();
                    goto Start;

                case ConsoleKey.B:
                    Above();
                    goto Start;

                case ConsoleKey.OemMinus:
                    Remove();
                    goto Start;

                case ConsoleKey.Enter:
                    Write();
                    goto Start;

                case ConsoleKey.Escape:
                    return;

                default:
                    Console.WriteLine();
                    goto Start;
            }
        }

        public static void Add()
        {
            StreamWriter file = new StreamWriter("base.txt", true);

            Console.WriteLine("\nВведiть новi данi");

            Console.Write("Номер: ");

            file.WriteLine(Console.ReadLine());

            Console.Write("Оператор: ");

            file.WriteLine(Console.ReadLine());

        RetryDate:
            Console.Write("Дата: ");

            try
            {
                file.WriteLine(DateTime.Parse(Console.ReadLine()));
            }
            catch (SystemException)
            {
                Console.WriteLine("Неправильно вказана дата!");

                goto RetryDate;
            }

        RetryMinutesCount:
            Console.Write("Кiлькiсть хвилин: ");

            try
            {
                file.WriteLine(int.Parse(Console.ReadLine()));
            }
            catch (SystemException)
            {
                Console.WriteLine("Кiлькiсть хвилин має бути вказана лише числом!");

                goto RetryMinutesCount;
            }

        RetrySpentMoney:
            Console.Write("Витраченi грошi: ");

            try
            {
                file.WriteLine(int.Parse(Console.ReadLine()));
            }
            catch (SystemException)
            {
                Console.WriteLine("Кiлькiсть витрачених грошей має бути вказана лише числом!");

                goto RetrySpentMoney;
            }

            file.Close();

            ReadBase();
        }

        public static void Remove()
        {
            Console.WriteLine();

            Write();

            Console.Write("Порядковий номер запису для видалення: ");

            bool[] remove = new bool[Program.abonents.Length];

            for (int i = 0; i < remove.Length; ++i)
            {
                remove[i] = false;
            }

            try
            {
                remove[int.Parse(Console.ReadLine()) - 1] = true;
            }
            catch (SystemException)
            {
                Console.WriteLine("Такого запису не iснує!");
                return;
            }

            StreamWriter file = new StreamWriter("base.txt");

            for (int i = 0; i < Program.abonents.Length; ++i)
            {
                if (!remove[i])
                {
                    file.WriteLine(Program.abonents[i].Number);
                    file.WriteLine(Program.abonents[i].Operator);
                    file.WriteLine(Program.abonents[i].Date.ToShortDateString());
                    file.WriteLine(Program.abonents[i].MinutesCount);
                    file.WriteLine(Program.abonents[i].SpentMoney);
                }
            }

            Console.Write("Видалено.\n");

            file.Close();

            ReadBase();
        }

        public static void Edit()
        {
            Console.WriteLine();

            Write();

            Console.Write("Порядковий номер запису для редагування: ");

            bool[] edit = new bool[Program.abonents.Length];

            for (int i = 0; i < edit.Length; ++i)
            {
                edit[i] = false;
            }

            try
            {
                edit[int.Parse(Console.ReadLine()) - 1] = true;
            }
            catch (SystemException)
            {
                Console.WriteLine("Такого запису не iснує!");
                return;
            }

            StreamWriter file = new StreamWriter("base.txt");

            for (int i = 0; i < Program.abonents.Length; ++i)
            {
                if (edit[i])
                {
                    Console.WriteLine("Введiть новi данi");

                    Console.Write("Номер: ");

                    file.WriteLine(Console.ReadLine());

                    Console.Write("Оператор: ");

                    file.WriteLine(Console.ReadLine());

                RetryDate:
                    Console.Write("Дата: ");

                    try
                    {
                        file.WriteLine(DateTime.Parse(Console.ReadLine()));
                    }
                    catch (SystemException)
                    {
                        Console.WriteLine("Неправильно вказана дата!");

                        goto RetryDate;
                    }

                RetryMinutesCount:
                    Console.Write("Кiлькiсть хвилин: ");

                    try
                    {
                        file.WriteLine(int.Parse(Console.ReadLine()));
                    }
                    catch (SystemException)
                    {
                        Console.WriteLine("Кiлькiсть хвилин має бути вказана лише числом!");

                        goto RetryMinutesCount;
                    }

                RetrySpentMoney:
                    Console.Write("Витраченi грошi: ");

                    try
                    {
                        file.WriteLine(int.Parse(Console.ReadLine()));
                    }
                    catch (SystemException)
                    {
                        Console.WriteLine("Кiлькiсть витрачених грошей має бути вказана лише числом!");

                        goto RetrySpentMoney;
                    }
                }
                else
                {
                    file.WriteLine(Program.abonents[i].Number);
                    file.WriteLine(Program.abonents[i].Operator);
                    file.WriteLine(Program.abonents[i].Date.ToShortDateString());
                    file.WriteLine(Program.abonents[i].MinutesCount);
                    file.WriteLine(Program.abonents[i].SpentMoney);
                }
            }

            Console.Write("Змiни внесено.\n");

            file.Close();

            ReadBase();
        }

        public static void InitialiseBase(int n)
        {
            Program.abonents = new Calls[n];
        }

        public static void Average()
        {
            Console.Write("Середня платня в день: ");

            double sum = 0;

            for (int i = 0; i < Program.abonents.Length; ++i)
            {
                sum += Program.abonents[i].SpentMoney;
            }

            Console.WriteLine(sum / Program.abonents.Length);
        }

        public static void Above()
        {
            Console.WriteLine("Кiлькiсть хвилин: ");

            int n = int.Parse(Console.ReadLine());

            Console.WriteLine("Днi, коли кiлькiсть хвилин була бiльшою за вказане значення: ");

            Console.WriteLine(_format, "Номер", "Оператор", "Дата", "Кiлькiсть хвилин", "Використанi кошти");

            for (int i = 0; i < Program.abonents.Length; ++i)
            {
                if (Program.abonents[i].MinutesCount % 2 == 0)
                {
                    Console.WriteLine(_format, Program.abonents[i].Number, Program.abonents[i].Operator, Program.abonents[i].Date.ToShortDateString(), Program.abonents[i].MinutesCount, Program.abonents[i].SpentMoney);
                }
            }
        }

        public static void Odd()
        {
            Console.WriteLine("Днi, коли кiлькiсть хвилин була парна: ");

            Console.WriteLine(_format, "Номер", "Оператор", "Дата", "Кiлькiсть хвилин", "Використанi кошти");

            for (int i = 0; i < Program.abonents.Length; ++i)
            {
                if (Program.abonents[i].MinutesCount % 2 == 0)
                {
                    Console.WriteLine(_format, Program.abonents[i].Number, Program.abonents[i].Operator, Program.abonents[i].Date.ToShortDateString(), Program.abonents[i].MinutesCount, Program.abonents[i].SpentMoney);
                }
            }
        }

        public static void Write()
        {
            Console.WriteLine(_format, "Номер", "Оператор", "Дата", "Кiлькiсть хвилин", "Використанi кошти");

            for (int i = 0; i < Program.abonents.Length; ++i)
            {
                Console.WriteLine(_format, Program.abonents[i].Number, Program.abonents[i].Operator, Program.abonents[i].Date.ToShortDateString(), Program.abonents[i].MinutesCount, Program.abonents[i].SpentMoney);
            }
        }
    }
}
