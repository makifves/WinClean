﻿using System.Security;
using System.Text.RegularExpressions;
using System.Xml;
using Scover.WinClean.DataAccess;

namespace Scover.WinClean.BusinessLogic;

/// <summary>
/// Provides a set of extension methods that fulfill a relatively generic role in the <see cref="BusinessLogic"/> layer.
/// </summary>
public static class Extensions
{
    private static readonly char[] invalidFileNameChars = Path.GetInvalidFileNameChars();

    public static LocalizedString GetLocalizedString(this XmlDocument doc, string name)
    {
        LocalizedString localizedNodeTexts = new();
        foreach (XmlElement element in doc.GetElementsByTagName(name))
        {
            localizedNodeTexts.SetFromXml(element);
        }
        return localizedNodeTexts;
    }

    /// <summary>Gets the single child element with the specified name.</summary>
    /// <param name="parent">The parent node.</param>
    /// <param name="name">The tag name of the element to search for.</param>
    /// <returns>The <see cref="XmlNode.InnerText"/> of the single child node.</returns>
    /// <exception cref="XmlException">There are no or multiple elements named <paramref name="name"/>.</exception>
    public static string GetSingleChild(this XmlElement parent, string name)
    {
        var elements = parent.GetElementsByTagName(name);
        return elements.Count > 1
            ? throw new XmlException($"'{parent.Name}' has {elements.Count} childs named '{name}' but only one was expected.")
            : elements[0]?.InnerText ?? throw new XmlException($"'{parent.Name}' has no child named '{name}'.");
    }

    /// <inheritdoc cref="GetSingleChild(XmlElement, string)"/>
    public static string GetSingleChild(this XmlDocument parent, string name)
        => GetSingleChild(parent.DocumentElement ?? throw new XmlException("No root exists in document."), name);

    /// <summary>Checks if an exception could have been thrown by the filesystem API.</summary>
    /// <returns>
    /// <para><see langword="true"/> if <paramref name="e"/> is of or derived from any of the following types :</para>
    /// <br><see cref="IOException"/></br><br><see cref="UnauthorizedAccessException"/></br><br><see cref="SecurityException"/></br>
    /// <para>Otherwise; <see langword="false"/>.</para>
    /// </returns>
    /// <remarks>Note that unrelated methods may throw any of these exceptions.</remarks>
    public static bool IsFileSystem(this Exception e)
        => e is IOException or UnauthorizedAccessException or SecurityException;

    public static void SetFromXml(this LocalizedString str, XmlNode node)
        => str.Set(new(node.Attributes?["xml:lang"]?.Value ?? ""), node.InnerText);

    /// <summary>Creates a valid Windows filename from a string.</summary>
    /// <param name="filename">The filename candidate.</param>
    /// <param name="replaceInvalidCharsWith">What to replace invalid filename chars in <paramref name="filename"/> with.</param>
    /// <returns>
    /// A new <see cref="string"/>, equivalent to <paramref name="filename"/>, but modified to be a valid Windows filename if it
    /// <paramref name="filename"/> wasn't already.
    /// </returns>
    /// <exception cref="ArgumentException"/>
    /// <remarks>The length of the filename is not checked, and the casing is not modified.</remarks>
    public static string ToFilename(this string filename, string replaceInvalidCharsWith = "_")
        => string.IsNullOrWhiteSpace(filename)
             ? throw new ArgumentException("Is null, empty, or whitespace.", nameof(filename))

         : string.IsNullOrEmpty(replaceInvalidCharsWith)
             ? throw new ArgumentException("Is null or empty.", nameof(replaceInvalidCharsWith))

         : filename.All(c => c == '.')
             ? throw new ArgumentException("Consists only of dots", nameof(filename))

         : replaceInvalidCharsWith.All(c => c == '.')
             ? throw new ArgumentException("Consists only of dots.", nameof(replaceInvalidCharsWith))

         : replaceInvalidCharsWith.IndexOfAny(invalidFileNameChars) != -1
             ? throw new ArgumentException("Contains invalid filename chars.", nameof(replaceInvalidCharsWith))

         : Regex.Replace(filename.Trim(), $"[{Regex.Escape(new(invalidFileNameChars))}]", replaceInvalidCharsWith,
                         RegexOptions.Compiled | RegexOptions.CultureInvariant);
}