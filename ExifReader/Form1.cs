using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace ExifReader
{
    public partial class Form1 : Form
    {
        private string exifToolPath = string.Empty;

        public Form1(string[] args)
        {
            InitializeComponent();

            exifToolPath = GetExifPath();
            if (string.IsNullOrWhiteSpace(exifToolPath))
            {
                MessageBox.Show("Please add the path to the EXIF tool (exiftool.org) in the appsettings.json file.", "EXIFTOOL not configured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }

            ParseArgs(args);
        }

        private IConfigurationRoot LoadConfig()
        {
            var config = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            return config;
        }

        private string GetExifPath()
        {
            var config = LoadConfig();
            return config["ExifToolPath"] ?? string.Empty;
        }

        private void ParseArgs(string[] args)
        {
            if (args.Length == 0)
            {
                UpdateLabel("None");
                return;
            }

            LoadFile(args[0]);
        }

        private string LaunchAndReadExif(string imagePath)
        {
            var p = new Process();
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.CreateNoWindow = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.Arguments = $"\"{imagePath}\"";
            p.StartInfo.FileName = exifToolPath;
            p.Start();

            var output = p.StandardOutput.ReadToEnd();
            p.WaitForExit();

            var lines = output.Split(Environment.NewLine);
            var promptLine = lines.FirstOrDefault(l => l.StartsWith("Prompt"));

            if (promptLine != null)
            {
                promptLine = promptLine.Substring(promptLine.IndexOf(":") + 2);
            }
            else
            {
                MessageBox.Show("No PROMPT tag was found in the EXIF data of the file.", "No EXIF Data Fount", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return string.Empty;
            }

            return promptLine;
        }

        private void LoadExif(string prompt)
        {
            var parsed = JObject.Parse(prompt);
            var rootNode = new JsonRootNode(parsed);

            propertyGrid1.SelectedObject = rootNode;
            propertyGrid1.Visible = true;
        }

        private void UpdateLabel(string fileName)
        {
            lblDrag.Text = $"Drag your PNG file here to read the EXIF data\r\nCurrent file: {fileName}";
            if (fileName != "None")
            {
                Text = $"ComfyUI PNG EXIF Reader - {fileName}";
            }
            else
            {
                Text = $"ComfyUI PNG EXIF Reader";
            }
        }

        private void lblDrag_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    LoadFile(files[0]);
                }
            }
        }

        private void lblDrag_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void LoadFile(string fileName)
        {
            pbFile.SizeMode = PictureBoxSizeMode.Zoom;
            pbFile.Image = Image.FromFile(fileName);

            var fi = new FileInfo(fileName);
            UpdateLabel(fi.Name);

            var promptData = LaunchAndReadExif(fileName);
            LoadExif(promptData);
        }
    }
}
