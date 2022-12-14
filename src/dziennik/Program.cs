using System;

namespace dziennik
{
    class Program
    {
        static void Main(string[] args)
        {
            var student = new StudentInMemory("Damian");
            //var student = new SavedStudent("Damian");
            student.GradeAlert += GradeInformation;

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Czesc! {student.Name}");
                Console.WriteLine();
                Console.WriteLine("Wyjscie: 'q' ");
                Console.WriteLine("Statystyki:  's' ");
                Console.WriteLine("Dodaj Ocene 'e' ");
                Console.WriteLine("Zmien imie ucznia: 'x' ");
                Console.WriteLine();
                Console.WriteLine("Wybierz opcje: ");
                var userInput = Console.ReadLine();

                if (userInput == "q") break;
                switch (userInput)
                {
                    case "s":
                        student.ShowStatistics();
                        break;

                    case "e":
                        student.EnterOpinion();
                        break;

                    case "x":
                        {
                            var newName = student.ChangeStudent();
                            if(newName != null)
                            {
                                student = new StudentInMemory(newName);
                                //student = new SavedStudent(newName);
                                student.GradeAlert += GradeInformation;
                                Console.WriteLine($"Imie zmieniono na: {student.Name}");
                            }
                        }
                        break;
                }
            }
        }

        static void GradeInformation(object sender, EventArgs args)
        {
            Console.Clear();
            Console.WriteLine($"Ooops, musimy poinformowac rodzicow o tej ocenie!");
            Console.WriteLine();
            Console.WriteLine("Nacisnij dowolny klawisz, aby kontynuowac...");
            Console.ReadKey();
        }
    }
}