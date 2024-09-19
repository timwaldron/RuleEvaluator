using RuleEvaluator.Exceptions;
using RuleEvaluator.Models;
using RuleEvaluator.Services;
using Xunit;

namespace RuleEvaluator.Tests
{
    public class RuleEvaluatorTest
    {
        private RuleEvaluatorService _res = new RuleEvaluatorService();

        private Transaction transaction = new Transaction()
            {
                { "Field1", 100 },
                { "Field2", 200 },
                { "Field3", 300 },
                { "Field4", 400 },
            };

        [Fact(DisplayName = "Should evaluate and return the correct result (1)")]
        public void TestEvaluate_1()
        {
            var config = new ClauseGroup
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
                                LOperand = new ClauseOperand { Entity = "Field1" },
                                Operator = ">=",
                                ROperand = new ClauseOperand { Entity = "Field2" }
                            },
                            new ClauseLine
                            {
                                LOperand = new ClauseOperand { Entity = "Field3" },
                                Operator = "=",
                                ROperand = new ClauseOperand { Entity = "Field4" }
                            }
                        }
                    },
                    new ClauseLine
                    {
                        LOperand = new ClauseOperand { Entity = "Field2" },
                        Operator = "<",
                        ROperand = new ClauseOperand { Value = 1000 }
                    }
                }
            };

            var result = _res.Evaluate(config, transaction);
            Assert.True(result);
        }

        [Fact(DisplayName = "Should evaluate and return the correct result (2)")]
        public void TestEvaluate_2()
        {
            var config = new ClauseGroup
            {
                Operator = LogicalOperatorType.AND,
                Clauses = new List<IClause>
                {
                    new ClauseLine
                    {
                        LOperand = new ClauseOperand { Entity = "Field1" },
                        Operator = "<",
                        ROperand = new ClauseOperand { Value = 1000 }
                    },
                    new ClauseGroup
                    {
                        Operator = LogicalOperatorType.OR,
                        Clauses = new List<IClause>
                        {
                            new ClauseLine
                            {
                                LOperand = new ClauseOperand { Entity = "Field1" },
                                Operator = ">=",
                                ROperand = new ClauseOperand { Entity = "Field2" }
                            },
                            new ClauseLine
                            {
                                LOperand = new ClauseOperand { Entity = "Field3" },
                                Operator = "=",
                                ROperand = new ClauseOperand { Value = 300 }
                            }
                        }
                    },
                    new ClauseLine
                    {
                        LOperand = new ClauseOperand { Entity = "Field4" },
                        Operator = ">",
                        ROperand = new ClauseOperand { Entity = "Field1" }
                    },
                }
            };

            var result = _res.Evaluate(config, transaction);
            Assert.True(result);
        }

        [Fact(DisplayName = "Should evaluate and return the correct result (3)")]
        public void TestEvaluate_3()
        {
            var config = new ClauseGroup
            {
                Operator = LogicalOperatorType.AND,
                Clauses = new List<IClause>
                {
                    new ClauseLine
                    {
                        LOperand = new ClauseOperand { Entity = "Field1" },
                        Operator = "<",
                        ROperand = new ClauseOperand { Value = 1000 }
                    },
                    new ClauseGroup
                    {
                        Operator = LogicalOperatorType.OR,
                        Clauses = new List<IClause>
                        {
                            new ClauseLine
                            {
                                LOperand = new ClauseOperand { Entity = "Field1" },
                                Operator = ">=",
                                ROperand = new ClauseOperand { Entity = "Field2" }
                            },
                            new ClauseLine
                            {
                                LOperand = new ClauseOperand { Entity = "Field3" },
                                Operator = "=",
                                ROperand = new ClauseOperand { Value = 300 }
                            }
                        }
                    },
                    new ClauseLine
                    {
                        LOperand = new ClauseOperand { Entity = "Field4" },
                        Operator = ">",
                        ROperand = new ClauseOperand { Entity = "Field1" }
                    },
                }
            };

            var result = _res.Evaluate(config, transaction);
            Assert.True(result);
        }

        [Fact(DisplayName = "Should evaluate and return the correct result (4)")]
        public void TestEvaluate_4()
        {
            var config = new ClauseGroup
            {
                Operator = LogicalOperatorType.AND,
                Clauses = new List<IClause>
                {
                    new ClauseLine
                    {
                        LOperand = new ClauseOperand { Entity = "Field1" },
                        Operator = "<",
                        ROperand = new ClauseOperand { Value = 1000 }
                    },
                    new ClauseLine
                    {
                        LOperand = new ClauseOperand { Entity = "Field2" },
                        Operator = "=",
                        ROperand = new ClauseOperand { Entity = "Field3" }
                    },
                    new ClauseLine
                    {
                        LOperand = new ClauseOperand { Entity = "Field4" },
                        Operator = ">",
                        ROperand = new ClauseOperand { Entity = "Field1" }
                    },
                }
            };

            var result = _res.Evaluate(config, transaction);
            Assert.False(result);
        }

        [Fact(DisplayName = "Should return the correct bool after evaluating a ClauseLine")]
        public void TestEvaluateLine()
        {
            var lines = new List<ClauseLine>
            {
                // Operators: =, !=, >, <, >=, <=
                new ClauseLine
                {
                    LOperand = new ClauseOperand { Entity = "Field1" },
                    Operator = "=",
                    ROperand = new ClauseOperand { Value = 100 },
                },
                new ClauseLine
                {
                    LOperand = new ClauseOperand { Entity = "Field2" },
                    Operator = "!=",
                    ROperand = new ClauseOperand { Value = 100 },
                },
                new ClauseLine
                {
                    LOperand = new ClauseOperand { Entity = "Field3" },
                    Operator = ">",
                    ROperand = new ClauseOperand { Entity = "Field2" },
                },
                new ClauseLine
                {
                    LOperand = new ClauseOperand { Entity = "Field1" },
                    Operator = "<",
                    ROperand = new ClauseOperand { Entity = "Field2" },
                },
                new ClauseLine
                {
                    LOperand = new ClauseOperand { Entity = "Field2" },
                    Operator = ">=",
                    ROperand = new ClauseOperand { Value = 200 },
                },
                new ClauseLine
                {
                    LOperand = new ClauseOperand { Entity = "Field3" },
                    Operator = "<=",
                    ROperand = new ClauseOperand { Value = 300 },
                },
            };

            foreach (var line in lines)
            {
                Assert.True(_res.EvaluateLine(line, transaction));
            }
        }

        [Fact(DisplayName = "Should get the correct value to use from the clause operand")]
        public void TestExtractOperandValue()
        {
            var entityOperand = new ClauseOperand { Entity = "Field2" };
            Assert.Equal(200, _res.ExtractOperandValue(entityOperand, transaction));

            var valueOperand = new ClauseOperand { Value = 4500 };
            Assert.Equal(4500, _res.ExtractOperandValue(valueOperand, transaction));

            Assert.Throws<InvalidOperandException>(() => _res.ExtractOperandValue(new ClauseOperand(), transaction));
        }

        [Fact(DisplayName = "Should extract the values out of the object")]
        public void TestExtractValue()
        {
            Assert.Equal(100, _res.ExtractValue("Field1", transaction));
            Assert.Equal(200, _res.ExtractValue("Field2", transaction));
            Assert.Equal(300, _res.ExtractValue("Field3", transaction));

            Assert.Throws<Exceptions.MissingFieldException>(() => _res.ExtractValue("Non_Existant_Field", transaction));
        }

        [Fact(DisplayName = "Should compare values correctly")]
        public void TestCompareInt()
        {
            // Operators: =, !=, >, <, >=, <=
            var ValueA = 100;
            var ValueB = 101;

            // =
            Assert.True(_res.CompareInt(ValueA, "=", ValueA));
            Assert.False(_res.CompareInt(ValueA, "=", ValueB));

            // !=
            Assert.True(_res.CompareInt(ValueA, "!=", ValueB));
            Assert.False(_res.CompareInt(ValueA, "!=", ValueA));

            // >
            Assert.True(_res.CompareInt(ValueB, ">", ValueA));
            Assert.False(_res.CompareInt(ValueA, ">", ValueB));

            // <
            Assert.True(_res.CompareInt(ValueA, "<", ValueB));
            Assert.False(_res.CompareInt(ValueB, "<", ValueA));

            // >=
            Assert.True(_res.CompareInt(ValueB, ">=", ValueA));
            Assert.True(_res.CompareInt(ValueB, ">=", ValueB));
            Assert.False(_res.CompareInt(ValueA, ">=", ValueB));

            // <=
            Assert.True(_res.CompareInt(ValueA, "<=", ValueB));
            Assert.True(_res.CompareInt(ValueA, "<=", ValueA));
            Assert.False(_res.CompareInt(ValueB, "<=", ValueA));

            // Exception
            Assert.Throws<InvalidOperatorException>(() => _res.CompareInt(ValueA, "@", ValueB));
        }
    }
}
