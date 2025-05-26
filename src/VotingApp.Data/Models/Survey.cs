namespace VotingApp.Data.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ExpirationTime { get; set; }
        public ICollection<Option> Options { get; set; }
    }
}
