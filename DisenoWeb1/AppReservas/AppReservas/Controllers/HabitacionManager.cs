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
    public class HabitacionManager
    {
        const string URL = "http://localhost:49220/api/habitacion/";
        const string URLIngresar = "http://localhost:49220/api/habitacion/ingresar/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");

            return client;
        }
        public async Task<IEnumerable<Habitacion>> ObtenerHabitaciones(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);

            return JsonConvert.DeserializeObject<IEnumerable<Habitacion>>(resultado);
        }

        public async Task<IEnumerable<Habitacion>> ObtenerHabitacion(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Habitacion>>(resultado);
        }
        public async Task<Habitacion> Ingresar(Habitacion habitacion, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(URLIngresar,
                new StringContent(JsonConvert.SerializeObject(habitacion), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Habitacion>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Habitacion> Actualizar(Habitacion habitacion, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(habitacion), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Habitacion>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);

            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}