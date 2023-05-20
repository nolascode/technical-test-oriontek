namespace Entities.Exceptions
{
    public sealed class UnprocessableEntity : Exception
    {
        public UnprocessableEntity(string message) : base(message) { }
    }
}
