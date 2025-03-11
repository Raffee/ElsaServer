using Elsa.Extensions;
using Elsa.Mediator.Contracts;
using Elsa.Workflows;
using Elsa.Workflows.Attributes;
using Elsa.Workflows.Models;
using Genesis.Common.Core;
using System.ComponentModel;
using System.Diagnostics;

namespace Workflow.Common.Activities
{
    /// <summary>
    ///  Write a line of text to the console.
    /// </summary>
    [Activity("Elsa", "Console", "Write a line of text to the console.")]
    public class CustomActivity<TInput, TOutput>(Mediator mediator) : CodeActivity<TOutput> 
    {
        private readonly Mediator _mediator = mediator;

        /// <summary>
        /// The text to write.
        /// </summary>
        [Description("The text to write.")]
        public TInput Data { get; set; } = default!;
        public string ActionName { get; set; }

        /// <inheritdoc />
        protected override void Execute(ActivityExecutionContext context)
        {
            //use mediator to send a message to the external system
            var output = _mediator.SendAsync<TInput, TOutput>(ActionName, Data);

            context.Set(Result, output.Result!);
        }
    }
}
