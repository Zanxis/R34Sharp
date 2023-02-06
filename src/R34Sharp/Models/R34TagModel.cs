namespace R34Sharp
{
    /// <summary>
    /// The Rule34 model Tag Builder.
    /// </summary>
    public class R34TagModel
    {
        public string Name
        {
            get => name;
            set => name = value.ToLower().Replace(' ', '_');
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
            name = value;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
