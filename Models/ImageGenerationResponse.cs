namespace Health_expert_B.Models;

public class ImageGenerationResponse
{
    public long created { get; set; }
    public List<ImageData> data { get; set; }
}

public class ImageData
{
    public string url { get; set; }
}
