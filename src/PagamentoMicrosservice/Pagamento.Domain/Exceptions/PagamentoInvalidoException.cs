using System;

namespace PagamentoMicrosservice.Pagamento.Domain.Exceptions
{
    public class PagamentoInvalidoException : DomainException
    {
        public PagamentoInvalidoException(string message) : base(message) { }

        public PagamentoInvalidoException(string message, Exception innerException) : base(message, innerException) { }
    }
}