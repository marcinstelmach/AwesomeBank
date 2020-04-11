namespace AwesomeBank.Identity.Domain.Entities
{
    public class Role
    {
        protected Role()
        {
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}