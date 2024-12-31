using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Serialization;


namespace TsaakAPI.Util
{
    public class CoreJsonRequest
    {
        private readonly HttpClient _apiClient;
        private DefaultContractResolver _contractResolver;        

        public CoreJsonRequest(HttpClient httpClient)
        {
            _apiClient = httpClient;
            _contractResolver = new DefaultContractResolver{
                NamingStrategy = new CamelCaseNamingStrategy()
            };            
        }


        /// <summary>
        /// Genera una llamada Post y devuelve la respuesta parseada en el objeto indicado
        /// </summary>
        /// <param name="requestPathService">URL de la llamada</param>
        /// <param name="body">Datos a envíar en el cuerpo de la petición</param>
        /// <param name="contractResolver">Configuración de serealizado.</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T?> PostRequestJsonServiceAsync<T>(string requestPathService, object? body = null, IContractResolver? contractResolver = null)
        {
            string jsonBody = string.Empty;
            if (body != null)
            {
                jsonBody = JsonConvert.SerializeObject(body,new JsonSerializerSettings(){
                    ContractResolver = contractResolver ?? _contractResolver
                 });
            }

            StringContent stringContent = new StringContent(
                jsonBody,
                Encoding.UTF8,
                "application/json"
                // MimeType.JSON.MimeTypeString()
                );

            var response = await _apiClient.PostAsync(requestPathService, stringContent).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                var list = JsonConvert.DeserializeObject<T>(content);
                if (list == null)
                {
                    return default(T);
                }
                else
                {
                    return list;
                }
            }
            else
            {                
                throw new Exception($"Error de comunicación con el servicio. HTTP - {response.StatusCode} - {response.ReasonPhrase} - {response.RequestMessage}");                
            }
        }
        
    }
}