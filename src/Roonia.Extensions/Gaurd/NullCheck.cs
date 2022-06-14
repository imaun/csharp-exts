using System;

namespace Roonia.Extensions;

public static class NullCheckExts {

    /// <summary>
    /// Check if an object is null or not.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNull(this object obj)
        => obj == null;

    /// <summary>
    /// Checks if an Object is not null.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool IsNotNull(this object obj)
        => !IsNull(obj);

    /// <summary>
    /// Throws ArgumentNullException if the obj is null.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="name"></param>
    public static void ThrowIfArgumentIsNull(this object obj, string name) {
        obj.ThrowIfIsNull(new ArgumentNullException(name));
    }

    /// <summary>
    /// Throws an Exceotion if the object is null.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="exception"></param>
    public static void ThrowIfIsNull(this object obj, Exception exception) {
        if(obj.IsNull())
            throw exception;
    }

    /// <summary>
    /// Throw <see ref="NullReferenceException">NullReferenceException</see> if the objects is null.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="name"></param>
    public static void ThrowIfReferenceIsNull(this object obj, string name) {
        obj.ThrowIfIsNull(new NullReferenceException(name));
    }

}