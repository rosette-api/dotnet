using System.IO.Compression;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Rosette.Api.Models;

public class Response
{
    // Private constructor for async factory
    private Response()
    {
        Content = new Dictionary<string, object>();
        Headers = new Dictionary<string, string>();
    }

    // Keep existing synchronous constructor for backward compatibility
    public Response(HttpResponseMessage responseMsg) : this()
    {
        StatusCode = (int)responseMsg.StatusCode;

        if (responseMsg.IsSuccessStatusCode)
        {
            ProcessHeaders(responseMsg);
            byte[] byteArray = responseMsg.Content.ReadAsByteArrayAsync().Result;
            string result = ProcessContent(byteArray);
            Content = JsonSerializer.Deserialize<Dictionary<string, object>>(result)!;
        }
        else
        {
            throw new HttpRequestException(
                $"{(int)responseMsg.StatusCode}: {responseMsg.ReasonPhrase}: {ContentToString(responseMsg.Content)}");
        }
    }

    /// <summary>
    /// CreateAsync creates a Response asynchronously from an HttpResponseMessage
    /// </summary>
    /// <param name="responseMsg">HTTP response message</param>
    /// <param name="cancellationToken">Optional cancellation token</param>
    /// <returns>Response</returns>
    public static async Task<Response> CreateAsync(HttpResponseMessage responseMsg, CancellationToken cancellationToken = default)
    {
        var response = new Response
        {
            StatusCode = (int)responseMsg.StatusCode
        };

        if (responseMsg.IsSuccessStatusCode)
        {
            response.ProcessHeaders(responseMsg);
            byte[] byteArray = await responseMsg.Content.ReadAsByteArrayAsync(cancellationToken).ConfigureAwait(false);
            string result = response.ProcessContent(byteArray);
            response.Content = JsonSerializer.Deserialize<Dictionary<string, object>>(result)!;
        }
        else
        {
            string errorContent = await ContentToStringAsync(responseMsg.Content, cancellationToken).ConfigureAwait(false);
            throw new HttpRequestException(
                $"{(int)responseMsg.StatusCode}: {responseMsg.ReasonPhrase}: {errorContent}");
        }

        return response;
    }

    /// <summary>
    /// Headers provides read access to the Response Headers collection
    /// </summary>
    public IDictionary<string, string> Headers { get; private set; }

    /// <summary>
    /// Content provides read access to the Response IDictionary
    /// </summary>
    public IDictionary<string, object> Content { get; private set; }

    /// <summary>
    /// StatusCode returns the HTTP status code
    /// </summary>
    public int StatusCode { get; private set; }

    public object ContentAsJson(bool pretty = false)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = pretty,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
        };

        return JsonSerializer.Serialize(Content, options);
    }

    private void ProcessHeaders(HttpResponseMessage responseMsg)
    {
        foreach (var header in responseMsg.Headers)
        {
            Headers.Add(header.Key, string.Join("", header.Value));
        }
        foreach (var header in responseMsg.Content.Headers)
        {
            Headers.Add(header.Key, string.Join("", header.Value));
        }
    }

    private string ProcessContent(byte[] byteArray)
    {
        if (byteArray.Length >= 3 && byteArray[0] == '\x1f' && byteArray[1] == '\x8b' && byteArray[2] == '\x08')
        {
            byteArray = Decompress(byteArray);
        }

        using (StreamReader reader = new StreamReader(new MemoryStream(byteArray), Encoding.UTF8))
        {
            return reader.ReadToEnd();
        }
    }

    private byte[] Decompress(byte[] gzip)
    {
        using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
        {
            const int size = 4096;
            byte[] buffer = new byte[size];
            using (MemoryStream memory = new MemoryStream())
            {
                int count;
                do
                {
                    count = stream.Read(buffer, 0, size);
                    if (count > 0)
                    {
                        memory.Write(buffer, 0, count);
                    }
                } while (count > 0);
                return memory.ToArray();
            }
        }
    }

    internal static string ContentToString(HttpContent httpContent)
    {
        return httpContent?.ReadAsStringAsync().Result ?? string.Empty;
    }

    internal static async Task<string> ContentToStringAsync(HttpContent httpContent, CancellationToken cancellationToken = default)
    {
        if (httpContent != null)
        {
            return await httpContent.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        }
        return string.Empty;
    }
}