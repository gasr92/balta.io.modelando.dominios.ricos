using System;
using System.Linq.Expressions;
using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Queries
{
    public static class StudentQueries
    {
        public static Expression<Func<Student, bool>> GetStudentInfo(string documento)
        {
            return x => x.Document.Number == documento;
        }
    }
}