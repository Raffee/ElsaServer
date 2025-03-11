using Elsa.Mediator.Contracts;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Genesis.Modules.BorrowersModule.Models;
using Genesis.Modules.BorrowersModule.ServiceInterfaces;
using Workflow.Common.Activities;

namespace ElsaServer.Workflows
{
    public class CreateLoan : WorkflowBase
    {
        private readonly Genesis.Common.Core.Mediator _mediator;
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
                    new Sequence
                    {
                        Activities =
                        {
                            new CustomActivity<CreateBorrowerRequestDto, BorrowerProjection>(_mediator)
                            {
                                Data = new CreateBorrowerRequestDto
                                {
                                    FirstName = "John",
                                    LastName = "Doe",
                                },
                                ActionName = ServiceActions.Borrowers.CreateBorrower,
                            }
                        }
                    }
                }
            };
        }
    }
}