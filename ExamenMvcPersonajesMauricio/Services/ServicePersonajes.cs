using ExamenMvcPersonajesMauricio.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace ExamenMvcPersonajesMauricio.Services
{
    public class ServicePersonajes
    {
        private string UrlApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServicePersonajes(IConfiguration config)
        {
            this.UrlApi = config.GetValue<string>("ApiUrls:ApiPersonajes");
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        private async Task<T> CallGetApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> CallPostApiAsync<T>(string request, T dataObj)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(dataObj);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<bool> CallPutApiAsync<T>(string request, T dataObj)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                string json = JsonConvert.SerializeObject(dataObj);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private async Task<bool> CallDeleteApiAsync(string request)
        {
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(this.UrlApi);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.DeleteAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "api/Personajes";
            return await this.CallGetApiAsync<List<Personaje>>(request);
        }
        public async Task<List<Personaje>> GetPersonajesBySerieAsync(string serie)
        {
            string request = "api/Personajes/PersonajesSeries/" + serie;
            return await this.CallGetApiAsync<List<Personaje>>(request);
        }
        public async Task<List<string>> GetSeriesAsync()
        {
            string request = "api/Personajes/Series";
            return await this.CallGetApiAsync<List<string>>(request);
        }
        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "api/Personajes/" + id;
            return await this.CallGetApiAsync<Personaje>(request);
        }
        public async Task<Personaje> InsertPersonajeAsync(string nombre, string imagen, string serie)
        {
            string request = "api/Personajes/InsertPersonaje";
            Personaje personaje = new Personaje();
            personaje.IdPersonaje = 0;
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            personaje.Serie = serie;
            return await this.CallPostApiAsync<Personaje>(request, personaje);
        }
        public async Task<bool> UpdatePersonajeAsync(int id, string nombre, string imagen, string serie)
        {
            string request = "api/Personajes/UpdatePersonaje";
            Personaje personaje = new Personaje();
            personaje.IdPersonaje = id;
            personaje.Nombre = nombre;
            personaje.Imagen = imagen;
            personaje.Serie = serie;
            return await this.CallPutApiAsync<Personaje>(request, personaje);
        }
        public async Task<bool> DeletePersonajeAsync(int id)
        {
            string request = "api/Personajes/DeletePersonaje/" + id;
            return await this.CallDeleteApiAsync(request);
        }
    }
}
