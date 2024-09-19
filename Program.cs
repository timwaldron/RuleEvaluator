using Newtonsoft.Json;
using RuleEvaluator.Models;
using RuleEvaluator.Services;

namespace RuleEvaluator
{
    public class Program
    {
        static void Main(string[] args)
        {
            var _res = new RuleEvaluatorService();
            var transaction = new Transaction()
            {
                { "Amount1", 100 },
                { "Amount2", 200 },
                { "Amount3", 300 },
                { "Amount4", 400 }
            };

            var result = false;

            try
            {
                Console.WriteLine("Running rule against transaction:\r\n\r\n\t{0}\r\n", JsonConvert.SerializeObject(transaction));

                var config = _res.CreateRuleConfig();
                result = _res.Evaluate(config, transaction);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
                Console.WriteLine("----------");
                Console.WriteLine("Cannot continue, exiting...");
                Environment.Exit(1);
            }

            Console.WriteLine("Result: {0}", result);
        }
    }
}
