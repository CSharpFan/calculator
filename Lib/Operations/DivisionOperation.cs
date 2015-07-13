namespace Lib.Operations
{
    internal class DivisionOperation : BaseOperation
    {
        public override int Priority
        {
            get { return 1; }
        }        public const char Op = '/';

        public override Operand Execute(Operand left, Operand right)
        {
            return left / right;
        }
    }
}