using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ImageMagick;
using VerifyTests.AngleSharp;

namespace TestProject1;

public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void Initialize()
    {
        VerifierSettings.ScrubEmptyLines();
        VerifierSettings.ScrubLinesWithReplace(s =>
        {
            string scrubbed = Regex.Replace(s, @"\sb-\w+=(\""\"")", "");
            return string.IsNullOrWhiteSpace(scrubbed) ? null : scrubbed;
        });
        VerifierSettings.ScrubLinesWithReplace(s =>
        {
            string currentYear = DateTime.Now.Year.ToString();
            string scrubbedCurrentYear = Regex.Replace(s, $@"\b{currentYear}\b", "YYYY");
            return string.IsNullOrWhiteSpace(scrubbedCurrentYear) ? null : scrubbedCurrentYear;
        });
        VerifierSettings.ScrubInlineGuids(ScrubberLocation.First);

        VerifyBase.DerivePathInfo((source, projectPath, type, method)
                => new PathInfo(Path.Combine(projectPath, "_snapshots_", type.Name)));

        VerifyBunit.Initialize();

        VerifyImageMagick.RegisterComparers(
            threshold: .01,
            metric: ErrorMetric.MeanAbsolute);

        VerifyDiffPlex.Initialize();

        HtmlPrettyPrint.All();
    }
}
