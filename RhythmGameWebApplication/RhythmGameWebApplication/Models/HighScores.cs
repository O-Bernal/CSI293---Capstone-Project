namespace RhythmGameWebApplication.Models
{
    public class HighScore
    {
        public int HighScoreId { get; set; }
        public string? UserName { get; set; }
        public int Score { get; set; }
        public DateTime DateAchieved { get; set; }
    }
}
