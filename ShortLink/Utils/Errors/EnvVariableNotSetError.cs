namespace ShortLink.Utils.Errors
{
    public class EnvVariableNotSetError : Exception
    {
        public EnvVariableNotSetError(string variable) : base($"Parameter '{variable}' is not set")
        {

        }
    }
}