namespace VotingApp.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Survey Survey { get; set; }
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}