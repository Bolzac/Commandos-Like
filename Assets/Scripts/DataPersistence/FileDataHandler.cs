using System;
using System.IO;
using UnityEngine;
using File = UnityEngine.Windows.File;

public class FileDataHandler
{
    private string _dataDirPath = "";
    private string _dataFileName = "";

    private string dataToStore;
    private string dataToLoad;
    private string fullPath;

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        _dataDirPath = dataDirPath;
        _dataFileName = dataFileName;
    }

    public GameData Load()
    {
        fullPath = Path.Combine(_dataDirPath, _dataFileName);
        GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                dataToLoad = "";
                using FileStream stream = new FileStream(fullPath, FileMode.Open);
                using StreamReader reader = new StreamReader(stream);
                dataToLoad = reader.ReadToEnd();
                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        return loadedData;
    }

    public void Save(GameData data)
    {
        fullPath = Path.Combine(_dataDirPath, _dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            dataToStore = JsonUtility.ToJson(data, true);
            using FileStream stream = new FileStream(fullPath,FileMode.Create);
            using StreamWriter writer = new StreamWriter(stream);
            writer.Write(dataToStore);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}