using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Health_expert_B.Data
{
    public class ChatOptions
    {
        public string ApiKey { get; set; }
        public string ApiUrl { get; set; }
        public string GptModel { get; set; }


    }
}
