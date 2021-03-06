using System;

namespace LLVMSharp
{
    public unsafe partial struct LLVMMetadataRef
    {
        public LLVMMetadataRef(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer;

        public static implicit operator LLVMMetadataRef(LLVMOpaqueMetadata* value)
        {
            return new LLVMMetadataRef((IntPtr)value);
        }

        public static implicit operator LLVMOpaqueMetadata*(LLVMMetadataRef value)
        {
            return (LLVMOpaqueMetadata*)value.Pointer;
        }
    }
}
