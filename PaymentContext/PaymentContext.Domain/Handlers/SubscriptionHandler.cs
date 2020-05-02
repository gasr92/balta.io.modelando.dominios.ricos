using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable
                                        , IHandler<CreateBoletoSubscriptionCommand>
                                        //, IHandler<CreatePaypalSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            this._repository = repository;
            this._emailService = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // fail fast validation
            command.Validate();
            if(command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar sua assinatura");
            }

            // verificar se documento ja esta cadastrado
            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já existe");
            }
            
            // verificar se email ja esta cadastrado
            if(_repository.EmailExists(command.Email))
            {
                AddNotification("Email", "Este E-mail já existe");
            }

            // geradad os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            // gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscriptions(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.Barcode
                                            , command.BoletoNumber
                                            , command.PaidDate
                                            , command.ExpireDate
                                            , command.Total
                                            , command.TotalPaid
                                            , document
                                            , command.PayerEmail
                                            , address
                                            , email
                                            );

            // relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // agrupar as validacoes
            AddNotifications(name, document, email, address, student, subscription, payment);

            // checar as notificacoes
            if(Invalid)
            {
                return new CommandResult(false, "Não foi possível realizar a assinatura");
            }

            // salvar informacoes
            _repository.CreateSubscription(student);

            // enviar e-mail de boas vindas
            _emailService.Send(student.ToString(), student.Email.Address, "Bem vindo", "Assinatura criata");

            // retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePaypalSubscriptionCommand command)
        {
            // verificar se documento ja esta cadastrado
            if(_repository.DocumentExists(command.Document))
            {
                AddNotification("Document", "Este CPF já existe");
            }
            
            // verificar se email ja esta cadastrado
            if(_repository.EmailExists(command.Email))
            {
                AddNotification("Email", "Este E-mail já existe");
            }

            // geradad os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            
            // gerar as entidades
            var student = new Student(name, document, email);
            var subscription = new Subscriptions(DateTime.Now.AddMonths(1));
            
            // só caso do pagamento paypal so muda a implementacao do pagamento
            var payment = new PaypalPayment(command.TransactionCode
                                            , command.PaidDate
                                            , command.ExpireDate
                                            , command.Total
                                            , command.TotalPaid
                                            , document
                                            , command.PayerEmail
                                            , address
                                            , email
                                            );

            // relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // agrupar as validacoes
            AddNotifications(name, document, email, address, student, subscription, payment);

            // salvar informacoes
            _repository.CreateSubscription(student);

            // enviar e-mail de boas vindas
            _emailService.Send(student.ToString(), student.Email.Address, "Bem vindo", "Assinatura criata");

            // retornar informações
            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}