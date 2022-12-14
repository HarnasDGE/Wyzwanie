using System;
using Xunit;
using dziennik;

namespace dziennik.tests
{
    public class StudentTests
    {
        [Fact]
        public void Test1()
        {
            // arrange

            var emp = new SavedStudent("Damian");
            emp.AddOpinion(4.5);
            emp.AddOpinion(6);
            emp.AddOpinion(2);
            // act

            var result = emp.GetStatistics();
            // assert

            Assert.Equal(4.17, result.Average, 2);
            Assert.Equal(6, result.Max);
            Assert.Equal(2, result.Min);
        }
    }
}
