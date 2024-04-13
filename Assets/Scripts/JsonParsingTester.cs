using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JsonParsingTester : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad() {
        var sinnersDir = Path.Join(Application.dataPath, "Sinners");
        var directoryInfo = new DirectoryInfo(sinnersDir);
        var sinnerJsons = directoryInfo.GetFiles("*.json");

        var sinners = new List<SinnerDataModel>{};
        foreach(var json in sinnerJsons) {
            var reader = new StreamReader(json.OpenRead());
            var jsonStr = reader.ReadToEnd();

            var sinnerData = SinnerDataModel.CreateFromJson(jsonStr);
            sinners.Add(sinnerData);
        }

        int a = 10;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
