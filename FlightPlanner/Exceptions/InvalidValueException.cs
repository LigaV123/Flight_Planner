namespace FlightPlanner.Exceptions
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException() : base("Value should not be null or empty") { }
    }
}
