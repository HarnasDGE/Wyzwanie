using System;
using System.Collections.Generic;
using System.Linq;

namespace dziennik
{
    public class StudentInMemory : StudentBase
    {
        private double minusGrade = 0.25;
        private double plusGrade = 0.50;

        public StudentInMemory(string name) : base(name)
        {

        }
        public List<double> grades = new List<double>();

        public override void AddOpinion(double grade)
        {
            this.grades.Add(grade);
            if (grade < 3) ActiveGradeAlert();
        }

        public override void AddOpinion(double grade, char AfterGrade)
        {
            if (AfterGrade == '-') grade -= minusGrade;
            if (AfterGrade == '+') grade += plusGrade;
            this.grades.Add(grade);
            if (grade < 3) ActiveGradeAlert();
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            foreach (var grade in this.grades)
            {
                result.Add(grade);
            }
            return result;
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
                foreach (var grade in grades)
                {
                    Console.Write($" {grade} ");
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
                        var gradeString = arrayInput[0].ToString();
                        var grade = double.Parse(gradeString);
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

        public override void ShowStatistics()
        {
            var statistics = GetStatistics();
            Console.Clear();
            Console.WriteLine($"Uczen: {this.Name}");
            Console.Write($"Oceny: ");
            foreach (var grade in this.grades)
            {
                Console.Write($"{grade} ");
            }
            Console.WriteLine($"({statistics.Count})");
            Console.WriteLine();
            Console.WriteLine($"Ocena najwyzsza: {statistics.Max}");
            Console.WriteLine($"Ocena najnizsza: {statistics.Min}");
            Console.WriteLine($"Srednia: {statistics.Average:N2}");
            WaitForKey();
        }
    }
}
