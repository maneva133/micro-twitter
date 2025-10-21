namespace MCAProject_Twitter.CQRS.Queries
{
    public class GetMyPostsQuery
    {
        public string Username { get; set; }
        public GetMyPostsQuery(string username) { Username = username; }
    }
}
