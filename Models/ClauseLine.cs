namespace RuleEvaluator.Models
{
    public class ClauseLine : IClause
    {
        public required ClauseOperand LOperand { get; set; }
        public required string Operator { get; set; }
        public required ClauseOperand ROperand { get; set; }
    }
}
