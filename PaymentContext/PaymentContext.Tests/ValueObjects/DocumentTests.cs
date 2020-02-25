using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Tests
{
    [TestClass]
    public class DocumentTests
    {
        // red, green, refactor
        [TestMethod]
        public void ShouldReturnErrorWhenCnpjIsInvalid()
        {
            var doc = new Document("123", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCnpjIsValid()
        {
            var doc = new Document("82581863000164", EDocumentType.CNPJ);
            Assert.IsTrue(doc.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCpfIsInvalid()
        {
            var doc = new Document("67345787044", EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }

        // exemplo de como realizar mais de um teste
        [TestMethod]
        [DataTestMethod]
        [DataRow("12345678920")]
        [DataRow("5020")]
        [DataRow("85274196300")]
        [DataRow("45632198701")]
        public void ShouldReturnSuccessWhenCpfIsValid(string cpf)
        {
            var doc = new Document(cpf, EDocumentType.CPF);
            Assert.IsTrue(doc.Valid);
        }
    }
}
