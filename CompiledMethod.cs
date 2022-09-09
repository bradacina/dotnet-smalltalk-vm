namespace SmalltalkVM;

public record CompiledMethod
{
    public ushort Header;

    // we don't store HeaderExtension in the LiteralFrame, instead we keep it here
    public ushort HeaderExtension;

    // we don't store Association in the LiteralFrame, instead we keep it here
    public SClass? Association;
    public IObject[]? LiteralFrame;
    public byte[] ByteCodes = null!;

    public CompiledMethod() { }

    public int GetLiteralCount()
    {
        return (Header >> 1) & 63;
    }

    public bool IsLargeContext()
    {
        return ((Header >> 7) & 1) == 1;
    }

    public int GetTemporaryCount()
    {
        return (Header >> 8) & 31;
    }

    public byte GetFlagValue()
    {
        return (byte)((Header >> 13) & 7);
    }

    public ushort GetArgumentCount()
    {
        var flagValue = GetFlagValue();
        if (flagValue < 5)
        {
            return flagValue;
        }

        if (flagValue < 7)
        {
            return 0;
        }

        return (ushort)((HeaderExtension >> 9) & 31);
    }

    public ushort GetPrimitiveIndex()
    {
        var flagValue = GetFlagValue();

        if (flagValue != 7)
        {
            return 0;
        }

        return (ushort)((Header >> 1) & 255);
    }
}