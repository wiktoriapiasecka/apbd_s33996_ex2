namespace apbd_s33996_lab3.Exceptions;

public class RentalLimitExceededException : Exception
{
    public RentalLimitExceededException(string message) : base(message)
    {
    }
}