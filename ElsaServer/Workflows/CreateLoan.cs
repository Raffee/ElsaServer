using Elsa.Workflows;
using Elsa.Workflows.Activities;

namespace ElsaServer.Workflows
{
    public class CreateLoan : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            var loanAmountVariable = builder.WithVariable<decimal>();
            var loanTermVariable = builder.WithVariable<int>();
            var loanInterestRateVariable = builder.WithVariable<decimal>();

            builder.Root = new Sequence
            {
                Activities =
                {
                    new SetVariable
                    {
                        Variable = loanAmountVariable,
                        Value = new(1000)
                    },
                    new SetVariable
                    {
                        Variable = loanTermVariable,
                        Value = new(12)
                    },
                    new SetVariable
                    {
                        Variable = loanInterestRateVariable,
                        Value = new(0.1)
                    },
                    new Elsa.Workflows.Activities.Sequence
                    {
                        Activities =
                        {
                            // execute step 1: create loan
                            // this should be a custom activity that calls the appropriate Adapter Plugin
                            // to create a loan in the external system
                        }
                    }
                }
            };
        }
    }
}