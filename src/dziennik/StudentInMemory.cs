using System;
using System.Collections.Generic;
using System.Linq;

namespace dziennik
{

    public class StudentInMemory : StudentBase
    {
        public StudentInMemory(string name) : base(name)
        {

        }
        public List<double> grades = new List<double>();
        public override event GradeAddedDelegate GradeAdded;

        public override void AddOpinion(double grade)
        {
            this.grades.Add(grade);
            if(GradeAdded != null && grade < 3)
            {
                GradeAdded(this, new EventArgs());
            }
        }

        public override void AddOpinion(double grade, char control)
        {
            if(control == '-')
                grade -= 0.25;
            if(control == '+')
                grade += 0.25;

            this.grades.Add(grade);
            if(GradeAdded != null && grade < 3)
            {
                GradeAdded(this, new EventArgs());
            }
        }

        public override void ChangeStudent()
        {
                Console.WriteLine("Podaj nowe imie: ");
                var name = Console.ReadLine();
                char[] arr = name.ToCharArray();
                var result = false;
                var s = false;
                foreach (var a in arr)
                {
                    s = Char.IsDigit(a);
                    if(s == true)
                    {
                        result = s;
                    }
                }
                if(result) Console.WriteLine("W podanym imieniu wystepuje cyfra. Imienia nie dodano.");
                else 
                {
                    this.Name = name;
                    this.grades.Clear();

                    Console.WriteLine($"Imie zmieniono na: {this.Name}");
                    Console.WriteLine($"Wyczyszono liste ocen.");

                    Console.WriteLine();
                    Console.WriteLine("Nacisnij dowolny klawisz zeby kontynuowac...");
                    Console.ReadLine();
                }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            foreach(var number in grades)
            {
                result.Add(number);
            }
                
            return result;
        }

        public override void EnterOpinion()
        {
            while(true)
                {
                    Console.Clear();
                    Console.WriteLine("Zeby wyjsc wcisnij: q");
                    Console.WriteLine();
                    Console.WriteLine($"Imie: {Name}");
                    Console.Write("Oceny: ");
                    foreach(var grade in grades)
                    {
                        Console.Write($" {grade} ");
                    }
                    Console.WriteLine();
                    Console.WriteLine("Wpisz ocene: ");
                    var input = Console.ReadLine();

                    if(input == "q")
                    {
                        break;
                    }
                    if(!string.IsNullOrEmpty(input))
                    {
                        char[] arr = input.ToCharArray();
                        var result = arr.Count();
                    
                        if(result <= 2 && Char.IsDigit(arr[0]))
                        {
                            var sgrade = arr[0].ToString();
                            var grade = double.Parse(sgrade);
                            if(grade > 0 && grade <= 6)
                            {
                                switch(result)
                                {
                                    case 1:
                                    {
                                            this.AddOpinion(grade);
                                    }
                                    break;

                                    case 2:
                                    {
                                        if(arr[1] == '+' || arr[1] == '-')
                                        {
                                            this.AddOpinion(grade, arr[1]);
                                        }
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Nieprawidlowy format");

                                            Console.WriteLine();
                                            Console.WriteLine("Nacisnij dowolny klawisz zeby kontynuowac...");
                                            Console.ReadLine();
                                        }
                                    }
                                    break;

                                    default:
                                    break;
                                }
                            }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Ocena jest poza zakresem 0-6");

                                    Console.WriteLine();
                                    Console.WriteLine("Nacisnij dowolny klawisz zeby kontynuowac...");
                                    Console.ReadLine();
                                }
                        }
                        else
                        throw new ArgumentException("Ta ocena jest nieprawidlowa");
                    }
                }
                
        }

        public override void ShowStatistics()
        {
           var stat = GetStatistics();

            Console.Clear();
            Console.WriteLine($"Uczen: {this.Name}");
            Console.Write($"Oceny: ");
            foreach(var line in this.grades)
            {
                Console.Write($"{line} ");
            }
            Console.WriteLine($"({stat.Count})");
            Console.WriteLine();
            Console.WriteLine($"Ocena najwyzsza: {stat.Max}");
            Console.WriteLine($"Ocena najnizsza: {stat.Min}");
            Console.WriteLine($"Srednia: {stat.Average:N2}");

            Console.WriteLine();
            Console.WriteLine("Nacisnij dowolny klawisz zeby kontynuowac...");
            Console.ReadLine();
            
        }

    }

}
