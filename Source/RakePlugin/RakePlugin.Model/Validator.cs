namespace RakePlugin.Model
{
    public static class Validator
    {
        public static bool Validate(Parameter parameter)
        {
            if (parameter.Value > parameter.MaxValue || parameter.Value < parameter.MinValue)
            {
                return false;
            }
            return true;
        }
    }
}
