using System.Collections.Generic;
using System.Linq;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscriptions> _subscriptions;
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscriptions>();

            AddNotifications(name, document, email);
        }

        // public string FirstName { get; private set; }
        // public string LastName { get; private set; }

        public Name Name{get;private set;}
        public  Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; set; }
        public IReadOnlyCollection<Subscriptions> Subscriptions { get => _subscriptions.ToArray(); }

        public void AddSubscription(Subscriptions subscription)
        {
            var hasSubscriptionActive = false;

            foreach(var sub in Subscriptions)
                if(sub.Active)
                    hasSubscriptionActive = true;

            _subscriptions.Add(subscription);

            AddNotifications(new Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Você já tem uma assinatura ativa")
                .IsLowerOrEqualsThan(0, subscription.Payments.Count, "Student.SUbscription.Payments", "Esta assinatura não possui pagamentos")
            );

            // outra alternativa
            // if(hasSubscriptionActive)
            //     AddNotification("Student.Subscriptions", "Você já tem uma assinatura ativa");
        }
    }
}