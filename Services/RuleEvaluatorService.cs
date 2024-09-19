using RuleEvaluator.Exceptions;
using RuleEvaluator.Models;

namespace RuleEvaluator.Services
{
    public class RuleEvaluatorService
    {
        /**
         * Returns true if the given clause configuration is evaluated as true based on the transaction amount
         * Returns false if otherwise
         */
        public bool Evaluate(IClause? clauseConfiguration, Transaction transaction)
        {
            if (clauseConfiguration is ClauseGroup group)
            {
                var results = new List<bool>();

                foreach (var clause in group.Clauses)
                {
                    var result = Evaluate(clause, transaction);
                    results.Add(result);
                }

                if (group.Operator == LogicalOperatorType.AND && !results.Contains(false) ||
                    group.Operator == LogicalOperatorType.OR && results.Contains(true))
                {
                    return true;
                }
            }
            else if (clauseConfiguration is ClauseLine line)
            {
                return EvaluateLine(line, transaction);
            }

            return false;
        }

        public bool EvaluateLine(ClauseLine line, Transaction transation)
        {
            int LValue = ExtractOperandValue(line.LOperand, transation);
            int RValue = ExtractOperandValue(line.ROperand, transation);

            return CompareInt(LValue, line.Operator, RValue);
        }

        public int ExtractOperandValue(ClauseOperand operand, Transaction transaction)
        {
            if (String.IsNullOrEmpty(operand.Entity))
            {
                if (operand.Value == null)
                {
                    throw new InvalidOperandException();
                }

                return (int)operand.Value;
            }

            return ExtractValue(operand.Entity, transaction);
        }

        public int ExtractValue(string key, Transaction transaction)
        {
            int value;

            if (!transaction.TryGetValue(key, out value))
            {
                throw new Exceptions.MissingFieldException($"Cannot find the key '{key}' in transaction");
            }

            return value;
        }

        public bool CompareInt(int LValue, string op, int RValue)
        {
            // Operators: =, !=, >, <, >=, <=
            return op switch
            {
                "=" => (LValue == RValue),
                "!=" => (LValue != RValue),
                ">" => (LValue > RValue),
                "<" => (LValue < RValue),
                ">=" => (LValue >= RValue),
                "<=" => (LValue <= RValue),
                _ => throw new InvalidOperatorException($"Operator '{op}' is not supported"),
            };
        }

        public ClauseGroup CreateRuleConfig()
        {
            // rule: ((Amount1 >= Amount2 AND Amount3 = Amount4) OR (Amount2 < 1000))
            return new ClauseGroup
            {
                Operator = LogicalOperatorType.OR,
                Clauses = new List<IClause>
                {
                    new ClauseGroup
                    {
                        Operator = LogicalOperatorType.AND,
                        Clauses = new List<IClause>
                        {
                            new ClauseLine
                            {
                                LOperand = new ClauseOperand { Entity = "Amount1" },
                                Operator = ">=",
                                ROperand = new ClauseOperand { Entity = "Amount2" }
                            },
                            new ClauseLine
                            {
                                LOperand = new ClauseOperand { Entity = "Amount3" },
                                Operator = "=",
                                ROperand = new ClauseOperand { Entity = "Amount4" }
                            }
                        }
                    },
                    new ClauseLine
                    {
                        LOperand = new ClauseOperand { Entity = "Amount2" },
                        Operator = "<",
                        ROperand = new ClauseOperand { Value = 1000 }
                    }
                }
            };
        }
    }
}
