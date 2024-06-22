using AthleteHub.Domain.Enums;

namespace AthleteHub.Domain.Entities;

public class Content
{
    public int Id { get; set; }
    public int CoachId {  get; set; }
    public string Url { get; set; }
    public ContentType ContentType { get; set; }
    public virtual Coach Coach { get; set; }
    public virtual ICollection<PostContent> PostsContents { get; set; } = new List<PostContent>();

}
