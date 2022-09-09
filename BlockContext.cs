namespace SmalltalkVM;

public class BlockContext : IContext
{
    // Caller is the context to return to when the block has finished execution
    public IContext Caller { get; set; }
    public int InstructionPointer { get; set; }
    public int StackPointer { get; set; }
    public byte ArgumentCount;
    public int InitialInstructionPointer;
    public MethodContext Home;
    public IObject[] Stack { get; set; }
}