namespace R34Sharp
{
    /// <summary>
    /// Represents an R34Sharp entity.
    /// </summary>
    public abstract class R34Entity
    {
        protected R34ApiClient R34Client { get; private set; }

        internal async Task BuildAsync(R34ApiClient instance)
        {
            R34Client = instance;
            await OnBuildAsync();
        }
        protected abstract Task OnBuildAsync();
    }
}
