namespace Genesis.Common.Core
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class ServiceActionAttribute : Attribute
    {
        public string Action { get; }

        public ServiceActionAttribute(string action)
        {
            Action = action;
        }
    }
}
