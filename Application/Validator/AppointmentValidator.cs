using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using TimeEntryApproval.API.Domain;

namespace TimeEntryApproval.API.Application.Validator
{
    public class AppointmentValidator
    {
        public static async Task<bool?> EvaluateRulesAsync(TimeEntryValidation timeEntry)
        {
            try
            {
                var jsonString = await File.ReadAllTextAsync("C:\\dev\\TimeEntryApproval.API\\rules.json");
                var jsonObject = JObject.Parse(jsonString);
                var rules = jsonObject["appointmentRules"].ToObject<List<string>>();

                foreach (var rule in rules)
                {
                    var result = await CSharpScript.EvaluateAsync<bool>(
                    rule,
                    ScriptOptions.Default.AddReferences(typeof(TimeEntry).Assembly)
                                        .AddImports("System")
                                        .AddImports("System.Linq"),
                    globals: timeEntry);

                    if (!result)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
