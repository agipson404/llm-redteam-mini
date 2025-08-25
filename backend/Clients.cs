// Clients.cs
using System.Net.Http.Json;

public interface ILLMClient { Task<LLMResponse> AskAsync(LLMRequest r, CancellationToken ct = default); }

public sealed class OllamaClient(HttpClient http, string model) : ILLMClient
{
    public async Task<LLMResponse> AskAsync(LLMRequest r, CancellationToken ct = default)
    {
        http.BaseAddress ??= new Uri("http://localhost:11434/");
        var body = new { model, prompt = $"{r.System}\nUser: {r.User}", stream = false };
        var resp = await http.PostAsJsonAsync("api/generate", body, ct);
        resp.EnsureSuccessStatusCode();
        var json = await resp.Content.ReadFromJsonAsync<System.Text.Json.JsonElement>(cancellationToken: ct);
        var text = json.TryGetProperty("response", out var node) ? (node.GetString() ?? string.Empty) : string.Empty;
        return new LLMResponse(text);
    }
}

public sealed class OpenAIClient(HttpClient http, string model, string apiKey) : ILLMClient
{
    public async Task<LLMResponse> AskAsync(LLMRequest r, CancellationToken ct = default)
    {
        http.BaseAddress ??= new Uri("https://api.openai.com/v1/");
        http.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", apiKey);

        var body = new
        {
            model,
            messages = new[]
            {
                new { role = "system", content = r.System },
                new { role = "user", content = r.User }
            },
            max_tokens = r.MaxTokens ?? 512
        };

        var resp = await http.PostAsJsonAsync("chat/completions", body, ct);

        var raw = await resp.Content.ReadAsStringAsync(ct); // read for diagnostics
        if (!resp.IsSuccessStatusCode)
        {
            // bubble the exact status + body up so the endpoint can format it
            throw new HttpRequestException(
                $"OpenAI error {(int)resp.StatusCode} {resp.StatusCode}: {raw}",
                null,
                resp.StatusCode);
        }

        using var json = System.Text.Json.JsonDocument.Parse(raw);
        var text = json.RootElement
                       .GetProperty("choices")[0]
                       .GetProperty("message")
                       .GetProperty("content").GetString() ?? string.Empty;

        return new LLMResponse(text);
    }
}
