namespace dziennik
{
    public abstract class StudentBase : NamedObject, IStudent
    {
        public StudentBase(string name) : base(name)
        {
        }

        public abstract event GradeAddedDelegate GradeAdded;

        public abstract void AddOpinion(double oceny);
        public abstract void AddOpinion(double oceny, char control);

        public abstract void ShowStatistics();

        public abstract Statistics GetStatistics();

        public abstract void EnterOpinion();

        public abstract void ChangeStudent();

    }
}


