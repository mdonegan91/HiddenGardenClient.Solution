using RestSharp;
using System.Threading.Tasks;

namespace HiddenGarden.Models
{
  public class ApiHelper
  {
    public static async Task<string> GetAll()
    {
      RestClient client = new RestClient("https://localhost:7225/");
      RestRequest request = new RestRequest($"api/Backyards", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async Task<string> Get(int id)
    {
      RestClient client = new RestClient("https://localhost:7225/");
      RestRequest request = new RestRequest($"api/Backyards/{id}", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async void Post(string newBackyards)
    {
      RestClient client = new RestClient("https://localhost:7225/");
      RestRequest request = new RestRequest($"api/Backyards", Method.Post);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newBackyards);
      await client.PostAsync(request);
    }

    public static async void Put(int id, string newBackyards)
    {
      RestClient client = new RestClient("https://localhost:7225/");
      RestRequest request = new RestRequest($"api/Backyards/{id}", Method.Put);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newBackyards);
      await client.PutAsync(request);
    }
    
    public static async void Delete(int id)
    {
      RestClient client = new RestClient("https://localhost:7225/");
      RestRequest request = new RestRequest($"api/Backyards/{id}", Method.Delete);
      request.AddHeader("Content-Type", "application/json");
      await client.DeleteAsync(request);
    }
  }
}