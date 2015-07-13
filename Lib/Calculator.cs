namespace Lib
{
    using System.Collections.Generic;
    using System.Linq;
    using Operations;

    public class Calculator
    {
        private readonly string _math;

        public Calculator(string math)
        {
            this._math = math;
        }

        /// <summary>
        ///     Calculates the result
        /// </summary>
        /// <returns></returns>
        public decimal Calculate()
        {
            // clean up the input 
            var cleanedUpMath = this._math.Replace(" ", "");

            // parse the string into chunks we can process
            // each part has a left, a right and a body (which is +-/*)

            char[] operations = { DivisionOperation.Op, MultiplyOperation.Op, PlusOperation.Op, MinusOperation.Op };

            List<IMathPiece> parsed = new List<IMathPiece>();


            var startOfCurrentChunk = 0;

            for (var x = 0; x < cleanedUpMath.Length; x++)
            {
                if (operations.Contains(cleanedUpMath[x]))
                {
                    // write the previous chunk to parsed, which is by definition an Operand
                    parsed.Add(new Operand(decimal.Parse(cleanedUpMath.Substring(startOfCurrentChunk, x - startOfCurrentChunk))));

                    // parse current field to operation
                    switch (cleanedUpMath[x])
                    {
                        case DivisionOperation.Op:
                        {
                            parsed.Add(new DivisionOperation());
                            break;
                        }
                        case MultiplyOperation.Op:
                        {
                            parsed.Add(new MultiplyOperation());
                            break;
                        }
                        case MinusOperation.Op:
                        {
                            parsed.Add(new MinusOperation());
                            break;
                        }
                        case PlusOperation.Op:
                        {
                            parsed.Add(new PlusOperation());
                            break;
                        }
                    }

                    startOfCurrentChunk = x + 1;
                }

                if (x == cleanedUpMath.Length - 1)
                {
                    // we'll never hit a last operation, let's take this chunk and push it too
                    parsed.Add(new Operand(decimal.Parse(cleanedUpMath.Substring(startOfCurrentChunk, x - startOfCurrentChunk + 1))));
                }
            }

            // start at prio 1 (highest)
            return Solve(parsed, 1);
        }

        private static decimal Solve(IList<IMathPiece> parsed, int prio)
        {
            while (parsed.OfType<BaseOperation>().Count() != 0)
            {
                // keep looping until we've solved each piece
                // find pieces with prio 1
                for (var index = 0; index < parsed.Count; index++)
                {
                    var mathPiece = parsed[index];

                    var operation = mathPiece as BaseOperation;

                    if (null == operation || operation.Priority != prio)
                    {
                        continue;
                    }

                    // execute the operation
                    // take the one on the left
                    var left = (Operand) parsed[index - 1];
                    var right = (Operand) parsed[index + 1];

                    parsed[index] = operation.Execute(left, right);

                    parsed[index - 1] = null;
                    parsed[index + 1] = null;

                    // clean up the nulls
                    return Solve(parsed.Where(e => null != e).ToList(), prio);
                }

                // none with the prio we want? ok, continue
                return Solve(parsed, prio + 1);
            }


            return ((Operand) parsed[0]).Op;
        }
    }
}