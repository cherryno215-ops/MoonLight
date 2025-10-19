using UnityEngine;
using UnityEditor;
using UnityEditor.U2D.Sprites;
using UnityEditor.U2D;

public class PivotChanger
{
    [MenuItem("Tools/Change Pivot to Center")]
    static void ChangePivot()
    {
        string path = "Assets/Graphics/Player_Graphics.png";

        // 경로에서 Texture2D 가져오기
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (texture == null)
        {
            Debug.LogError("해당 경로에 Texture가 없습니다: " + path);
            return;
        }

        // SpriteDataProviderFactories 초기화
        SpriteDataProviderFactories factory = new SpriteDataProviderFactories();
        factory.Init();

        ISpriteEditorDataProvider dataProvider = factory.GetSpriteEditorDataProviderFromObject(texture);
        dataProvider.InitSpriteEditorDataProvider();

        // 스프라이트 정보 가져오기
        var spriteRects = dataProvider.GetSpriteRects();

        // pivot 변경
        foreach (var rect in spriteRects)
        {
            rect.pivot = new Vector2(0.5f, 0.505f);
            rect.alignment = SpriteAlignment.Custom;
        }

        // 적용
        dataProvider.SetSpriteRects(spriteRects);
        dataProvider.Apply();

        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }
}

