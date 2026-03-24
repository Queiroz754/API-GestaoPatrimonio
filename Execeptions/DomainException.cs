namespace GerenciamentoPatrimonio.Execeptions
{
    public class DomainException : Exception
    {
        public DomainException(string mensagem) : base(mensagem) { } 
    }
}
