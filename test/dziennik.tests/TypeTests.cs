using System;
using Xunit;
using dziennik;

namespace dziennik.tests
{
    public class TypeTests
    {
        [Fact]
        public void GetStudentReturnsDifferentObjects()
        {
            // arrange

            var std1 = GetStudent("Damian");
            var std2 = GetStudent("Pawel");

            // act

            // assert

            Assert.NotSame(std2, std1);
            Assert.False(Object.ReferenceEquals(std1, std2));
        }

        [Fact]
        public void TwoVarsCanReferenceSameObjects()
        {
            var std1 = GetStudent("Pawel");
            var std2 = std1;

            Assert.Same(std1, std2);
            Assert.True(Object.ReferenceEquals(std1, std2));

        }

        [Fact]

        public void CanSetNameFromReference()
        {
            var std1 = GetStudent("Pawel");
            this.SetName(std1, "NewName");

            Assert.Equal("NewName", std1.Name);
        }




        private SavedStudent GetStudent(string name)
        {
            return new SavedStudent(name);
        }

        private void SetName(SavedStudent student, string name)
        {
            student.Name = name;
        }

        private StudentInMemory GetStudentInMemory(string name)
        {
            return new StudentInMemory(name);
        }
    }
}
