namespace SmalltalkVM;

public class SObject : IObject
{
    public SClass Class;

    // how to store instance variables from all super classes
    // for fast lookup
    public object[] InstanceVariables;

    public short[] ShortValues;
    public byte[] ByteValues;

    public static SObject NewSObjectWithPointers(SClass cls, int instanceSize) =>
        new SObject
        {
            Class = cls,
            InstanceVariables = new object[instanceSize]
        };

    public static SObject NewSObjectWithWords(SClass cls, int instanceSize) =>
         new SObject
         {
             Class = cls,
             ShortValues = new short[instanceSize]
         };

    public static SObject NewSObjectWithBytes(SClass cls, int instanceSize) =>
        new SObject
        {
            Class = cls,
            ByteValues = new byte[instanceSize]
        };
}