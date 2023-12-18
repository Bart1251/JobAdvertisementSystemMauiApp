namespace JobAdvertisementApp.Services
{
    public class MapService
    {
        private readonly HttpClient httpClient;
        private readonly string bingMapsApiKey = "AsUeck_Ez--mXKxU6JpA3KZmTmvAuMDmEfYZuQ6gpeE0wmcFOynbPUnxHkk2Waqn";

        public MapService()
        {
            httpClient = new HttpClient();
        }

        public async Task<ImageSource> GetMapImageAsync(string location, int zoomLevel = 13)
        {
            string url = $"http://dev.virtualearth.net/REST/v1/Imagery/Map/Road/{location}/{zoomLevel}?mapSize=500,500&pushpin={location};66&key={bingMapsApiKey}";
            var response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            Stream imageStream = await response.Content.ReadAsStreamAsync();
            return ImageSource.FromStream(() => imageStream);
        }
    }
}
