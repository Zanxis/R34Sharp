using R34Sharp.Entities;

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace R34Sharp.Components
{
    /// <summary>
    /// Component responsible for creating expandable processes for the API.
    /// </summary>
    public abstract class R34BaseApiComponent
    {
        /// <summary>
        /// API Client.
        /// </summary>
        protected R34Client ApiClient { get; private set; }

        internal void Build(R34Client client)
        {
            this.ApiClient = client;
        }

        /// <summary>
        /// Responsible for making requests to a certain URL in order to receive string contents, especially XML.
        /// </summary>
        /// <typeparam name="T">Generic data that will be used for return.</typeparam>
        /// <param name="url">Url where the request will be made.</param>
        /// <param name="serializer">XML serializer that will be used for conversion operations.</param>
        /// <returns>Generic data filled with requested data.</returns>
        protected async Task<T> GetAsync<T>(string url, XmlSerializer serializer) where T : R34Data
        {
            T result = default(T);

            try
            {
                HttpRequestMessage message = new(HttpMethod.Get, url);
                HttpResponseMessage msg = await this.ApiClient.Client.SendAsync(message);

                msg.EnsureSuccessStatusCode();

                result = await Task.Run(async () => (T)serializer.Deserialize(new StringReader(await msg.Content.ReadAsStringAsync())));
                await result.BuildAsync(this.ApiClient);
            }
            catch (Exception) { }

            return await Task.FromResult(result);
        }
    }
}
