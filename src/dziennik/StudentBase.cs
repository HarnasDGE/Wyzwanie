using System;

namespace dziennik
{
    public delegate void GradeAlertDelegate(object sender, EventArgs args);

    public abstract class StudentBase : NamedObject, IStudent
    {
        public StudentBase(string name) : base(name)
        {
        }

        public event GradeAlertDelegate GradeAlert;

        public abstract void AddOpinion(double oceny);
        public abstract void AddOpinion(double oceny, char control);

        public abstract void ShowStatistics();

        public abstract Statistics GetStatistics();

        public abstract void EnterOpinion();


        protected void ActiveGradeAlert()
        {
            if (GradeAlert != null) GradeAlert(this, new EventArgs());
        }

        protected void WaitForKey()
        {
            Console.WriteLine();
            Console.WriteLine("Nacisnij dowolny klawisz, aby kontynuowac...");
            Console.ReadKey();
        }

        internal string ChangeStudent()
        {
            Console.WriteLine("Podaj nowe imie: ");
            var inputName = Console.ReadLine();
            char[] arrayInput = inputName.ToCharArray();
            var checkName = false;
            foreach (var CharInName in arrayInput)
            {
                if (Char.IsDigit(CharInName)) checkName = true;
            }
            if (checkName)
            {
                Console.WriteLine("W podanym imieniu wystepuje cyfra. Imienia nie zmieniono.");
                WaitForKey();
                return null;
            }
            else return inputName;
        }
    }
}


