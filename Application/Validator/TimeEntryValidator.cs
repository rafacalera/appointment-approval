using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Newtonsoft.Json.Linq;
using TimeEntryApproval.API.Domain;

namespace TimeEntryApproval.API.Application.Validator
{
    public class TimeEntryValidator
    {
        private readonly JObject _json;
        private readonly List<string> _rules;

        public TimeEntryValidator()
        {
            _json = JObject.Parse(File.ReadAllText("C:\\dev\\TimeEntryApproval.API\\rules.json"));
            _rules = _json["appointmentRules"].ToObject<List<string>>();
        }

        public async Task<bool?> EvaluateRulesAsync(TimeEntryValidation timeEntry)
        {
            try
            {
                foreach (var rule in _rules)
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
