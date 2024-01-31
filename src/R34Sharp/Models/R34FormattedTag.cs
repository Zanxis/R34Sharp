namespace R34Sharp.Models
{
    /// <summary>
    /// Represents a Rule34-formatted tag.
    /// </summary>
    public sealed class R34FormattedTag
    {
        /// <summary>
        /// Gets or sets the current tag name formatted for the Rule34 tag template.
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }

        private string name;

        /// <summary>
        /// Initializes a new instance of the <see cref="R34FormattedTag"/> class.
        /// </summary>
        /// <param name="value">The name of the Rule34 Tag.</param>
        public R34FormattedTag(string value)
        {
            this.name = value.Trim().Replace(' ', '_').ToLower();
        }

        /// <summary>
        /// Returns the current Tag name.
        /// </summary>
        /// <returns>Tag Name.</returns>
        public override string ToString()
        {
            return this.name;
        }
    }
}