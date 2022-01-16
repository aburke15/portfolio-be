namespace ABU.Portfolio;

public static class DotEnv
{
    public static void LoadEnvironmentVariables()
    {
        var root = Directory.GetCurrentDirectory();
        var dotenv = Path.Combine(root, ".env");
        
        SetEnvironmentVariables(dotenv);
    }
    
    private static void SetEnvironmentVariables(string filePath)
    {
        if (!File.Exists(filePath))
            return;

        const int envVariableLength = 2;
        foreach (var line in File.ReadAllLines(filePath))
        {
            var parts = line.Split('=', 2, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length != envVariableLength)
                continue;
            
            Environment.SetEnvironmentVariable(parts[0].Trim(), parts[1].Trim());
        }
    }
}