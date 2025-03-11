﻿using Elsa.Http;
using Elsa.Workflows;
using Elsa.Workflows.Activities;
using Genesis.Modules.BorrowersModule.Models;
using Genesis.Modules.BorrowersModule.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Workflow.Common.Activities;

namespace ElsaServer.Workflows
{
    public class CreateLoan : WorkflowBase
    {
        protected override void Build(IWorkflowBuilder builder)
        {
            var routeDataVariable = builder.WithVariable<IDictionary<string, object>>();
            var loanAmountVariable = builder.WithVariable<decimal>();
            var loanTermVariable = builder.WithVariable<int>();
            var loanInterestRateVariable = builder.WithVariable<decimal>();

            builder.Root = new Sequence
            {
                Activities =
                    {
                        new HttpEndpoint
                        {
                            Path = new("loans/"),
                            SupportedMethods = new([HttpMethods.Get]),
                            CanStartWorkflow = true,
                            RouteData = new(routeDataVariable)
                        },
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
                                new GenesisModeuleAction<CreateBorrowerRequestDto, BorrowerProjection>()
                                {
                                    Data = new Elsa.Workflows.Models.Input<CreateBorrowerRequestDto>(new CreateBorrowerRequestDto
                                    {
                                        FirstName = "John",
                                        LastName = "Doe",
                                    }),
                                    ActionName = ServiceActions.Borrowers.CreateBorrower,
                                }
                            }
                        }
                    }
            };
        }
    }
}