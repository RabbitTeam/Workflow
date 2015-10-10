using System.Collections.Generic;
using System.Linq;

namespace Workflow.Engine.Services
{
    public interface IActivitiesManager
    {
        IEnumerable<IActivity> GetActivities();

        IActivity GetActivityByName(string name);
    }

    public class ActivitiesManager : IActivitiesManager
    {
        private readonly IEnumerable<IActivity> _activities;

        public ActivitiesManager(IEnumerable<IActivity> activities)
        {
            _activities = activities;
        }

        public IEnumerable<IActivity> GetActivities()
        {
            return _activities.OrderBy(x => x.Name).ToArray();
        }

        public IActivity GetActivityByName(string name)
        {
            return _activities.FirstOrDefault(x => x.Name == name);
        }
    }
}