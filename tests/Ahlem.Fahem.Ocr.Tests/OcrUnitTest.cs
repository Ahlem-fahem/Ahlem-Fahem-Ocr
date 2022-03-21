using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace Ahlem.Fahem.Ocr.Tests ;

public class OcrUnitTest
{
    [Fact]
    public async Task ImagesShouldBeReadCorrectly()
    {
        var executingPath = GetExecutingPath();
        var images = new List<byte[]>();
        foreach (var imagePath in
                 Directory.EnumerateFiles(Path.Combine(executingPath, "images")))
        {
            var imageBytes = await File.ReadAllBytesAsync(imagePath);
            images.Add(imageBytes);
        }

        var ocrResults = await new Ocr().ReadTextInImage(images);

        Assert.Equal(ocrResults[0].Text, "développeur C# au sens large. Car si\nvous savez coder en C#, potentiellemer\nvous savez coder avec toutes les\n");
        Assert.Equal(ocrResults[0].Confidence, 0.9399999976158142);
        Assert.Equal(ocrResults[1].Text, "Malheureusement les cabinets de\nrecrutement et les annonces, ainsi que les\nétudes sur les salaires, gardent cette\nterminologie obsolète.\n");
        Assert.Equal(ocrResults[1].Confidence, 0.949999988079071);
        Assert.Equal(ocrResults[2].Text, "Quel salaire peut-on espérer ? Comme\ntoujours, les écarts moyens sont\nimportants. La base serait :\n");
        Assert.Equal(ocrResults[2].Confidence, 0.949999988079071);
    }
    private static string GetExecutingPath()
    {
        var executingAssemblyPath =
            Assembly.GetExecutingAssembly().Location;
        var executingPath =
            Path.GetDirectoryName(executingAssemblyPath);
        return executingPath;
    }
}