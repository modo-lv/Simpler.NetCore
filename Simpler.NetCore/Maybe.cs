using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Simpler.NetCore {
  /// <summary>
  /// A thin wrapper turning a nullable value into a single-element collection. 
  /// </summary>
  /// <remarks>
  /// This allows to operate with null/non-null values the same as with empty/non-empty collections.
  /// </remarks>
  /// <typeparam name="T">Value type.</typeparam>
  public class Maybe<T> : IEnumerable<T> where T : notnull {
    /// <summary>
    /// Underlying list holding the value (if non-null). 
    /// </summary>
    private readonly IList<T> _value = new List<T>(capacity: 1);

    /// <summary>
    /// Create a Maybe with a given value.
    /// </summary>
    /// <param name="value"></param>
    public Maybe(T? value) {
      this.Set(value);
    }

    /// <summary>
    /// Create a Maybe without a value.
    /// </summary>
    public Maybe() { }

    /// <summary>
    /// Retrieve the value, if present. Will throw if empty!
    /// </summary>
    public T Get => this._value.Any()
      ? this._value.First()
      : throw new NullReferenceException(
        $"{this.GetType().Name}<{this.GetType().GenericTypeArguments.First().Name}> has no value."
      );

    /// <summary>
    /// Set the new value of this Maybe.
    /// </summary>
    public Maybe<T> Set(T? value) {
      this._value.Clear();
      if (value != null)
        this._value.Add(value);
      return this;
    }

    /// <summary>
    /// Set the value of this Maybe only if it doesn't have one already.
    /// </summary>
    public Maybe<T> SetIfEmpty(T? value) {
      if (this.IsEmpty)
        this.Set(value);
      return this;
    }

    /// <summary>
    /// Syntactic sugar for checking if this Maybe has no value.
    /// </summary>
    public Boolean IsEmpty => !this._value.Any();

    /// <summary>
    /// Syntactic sugar for checking if this Maybe has a value.
    /// </summary>
    public Boolean NotEmpty => !this.IsEmpty;

    /// <summary>
    /// Return value if this Maybe has a value, or a provided fallback value if it doesn't.
    /// </summary>
    /// <param name="fallback">Fallback value to return if this Maybe has no value.</param>
    /// <typeparam name="TOut">Return value type.</typeparam>
    /// <returns></returns>
    public T GetOr<TOut>(TOut fallback) where TOut : T {
      return this.FirstOrDefault() ?? fallback;
    }

    /// <summary>
    /// Map this Maybe-value to another.
    /// </summary>
    /// <param name="transformer">Function to use on the value of Maybe.</param>
    /// <typeparam name="TOut">Type of the resulting Maybe.</typeparam>
    /// <returns>The new maybe-value.</returns>
    public Maybe<TOut> Map<TOut>(Func<T, TOut> transformer) where TOut : notnull =>
      this.MapGetOr(_ => May.Be(transformer(_)), May.BeNot<TOut>());

    /// <summary>
    /// Map (transform) the value of this Maybe if it has one, or to a fallback value if not.
    /// </summary>
    /// <param name="transformer">Function to use to map the current value to a new one.</param>
    /// <param name="fallback">Fallback value to return if maybe has no value.</param>
    /// <typeparam name="TOut">The type of the resulting value.</typeparam>
    /// <returns>Mapping result or fallback.</returns>
    public TOut MapGetOr<TOut>(Func<T, TOut> transformer, TOut fallback) =>
      this._value.Select(transformer).FirstOrDefault() ?? fallback;

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() => this._value.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();


    /// <inheritdoc />
    public override Boolean Equals(Object obj) {
      if (obj is Maybe<T> maybe) {
        return Object.Equals(this._value.FirstOrDefault(), maybe._value.FirstOrDefault());
      }

      return false;
    }

    /// <inheritdoc />
    public override Int32 GetHashCode() =>
      this.GetType().GetHashCode() + (this._value.FirstOrDefault()?.GetHashCode() ?? 0);

    /// <inheritdoc />
    public override String ToString() {
      return $"{this.GetType().Name}({this.Map(_ => _.ToString()).GetOr("")})";
    }
  }

  /// <summary>
  /// Static class for more fluent usage of Maybe values.
  /// </summary>
  public static class May {
    /// <summary>
    /// Create a Maybe with a given value.
    /// </summary>
    public static Maybe<T> Be<T>(T? value) where T : notnull {
      return new(value);
    }

    /// <summary>
    /// Fluent alternative for creating a Maybe without value.
    /// </summary>
    public static Maybe<T> BeNot<T>() where T : notnull {
      return new();
    }
  }
}