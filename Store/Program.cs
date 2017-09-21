using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp9
{
    class BaseEquipment
    {
        public int Id { get; protected set; }
        public int Price { get; protected set; }
        public BaseEquipment(int id, int price)
        {
            Id = id;
            Price = price;
        }
        public virtual string Info()
        {
            return ($"Id:{Id};Price:{Price};");
        }
    }
    class BaseSkiing : BaseEquipment
    {
        public string Model { get; protected set; }
        public string Level { get; protected set; }
        public BaseSkiing(int id, int price, string model, string level) : base(id, price)
        {
            Model = model;
            Level = level;
        }
        public override string Info()
        {
            return ($"Id:{Id};Price:{Price}; Model:{Model}; Level:{Level};");
        }
    }
    class BaseFoothball : BaseEquipment
    {
        public string Model { get; protected set; }
        public BaseFoothball(int id, int price, string model) : base(id, price)
        {
            Model = model;
        }
        public override string Info()
        {
            return ($"Id:{Id};Price:{Price}; Model:{Model};");
        }

    }
    class Boots : BaseSkiing
    {
        public int Size;
        public Boots(int id, int price, string model, string level, int size) : base(id, price, model, level)
        {
            this.Size = size;
        }
        public override string Info()
        {
            return base.Info() + $"Size:{Size.ToString()};";
        }
    }
    class Sticks : BaseSkiing
    {
        public string Length;
        public Sticks(int id, int price, string model, string level, string length) : base(id, price, model, level)
        {
            Length = length;
        }
        public override string Info()
        {
            return base.Info() + $"Length:{Length};";
        }
    }
    class Ball : BaseFoothball
    {
        public string Colour;
        public Ball(int id, int price, string model, string colour) : base(id, price, model)
        {
            Colour = colour;
        }
        public override string Info()
        {
            return base.Info() + $"Colour:{Colour };";
        }

    }
    class Uniform : BaseFoothball
    {
        public int Size;
        public int Number;
        public Uniform(int id, int price, string model, int size, int number) : base(id, price, model)
        {
            Size = size;
            Number = number;
        }
        public override string Info()
        {
            return base.Info() + $"Size:{Size.ToString()}; Number:{Number};";
        }
    }
    class Collection
    {
        static public BaseEquipment[] equipments;
        public Collection()
        {
            equipments = new BaseEquipment[] { };
        }
        static public void AddEq(BaseEquipment ski)
        {
            if (equipments == null)
            {
                equipments = new BaseEquipment[1];
            }
            else
            {
                equipments = Copy();
            }
            equipments[equipments.Length - 1] = ski;
        }
        static public BaseEquipment[] Copy()
        {
            BaseEquipment[] final = new BaseEquipment[equipments.Length + 1];
            for (int i = 0; i > equipments.Length; i++)
            {
                final[i] = equipments[i];
            }
            return final;
        }
    }
    class Store
    {
        public enum Error
        {
            InputValue,
            Command
        }
        private Collection collection;
        public Store()
        {
            collection = new Collection();
        }
        public void MainMenu()
        {
            StringBuilder menu = new StringBuilder();
            menu.AppendLine("Please enter 1 if you want to add  equipment ");
            menu.AppendLine("Please enter 2 if you want to see list of all equipment ");
            menu.AppendLine("Any other if you want to quit");
            Console.WriteLine(menu);
            int a = int.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    AddFunction();
                    break;
                case 2:

                    break;
                default:
                    Console.WriteLine("Bye");
                    break;
            }
        }

        public void AddFunction()
        {
            while (true)
            {
                StringBuilder add_menu = new StringBuilder();
                add_menu.AppendLine("Please enter 1 if you want to add skiing equipment");
                add_menu.AppendLine("Please enter 2 if you want to add foothball equipment");
                add_menu.AppendLine("Any other if you want to quit");
                Console.WriteLine(add_menu);
                int a = int.Parse(Console.ReadLine());
                switch (a)
                {
                    case 1:
                        AddSkiingEq();
                        break;
                    case 2:
                        AddFoothballEq();
                        break;
                    default:
                        MainMenu();
                        break;
                }
            }
        }
        public void AddSkiingEq()
        {
            int id = 0;
            int price = 0;
            string model = "";
            string level = "";

            if (!BaseInfoSkiing(out id, out price, out model, out level))
                return;
            SetSkiingEq(id, price, model, level);
        }
        public bool BaseInfoSkiing(out int id, out int price, out string model, out string level)
        {
            bool Flag = true;
            Console.Clear();
            Console.WriteLine("Id of the equipment:");
            id = int.Parse(Console.ReadLine());
            Console.WriteLine("Price of the equipment:");
            bool result = int.TryParse(Console.ReadLine(), out price);
            Console.WriteLine("Model of the equipment:");
            model = Console.ReadLine();
            Console.WriteLine("Level of the equipment:");
            level = Console.ReadLine();
            if (result)
            {
                result = price >= 0;
            }
            if (!result)
            {
                if (IncorrectInput(Error.InputValue))
                    BaseInfoSkiing(out id, out price, out model, out level);
                else
                    Flag = false;
            }
            return Flag;
        }
        public bool IncorrectInput(Error error = Error.Command)
        {
            bool result = false;
            StringBuilder str = new StringBuilder();
            if (error == Error.Command)
            {
                str.Append("Can't undestand this command");
            }
            else
            {
                str.Append("Invalid input");
            }
            str.Append("If you want to try again press 1");
            str.Append("If you want to quit press any other");
            Console.WriteLine(str);
            string answer = Console.ReadLine();
            if (answer == "1")
            {
                result = true;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Goodbye");
            }
            return result;
        }
        public void SetSkiingEq(int id, int price, string model, string level)
        {
            Console.Clear();
            StringBuilder add_menu = new StringBuilder();
            add_menu.AppendLine("Please enter 1 if you want to add boots");
            add_menu.AppendLine("Please enter 2 if you want to add sticks");
            add_menu.AppendLine("Any other if you want to quit");
            Console.WriteLine(add_menu);
            int a = int.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    AddBoots(id, price, model, level);
                    break;
                case 2:
                    AddSticks(id, price, model, level);
                    break;
                default:
                    {
                        if (IncorrectInput()) ;
                        SetSkiingEq(id, price, model, level);
                        break;
                    };
            }
        }
        public void AddBoots(int id, int price, string model, string level)
        {
            Console.Clear();
            Console.WriteLine("Please enter size of boots (from 34 to 45)");
            int size = int.Parse(Console.ReadLine());
            if (size > 34 && size < 45)
            {
                AddFinallSkiingEq(id, price, model, level, size);
            }
            else
            {
                Console.WriteLine("Invalid value");
            }
        }
        public void AddSticks(int id, int price, string model, string level)
        {
            Console.Clear();
            Console.WriteLine("Please enter length of sticks");
            string length = Console.ReadLine();
            AddFinallSkiingEq(id, price, model, level, length);
        }
        public void AddFinallSkiingEq(int id, int price, string model, string level, int size)
        {
            Console.Clear();
            Collection.AddEq(new Boots(id, price, model, level, size));
            Console.WriteLine("Congratulations!You added Boots");
            MainMenu();
        }
        public void AddFinallSkiingEq(int id, int price, string model, string level, string length)
        {
            Console.Clear();
            Collection.AddEq(new Sticks(id, price, model, level, length));
            Console.WriteLine("Congratulations!You added Boots");
            MainMenu();
        }

        public void AddFoothballEq()
        {
            int id = 0;
            int price = 0;
            string model = "";
            if (!BaseInfoFoothball(out id, out price, out model))
                return;
            SetFoothballEq(id, price, model);
        }
        public bool BaseInfoFoothball(out int id, out int price, out string model)
        {
            bool Flag = true;
            Console.Clear();
            Console.WriteLine("Id of the equipment:");
            id = int.Parse(Console.ReadLine());
            Console.WriteLine("Price of the equipment:");
            bool result = int.TryParse(Console.ReadLine(), out price);
            Console.WriteLine("Model of the equipment:");
            model = Console.ReadLine();
            if (result)
            {
                result = price >= 0;
            }
            if (!result)
            {
                if (IncorrectInput(Error.InputValue))
                    BaseInfoFoothball(out id, out price, out model);
            }
            else
            {
                Flag = false;
            }
            return Flag;
        }

        public void SetFoothballEq(int id, int price, string model)
        {
            StringBuilder add_menu = new StringBuilder();
            add_menu.AppendLine("Please enter 1 if you want to add ball");
            add_menu.AppendLine("Please enter 2 if you want to add uniform");
            add_menu.AppendLine("Please enter 3 if you want to quit");
            Console.WriteLine(add_menu);
            int a = int.Parse(Console.ReadLine());
            switch (a)
            {
                case 1:
                    AddBall(id, price, model);
                    break;
                case 2:
                    AddUniform(id, price, model);
                    break;
                case 3:
                    break;
            }
        }
        public void AddBall(int id, int price, string model)
        {
            Console.Clear();
            Console.WriteLine("Please enter colour of ball");
            string colour = Console.ReadLine();
            AddFinallFoothballEq(id, price, model, colour);
        }
        public void AddUniform(int id, int price, string model)
        {
            Console.Clear();
            Console.WriteLine("Please enter size of uniform");
            int size = int.Parse(Console.ReadLine());
            Console.WriteLine("Please enter number of uniform");
            int number = int.Parse(Console.ReadLine());
            AddFinallFoothballEq(id, price, model, size, number);
        }
        public void AddFinallFoothballEq(int id, int price, string model, string colour)
        {
            Console.Clear();
            Collection.AddEq(new Ball(id, price, model, colour));
            Console.WriteLine("Congratulations!You added Ball");
            MainMenu();
        }
        public void AddFinallFoothballEq(int id, int price, string model, int size, int number)
        {
            Console.Clear();
            Collection.AddEq(new Uniform(id, price, model, size, number));
            Console.WriteLine("Congratulations!You added Uniform");
            MainMenu();
        }
    }
    class Program
    {
        static Store store;
        static void Main(string[] args)
        {
            store = new Store();
            store.MainMenu();
            Console.ReadLine();
        }

    }


}


