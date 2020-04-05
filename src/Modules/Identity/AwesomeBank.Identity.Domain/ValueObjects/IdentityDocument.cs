namespace AwesomeBank.Identity.Domain.ValueObjects
{
    using System.Collections.Generic;
    using AwesomeBank.BuildingBlocks.Domain;
    using AwesomeBank.Identity.Domain.Enums;

    public class IdentityDocument : ValueObject<IdentityDocument>
    {
        public IdentityDocument(IdentityDocumentType type, string value)
        {
            Insist.IsNotNullOrWhiteSpace(value, nameof(value));

            Type = type;
            Value = value;
        }

        protected IdentityDocument()
        {
        }

        public IdentityDocumentType Type { get; private set; }

        public string Value { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
            yield return Value;
        }
    }
}