using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// Component responsible for creating expandable processes for the API.
    /// </summary>
    public abstract class R34BaseApiComponent
    {
        /// <summary>
        /// API Client.
        /// </summary>
        protected R34ApiClient ApiClient { get; private set; }

        internal void Build(R34ApiClient client)
        {
            ApiClient = client;
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
                result = await Task.Run(async () => (T)serializer.Deserialize(new StringReader(await ApiClient.ApiClient.GetStringAsync(url))));
                await result.BuildAsync(ApiClient);
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

            return await Task.FromResult(result);
        }
    }
}
