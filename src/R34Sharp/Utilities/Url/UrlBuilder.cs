using System.Text;

namespace R34Sharp
{
    internal class UrlBuilder
    {
        public string BaseAddress { get; private set; }

        private readonly Dictionary<string, string> parameters = new();

        public UrlBuilder(string address)
        {
            BaseAddress = address;
        }
        public void AddParameter(string name, string value)
        {
            parameters.TryAdd(name, value);
        }

        public string Build()
        {
            StringBuilder addressBuilder = new();
            addressBuilder.Append($"{BaseAddress}?");

            for (int i = 0; i < parameters.Count; i++)
            {
                KeyValuePair<string, string> parameter = parameters.ElementAt(i);
                _ = addressBuilder.Append($"{parameter.Key.ToLower()}={parameter.Value.ToLower()}");

                if (i < parameters.Count - 1)
                    _ = addressBuilder.Append('&');
            }

            return addressBuilder.ToString();
        }
    }
}
