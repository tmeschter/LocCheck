using System.Threading.Tasks;
using Octokit;

namespace LocCheck.Services
{
    internal interface IGithubConnectionCache
    {
        Task<IConnection> GetConnectionAsync(long installationId);
    }
}