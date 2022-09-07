namespace SmalltalkVM;

public class SClass
{
    public Dictionary<string, CompiledMethod> Methods = new Dictionary<string, CompiledMethod>();
    public List<string> InstanceVarNames = new List<string>();
    public SClass? Super;
}