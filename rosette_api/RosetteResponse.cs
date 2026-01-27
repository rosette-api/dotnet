using System.IO.Compression;
using System.Text;
using System.Text.Json;

namespace rosette_api;

public class RosetteResponse
{
    public RosetteResponse(HttpResponseMessage responseMsg) {
        Content = new Dictionary<string, object>();
        Headers = new Dictionary<string, string>();

        StatusCode = (int)responseMsg.StatusCode;

        if (responseMsg.IsSuccessStatusCode) {
            foreach (var header in responseMsg.Headers) {
                Headers.Add(header.Key, string.Join("", header.Value));
            }
            foreach (var header in responseMsg.Content.Headers) {
                Headers.Add(header.Key, string.Join("", header.Value));
            }
            byte[] byteArray = responseMsg.Content.ReadAsByteArrayAsync().Result;
            if(byteArray[0] == '\x1f' && byteArray[1] == '\x8b' && byteArray[2] == '\x08') {
                byteArray = Decompress(byteArray);
            }
            string result = string.Empty;
            using (StreamReader reader = new StreamReader(new MemoryStream(byteArray), Encoding.UTF8)) {
                result = reader.ReadToEnd();
            }

            Content = JsonSerializer.Deserialize<Dictionary<string, object>>(result)!;
        }
        else {
            throw new HttpRequestException(string.Format("{0}: {1}: {2}", (int)responseMsg.StatusCode, responseMsg.ReasonPhrase, ContentToString(responseMsg.Content)));
        }
    }

    /// <summary>
    /// Headers provides read access to the Response Headers collection
    /// </summary>
    /// <returns>IDictionary of string, string</returns>
    public IDictionary<string, string> Headers {get; private set;}

    /// <summary>
    /// Content provides read access to the Response IDictionary
    /// </summary>
    /// <returns>IDictionary of string, object</returns>
    public IDictionary<string, object> Content {get; private set;}

    public object ContentAsJson(bool pretty=false) {
        return pretty ?
        JsonSerializer.Serialize(Content, new JsonSerializerOptions { WriteIndented = true }) :
        JsonSerializer.Serialize(Content); }
    public int StatusCode {get; private set;}


    /// <summary>Decompress decompresses GZIP files
    /// Source: http://www.dotnetperls.com/decompress
    /// </summary>
    /// <param name="gzip">(byte[]): Data in byte form to decompress</param>
    /// <returns>(byte[]) Decompressed data</returns>
    private byte[] Decompress(byte[] gzip) {
        // Create a GZIP stream with decompression mode.
        // ... Then create a buffer and write into while reading from the GZIP stream.
        using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress)) {
            const int size = 4096;
            byte[] buffer = new byte[size];
            using (MemoryStream memory = new MemoryStream()) {
                int count = 0;
                do {
                    count = stream.Read(buffer, 0, size);
                    if (count > 0) {
                        memory.Write(buffer, 0, count);
                    }
                }
                while (count > 0);
                return memory.ToArray();
            }
        }
    }
    internal static string ContentToString(HttpContent httpContent) {
        if (httpContent != null) {
            var readAsStringAsync = httpContent.ReadAsStringAsync();
            return readAsStringAsync.Result;
        }
        else {
            return string.Empty;
        }
    }
}