using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentQueriesTest
    {
        private IList<Student> _students;

        public StudentQueriesTest()
        {
            for(var i = 0; i < 10; i++)
            {
                _students.Add(new Student(new Name("Nome" + i.ToString(), "Sobrenome")
                                        , new Document("123456789", EDocumentType.CPF)
                                        , new Email(i.ToString() + "@teste.com"))
                );
            }
        }

        [TestMethod]
        public void ShouldReturnNullWhenDocumentNotExists()
        {
            var exp = StudentQueries.GetStudentInfo("12345678911");
            var estdn = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreNotEqual(null, estdn);
        }
    }
}
