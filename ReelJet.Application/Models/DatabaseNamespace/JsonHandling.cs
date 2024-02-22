using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace ReelJet.Application.Models.DatabaseNamespace;

public static class JsonHandling {

    public static string path = "";

    public static void WriteData<T>(T? list, string filename) {
        JsonSerializerOptions op = new();
        op.WriteIndented = true;

        File.WriteAllText(path + filename + ".json", JsonSerializer.Serialize(list, op));
    }

    public static T? ReadData<T>(string filename) where T : new() {
        T? readData = new T();

        JsonSerializerOptions op = new JsonSerializerOptions();
        op.WriteIndented = true;
        using FileStream fs = new FileStream(path + filename + ".json", FileMode.OpenOrCreate);
        if (fs.Length != 0) readData = JsonSerializer.Deserialize<T>(fs, op);

        return readData;
    }
}
