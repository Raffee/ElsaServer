﻿using Elsa.Extensions;
using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using Genesis.Common.Core;
using System.Diagnostics;

namespace Workflow.Common.Activities
{
    /// <summary>
    ///  Write a line of text to the console.
    /// </summary>
    [Activity("Elsa", "Console", "Write a line of text to the console.")]
    public class GenesisModuleAction<TInput, TOutput>() : CodeActivity<TOutput> 
    {
        [Output(Description = "The input of the action.")]
        public Input<TInput> InputData { get; set; } = default!;
        public required string ActionName { get; set; }

        /// <inheritdoc />
        protected override void Execute(ActivityExecutionContext context)
        {
            var mediator = context.GetRequiredService<Mediator>();
            var data = InputData.Get(context);

            if ((data == null))
            {
                throw new SystemException("Input is required.");
            }
            //use mediator to send a message to the external system
            var output = mediator.SendAsync<TInput, TOutput>(ActionName, data);

            context.Set(Result, output.Result!);
        }
    }
}
