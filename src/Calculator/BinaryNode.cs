using System;
using System.Collections.Generic;

namespace Calculator
{
    internal class BinaryNode<T> : IEquatable<BinaryNode<T>>
    {
        public BinaryNode(T value) : this(value, null, null)
        { }

        public BinaryNode(T value, BinaryNode<T> left, BinaryNode<T> right)
        {
            Value = value;
            Left = left;
            Right = right;
        }

        public T Value;

        public BinaryNode<T> Left { get; }
        public BinaryNode<T> Right { get; }

        public override bool Equals(object obj)
        {
            return Equals(obj as BinaryNode<T>);
        }

        public bool Equals(BinaryNode<T> other)
        {
            return other != null &&
                   EqualityComparer<T>.Default.Equals(Value, other.Value) &&
                   EqualityComparer<BinaryNode<T>>.Default.Equals(Left, other.Left) &&
                   EqualityComparer<BinaryNode<T>>.Default.Equals(Right, other.Right);
        }

        public override int GetHashCode()
        {
            var hashCode = -533118049;
            hashCode = hashCode * -1521134295 + EqualityComparer<T>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + EqualityComparer<BinaryNode<T>>.Default.GetHashCode(Left);
            hashCode = hashCode * -1521134295 + EqualityComparer<BinaryNode<T>>.Default.GetHashCode(Right);
            return hashCode;
        }

        public static bool operator ==(BinaryNode<T> node1, BinaryNode<T> node2)
        {
            return EqualityComparer<BinaryNode<T>>.Default.Equals(node1, node2);
        }

        public static bool operator !=(BinaryNode<T> node1, BinaryNode<T> node2)
        {
            return !(node1 == node2);
        }
    }
}