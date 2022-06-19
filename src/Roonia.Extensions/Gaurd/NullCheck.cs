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

    public static void ThrowIfIsNull<T>(this T input, Exception exception) {
        if(input.IsNull())
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

    public static bool IsNullOrEmpty(this string value) 
        => string.IsNullOrEmpty(value);
    
    public static void ThrowIfIsNullOrEmpty(this string value, Exception exception) {
        if(value.IsNullOrEmpty())
            throw exception;
    }

    public static bool IsNullOrWhiteSpace(this string value)
        => string.IsNullOrWhiteSpace(value);

    public static void ThrowIfArgumentIsNullOrEmpty(this string value) 
        => ThrowIfIsNullOrEmpty(value, new ArgumentNullException(nameof(value)));
    
    public static void ThrowIfIsNullOrWhiteSpace(this string value, Exception exception) {
        if(IsNullOrWhiteSpace(value))
            throw exception;
    }

    public static void ThrowIfArgumentIsNullOrWhiteSpace(this string value)
        => ThrowIfIsNullOrWhiteSpace(value, new ArgumentNullException(nameof(value)));

    public static bool IsEmpty(this Guid value)
        => value == Guid.Empty;

    public static void ThrowIfIsEmpty(this Guid value, Exception exception) {
        if(value.IsEmpty())
            throw exception;
    }

    /// <summary>
    /// Check if a <see ref="DateTime"/> is the default value for this type or not.
    /// </summary>
    /// <param name="value">The DateTime value to check.</param>
    /// <returns>true if the value is default(DateTime).</returns>
    public static bool IsDefault(this DateTime value)
        => value == default(DateTime);

    /// <summary>
    /// Throws an Exception if the value of a DateTime is the default value.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="exception"></param>
    public static void ThrowIfDateTimeIsDefault(this DateTime value, Exception exception) {
        if(value.IsDefault())
            throw exception;
    }

}