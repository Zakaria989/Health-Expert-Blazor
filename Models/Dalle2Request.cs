namespace Health_expert_B.Models;
public class Dalle2Request
{

   public string Prompt { get; set; }
    public int N { get; set; } = 1;
    public string Size { get; set; } = "1024x1024";
    public string Response_format { get; set; } = "url";
    public string User { get; set; } = "User";
}
