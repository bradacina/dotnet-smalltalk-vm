namespace SmalltalkVM;

public class MethodContext
{
    public object Sender;
    public int InstructionPointer;
    public int StackPointer;
    public CompiledMethod Method;
    public SObject Receiver;
    public object[] Arguments;
    public object[] Temporaries;
    public object[] Stack;

}