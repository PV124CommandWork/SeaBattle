namespace GameDBContext.Entities
{
    public class Registration
    {
        public Registration(DateTime deadline, string regCode)
        {
            Deadline = deadline;
            RegCode = regCode;
        }

        public int Id { get; set; }
        public DateTime Deadline { get; set; }
        public string RegCode { get; set; }

        // Navigation Properties

        public virtual IList<User> Users { get; set; }
    }
}
