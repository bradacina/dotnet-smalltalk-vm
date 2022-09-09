namespace SmalltalkVM;

public partial class Memory
{
    private static Memory? _instance;
    public static Memory Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }

            _instance = new Memory();
            return _instance;
        }
    }

    private Memory()
    {
        Initialize();
    }

    public object FetchPointerOfObject(int index, SObject obj)
    {
        return obj.InstanceVariables[index];
    }

    public void StorePointerOfObjectWithValue(int index, SObject obj, object value)
    {
        obj.InstanceVariables[index] = value;
    }

    public short FetchWordOfObject(int index, SObject obj)
    {
        return obj.ShortValues[index];
    }

    public void StoreWordOfObjectWithValue(int index, SObject obj, short value)
    {
        obj.ShortValues[index] = value;
    }

    public byte FetchByteOfObject(int index, SObject obj)
    {
        return obj.ByteValues[index];
    }

    public void StoreByteOfObjectWithIndex(int index, SObject obj, byte value)
    {
        obj.ByteValues[index] = value;
    }

    public void IncreaseReferencesTo(SObject obj) { }

    public void DecreaseReferencesTo(SObject obj) { }

    public SClass FetchClassOf(SObject obj)
    {
        return obj.Class;
    }

    public int FetchWordLengthOf(SObject obj)
    {
        // todo: do we count the Class as a field as well?

        throw new NotImplementedException();
    }

    public int FetchByteLengthOf(SObject obj)
    {
        // todo: do we count the Class as a field as well?
        throw new NotImplementedException();
    }

    public SObject InstantiateClassWithPointers(SClass cls, int instanceSize) =>
        SObject.NewSObjectWithPointers(cls, instanceSize);

    public SObject InstantiateClassWithWords(SClass cls, int instanceSize) =>
        SObject.NewSObjectWithWords(cls, instanceSize);

    public SObject InstantiateClassWithBytes(SClass cls, int instanceSize) =>
        SObject.NewSObjectWithBytes(cls, instanceSize);

    public SObject InitialInstanceOf(SClass cls)
    {
        // return the first instantiated SObject of class cls
        throw new NotImplementedException();
    }

    public SObject InstanceAfter(SObject obj)
    {
        // return the next instantiated SObject that is of the same SClass as obj
        throw new NotImplementedException();
    }

    public void SwapPointers(SObject obj1, SObject obj2)
    {
        (obj1, obj2) = (obj2, obj1);
    }

    public bool IsIntegerValue(int value)
    {
        return value >= -16384 && value <= 16383;
    }

    public bool IsIntegerObject(object obj)
    {
        return obj is SmallInteger;
    }

    public SmallInteger IntegerObjectOf(short value)
    {
        // todo: optimize to return an already existing instance of SmallInteger
        return new SmallInteger { Value = value };
    }

    public short IntegerValueOf(SmallInteger value)
    {
        return value.Value;
    }

    public short FetchIntegerOfObject(int index, SObject obj)
    {
        if (index < 0 || obj.ShortValues == null || obj.ShortValues.Length <= index)
        {
            return PrimitiveFail();
        }

        return obj.ShortValues[index];
    }

    public void StoreIntegerOfObjectWithIndex(int index, SObject obj, short value)
    {
        if (index < 0 || obj.ShortValues == null || obj.ShortValues.Length <= index)
        {
            return; //todo:  PrimitiveFail();
        }

        obj.ShortValues[index] = value;
    }

    public void TransferFromIndexOfObjectToIndexOfObject(int count, int fromIndex, SObject source,
        int toIndex, SObject destination)
    {
        // todo: move count items from source[fromIndex] to destination[toIndex]
        throw new NotImplementedException();
    }

    public byte HighByteOf(short value)
    {
        return (byte)(value >> 8);
    }

    public byte LowByteOf(short value)
    {
        return (byte)(value & 0xFF);
    }

    public short ExtractBits(int firstBitIndex, int lastBitIndex, short value)
    {
        var mask = 2 ^ (lastBitIndex - firstBitIndex + 1) - 1;
        value = (short)(value >> (15 - lastBitIndex));
        return (short)(value & mask);
    }

    public ushort HeaderOf(CompiledMethod method)
    {
        return method.Header;
    }

    public IObject? LiteralOfMethod(int index, CompiledMethod method)
    {
        return method.LiteralFrame![index];
    }

    public int TemporaryCountOf(CompiledMethod method)
        => method.GetTemporaryCount();


    public bool LargeContextFlagOf(CompiledMethod method) =>
        method.IsLargeContext();

    public int LiteralCountOf(CompiledMethod method) =>
        method.GetLiteralCount();

    public int ObjectPointerCountOf(CompiledMethod method)
    {
        // todo: return the count of literals + 1 (for the header, which counts as a pointer)
        throw new NotImplementedException();
    }

    // if value is 0-4 - the method takes 4 parameters
    // if value is 5 - the method returns self
    // if value is 6 - the method returns an instance variable (the index of the instance 
    // variable to return resides in Header > Temporary Variables Count )
    // if value is 7 - there is a header extension on the second last entry in the LiteralFrame
    public byte FlagValueOf(CompiledMethod method) =>
        method.GetFlagValue();

    public int FieldIndexOf(CompiledMethod method) =>
        method.GetTemporaryCount();

    public ushort HeaderExtensionOf(CompiledMethod method) =>
        method.HeaderExtension;

    public ushort ArgumentCountOf(CompiledMethod method) =>
        method.GetArgumentCount();

    public ushort PrimitiveIndexOf(CompiledMethod method) =>
        method.GetPrimitiveIndex();

    public SClass? MethodClassOf(CompiledMethod method) =>
        method.Association;

    public short PrimitiveFail()
    {
        // todo: implement
        throw new NotImplementedException();
    }
}