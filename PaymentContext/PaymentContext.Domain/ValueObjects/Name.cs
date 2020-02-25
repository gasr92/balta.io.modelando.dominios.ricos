using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.First", "Nome precisa de pelo menos 3 caracteres")
                .HasMaxLen(LastName, 40, "Name.First", "Nome precisa ter até 40 caracteres")
            );

            // if(string.IsNullOrEmpty(FirstName))
            //     AddNotification("FirstName", "Nome inválido");
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}