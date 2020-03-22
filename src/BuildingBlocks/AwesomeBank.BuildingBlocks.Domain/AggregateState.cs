namespace AwesomeBank.BuildingBlocks.Domain
{
    using System.Collections.Generic;
    using System.Linq;

    public class AggregateState
    {
        private readonly List<string> _errors;

        public AggregateState()
        {
            _errors = new List<string>();
        }

        public IReadOnlyCollection<string> Errors => _errors.AsReadOnly();

        public bool HasErrors => _errors.Any();

        public void AddError(string error) => _errors.Add(error);
    }
}