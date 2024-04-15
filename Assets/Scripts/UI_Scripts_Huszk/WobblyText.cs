using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class WobblyText : MonoBehaviour
{
   public TMP_Text textComponent;

   public float wobblyRate = 0.01f;
    // Update is called once per frame
    void Update()
    {
        textComponent.ForceMeshUpdate();
        var textInfo = textComponent.textInfo;

        for(int i = 0; i < textInfo.characterCount-1; i++)
        {
            var charinfo = textInfo.characterInfo[i];
            if(!charinfo.isVisible)
            {
                continue;
            }

            var verts = textInfo.meshInfo[charinfo.materialReferenceIndex].vertices;

            for(int j = 0; j < 4; j++)
            {
                var orig = verts[charinfo.vertexIndex + j];
                verts[charinfo.vertexIndex + j] = orig + new Vector3(1,Mathf.Sin(Time.time*2 + orig.x * wobblyRate) * 10, 0);
            }
        }

        for(int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices;
            textComponent.UpdateGeometry(meshInfo.mesh,i);
        }
    }
}
