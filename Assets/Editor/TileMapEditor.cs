using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor
{
    private TileMap tileMap;

    public void OnEnable() // �ν����� Ȱ��ȭ
    {
        Debug.Log("OnEnable");
        tileMap = target as TileMap;
        Tools.current = Tool.View;

        if(tileMap.texture2D != null)
        {
            var path = AssetDatabase.GetAssetPath(tileMap.texture2D);
            //Debug.Log(path);
            var array = AssetDatabase.LoadAllAssetsAtPath(path);
            //Debug.Log(array.Length);

            tileMap.sprites = new Sprite[array.Length - 1];
            for(int i = 1; i < array.Length; i++)
            {
                tileMap.sprites[i - 1] = array[i] as Sprite;
                //Debug.Log(tileMap.sprites[i - 1].name);
            }
            var w = tileMap.sprites[0].textureRect.width;
            var h = tileMap.sprites[0].textureRect.height;
            tileMap.tileSize = new Vector2(w,h);
        }
    }

    public void OnDisable() // �ν����� ��Ȱ��ȭ(�ٸ� �ν�����â���� �ٲ�)
    {
        Debug.Log("OnDisable");
        tileMap = null;
    }

    public override void OnInspectorGUI() // �ν������� ������ ���ŵ� ������ ȣ��
    {
        // �⺻ ����
        // base.OnInspectorGUI();
        // DrawDefaultInspector();
        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("Our custom editor"); // �б⸸ ����

        tileMap.mapSize = EditorGUILayout.Vector2Field("Map Size", tileMap.mapSize);
        tileMap.texture2D = EditorGUILayout.ObjectField("Texture2D: ", tileMap.texture2D, typeof(Texture2D), false) as Texture2D;

        if(tileMap.texture2D == null )
        {
            EditorGUILayout.HelpBox("�ؽ��ĸ� �����ϼ���.", MessageType.Warning); // MessageType.Warning : ���� ��
        }
        else
        {
            EditorGUILayout.LabelField($"Tile Size: { tileMap.tileSize }");
        }


        EditorGUILayout.EndVertical();
        EditorUtility.SetDirty(tileMap); // �ش� ���ӿ�����Ʈ�� TileMap������Ʈ�� �� ����
    }
}
