// See https://aka.ms/new-console-template for more information
using Ahlem.Fahem.Ocr;

static class MyConsoleProgram
{
    static async Task Main(string[] args)
    {

        var imagesData = new List<byte[]>();
        foreach (var imgPath in Directory.EnumerateFiles(args[0]))
        {
            var imgBytes = await File.ReadAllBytesAsync(imgPath);
            imagesData.Add(imgBytes);
        }

        var ocr = new Ocr();
        var ocrResults = await  ocr.ReadTextInImage(imagesData);
        foreach (var ocrResult in ocrResults)
        {
            System.Console.WriteLine($"Confidence :{ocrResult.Confidence}");
            System.Console.WriteLine($"Text :{ocrResult.Text}"); 
        }
    }
}
