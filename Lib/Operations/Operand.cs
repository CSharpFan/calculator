namespace Lib.Operations
{
    public class Operand : IMathPiece
    {
        public decimal Op { get; private set; }

        public Operand(decimal op)
        {
            this.Op = op;
        }

        public static implicit operator decimal(Operand operand)
        {
            return operand.Op;
        }

        public static implicit operator Operand(decimal d)
        {
            return new Operand(d);
        }
    }
}