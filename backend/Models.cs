// Models.cs
using System.Text.Json.Serialization;

public record LLMRequest(string System, string User, int? MaxTokens = 512);
public record LLMResponse(string Text, int PromptTokens = 0, int CompletionTokens = 0);

public record TestCase(string Id, string Name, string Severity, string Payload, string ExpectedPolicy);
public record TestSuite(string Suite, string Version, List<TestCase> Tests);

public record TestResult(
    string TestId,
    bool Passed,
    int Score,
    string Evidence,
    string OutputSnippet,
    [property: JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)] string? FullOutput = null
);

public record RunRequest(
    string Provider,          // "openai" | "ollama"
    string Model,             // example "gpt-4o-mini" or "llama3"
    TestSuite Suite,
    string? OpenAIApiKey = null,
    int? MaxTokens = 200
);

public record RunReport(
    string Suite,
    string Version,
    string Provider,
    string Model,
    DateTime RanAt,
    List<TestResult> Results
);