using System.Xml.Serialization;

namespace R34Sharp
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class R34BaseApiComponent
    {
        /// <summary>
        /// 
        /// </summary>
        protected R34ApiClient ApiClient { get; private set; }

        internal void Build(R34ApiClient client)
        {
            ApiClient = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        protected async Task<T> GetAsync<T>(string url, XmlSerializer serializer) where T : R34Data
        {
            T result = default(T);

            try
            {
                using StringReader reader = new(await ApiClient.ApiClient.GetStringAsync(url));

                result = await Task.Run(() => (T)serializer.Deserialize(reader));
                await Task.Run(() => result.BuildAsync(ApiClient));
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
            }

            return await Task.FromResult(result);
        }
    }
}
