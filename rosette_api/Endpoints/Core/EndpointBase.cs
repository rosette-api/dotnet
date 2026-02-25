using Rosette.Api.Models;
using System.Collections.Specialized;

namespace Rosette.Api.Endpoints.Core;

public class EndpointBase<T> where T : EndpointBase<T>
{
    private EndpointExecutor _funcs = null;
    public Dictionary<string, object> Options { get; private set; }
    public Dictionary<string, object> Params { get; set; }
    public NameValueCollection UrlParameters { get; private set; }
    public string Endpoint { get; protected set; }
    public EndpointExecutor Funcs {
        get {
            if (_funcs == null) {
                _funcs = new EndpointExecutor(Params, Options, UrlParameters, Endpoint);
            }
            return _funcs;
        }
    }

    public EndpointBase(string endpoint) {
        Options = [];
        Params = [];
        UrlParameters = [];
        Endpoint = endpoint;
    }

    /// <summary>
    /// SetOption sets an option and value
    /// </summary>
    /// <param name="optionName">Name of option</param>
    /// <param name="optionValue">Value of option</param>
    /// <returns>Update object</returns>
    public T SetOption(string optionName, object optionValue)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(optionName);
        ArgumentNullException.ThrowIfNull(optionValue);
        
        Options[optionName] = optionValue;
        return (T)this;
    }

    /// <summary>
    /// RemoveOption removes an option
    /// </summary>
    /// <param name="optionName">Name of option</param>
    /// <returns>Update object</returns>
    public T RemoveOption(string optionName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(optionName);
        Options.Remove(optionName);
        return (T)this;
    }

    /// <summary>
    /// ClearOptions removes all options
    /// </summary>
    /// <returns>Updated object</returns>
    public T ClearOptions() {
        Options.Clear();
        return (T)this;
    }

    /// <summary>
    /// SetUrlParameter sets a key/value for a query string, e.g. ?output=rosette
    /// </summary>
    /// <param name="parameterKey">parameter key</param>
    /// <param name="parameterValue">parameter value</param>
    /// <returns>Updated object</returns>
    public T SetUrlParameter(string parameterKey, string parameterValue)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(parameterKey);
        ArgumentException.ThrowIfNullOrWhiteSpace(parameterValue);
        
        UrlParameters.Add(parameterKey, parameterValue);
        return (T)this;
    }

    /// <summary>
    /// RemoveUrlParameter removes all values associated with a specified key
    /// </summary>
    /// <param name="parameterKey">parameter key</param>
    /// <returns>Update object</returns>
    public T RemoveUrlParameter(string parameterKey)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(parameterKey);
        UrlParameters.Remove(parameterKey);
        return (T)this;
    }

    /// <summary>
    /// CallAsync passes the data to the server and returns the response asynchronously
    /// </summary>
    /// <param name="api">ApiClient object</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Response</returns>
    public Task<Response> CallAsync(ApiClient api, CancellationToken cancellationToken = default)
    {
        return Funcs.GetCallAsync(api, cancellationToken);
    }

    /// <summary>
    /// Call passes the data to the server and returns the response
    /// </summary>
    /// <param name="api">ApiClient object</param>
    /// <returns>Response</returns>
    public Response Call(ApiClient api) => Funcs.GetCall(api);

}
