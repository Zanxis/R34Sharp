using System.Threading.Tasks;

namespace R34Sharp.Entities
{
    /// <summary>
    /// Represents an R34Sharp entity.
    /// </summary>
    public abstract class R34Entity
    {
        /// <summary>
        /// The current entity's client.
        /// </summary>
        protected R34ApiClient R34Client { get; private set; }

        internal async Task BuildAsync(R34ApiClient instance)
        {
            this.R34Client = instance;
            await OnBuildAsync();
        }

        /// <summary>
        /// Special constructor responsible for initializing basic information of classes with inheritance.
        /// </summary>
        protected abstract Task OnBuildAsync();
    }
}
