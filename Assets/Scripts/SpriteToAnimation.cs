using UnityEngine;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
 
public class SpriteToAnimation
{
    [MenuItem("Assets/Sprites to Animations")]
    private static void CreateAnimationsFromSprites()
    {
        var guids = Selection.assetGUIDs;
 
        var assets = new List<Texture2D>();
        foreach (var guid in guids)
        {
            GuidToList(guid, assets);
        }
 
        if (assets.Count == 0)
        {
            return;
        }
 
        foreach (var asset in assets)
        {
            CreateAnimation(asset);
        }
    }
 
    private static void CreateAnimation(Texture2D texture)
    {
        var path = AssetDatabase.GetAssetPath(texture);
        var sprites = AssetDatabase.LoadAllAssetsAtPath(path).OfType<Sprite>().ToArray();
        if (sprites.Length == 0)
        {
            return;
        }
 
        // Create animations folder.
        var animationPath = Path.Combine(Path.GetDirectoryName(path), "Animations");
        if (!Directory.Exists(animationPath))
        {
            Directory.CreateDirectory(animationPath);
        }
 
        // Create animation asset.
        var clip = new AnimationClip();
 
        // Create binding curve.
        var curveBinding = new EditorCurveBinding();
        curveBinding.path = "Base";
        curveBinding.propertyName = "m_Sprite";
        curveBinding.type = typeof(SpriteRenderer);
 
        // Create keyframes.
        var frameRate = 6f;
        var objectReferences = new List<ObjectReferenceKeyframe>();
        for (int i = 0; i <= sprites.Length; i++)
        {
            // Allows adding a hold frame to make sure the last sprite frame doesn't get cut off too soon.
            var index = i;
            if (index >= sprites.Length)
            {
                index = sprites.Length - 1;
            }
 
            var sprite = sprites[index];
            var newKeyframe = new ObjectReferenceKeyframe();
            newKeyframe.value = sprite;
            newKeyframe.time = i * (1f / frameRate);
 
            objectReferences.Add(newKeyframe);
        }
        AnimationUtility.SetObjectReferenceCurve(clip, curveBinding, objectReferences.ToArray());
 
        // Save.
        AssetDatabase.CreateAsset(clip, Path.Combine(animationPath, texture.name + ".anim"));
    }
 
    private static void GuidToList(string guid, List<Texture2D> assetList)
    {
        var path = AssetDatabase.GUIDToAssetPath(guid);
        var asset = AssetDatabase.LoadAssetAtPath<Object>(path);
        if (asset == null)
        {
            return;
        }
 
        if (asset is Texture2D texture)
        {
            assetList.Add(texture);
        }
    }
}
 
