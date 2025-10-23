namespace SearchService.DAL.Options;
public class ElasticOptions
{
    public string Url { get; set; }
    public string DefaultIndex { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}