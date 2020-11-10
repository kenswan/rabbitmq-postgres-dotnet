using System.Threading.Tasks;

namespace MessengerApp.Blazor.Services
{
    public interface IRestClient
    {
        ValueTask<T> GetContentAsync<T>(string url);

        ValueTask<T> PostContentAsync<T>(string relativeUrl, object data);
    }
}
