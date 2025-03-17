using System.Diagnostics;

public class EnergyPredictionService
{
    public string RunPythonScript()
    {
        string pythonExePath = @"C:\Users\Lejla\AppData\Local\Programs\Python\Python313\python.exe";
        string scriptPath = @"C:\Users\Lejla\Documents\ml_1_.py";
        string outputCsvPath = @"C:\Users\Lejla\Desktop\USPVDB_energy_predictions.csv";
        string csvFilePath = @"C:\Users\Lejla\Desktop\uspvdb.csv";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = pythonExePath,
            Arguments = $"\"{scriptPath}\" \"{csvFilePath}\" \"{outputCsvPath}\"",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = new Process { StartInfo = psi })
        {
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception($"Python Error: {error}");
            }

            return result;
        }
    }
}
