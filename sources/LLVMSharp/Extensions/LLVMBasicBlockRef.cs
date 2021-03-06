using System;

namespace LLVMSharp
{
    public unsafe partial struct LLVMBasicBlockRef : IEquatable<LLVMBasicBlockRef>
    {
        public LLVMValueRef FirstInstruction => (Pointer != IntPtr.Zero) ? LLVM.GetFirstInstruction(this) : default;

        public LLVMValueRef LastInstruction => (Pointer != IntPtr.Zero) ? LLVM.GetLastInstruction(this) : default;

        public LLVMBasicBlockRef Next => (Pointer != IntPtr.Zero) ? LLVM.GetNextBasicBlock(this) : default;

        public LLVMValueRef Parent => (Pointer != IntPtr.Zero) ? LLVM.GetBasicBlockParent(this) : default;

        public LLVMBasicBlockRef Previous => (Pointer != IntPtr.Zero) ? LLVM.GetPreviousBasicBlock(this) : default;

        public LLVMValueRef Terminator => (Pointer != IntPtr.Zero) ? LLVM.GetBasicBlockTerminator(this) : default;

        public static bool operator ==(LLVMBasicBlockRef left, LLVMBasicBlockRef right) => left.Pointer == right.Pointer;

        public static bool operator !=(LLVMBasicBlockRef left, LLVMBasicBlockRef right) => !(left == right);

        public override bool Equals(object obj) => obj is LLVMBasicBlockRef other && Equals(other);

        public bool Equals(LLVMBasicBlockRef other) => Pointer == other.Pointer;

        public override int GetHashCode() => Pointer.GetHashCode();

        public LLVMValueRef AsValue() => LLVM.BasicBlockAsValue(this);

        public void Delete() => LLVM.DeleteBasicBlock(this);

        public void Dump() => LLVM.DumpValue(this);

        public LLVMBasicBlockRef InsertBasicBlock(string Name)
        {
            using (var marshaledName = new MarshaledString(Name))
            {
                return LLVM.InsertBasicBlock(this, marshaledName);
            }
        }

        public void MoveAfter(LLVMBasicBlockRef MovePos) => LLVM.MoveBasicBlockAfter(this, MovePos);

        public void MoveBefore(LLVMBasicBlockRef MovePos) => LLVM.MoveBasicBlockBefore(this, MovePos);

        public string PrintToString()
        {
            var pStr = LLVM.PrintValueToString(this);

            if (pStr is null)
            {
                return string.Empty;
            }
            var span = new ReadOnlySpan<byte>(pStr, int.MaxValue);

            var result = span.Slice(0, span.IndexOf((byte)'\0')).AsString();
            LLVM.DisposeMessage(pStr);
            return result;
        }

        public void RemoveFromParent() => LLVM.RemoveBasicBlockFromParent(this);

        public override string ToString() => (Pointer != IntPtr.Zero) ? PrintToString() : string.Empty;
    }
}
