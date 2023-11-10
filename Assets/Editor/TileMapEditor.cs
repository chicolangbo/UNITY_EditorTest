using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(TileMap))]
public class TileMapEditor : Editor
{
    private TileMap tileMap;

    public void OnEnable() // 인스펙터 활성화
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

    public void OnDisable() // 인스펙터 비활성화(다른 인스펙터창으로 바뀜)
    {
        Debug.Log("OnDisable");
        tileMap = null;
    }

    public override void OnInspectorGUI() // 인스펙터의 내용이 갱신될 때마다 호출
    {
        // 기본 구현
        // base.OnInspectorGUI();
        // DrawDefaultInspector();
        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("Our custom editor"); // 읽기만 가능

        tileMap.mapSize = EditorGUILayout.Vector2Field("Map Size", tileMap.mapSize);
        tileMap.texture2D = EditorGUILayout.ObjectField("Texture2D: ", tileMap.texture2D, typeof(Texture2D), false) as Texture2D;

        if(tileMap.texture2D == null )
        {
            EditorGUILayout.HelpBox("텍스쳐를 선택하세요.", MessageType.Warning); // MessageType.Warning : 오류 뜸
        }
        else
        {
            EditorGUILayout.LabelField($"Tile Size: { tileMap.tileSize }");
        }


        EditorGUILayout.EndVertical();
        EditorUtility.SetDirty(tileMap); // 해당 게임오브젝트의 TileMap컴포넌트에 값 저장
    }
}
