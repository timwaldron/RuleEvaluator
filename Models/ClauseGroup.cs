namespace RuleEvaluator.Models
{
    public class ClauseGroup : IClause
    {
        public required LogicalOperatorType Operator { get; set; }
        public required IList<IClause> Clauses { get; set; }
    }
}
