using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    internal class Polynomial
    {
        private Dictionary<int, double> _expression;
        public Dictionary<int, double> Expression
        {
            get { return _expression; }
        }
        private int _degree;
        public int Degree
        {
            get { return _degree; }
        }

        /// <summary>
        /// Default constructor - create a zero polynomial
        /// </summary>
        public Polynomial()
        {
            InitializeZeroDegree();
        }

        /// <summary>
        /// Create a polynomial of the specified degree
        /// </summary>
        /// <param name="degree">The degree of tergeted polynomial</param>
        /// <param name="randomCoeffs">Random coefficents in (0.0; 1.0) range if true, all coefficents</param>
        public Polynomial(int degree, bool randomCoeffs = false)
        {
            if (degree < 0) { InitializeZeroDegree(); }
            else
            {
                var rGen = new Random();
                var keys = Enumerable.Range(0, degree + 1);
                var tmpExpr = keys.ToDictionary(key => key, value => randomCoeffs ? rGen.NextDouble() : 1);
                Initialize(tmpExpr, degree);
            }
        }

        /// <summary>
        /// Create a polynom with specified coefficients 
        /// (trailing zeroes are removed from the end of array;
        /// the degree of polynom is defined by the last non-zero coefficient; 
        /// </summary>
        /// <param name="coefficients"></param>
        public Polynomial(IList<double> coefficients)
        {
            Initialize(coefficients);
        }

        /// <summary>
        /// Create a polynom from collection 
        /// of term/coefficient pairs
        /// </summary>
        /// <param name="copyFrom"></param>
        public Polynomial(Dictionary<int, double> copyFrom)
        {
            Initialize(copyFrom, copyFrom.Count);
        }

        public bool IsZeroDegree()
        {
            return ((-1 == _degree) && (!_expression.Any()));
        }

        public static Polynomial operator +(Polynomial Pol1, Polynomial Pol2)
        {
            if ((null == Pol1) || (null == Pol2)) return null;

            if (Pol1.IsZeroDegree()) return Pol2;
            if (Pol2.IsZeroDegree()) return Pol1;

            var resExpr = new Dictionary<int, double>();

            
            foreach (var key in Pol1.Expression.Keys.Except(Pol2.Expression.Keys).Union(Pol2.Expression.Keys.Except(Pol1.Expression.Keys)))
            {
                double value;
                if (!Pol1.Expression.TryGetValue(key, out value)) Pol2.Expression.TryGetValue(key, out value);
                resExpr.Add(key, value);
            }

            foreach (var key in Pol1.Expression.Keys.Intersect(Pol2.Expression.Keys))
            {
                double valuePol1;
                double valuePol2;
                Pol1.Expression.TryGetValue(key, out valuePol1);
                Pol2.Expression.TryGetValue(key, out valuePol2);
                resExpr.Add(key, valuePol1 + valuePol2);
            }

            return new Polynomial(resExpr);
        }

        public static Polynomial operator +(Polynomial Pol1, double additive)
        {
            if (null == Pol1) return null;
            if (additive < double.Epsilon) return Pol1;
            if (Pol1.IsZeroDegree()) return new Polynomial(new[] { additive });

            var tmpExpr = Pol1.Expression.Select(x => x).ToDictionary(x => x.Key, x => x.Value + additive);
            var Pol1Copy = new Polynomial(tmpExpr);
            Pol1Copy.RemoveZeroMembers();
            return Pol1Copy;
        }

        public static Polynomial operator +(double additive, Polynomial Pol2)
        {
            return Pol2 + additive;
        }

        public static Polynomial operator *(Polynomial Pol1, Polynomial Pol2)
        {
            if ((null == Pol1) || (null == Pol2)) return null;
            if ((Pol1.IsZeroDegree()) || (Pol2.IsZeroDegree())) return new Polynomial();

            var tmpExpr = new Dictionary<int, double>();

            foreach (var key1 in Pol1.Expression.Keys)
            {
                //multiplying Pol1 by each term of Pol2
                var tmpMult = Pol2.Expression.Select(x => x)
                    .ToDictionary(x => x.Key + key1, x => x.Value * Pol1.Expression[key1]);

                //adding the above to the resulting polynomial 
                tmpExpr = tmpExpr.Zip(tmpMult,
                    (first, second) =>
                        tmpExpr.ContainsKey(second.Key)
                            ? (new KeyValuePair<int, double>(first.Key, first.Value + second.Value))
                            : second).ToDictionary(x => x.Key, x => x.Value);
            }

            var resPol = new Polynomial(tmpExpr);
            resPol.RemoveZeroMembers();
            return resPol;
        }

        public static Polynomial operator *(double multiplier, Polynomial Pol2)
        {
            if (null == Pol2) return null;
            if ((multiplier < double.Epsilon) || (Pol2.IsZeroDegree())) return new Polynomial();

            var tmpExpr = Pol2.Expression.Select(x => x).ToDictionary(x => x.Key, x => x.Value * multiplier);
            var Pol2Copy = new Polynomial(tmpExpr);
            Pol2Copy.RemoveZeroMembers();

            return Pol2Copy;
        }

        public static Polynomial operator *(Polynomial Pol1, double multiplier)
        {
            return multiplier * Pol1;
        }

        public static bool operator ==(Polynomial Pol1, Polynomial Pol2)
        {
            return CheckIfEqual(Pol1, Pol2);
        }

        public static bool operator !=(Polynomial Pol1, Polynomial Pol2)
        {
            return !CheckIfEqual(Pol1, Pol2);
        }

        public override bool Equals(object obj)
        {
            return CheckIfEqual(this, obj as Polynomial);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hash = 0;
                if (_expression != null)
                {
                    hash = _expression.Keys.Aggregate(17, (current, coeff) => current * 23 + coeff.GetHashCode());
                }
                return (hash * 397) ^ _degree;
            }
        }

        public override string ToString()
        {
            var terms = _expression.Select(x => x.Value + "x^" + x.Key).ToArray();

            return string.Join(" + ", terms);
        }

        private void Initialize(Dictionary<int, double> polynomToBe, int degree)
        {
            if (null == polynomToBe) InitializeZeroDegree(); //throw new ArgumentNullException("polynomToBe");
            else
            {
                _degree = degree;
                _expression = new Dictionary<int, double>(polynomToBe);
                RemoveZeroMembers();
            }
        }

        private void Initialize(IList<double> coeffs)
        {
            if ((0 == coeffs.Count) || (coeffs.All(coeff => coeff <= double.Epsilon))) InitializeZeroDegree();
            else
            {
                while (coeffs.Last() <= double.Epsilon) { coeffs.RemoveAt(coeffs.Count - 1); }
                _expression = coeffs.ToDictionary(coeffs.IndexOf, value => value);
                _degree = coeffs.Count - 1;
                RemoveZeroMembers();
            }
        }

        private void InitializeZeroDegree()
        {
            Initialize(new Dictionary<int, double> { { -1, 0 } }, -1);
        }

        private void RemoveZeroMembers()
        {
            _expression = _expression.Where(x => x.Value >= double.Epsilon).ToDictionary(x => x.Key, x => x.Value);
            _degree = _expression.Count - 1;
        }

        private static bool CheckIfEqual(Polynomial Pol1, Polynomial Pol2)
        {
            if (ReferenceEquals(null, Pol2)) return false;
            if (ReferenceEquals(Pol1, Pol2)) return true;
            if (Pol1.GetType() != Pol2.GetType()) return false;

            if (Pol1.Degree != Pol2.Degree) return false;

            if (Pol1.Expression.Keys.Except(Pol2.Expression.Keys).Any()) return false;
            var difference = from val1 in Pol1.Expression.Values
                             from val2 in Pol2.Expression.Values
                             where val1 - val2 >= double.Epsilon
                             select val1;
            return (!difference.Any());
        }
    }
}
