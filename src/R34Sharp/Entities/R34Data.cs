using System.Threading.Tasks;

namespace R34Sharp.Entities
{
    /// <summary>
    /// Represents a generic data collection of R34Sharp entities.
    /// </summary>
    public abstract class R34Data
    {
        internal abstract Task BuildAsync(R34ApiClient instance);
    }
}
