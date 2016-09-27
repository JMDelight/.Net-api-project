using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using TopYouTube.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TopYouTube.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            var youtubeClient = new RestClient("https://www.googleapis.com/youtube/v3/videos");
            var youtubeRequest = new RestRequest("?part=snippet&chart=mostPopular&maxResults=15&key=" + EnvironmentVariables.youtubeApiKey);
            var topVideoResponse = new RestResponse();
            Task.Run(async () =>
            {
                topVideoResponse = await GetResponseContentAsync(youtubeClient, youtubeRequest) as RestResponse;
            }).Wait();
            JObject jsonTopVideoResponse = JsonConvert.DeserializeObject<JObject>(topVideoResponse.Content);
            var youtubeVideoList = jsonTopVideoResponse["items"];
            ViewBag.VideoList = youtubeVideoList;

            return View();
        }



        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
