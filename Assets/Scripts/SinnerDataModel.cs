using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

[Serializable]
public class SinnerDataModel
{
    [Serializable]
    public class DocumentInfo {
        public string name;

        public uint age;

        public List<String> sins;
    }

    [Serializable]
    public enum Emotion {
        None,
        Neutral,
        Happy,
        Angry,
        Sad,
        Nervous,
    }

    [Serializable]
    public class Hint {
        public uint sinIdx;
        public string expression;
    };

    [Serializable]
    public class DialogueEntry {
        public string speakerName;

        [JsonConverter(typeof(StringEnumConverter))]
        public Emotion emotion;

        public List<Hint> hints;

        public string text;
    }

    public DocumentInfo documentInfo;
    public List<DialogueEntry> dialogue;
    public uint correctLayer;

    public static SinnerDataModel CreateFromJson(string jsonString) {
        return JsonConvert.DeserializeObject<SinnerDataModel>(jsonString);
    }

    public static List<SinnerDataModel> LoadSinnersFromJson() {
        var sinnersDir = Path.Join(Application.dataPath, "Sinners");
        var directoryInfo = new DirectoryInfo(sinnersDir);
        var sinnerJsons = directoryInfo.GetFiles("*.json");

        var sinners = new List<SinnerDataModel>{};
        foreach(var json in sinnerJsons) {
            var reader = new StreamReader(json.OpenRead());
            var jsonStr = reader.ReadToEnd();

            var sinnerData = CreateFromJson(jsonStr);
            sinners.Add(sinnerData);
        }

        return sinners;
    }
}
