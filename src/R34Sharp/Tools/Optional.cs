namespace R34Sharp.Tools
{
    /// <summary>
    /// An optional value that can be set or ignored.
    /// </summary>
    /// <typeparam name="T">The type of the optional value.</typeparam>
    public sealed class Optional<T>
    {
        /// <summary>
        /// The current instance has value.
        /// </summary>
        public bool HasValue { get; private set; }

        /// <summary>
        /// The value of the current instance.
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Construction of the optional value.
        /// </summary>
        public Optional()
        {
            this.HasValue = false;
        }

        /// <summary>
        /// Construction of the optional value.
        /// </summary>
        /// <param name="value">The value that the current instance will have.</param>
        public Optional(T value)
        {
            this.HasValue = true;
            this.Value = value;
        }
    }
}