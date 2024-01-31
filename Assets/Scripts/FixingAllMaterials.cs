using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FixingAllMaterials : MonoBehaviour
{
    //[MenuItem("Custom/Fix Materials with Shader Errors")]
    //static void FixMaterials()
    //{
    //    string searchFolder = "Assets"; // Specify the folder path here

    //    Shader urpLiteShader = Shader.Find("Universal Render Pipeline/Lit");

    //    string[] materialGuids = AssetDatabase.FindAssets("t:Material", new string[] { searchFolder });

    //    int count = 0;

    //    foreach (string materialGuid in materialGuids)
    //    {
    //        string materialPath = AssetDatabase.GUIDToAssetPath(materialGuid);
    //        Material material = AssetDatabase.LoadAssetAtPath<Material>(materialPath);

    //        if (material.shader == null || material.shader.name == "Hidden/InternalErrorShader")
    //        {
    //            material.shader = urpLiteShader;
    //            count++;
    //        }
    //        if (material.shader == null || material.shader.name == "Toony Colors Pro 2/Hybrid Shader 2")
    //        {
    //            material.shader = urpLiteShader;
    //            count++;
    //        }
    //        if (material.shader == null || material.shader.name == "Toony Colors Pro 2/Examples URP/Cat Demo/Vertex Colors Specular")
    //        {
    //            material.shader = urpLiteShader;
    //            count++;
    //        }
    //        if (material.shader == null || material.shader.name == "Shader Graphs/GlassShader")
    //        {
    //            material.shader = urpLiteShader;
    //            count++;
    //        }
    //    }
    //    Debug.Log("Fixed " + count + " materials with shader errors.");
    //}
}
