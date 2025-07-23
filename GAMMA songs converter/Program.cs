using SoxSharp;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

class Program
{
    static string SelectFolder(string description)
    {
        var dialog = new FolderBrowserDialog
        {
            Description = description,
            ShowNewFolderButton = true
        };

        return dialog.ShowDialog() == DialogResult.OK ? dialog.SelectedPath : null;
    }

    [STAThread]
    static void Main(string[] args)
    {
        var inputFolder = SelectFolder("Select folder with compositions:");

        if (string.IsNullOrEmpty(inputFolder))
        {
            Console.WriteLine("Folder selection is canceled. The default folder is used.");
            inputFolder = @"input";
        }

        var outputFolder = SelectFolder("select folder for ogg files:");

        if (string.IsNullOrEmpty(outputFolder))
        {
            Console.WriteLine("Folder selection is canceled. The default folder is used.");
            outputFolder = @"output";
        }

        if (!Directory.Exists(outputFolder))
            Directory.CreateDirectory(outputFolder);

        Console.WriteLine($"Input folder: {inputFolder}");
        Console.WriteLine($"Output folder: {outputFolder}");

        try
        {
            int i = 0;
            foreach (string mp3File in Directory.GetFiles(inputFolder, "*.mp3"))
            {
                string cleanName = FormatFileName(TR(Path.GetFileNameWithoutExtension(mp3File)));
                string oggFile = Path.Combine(outputFolder, cleanName + ".ogg");

                ConvertMp3ToOgg(mp3File, oggFile);
                Console.WriteLine($"{++i}. Конвертировано: {Path.GetFileName(mp3File)} => {cleanName}.ogg");
            }

            Console.WriteLine("Done!");
        }
        catch (DirectoryNotFoundException ex) {
            Console.WriteLine(ex.Message);
            Console.WriteLine("No folder with compositions is selected and there is no \"input\" folder in the program root. Create an \"input\" folder in the program folder or select another one.");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.ToString());
        }       
    }
    static string FormatFileName(string originalName)
    {
        string cleaned = Regex.Replace(originalName, @"\s*\(.*?\)", "");

        cleaned = cleaned.Replace(" ", "_");

        cleaned = Regex.Replace(cleaned, @"_+", "_");

        return cleaned.Trim('_');
    }
    static void ConvertMp3ToOgg(string mp3File, string oggFile)
    {
        using (var sox = new Sox("audioConverter\\sox.exe"))
        {
            sox.Process(mp3File, oggFile);
        }
    }
    private static string TR(string str)
    {
        str = str.Replace("а", "a");
        str = str.Replace("А", "A");
        str = str.Replace("б", "b");
        str = str.Replace("Б", "B");
        str = str.Replace("в", "v");
        str = str.Replace("В", "V");
        str = str.Replace("г", "g");
        str = str.Replace("Г", "G");
        str = str.Replace("д", "d");
        str = str.Replace("Д", "D");
        str = str.Replace("е", "e");
        str = str.Replace("Е", "E");
        str = str.Replace("ё", "e");
        str = str.Replace("Ё", "E");
        str = str.Replace("ж", "zh");
        str = str.Replace("Ж", "ZH");
        str = str.Replace("з", "z");
        str = str.Replace("З", "Z");
        str = str.Replace("и", "i");
        str = str.Replace("И", "I");
        str = str.Replace("й", "y");
        str = str.Replace("Й", "Y");
        str = str.Replace("к", "k");
        str = str.Replace("К", "K");
        str = str.Replace("л", "l");
        str = str.Replace("Л", "L");
        str = str.Replace("м", "m");
        str = str.Replace("М", "M");
        str = str.Replace("н", "n");
        str = str.Replace("Н", "N");
        str = str.Replace("о", "o");
        str = str.Replace("О", "O");
        str = str.Replace("п", "p");
        str = str.Replace("П", "P");
        str = str.Replace("р", "r");
        str = str.Replace("Р", "R");
        str = str.Replace("с", "s");
        str = str.Replace("С", "S");
        str = str.Replace("т", "t");
        str = str.Replace("Т", "T");
        str = str.Replace("У", "U");
        str = str.Replace("у", "u");
        str = str.Replace("ф", "f");
        str = str.Replace("Ф", "F");
        str = str.Replace("х", "h");
        str = str.Replace("Х", "H");
        str = str.Replace("ц", "ts");
        str = str.Replace("Ц", "TS");
        str = str.Replace("ч", "ch");
        str = str.Replace("Ч", "CH");
        str = str.Replace("ш", "sh");
        str = str.Replace("Ш", "SH");
        str = str.Replace("щ", "shch");
        str = str.Replace("Щ", "SHCH");
        str = str.Replace("ы", "y");
        str = str.Replace("Ы", "Y");
        str = str.Replace("э", "e");
        str = str.Replace("Э", "E");
        str = str.Replace("ю", "yu");
        str = str.Replace("Ю", "YU");
        str = str.Replace("Я", "YA");
        str = str.Replace("я", "ya");
        str = str.Replace("ь", "");
        str = str.Replace("Ь", "");
        str = str.Replace("ъ", "");
        str = str.Replace("Ъ", "");
        return str;
    }
}