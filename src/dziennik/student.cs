using System;
using System.IO;
using System.Linq;

namespace dziennik
{
    public delegate void GradeAddedDelegate (object sender, EventArgs args);

    public class SavedStudent : StudentBase
    {
        
        public SavedStudent(string name) : base(name)
        {    
        }

        private const string SLOG = "audit.txt";
        public override event GradeAddedDelegate GradeAdded;

        public override void AddOpinion(double oceny)
        {
                using(var writer = File.AppendText($"{Name}.txt"))
                {
                    writer.WriteLine(oceny);
                    if(GradeAdded != null && oceny < 3)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                 }
                var value = oceny.ToString();
                SaveLog(Name, value);
        }

        public override void AddOpinion(double oceny, char control)
        {
                if(control == '-')
                    oceny -= 0.25;
                if(control == '+')
                    oceny += 0.25;

                using(var writer = File.AppendText($"{Name}.txt"))
                {
                    writer.WriteLine(oceny);
                    if(GradeAdded != null && oceny < 3)
                    {
                        GradeAdded(this, new EventArgs());
                    }
                 }
                var value = oceny.ToString();
                SaveLog(Name, value);
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
                    if(File.Exists(Path) == true)
                    {
                        using(var reader = File.OpenText(Path))
                        {
                            var line = reader.ReadLine();
                            while(line != null)
                            {
                                var number = double.Parse(line);
                                Console.Write($"{number} ");
                                line = reader.ReadLine();
                            }
                        }
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
        
            public void SaveLog(string nazwa, string tresc)
            {
            var time = DateTime.UtcNow;

                using(var writer = File.AppendText($"{SLOG}"))
            {
                writer.WriteLine($"{time} :Log: {nazwa} - {tresc}");
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
                if(result) {Console.WriteLine("W podanym imieniu wystepuje cyfra. Imienia nie dodano.");}
                else 
                {
                    this.Name = name;
                    this.Path = Name + ".txt";
                    Console.WriteLine($"Imie zmieniono na: {this.Name}"); 
                    SaveLog(Name, "Zmieniono imie ucznia.");
                }
            }

            public override Statistics GetStatistics()
            {
                var result = new Statistics();

                
                if(File.Exists(Path) == true)
                {
                    using(var reader = File.OpenText(Path))
                    {
                        var line = reader.ReadLine();
                        while(line != null)
                        {
                            var number = double.Parse(line);
                            result.Add(number);
                            line = reader.ReadLine();
                        }
                    }
                }
                else 
                    throw new Exception ("Nie istnieje plik z ocenami tego ucznia.");
            
                return result;
            }

        public override void ShowStatistics()
        {
           var stat = GetStatistics();

            Console.Clear();
            Console.WriteLine($"Uczen: {this.Name}");
            Console.Write($"Oceny: ");
            if(File.Exists(Path) == true)
            {
                using(var reader = File.OpenText(Path))
                {
                    var line = reader.ReadLine();
                    while(line != null)
                    {
                        var number = double.Parse(line);
                        Console.Write($"{number} ");
                        line = reader.ReadLine();
                    }
                }
            }
            Console.WriteLine();
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


