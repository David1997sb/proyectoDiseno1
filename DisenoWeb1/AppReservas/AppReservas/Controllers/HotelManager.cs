using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppReservas.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Text;

namespace AppReservas.Controllers
{
    public class HotelManager
    {
        const string URL = "http://localhost:49220/api/hotel/";
        const string URLIngresar = "http://localhost:49220/api/hotel/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
        public async Task<IEnumerable<Hotel>> ObtenerHoteles(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Hotel>>(resultado);
        }

        public async Task<IEnumerable<Hotel>> ObtenerHotel(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Hotel>>(resultado);
        }
        public async Task<Hotel> Ingresar(Hotel hotel, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Hotel>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Hotel> Actualizar(Hotel hotel, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(hotel), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Hotel>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}