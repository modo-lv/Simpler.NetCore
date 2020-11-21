﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
    private readonly IList<T> _value = new List<T>(1);

    /// <summary>
    /// Create a Maybe with a given value.
    /// </summary>
    /// <param name="value"></param>
    public Maybe(T value) {
      this.Set(value);
    }

    /// <summary>
    /// Create a Maybe without a value.
    /// </summary>
    public Maybe() { }

    /// <summary>
    /// Retrieve the value, if present. Will throw if empty!
    /// </summary>
    public T Get => this._value.FirstOrDefault() ??
                    throw new NullReferenceException(
                      $"{this.GetType().Name}<{this.GetType().GenericTypeArguments.First().Name}> has no value."
                    );

    /// <summary>
    /// Set the new value of this Maybe.
    /// </summary>
    public Maybe<T> Set(T value) {
      this._value.Clear();
      if (value != null)
        this._value.Add(value);
      return this;
    }

    /// <summary>
    /// Return value if this Maybe has a value, or a provided fallback value if it doesn't.
    /// </summary>
    /// <param name="fallback">Fallback value to return if this Maybe has no value.</param>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    public T GetOr<TOut>(TOut fallback) where TOut : notnull, T {
      return this.FirstOrDefault() ?? fallback;
    }

    /// <summary>
    /// Map this Maybe value to another.
    /// </summary>
    /// <param name="transformer"></param>
    /// <typeparam name="TOut"></typeparam>
    /// <returns></returns>
    public Maybe<TOut> Map<TOut>(Func<T, TOut> transformer) where TOut : notnull {
      return this._value.Select(_ => May.Be(transformer(_))).FirstOrDefault() ?? May.BeNot<TOut>();
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator() => (IEnumerator<T>) this._value.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }


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
    public static Maybe<T> Be<T>(T value) where T : notnull {
      return new Maybe<T>(value);
    }

    /// <summary>
    /// Fluent alternative for creating a Maybe without value.
    /// </summary>
    public static Maybe<T> BeNot<T>() where T : notnull {
      return new Maybe<T>();
    }
  }
}