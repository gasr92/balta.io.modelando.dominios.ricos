using PaymentContext.Domain.Entities;

namespace PaymentContext.Domain.Repositories
{
    public interface IStudentRepository
    {
        bool DocumentExists(string doc);
        bool EmailExists(string email);
        void CreateSubscription(Student student);
    }
}