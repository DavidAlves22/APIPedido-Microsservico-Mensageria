using System;

namespace PedidoMicrosservice.Pedido.Domain.Exceptions
{
    public class PedidoNaoEncontradoException : DomainException
    {
        public PedidoNaoEncontradoException(string message) : base(message) { }

        public PedidoNaoEncontradoException(string message, Exception innerException) : base(message, innerException) { }
    }
}