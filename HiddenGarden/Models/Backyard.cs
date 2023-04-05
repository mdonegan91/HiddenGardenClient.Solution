using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HiddenGarden.Models
{
  public class Backyard
  {
    public int BackyardId { get; set; }
    public string Service { get; set; }
    public string Description { get; set; }
    public string Address { get; set; }
    public string Instructions { get; set; }
    public string UserId { get; set; }

    // public static Backyard[] GetBackyards()
    // {
    //   Task<string> apiCallTask = ApiHelper.GetAll();
    //   string result =  apiCallTask.Result;
      
    //   JArray jsonResponse = JArray.Parse(result);
    //   List<Backyard> backyardList = JsonConvert.DeserializeObject<List<Backyard>>(jsonResponse.ToString());

    //   return backyardList.ToArray();
    // }


    public static List<Backyard> GetBackyards()
    {
      var apiCallTask = ApiHelper.GetAll();
      var result = apiCallTask.Result;

      JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(result);
      List<Backyard> backyardList = JsonConvert.DeserializeObject<List<Backyard>>(jsonResponse.ToString());

      return backyardList;
    }
    public static Backyard GetDetails(int id)
    {
      var apiCallTask = ApiHelper.Get(id);
      var result = apiCallTask.Result;

      JObject jsonResponse = JObject.Parse(result);
      Backyard backyard = JsonConvert.DeserializeObject<Backyard>(jsonResponse.ToString());

      return backyard;
    }

    public static void Post(Backyard backyard)
    {
      string jsonBackyard = JsonConvert.SerializeObject(backyard);
      ApiHelper.Post(jsonBackyard);
    }

    public static void Put(Backyard backyard)
    {
      string jsonBackyard = JsonConvert.SerializeObject(backyard);
      ApiHelper.Put(backyard.BackyardId, jsonBackyard);
    }

    public static void Delete(int id)
    {
      ApiHelper.Delete(id);
    }
  }
}