using ShortLink.Utils.Errors;

namespace ShortLink.Utils
{
    public static class EnvService
    {
        public static string GetVariable(string variable)
        {
            return Environment.GetEnvironmentVariable(variable) ?? throw new EnvVariableNotSetError(variable);
        }
    }
}