﻿using System.Globalization;
using System.Reflection;
using System.Resources;

using Scover.WinClean.BusinessLogic.Scripts;
using Scover.WinClean.BusinessLogic.Xml;
using Scover.WinClean.DataAccess;
using Scover.WinClean.Properties;

namespace Scover.WinClean.BusinessLogic;

public static class AppInfo
{
    private static readonly IScriptMetadataDeserializer _deserializer = new ScriptMetadataXmlDeserializer();
    private static readonly Assembly assembly = Assembly.GetExecutingAssembly();

    #region Script metadata

    public static IReadOnlyList<Category> Categories { get; }
        = _deserializer.MakeCategories(OpenAppFile("Categories.xml")).ToList();

    public static IReadOnlyCollection<Host> Hosts { get; } = new[]
    {
        Host.Cmd,
        Host.PowerShell,
        Host.Regedit
    };

    public static IReadOnlyList<Impact> Impacts { get; }
        = _deserializer.MakeImpacts(OpenAppFile("Impacts.xml")).ToList();

    public static IReadOnlyList<RecommendationLevel> RecommendationLevels { get; }
        = _deserializer.MakeRecommendationLevels(OpenAppFile("RecommendationLevels.xml")).ToList();

    #endregion Script metadata

    #region Assembly attributes

    public static string Name
        => assembly.GetCustomAttribute<AssemblyProductAttribute>().AssertNotNull().Product;

    public static CultureInfo NeutralResourcesCulture
        => new(assembly.GetCustomAttribute<NeutralResourcesLanguageAttribute>().AssertNotNull().CultureName);

    public static string RepositoryUrl
        => (assembly.GetCustomAttributes<AssemblyMetadataAttribute>().SingleOrDefault(metadata => metadata.Key == "RepositoryUrl")?.Value).AssertNotNull();

    public static string Version
        => assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().AssertNotNull().InformationalVersion;

    #endregion Assembly attributes

    /// <summary>Gets or sets the error callback for reading application files.</summary>
    /// <remarks>This property must be set externally in the Presentation layer.</remarks>
    public static FSOperationCallback ReadAppFileRetryOrFail { get; set; } = (_, _, _)
        => throw new NotSupportedException(Resources.DevException.CallbackNotSet.FormatWith(nameof(ReadAppFileRetryOrFail)));

    public static Settings Settings => Settings.Default;

    private static Stream OpenAppFile(string filename)
    {
        string path = AppDirectory.InstallDir.Join(filename);
        while (true)
        {
            try
            {
                return File.OpenRead(path);
            }
            catch (Exception e) when (e.IsFileSystem())
            {
                if (!ReadAppFileRetryOrFail(e, FSVerb.Access, new FileInfo(path)))
                {
                    throw;
                }
            }
        }
    }
}