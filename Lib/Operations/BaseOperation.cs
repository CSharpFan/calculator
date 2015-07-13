namespace Lib.Operations
{
    public abstract class BaseOperation : IMathPiece
    {
        public abstract int Priority { get; }
        public abstract Operand Execute(Operand left, Operand right);
    }
}