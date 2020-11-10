using System;
using System.Threading.Tasks;

namespace MessengerApp.Blazor.Services
{
    public interface IRestClient
    {
        ValueTask<T> GetContent<T>(string url);

        ValueTask<T> PostContent<T>(string relativeUrl, object data);
    }
}
