using System;

namespace dziennik
{
    class Program
    {
        static void Main(string[] args)
        {
            //var student = new StudentInMemory("Damian");
            var student = new SavedStudent("Damian");

            student.GradeAdded += GradeForParents;

                
            while(true)
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
                var grade = Console.ReadLine();

                if( grade == "q")
                    {
                        break;
                    }
                switch(grade)
                {

                    case "s":
                    student.ShowStatistics();
                    break;

                    case "e": 
                    student.EnterOpinion();
                    break;

                    case "x":
                    student.ChangeStudent();
                    break;
                }
            }
         }

         static void GradeForParents (object sender, EventArgs args)
         {
            Console.WriteLine($"Ooops, musimy poinformowac rodzicow o tej ocenie!");
         }
    }
}