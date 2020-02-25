using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class StudentTests
    {
        private readonly Student _student;
        private readonly Subscriptions _subscription;
        private readonly  Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;

        public StudentTests()
        {
            _name = new Name("Bruce", "Wayne");
            _document = new Document("22288844480", EDocumentType.CPF);
            _email = new Email("batman@hotmail.com");
            _student = new Student(_name, _document, _email);
            _address = new Address("Rua 1", "20", "Bairro", "Gotham", "RS", "Brazil", "85687000");
            _subscription = new Subscriptions(null);
            //_subscription.AddPayment(p);
        }

        [TestMethod]
        public void ShouldReturnErroWhenHadActiveSubscription()
        {          
            var pay = new PaypalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10,_document, "Wayne Corp", _address, _email);
            _subscription.AddPayment(pay);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);
            
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErroWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);           
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErroWhenHadNoActiveSubscription()
        {
            var pay = new PaypalPayment("123456", DateTime.Now, DateTime.Now.AddDays(5), 10, 10,_document, "Wayne Corp", _address, _email);
            _subscription.AddPayment(pay);
            _student.AddSubscription(_subscription);
            
            Assert.IsTrue(_student.Invalid);
        }
    }
}
