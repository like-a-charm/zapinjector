using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ZapInjector.ExplicitServicesDeclarationSettings
{
    public static class ValueSettingsExtensions
    {
        private const string EnvRegexString = "^env\\((.+)\\)$";
        private static readonly Regex EnvRegex = new Regex(EnvRegexString);
        
        public static object GetValueObject(this ValueSettings valueSettings)
        {
            Type type = Type.GetType(valueSettings.Type)!;
            string valueString = valueSettings.Value;
            if (EnvRegex.IsMatch(valueString))
            {
                valueString = Environment.GetEnvironmentVariable(EnvRegex.Match(valueString).Groups[1].Value)!;
            }
            return Convert.ChangeType(valueString, type);
        }
    }
}