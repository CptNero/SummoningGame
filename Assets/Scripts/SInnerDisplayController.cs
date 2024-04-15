using System;
using System.Collections.Generic;
using UnityEngine;

public class SinnerDisplayController : MonoBehaviour
{
    private GameObject currentSinner;

    private List<string> sinnerAssetsPaths = new List<string>() {
        "Prefabs/JermaSinner",
        "Prefabs/ChildSinner",
        "Prefabs/FedoraSinner",
    };

    private Dictionary<string, GameObject> sinnerAssets;

    void OnEnable() {
        GameController.OnSinnerChange += ChangeSinner;

        sinnerAssets = new();
        foreach (var path in sinnerAssetsPaths) {
            sinnerAssets[path] = Resources.Load(path) as GameObject;
        }
    }

    void OnDisable() {
        GameController.OnSinnerChange -= ChangeSinner;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    void ChangeSinner(string sinnerAssetsPath) {
        if (currentSinner != null) {
                Destroy(currentSinner);
        }

        currentSinner = Instantiate(sinnerAssets["Prefabs/" + sinnerAssetsPath],
                                    transform.position,
                                    Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
