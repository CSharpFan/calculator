namespace Lib.Operations
{
    internal class MinusOperation : BaseOperation
    {
        public override int Priority
        {
            get { return 2; }
        }

        public const char Op = '-';

        public override Operand Execute(Operand left, Operand right)
        {
            return left - right;
        }
    }
}