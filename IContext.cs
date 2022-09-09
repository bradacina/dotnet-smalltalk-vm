namespace SmalltalkVM;

public interface IContext
{
    public IContext Caller { get; set; }
    public int InstructionPointer { get; set; }
    public int StackPointer { get; set; }

    public IObject[] Stack { get; set; }
}