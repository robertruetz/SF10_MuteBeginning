using System;
using SoundForge;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class EntryPoint
{
    public void Begin(IScriptableApp app)
    {
        string workingDir = GetWorkingDirectory(app);
        int threshold = GetThreshold(app);
        string outputPath = string.Empty;
        if (!CreateOutputDirectory(workingDir, out outputPath))
        {
            Environment.Exit(-1);
        }

        // TODO: Handle input and output extensions (file types)
        // TODO: Loop through the files and do the thing
        
    }

    public bool CreateOutputDirectory(string workingDir, out string outputPath)
    {
        outputPath = Path.Combine(workingDir, "output");
        try
        {
            Directory.CreateDirectory(outputPath);
        }
        catch(Exception ex)
        {
            MessageBox.Show(string.Format("Could not create output directory.\n{0}", ex.Message));
            return false;
        }
        return true;
    }

    public int GetThreshold(IScriptableApp app)
    {
        int threshold = int.MaxValue;
        bool haveThresh = false;
        while (!haveThresh)
        {
            string thresh = string.Empty;
            SfHelpers.WaitForInputString("Enter the threshold in negative dB (i.e. -35)", "-35");
            if (!int.TryParse(thresh, out threshold))
            {
                MessageBox.Show("You did not enter a valid number.");
            }
            else if (threshold > 0)
            {
                MessageBox.Show("You entered a positive number.");
            }
            else
            {
                haveThresh = true;
            }
        }
        return threshold;
    }

    public string GetWorkingDirectory(IScriptableApp app)
    {
        string workingDir = string.Empty;
        bool haveWorkingDir = false;
        while (!haveWorkingDir)
        {
            workingDir = SfHelpers.ChooseDirectory("Select the folder containing files to work on.", @"C:\");
            if (string.IsNullOrEmpty(workingDir))
            {
                MessageBox.Show("You must choose a valid folder containing audio files.");
            }
            else if (!Directory.Exists(workingDir))
            {
                MessageBox.Show(string.Format("{0} does not exist. Try again.", workingDir);
            }
            else
            {
                haveWorkingDir = true;
            }
            //TODO: Possibly check to see if there are files and if they are audio files
        }
        return workingDir;
    }

    public void FromSoundForge(IScriptableApp app)
    {
        ForgeApp = app; //execution begins here
        app.SetStatusText(String.Format("Script '{0}' is running.", Script.Name));
        Begin(app);
        app.SetStatusText(String.Format("Script '{0}' is done.", Script.Name));
    }
    public static IScriptableApp ForgeApp = null;
    public static void DPF(string sz) { ForgeApp.OutputText(sz); }
    public static void DPF(string fmt, params object[] args) { ForgeApp.OutputText(String.Format(fmt, args)); }
} //EntryPoint
