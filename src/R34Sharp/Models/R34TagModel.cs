namespace R34Sharp.Models
{
    /// <summary>
    /// The Rule34 model Tag Builder.
    /// </summary>
    public sealed class R34TagModel
    {
        /// <summary>
        /// Current tag name formatted for the Rule34 tag template.
        /// </summary>
        public string Name
        {
            get => this.name;
            set => this.name = value;
        }
        private string name;

        /// <summary>
        /// Build a Tag that is automatically formatted for the Rule34 style.
        /// </summary>
        /// <remarks>
        /// All spaces are automatically replaced by the "_" character and the string is fully formatted for lowercase.
        /// </remarks>
        /// <param name="value">The name of the Rule34 Tag.</param>
        public R34TagModel(string value)
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