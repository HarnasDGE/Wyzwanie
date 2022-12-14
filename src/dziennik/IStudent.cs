using System;

namespace dziennik
{
    public interface IStudent 
    {
        void AddOpinion(double oceny);
        void AddOpinion(double oceny, char control);
        Statistics GetStatistics();
        string Name { get; set;}
        static DateTime UtcNow { get; }
        event GradeAlertDelegate GradeAlert;
    }
}


