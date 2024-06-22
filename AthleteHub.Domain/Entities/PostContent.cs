namespace AthleteHub.Domain.Entities;

public class PostContent
{
    public int PostId { get; set; }
    public int ContentId { get; set; }
    public virtual Post Post { get; set; }
    public virtual Content Content { get; set; }
}
