using System;

namespace Simpler.NetCore {
  /// <summary>
  /// Gneral-purpose extension methods for simplifying common tasks.
  /// </summary>
  public static class SimplerExtensions {
    /// <summary>
    /// Run an action on an object if it's not <c>null</c>
    /// </summary>
    /// <param name="obj">Object to check.</param>
    /// <param name="action">Action to run.</param>
    /// <typeparam name="T">Object type.</typeparam>
    /// <returns><c>true</c> if the object was not <c>null</c>, <c>false</c> otherwise.</returns>
    public static Boolean IfNotNull<T>(this T obj, Action<T> action) {
      if (obj != null)
        action(obj);
      return obj != null;
    }

    /// <summary>
    /// Pass a value to a <see cref="Func{T, TOut}"/> if it's not <c>null</c>.
    /// A fluent alternative to <code>value != null ? func(value) : null;</code>
    /// </summary>
    /// <param name="value">Value to pass to the function.</param>
    /// <param name="func">Function to call.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <typeparam name="TOut">Result type.</typeparam>
    /// <returns>Result of the <paramref name="func"/> if <paramref name="value" /> is not <c>null</c>.
    /// Otherwise, return <c>null</c>.</returns>
    public static TOut? IfNotNull<T, TOut>(this T? value, Func<T, TOut?> func) where TOut: class => 
      value != null ? func(value) : null;

    /// <summary>
    /// Pass a value to a <see cref="Func{T, TOut}"/> if it's not <c>null</c> and return the result;
    /// or return a fallback value if it is.
    /// A fluent alternative to <code>value != null ? func(value) : fallback;</code>
    /// </summary>
    /// <param name="value">Value to pass to the function.</param>
    /// <param name="func">Function to call.</param>
    /// <param name="fallback">Fallback if <paramref name="value"/> is <c>null</c>.</param>
    /// <typeparam name="T">Value type.</typeparam>
    /// <typeparam name="TOut">Result type.</typeparam>
    /// <returns>Result of the <paramref name="func"/> if <paramref name="value" /> is not <c>null</c>.
    /// Otherwise, return <paramref name="fallback"/>.</returns>
    public static TOut IfNotNull<T, TOut>(this T? value, Func<T, TOut> func, TOut fallback) where TOut : notnull =>
      value != null ? func(value) : fallback;


    /// <summary>
    /// Create a <see cref="NetCore.Maybe{T}"/> from a given object/value.
    /// A fluent alternative to <c>May.Be(obj)</c>. 
    /// </summary>
    /// <param name="value">Value to wrap in a <see cref="NetCore.Maybe{T}"/>.</param>
    /// <typeparam name="T">Type of the value.</typeparam>
    /// <returns>A <see cref="NetCore.Maybe{T}"/> with the value provided in <paramref name="value"/> parameter.</returns>
    public static Maybe<T> ToMaybe<T>(this T? value) where T : notnull =>
      May.Be(value);

    /// <summary>
    /// Return a value's <see cref="String"/> representation, or a fallback if the value is null.
    /// A fluent alternative to <code>value?.ToString() ?? fallback;</code>
    /// </summary>
    /// <param name="value">Value to call <c>.ToString()</c> on.</param>
    /// <param name="fallback">Fallback value to return if <paramref name="value"/> is <c>null</c>.</param>
    /// <returns>String representation of <paramref name="value"/> or <paramref name="fallback"/>.</returns>
    public static String ToStringOr(this Object? value, String fallback = "") {
      return value?.ToString() ?? fallback;
    }

    /// <summary>
    /// Return a value's <see cref="String"/> representation, or an empty string if the value is null.
    /// A fluent alternative to
    /// <code>value?.ToString() ?? String.Empty;</code>
    /// </summary>
    /// <param name="value">Value to call <c>.ToString()</c> on.</param>
    /// <returns>String representation of <paramref name="value"/> or <see cref="string.Empty"/>.</returns>
    public static String ToStringOrEmpty(this Object? value) =>
      ToStringOr(value, String.Empty);
  }
}