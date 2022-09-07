namespace SmalltalkVM;

public class BlockContext
{
    public MethodContext Caller;
    public int InstructionPointer;
    public int StackPointer;
    public byte ArgumentCount;
    public int InitialInstructionPointer;
    public CompiledMethod Home;
    public object[] Stack;
}