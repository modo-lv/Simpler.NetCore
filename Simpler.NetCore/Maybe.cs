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
  public class Maybe<T> : IEnumerable<T> {
    /// <summary>
    /// Underlying list holding the value (if non-null). 
    /// </summary>
    private readonly IList<T> _value = new List<T>(1);

    /// <summary>
    /// Create a Maybe with a given value.
    /// </summary>
    /// <param name="value"></param>
    public Maybe(T value) {
      this.Value = value;
    }

    /// <summary>
    /// Retrieve the value, if present. Will throw if empty!
    /// </summary>
    public T Value {
      get => this._value.FirstOrDefault() ??
             throw new NullReferenceException(
               $"{this.GetType().Name}<{this.GetType().GenericTypeArguments.First().Name}> has no value."
             );
      set {
        this._value.Clear();
        if (value != null)
          this._value.Add(value);
      }
    }

    /// <summary>
    /// Return value if this Maybe has a value, or a provided fallback value if it doesn't.
    /// </summary>
    /// <param name="fallback">Fallback value to return if this Maybe has no value.</param>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    public T ValueOr<TOut>(TOut fallback) where TOut : notnull, T {
      return this.FirstOrDefault() ?? fallback;
    }

    /// <inheritdoc cref="IEnumerable{T}.GetEnumerator"/>
    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this._value.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }
  }

  /// <summary>
  /// Static class for more fluent usage of Maybe values.
  /// </summary>
  public static class May {
    /// <summary>
    /// Create a Maybe with a given value.
    /// </summary>
    public static Maybe<T> Be<T>(T value) {
      return new Maybe<T>(value);
    }
  }
}
