using System;

namespace dziennik
{
    public interface IStudent 
    {
        void AddOpinion(double grade);
        void AddOpinion(double grade, char AfterGrade);
        Statistics GetStatistics();
        string Name { get; set;}
        static DateTime UtcNow { get; }
        event GradeAlertDelegate GradeAlert;
    }
}


