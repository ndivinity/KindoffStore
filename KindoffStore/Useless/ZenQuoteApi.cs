using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace KindoffStore.Useless.ZenQuoteApi;

// Fields according to https://docs.zenquotes.io/zenquotes-documentation/
public struct ZenQuoteJsonObject
{
    [JsonPropertyName("q")]
    public string quote;

    [JsonPropertyName("a")]
    public string author;

    [JsonPropertyName("c")]
    public string characterCount;

    [JsonPropertyName("h")]
    public string html;
}

[Obsolete("The ZenQuote class isn't correctly implemented and isn't fixed yet.")]
public class ZenQuote
{
    public static readonly string ApiUrl = "https://zenquotes.io/api/quotes";

    // TODO: Think about the differences between using a List<> and an Array<>.
    public List<ZenQuoteJsonObject>? quotes { get; set; }

    public HttpClient connection = new();
    public Task<string> fetchSyncResult = null!;

    public int quoteRequestCount { get; private set; }
    public int quoteCount
    {
        get
        {
            return this.quotes != null
                ? this.quotes.Count()
                : 0;
        }

        private set { }
    }

    // TODO: Think about making this public.
    public string responseString = string.Empty;

    public ZenQuote()
    : this(1)
    { }

    public ZenQuote(int amount)
    {
        this.quoteRequestCount = amount;
    }

    public async Task FetchAsync()
    {
        this.responseString = await this.connection.GetStringAsync(ZenQuote.ApiUrl);

        this.quotes = JsonSerializer.Deserialize<List<ZenQuoteJsonObject>>(this.responseString)!;
    }

    ~ZenQuote() { }
}