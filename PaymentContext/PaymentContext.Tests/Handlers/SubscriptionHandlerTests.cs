using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;

namespace PaymentContext.Tests
{
    [TestClass]
    public class SubscriptionHandlerTeste
    {

        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.Barcode = "123456789";
            command.FirstName = "123456789";
            command.LastName = "123456789";
            command.Document = "123456789";
            command.Email = "123456789";
            command.BoletoNumber = "123456789";
            command.PaymentNumber = "123456789";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Street = "123456789";
            command.Number = "123456789";
            command.Neighborhood = "123456789";
            command.City = "123456789";
            command.State = "123456789";
            command.Country = "123456789";
            command.ZipCode = "123456789";

            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}