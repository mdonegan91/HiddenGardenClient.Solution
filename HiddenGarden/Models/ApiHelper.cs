using RestSharp;

namespace HiddenGarden.Models
{
  public class ApiHelper
  {
    public static async Task<string> GetAll()
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Backyard", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async Task<string> Get(int id)
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Backyard/{id}", Method.Get);
      RestResponse response = await client.GetAsync(request);
      return response.Content;
    }

    public static async void Post(string newBackyard)
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Backyard", Method.Post);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newBackyard);
      await client.PostAsync(request);
    }

    public static async void Put(int id, string newBackyard)
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Backyard/{id}", Method.Put);
      request.AddHeader("Content-Type", "application/json");
      request.AddJsonBody(newBackyard);
      await client.PutAsync(request);
    }
    
    public static async void Delete(int id)
    {
      RestClient client = new RestClient("https://localhost:5001/");
      RestRequest request = new RestRequest($"api/Backyard/{id}", Method.Delete);
      request.AddHeader("Content-Type", "application/json");
      await client.DeleteAsync(request);
    }
  }
}