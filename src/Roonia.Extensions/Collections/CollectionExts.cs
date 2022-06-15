using System.Diagnostics;
using System.Text;

namespace Roonia.Extensions;

[DebuggerStepThrough]
public static class CollectionExts
{
    
    #if NETSTANDARD2_0
    public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector, IEqualityComparer<TKey>? comparer = null)
        => source.GroupBy(keySelector, comparer).Select(x => x.First());
    #endif

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="action"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    [DebuggerStepThrough]
    public static IEnumerable<T>? ForEach<T>(this IEnumerable<T>? source, Action<T> action)
    {
        if (source is not null)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        return source;
    }


    /// <summary> Add the object top the end of IEnumerable </summary>
    /// <param name="element">object to append</param>
    /// <returns>updated IEnumerable</returns>
    [DebuggerStepThrough]
    public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T element)
    {
        foreach (var item in source)
        {
            yield return item;
        }

        yield return element;
    } 


    /// <summary> Check if all elements in IEnumerable are equals </summary>
    /// <exception cref="System.ArgumentNullException">enumerable is null</exception>
    /// <returns>true if they are equals, otherwise - false</returns>
    [DebuggerStepThrough]
    public static bool AreAllSame<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable == null)
        {
            throw new ArgumentNullException(nameof(enumerable));
        }

        using (var enumerator = enumerable.GetEnumerator())
        {
            var toCompare = default(T);
            if (enumerator.MoveNext())
            {
                toCompare = enumerator.Current;
            }

            while (enumerator.MoveNext())
            {
                if (toCompare != null && !toCompare.Equals(enumerator.Current))
                {
                    return false;
                }
            }
        }

        return true;
    }


    /// <summary>
    /// Extracts the elements of an IEnumerable<string> to string.
    /// </summary>
    /// <param name="enumerable"></param>
    /// <returns>concatenated string</returns>
    [DebuggerStepThrough]
    public static string ExtractAsString(this IEnumerable<string> enumerable)
    {
        var sb = new StringBuilder();

        foreach (var s in enumerable)
        {
            sb.AppendLine(s);
        }

        return sb.ToString();
    }

    public static async Task<IEnumerable<T>?> ForEachAsync<T>(
        this IEnumerable<T>? source, 
        Func<T, Task> action, 
        CancellationToken cancellationToken = default)
    {
        if (source is not null)
        {
            foreach (var item in source)
            {
                cancellationToken.ThrowIfCancellationRequested();
                await action.Invoke(item).ConfigureAwait(false);
            }
        }

        return source;
    }

    public static async Task<IEnumerable<TResult>?> SelectAsync<TSource, TResult>(
        this IEnumerable<TSource>? source, 
        Func<TSource, Task<TResult>> asyncSelector, 
        CancellationToken cancellationToken = default)
    {
        if (source is not null)
        {
            var result = new List<TResult>();
            foreach (var item in source)
            {
                cancellationToken.ThrowIfCancellationRequested();
                result.Add(await asyncSelector(item).ConfigureAwait(false));
            }

            return result;
        }

        return null;
    }

    public static async ValueTask<List<TSource>> ToListAsync<TSource>(this IAsyncEnumerable<TSource> source, CancellationToken cancellationToken = default)
    {
        var list = new List<TSource>();
        await foreach (var item in source.WithCancellation(cancellationToken).ConfigureAwait(false))
        {
            list.Add(item);
        }

        return list;
    }

    public static void RemoveWhere<T>(this ICollection<T> collection, Func<T, bool> predicate)
    {
        for (var i = collection.Count - 1; i >= 0; i--)
        {
            var element = collection.ElementAt(i);
            if (predicate(element))
            {
                collection.Remove(element);
            }
        }
    }

    public static bool IsNotEmpty<T>(this IEnumerable<T> source)
        => source.Any();

    public static bool IsEmpty<T>(this IEnumerable<T> source)
        => !source.IsNotEmpty();

    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        => !source?.Any() ?? true;

    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> source)
        => !source.IsNullOrEmpty();

    public static int GetCount<T>(this IEnumerable<T>? source, Func<T, bool>? predicate = null)
        => source?.Count(predicate!) ?? 0;

    public static long GetLongCount<T>(this IEnumerable<T>? source, Func<T, bool>? predicate = null)
        => source?.LongCount(predicate!) ?? 0;

    
}
