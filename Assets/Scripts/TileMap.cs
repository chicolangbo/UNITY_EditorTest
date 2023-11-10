using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public Vector2 mapSize = new Vector2(20, 10);
    public Texture2D texture2D; // 타일 전체 이미지
    public Vector2 tileSize; // 타일 하나의 사이즈
    public Vector2 tileOffset;
    public Vector2 tilePadding;
    public Sprite[] sprites; // slice된 타일 하나씩 배열로 추가
    public Vector2 gridSize; // 전체 사이즈
    public int pizelToUnits;

    public int tileId; // sprites의 index
    public GameObject tiles; // 게임오브젝트 부모 배치용
}
