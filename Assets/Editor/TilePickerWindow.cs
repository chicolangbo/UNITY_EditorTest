using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TilePickerWindow : EditorWindow
{
    public enum Scale
    {
        x1, x2, x3, x4, x5
    }

    Scale scale = Scale.x1;
    Vector2 scrollPosition;

    [MenuItem("Window/Tile Picker")]
    public static void Open()
    {
        var window = EditorWindow.GetWindow<TilePickerWindow>(); // 이미 생성되어 있으면 넘겨주고, 없으면 생성해서 넘겨줌
        var title = new GUIContent();
        title.text = "Tile Picker";
        window.titleContent = title;
    }

    private void OnGUI() // window 내에 draw
    {
        // 에디터 상에서 선택된 게임오브젝트
        if(Selection.activeGameObject == null)
        {
            return;
        }

        var tileMap = Selection.activeGameObject.GetComponent<TileMap>();
        if(tileMap == null)
        {
            return;
        }

        var texture2D = tileMap.texture2D;
        if(texture2D == null)
        {
            return;
        }

        scale = (Scale)EditorGUILayout.EnumPopup("Zoom", scale);
        var offset = new Vector2(10, 25);
        var rect = new Rect(0,0,texture2D.width * (int)(scale + 1),texture2D.height * (int)(scale + 1)); // 텍스쳐를 그리는 영역

        var viewPort = new Rect(offset.x, offset.y, position.width - offset.x, position.height - offset.y); // position = 현재 윈도우의 사이즈
        var contentRect = new Rect(0, 0, offset.x + rect.width, offset.y + rect.height);
        scrollPosition = GUI.BeginScrollView(viewPort, scrollPosition, contentRect);
        GUI.DrawTexture(rect, texture2D);
        GUI.EndScrollView();

        var boxTexture = new Texture2D(1, 1);
        var color = Color.blue;
        color.a = 0.3f;
        boxTexture.SetPixel(0, 0, color);
        boxTexture.Apply();

        var style = new GUIStyle(GUI.skin.customStyles[0]); // 기본값 세팅 후 원하는 것만 바꾸도록
        style.normal.background = boxTexture;

        var currentEvent = Event.current;
        var mousePos = currentEvent.mousePosition;
        mousePos.x += scrollPosition.x - offset.x;
        mousePos.y += scrollPosition.y - offset.y;

        var scaledSize = tileMap.tileSize * ((int)scale + 1);
        scaledSize += tileMap.tilePadding * ((int)scale + 1);

        var selectTileIndex = Vector2Int.zero;
        selectTileIndex.x = Mathf.FloorToInt(mousePos.x / scaledSize.x);
        selectTileIndex.y = Mathf.FloorToInt(mousePos.y / scaledSize.y);

        var highlightRect = 
            new Rect(selectTileIndex.x * scaledSize.x + offset.x,
                     selectTileIndex.y * scaledSize.y + offset.y,
                     scaledSize.x, scaledSize.y);
        GUI.Box(highlightRect, "", style);
        Repaint();
        Debug.Log(scrollPosition);
    }
}
