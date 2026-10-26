using System.IO;
using UnityEngine;

public class FileSaveHandler
{
    public string FilePath = "";

    public FileSaveHandler(string filePath)
    {
        FilePath = filePath;
    }

    public T Load<T>(string fileName) where T : class
    {
        string fullPath = Path.Combine(FilePath, fileName);

        T data = null;

        if (File.Exists(fullPath)) 
        {
            string dataToLoad = "";
            using (FileStream fs = new FileStream(fullPath, FileMode.Open))
            using (StreamReader sr = new StreamReader(fs))
                dataToLoad = sr.ReadToEnd();

            data = JsonUtility.FromJson<T>(dataToLoad);
        }

        return data;
    }

    public void Save<T>(T data, string fileName)
    {
        string fullPath = Path.Combine(FilePath, fileName);

        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

        string dataToStore = JsonUtility.ToJson(data, true);

        using (FileStream fs = new FileStream(fullPath, FileMode.Create))
        using (StreamWriter sr = new StreamWriter(fs))
        {
            sr.Write(dataToStore);
        }
    }
}
