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

        public static bool ValidateTwoParameter(Parameter firstParameter, Parameter secondParameter)
        {
            if ((secondParameter.Value - firstParameter.Value) >= 10 && (secondParameter.Value - firstParameter.Value) <= 50)
            {
                return true;
            }
            return false;
        }
    }
}
