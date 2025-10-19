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

        // ��ο��� Texture2D ��������
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        if (texture == null)
        {
            Debug.LogError("�ش� ��ο� Texture�� �����ϴ�: " + path);
            return;
        }

        // SpriteDataProviderFactories �ʱ�ȭ
        SpriteDataProviderFactories factory = new SpriteDataProviderFactories();
        factory.Init();

        ISpriteEditorDataProvider dataProvider = factory.GetSpriteEditorDataProviderFromObject(texture);
        dataProvider.InitSpriteEditorDataProvider();

        // ��������Ʈ ���� ��������
        var spriteRects = dataProvider.GetSpriteRects();

        // pivot ����
        foreach (var rect in spriteRects)
        {
            rect.pivot = new Vector2(0.5f, 0.505f);
            rect.alignment = SpriteAlignment.Custom;
        }

        // ����
        dataProvider.SetSpriteRects(spriteRects);
        dataProvider.Apply();

        AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
    }
}

