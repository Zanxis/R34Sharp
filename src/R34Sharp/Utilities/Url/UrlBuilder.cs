using System.Text;

namespace R34Sharp
{
    internal sealed class UrlBuilder
    {
        internal string BaseAddress { get; private set; }
        private readonly Dictionary<string, string> parameters = new();

        internal UrlBuilder(string address)
        {
            BaseAddress = address;
        }

        internal void AddParameter(string name, string value)
        {
            parameters.TryAdd(name, value);
        }

        internal string Build()
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
