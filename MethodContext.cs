namespace SmalltalkVM;

public class MethodContext : IContext
{
    // Caller is the MethodContext or BlockContext from which this method was called
    public IContext Caller { get; set; }
    public int InstructionPointer { get; set; }
    public int StackPointer { get; set; }
    public CompiledMethod Method;
    public IObject Receiver;
    public IObject[] Arguments;
    public IObject[] Temporaries;
    public IObject[] Stack { get; set; }

}