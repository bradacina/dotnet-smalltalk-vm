namespace SmalltalkVM;

public class Interpreter
{
    private static Interpreter? _instance;
    public static Interpreter Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            _instance = new Interpreter();
            return _instance;
        }
    }
    private Interpreter() { }

    public IContext ActiveContext = null!;
    public MethodContext HomeContext;

    public CompiledMethod Method;
    public IObject Receiver;
    public int InstructionPointer;
    public int StackPointer;

    // These are class related register that the interpreter keeps
    // to cache the state of the message lookup process
    // public string MessageSelector;
    // public int ArgumentCount;
    // public CompiledMethod NewMethod;
    // public byte PrimitiveIndex;

    public bool IsBlockContext =>
        ActiveContext is BlockContext;

    public void FetchContextRegisters()
    {
        if (IsBlockContext)
        {
            HomeContext = (ActiveContext as BlockContext).Home;
        }

        Receiver = HomeContext.Receiver;
        Method = HomeContext.Method;
        InstructionPointer = ActiveContext.InstructionPointer;
        StackPointer = ActiveContext.StackPointer;
    }

    public void StoreContextRegisters()
    {
        ActiveContext.InstructionPointer = InstructionPointer;
        ActiveContext.StackPointer = StackPointer;
    }

    public void Push(IObject obj)
    {
        StackPointer += 1;
        ActiveContext.Stack[StackPointer] = obj;
    }

    public IObject PopStack()
    {
        var top = ActiveContext.Stack[StackPointer];
        StackPointer -= 1;
        return top;
    }

    public IObject StackTop() =>
        ActiveContext.Stack[StackPointer];

    public IObject StackValueAtOffset(int offset) =>
        ActiveContext.Stack[StackPointer - offset];

    public void Pop(int howMany)
    {
        StackPointer -= howMany;
    }

    public void UnPop(int howMany)
    {
        StackPointer += howMany;
    }

    public IContext Sender() => HomeContext.Caller;

    public IContext Caller() => ActiveContext.Caller;

    public IObject Temporary(int offset) => HomeContext.Temporaries[offset];
    public IObject Literal(int offset) => Method.LiteralFrame[offset];

    public CompiledMethod LookupMethodInClass(SClass cls, string selector)
    {
        // find method in current class, or walk up the super hierarchy
        var currentClass = cls;

        while (currentClass != null)
        {
            if (cls.Methods.ContainsKey(selector))
            {
                return cls.Methods[selector];
            }

            currentClass = cls.Super;
        }

        // if we didn't find the selector, we look for DoesNotUnderstand selector instead
        return LookupMethodInClass(cls, "DoesNotUnderstand");
    }
}