using System;
using System.Collections.Generic;
using UnityEngine;

public class SinnerDisplayController : MonoBehaviour
{
    private GameObject currentSinner;

    private List<string> sinnerAssetsPaths = new List<string>() {
        "Prefabs/ChildSinner",
        "Prefabs/FedoraSinner",
    };

    private Dictionary<string, GameObject> sinnerAssets = new();

    // Start is called before the first frame update
    void Start()
    {
        foreach (var path in sinnerAssetsPaths) {
            sinnerAssets[path] = Resources.Load(path) as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp)) {
            if (currentSinner != null) {
                Destroy(currentSinner);
            }

            currentSinner = Instantiate(sinnerAssets["Prefabs/ChildSinner"],
                                        transform.position,
                                        Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.PageDown)) {
            if (currentSinner != null) {
                Destroy(currentSinner);
            }

            currentSinner = Instantiate(sinnerAssets["Prefabs/FedoraSinner"],
                                        transform.position,
                                        Quaternion.identity);
        }

    }
}
