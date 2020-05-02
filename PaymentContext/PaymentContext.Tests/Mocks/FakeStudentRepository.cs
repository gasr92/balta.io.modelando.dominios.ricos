using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
            throw new System.NotImplementedException();
        }

        public bool DocumentExists(string doc)
        {
            return doc == "99999999999";
        }

        public bool EmailExists(string email)
        {
            return email == "hello@balta.io";
        }
    }
}