namespace VotingApp.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public int OptionId { get; set; }
        public Option Option { get; set; }
    }
}
