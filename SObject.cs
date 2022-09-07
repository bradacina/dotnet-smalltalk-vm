namespace SmalltalkVM;

public class SObject
{
    public SClass Class;

    // how to store instance variables from all super classes
    // for fast lookup
    public object[] InstanceVariables;
}