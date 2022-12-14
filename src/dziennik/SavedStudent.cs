using System;
using System.IO;
using System.Linq;

namespace dziennik
{
    public class SavedStudent : StudentBase
    {
        private string Path;
        private double minusGrade = 0.25;
        private double plusGrade = 0.50;

        public SavedStudent(string name) : base(name)
        {
            Path = Name + ".txt";
        }

        private const string FILE_LOG = "audit.txt";

        public override void AddOpinion(double grade)
        {
            using (var writer = File.AppendText(Path))
            {
                writer.WriteLine(grade);
                if (grade < 3) ActiveGradeAlert();
            }
            SaveLog(Name, grade.ToString());
        }

        public override void AddOpinion(double grade, char AfterGrade)
        {
            if (AfterGrade == '-') grade -= minusGrade;
            if (AfterGrade == '+') grade += plusGrade;
            using (var writer = File.AppendText(Path))
            {
                writer.WriteLine(grade);
                if (grade < 3) ActiveGradeAlert();
            }
            SaveLog(Name, grade.ToString());
        }

        public override void EnterOpinion()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Zeby wyjsc wcisnij: q");
                Console.WriteLine();
                Console.WriteLine($"Imie: {Name}");
                Console.Write("Oceny: ");
                if (File.Exists(Path))
                {
                    using (var reader = File.OpenText(Path))
                    {
                        var line = reader.ReadLine();
                        while (line != null)
                        {
                            var grade = double.Parse(line);
                            Console.Write($"{grade} ");
                            line = reader.ReadLine();
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine("Wpisz ocene: ");
                var userInput = Console.ReadLine();

                if (userInput == "q") break;
                if (!string.IsNullOrEmpty(userInput))
                {
                    char[] arrayInput = userInput.ToCharArray();
                    var charsCount = arrayInput.Count();
                    if (charsCount <= 2 && Char.IsDigit(arrayInput[0]))
                    {
                        var stringGrade = arrayInput[0].ToString();
                        var grade = double.Parse(stringGrade);
                        if (grade > 0 && grade <= 6)
                        {
                            switch (charsCount)
                            {
                                case 1:
                                    this.AddOpinion(grade);
                                    break;

                                case 2:
                                    {
                                        if (arrayInput[1] == '+' || arrayInput[1] == '-') this.AddOpinion(grade, arrayInput[1]);
                                        else
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Nieprawidlowy format");
                                            WaitForKey();
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
                            WaitForKey();
                        }
                    }
                    else throw new ArgumentException("Ta ocena jest nieprawidlowa");
                }
            }
        }

        public void SaveLog(string nazwa, string tresc)
        {
            var time = DateTime.UtcNow;
            using (var writer = File.AppendText($"{FILE_LOG}"))
            {
                writer.WriteLine($"{time} :Log: {nazwa} - {tresc}");
            }
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            if (File.Exists(Path))
            {
                using (var reader = File.OpenText(Path))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var grade = double.Parse(line);
                        result.Add(grade);
                        line = reader.ReadLine();
                    }
                }
            }
            else throw new Exception("Nie istnieje plik z ocenami tego ucznia.");
            return result;
        }

        public override void ShowStatistics()
        {
            var statistics = GetStatistics();
            Console.Clear();
            Console.WriteLine($"Uczen: {this.Name}");
            Console.Write($"Oceny: ");
            if (File.Exists(Path))
            {
                using (var reader = File.OpenText(Path))
                {
                    var line = reader.ReadLine();
                    while (line != null)
                    {
                        var grade = double.Parse(line);
                        Console.Write($"{grade} ");
                        line = reader.ReadLine();
                    }
                }
            }
            Console.WriteLine();
            Console.WriteLine($"({statistics.Count})");
            Console.WriteLine();
            Console.WriteLine($"Ocena najwyzsza: {statistics.Max}");
            Console.WriteLine($"Ocena najnizsza: {statistics.Min}");
            Console.WriteLine($"Srednia: {statistics.Average:N2}");
            WaitForKey();

        }
    }
}


