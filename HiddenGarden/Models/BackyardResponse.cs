namespace HiddenGarden.Models;

    public class BackyardResponse
    {
        public List<Backyard> Backyards { get; set; } = new List<Backyard>();
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }