using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {

        public static async Task <Tempo?> GetPrevisao(string Cidade) {
            Tempo? t = null;
            string chave = "0dd2f0b0aec202d4a722b2b033578b2b";
            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                $"q={Cidade}&units=metric&appid={chave}";

            using (HttpClient httpClient = new HttpClient())
            {
                
                HttpResponseMessage resp = await httpClient.GetAsync(url);

                if (resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();

                    var rascunho = JObject.Parse(json);

                    DateTime time = new();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();

                    t = new()
                    {
                        lat = (double)rascunho["coord"]["lat"],
                        lon = (double)rascunho["coord"]["lon"],
                        description = (string)rascunho["weather"][0]["description"],
                        main = (string)rascunho["weather"][0]["main"],
                        temp_min = (double)rascunho["main"]["temp_min"],
                        temp_max = (double)rascunho["main"]["temp_max"],
                        sunrise = sunrise.ToString(),
                        sunset = sunset.ToString(),
                        speed = (double)rascunho["wind"]["speed"],
                        visibility = (int)rascunho["visibility"],
                    };
                } else if (resp.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        throw new Exception("A cidade não foi encontrada");
                    }
                else if (resp.StatusCode == System.Net.HttpStatusCode.RequestTimeout)
                {
                    throw new Exception("Você está sem conexão com a internet");
                }
            }


            return t;
                }
    }
}
