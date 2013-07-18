using System;

namespace Eluant
{
    public abstract class LuaValue : IDisposable
    {
        internal LuaValue() { }

        // Don't implement the full disposable pattern, but require sub-types to implement it.  For example, this allows
        // LuaValueType types to completely omit the finalizer with a no-op Dispose() implementation.
        public abstract void Dispose();

        public LuaValue CopyReference()
        {
            return CopyReferenceImpl();
        }

        protected abstract LuaValue CopyReferenceImpl();

        public virtual LuaValue this[LuaValue key]
        {
            get { throw new NotSupportedException("Type does not support indexing."); }
            set { throw new NotSupportedException("Type does not support indexing."); }
        }

        public virtual double? ToNumber()
        {
            throw new NotSupportedException("Type cannot be converted to a number.");
        }

        // All types should be able to implement this.
        public abstract bool ToBoolean();

        public abstract override string ToString();

        internal abstract void Push(LuaRuntime runtime);

        public static implicit operator LuaValue(bool v)
        {
            return (LuaBoolean)v;
        }

        public static implicit operator LuaValue(string v)
        {
            return (LuaString)v;
        }

        public static implicit operator LuaValue(double? n)
        {
            return (LuaNumber)n;
        }
    }
}
