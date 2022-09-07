namespace SmalltalkVM;

public record CompiledMethod
{
    public ushort Header;
    public object[]? LiteralFrame;
    public byte[] ByteCodes = null!;

    public CompiledMethod() { }

    public int GetLiteralCount()
    {
        return (Header >> 1) & 64;
    }

    public bool IsLargeContext()
    {
        return ((Header >> 7) & 1) == 1;
    }

    public int GetTemporaryCount()
    {
        return (Header >> 8) & 32;
    }

    public int GetFlagValue()
    {
        return (Header >> 13) & 8;
    }
}