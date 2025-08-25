// Detectors.cs
using System.Text.RegularExpressions;

public static class Detectors
{
    static readonly Regex SystemLeak = new(@"(?i)(system prompt|developer message|you are an ai assistant)");
    static readonly Regex Pii = new(@"(\b\d{3}-\d{2}-\d{4}\b)|(\bAKIA[0-9A-Z]{16}\b)|(\bsk-[0-9A-Za-z]{20,}\b)");
    public static bool LeaksSystem(string t) => SystemLeak.IsMatch(t);
    public static bool ContainsPIIorKeys(string t) => Pii.IsMatch(t);
    public static bool TooLong(string t, int chars = 4000) => t.Length > chars;
}