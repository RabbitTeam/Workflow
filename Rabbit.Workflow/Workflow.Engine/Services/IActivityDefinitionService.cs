using System.Collections.Generic;
using Workflow.Engine.Models;

namespace Workflow.Engine.Services
{
    public interface IActivityDefinitionService
    {
        IEnumerable<ActivityDefinition> GetStartActivityDefinitions(string name);

        IEnumerable<AwaitingActivityDefinition> GetAwaitingActivityDefinitions(string name);
    }
}