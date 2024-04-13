using System;
using System.Collections.Generic;
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
}
