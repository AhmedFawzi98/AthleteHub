namespace AthleteHub.Domain.Entities;

public class Post
{
    public int Id { get; set; }
    public int AthleteId {  get; set; }
    public string Message { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public virtual Athlete Athlete { get; set; }
    public virtual ICollection<PostContent> PostsContents { get; set; } = new List<PostContent>();

}
