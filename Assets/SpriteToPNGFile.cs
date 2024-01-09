using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using UnityEditor;


public class SpriteToPNGFile : MonoBehaviour
{
    [SerializeField] private Sprite _sprite;
    public Sprite Sprite => _sprite;

    [SerializeField] private Texture2D _texturePreview;
    public Texture2D TexturePreview => _texturePreview;

    public void SaveTexture(Texture2D texture2D)
    {
        _texturePreview = texture2D;

        byte[] bytes = texture2D.EncodeToPNG();
        string directoryPath = Application.dataPath + "/SpriteToPNGFileOutput";
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string filePath = directoryPath + "/Tex_" + System.Guid.NewGuid().ToString() + ".png";
        File.WriteAllBytes(filePath, bytes);

        Debug.Log(bytes.Length / 1024 + "Kb was saved as: " + filePath);

#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    public void SaveSpriteAsTexture(Sprite sprite)
    {
        Texture2D texture = new Texture2D(
            (int)sprite.rect.width,
            (int)sprite.rect.height
        );

        Color[] pixels = sprite.texture.GetPixels(
            (int)sprite.textureRect.x,
            (int)sprite.textureRect.y,
            (int)sprite.textureRect.width,
            (int)sprite.textureRect.height
        );

        texture.SetPixels(pixels);
        texture.Apply();

        SaveTexture(texture);
    }


   

}
[CustomEditor(typeof(SpriteToPNGFile))]
public class SpriteToPNGFileEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        SpriteToPNGFile spriteExporter = (SpriteToPNGFile)target;

        if (GUILayout.Button("Generate PNG"))
        {
            spriteExporter.SaveSpriteAsTexture(spriteExporter.Sprite);
        }
    }
}