using Newtonsoft.Json;
using System;

namespace qwik.helpers.Chatters
{
    public class ScreenName
    {
        private readonly Lazy<string> _lazyLowerCased;
        private readonly Lazy<string> _lazyReadable;

        public ScreenName(string formatted)
        {
            Formatted = formatted;

            _lazyLowerCased = new Lazy<string>(() => Formatted.ToLower());
            _lazyReadable = new Lazy<string>(() => Regexes.Spaces.Replace(LowerCased, string.Empty));
        }

        public string Formatted { get; }

        [JsonIgnore]
        public string LowerCased => _lazyLowerCased.Value;

        [JsonIgnore]
        public string Readable => _lazyReadable.Value;

        protected bool Equals(ScreenName other)
        {
            return Equals(Readable, other.Readable);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ScreenName) obj);
        }

        public override int GetHashCode()
        {
            return Readable?.GetHashCode() ?? 0;
        }
    }
}