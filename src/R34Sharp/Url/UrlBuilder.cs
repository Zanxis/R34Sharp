using System.Collections.Generic;
using System.Text;

namespace R34Sharp.Url
{
    internal sealed class UrlBuilder
    {
        internal string BaseAddress { get; private set; }
        private readonly List<(string name, string value)> parameters = new();

        internal UrlBuilder(string address)
        {
            BaseAddress = address;
        }

        internal void AddParameter(string name, string value)
        {
            parameters.Add((name, value));
        }

        internal void Clear()
        {
            this.parameters.Clear();
        }

        internal string Build()
        {
            StringBuilder addressBuilder = new();
            addressBuilder.Append($"{BaseAddress}?");

            for (int i = 0; i < parameters.Count; i++)
            {
                (string, string) parameter = parameters[i];
                _ = addressBuilder.Append($"{parameter.Item1.ToLower()}={parameter.Item2.ToLower()}");

                if (i < parameters.Count - 1)
                    _ = addressBuilder.Append('&');
            }

            return addressBuilder.ToString();
        }
    }
}
