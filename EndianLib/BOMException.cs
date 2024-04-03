namespace System
{
    public class BOMException : Exception
    {
        public BOMException() : base("This file has invalid endianess.") {}
    }
}
