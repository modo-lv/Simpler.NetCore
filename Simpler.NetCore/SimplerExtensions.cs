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
    public static Boolean IfNotNull<T>(this T obj, Action<T> action){
      if (obj != null)
        action(obj);
      return obj != null;
    }
  }
}
