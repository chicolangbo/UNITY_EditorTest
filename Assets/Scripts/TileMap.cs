using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
    public Vector2 mapSize = new Vector2(20, 10);
    public Texture2D texture2D; // Ÿ�� ��ü �̹���
    public Vector2 tileSize; // Ÿ�� �ϳ��� ������
    public Vector2 tileOffset;
    public Vector2 tilePadding;
    public Sprite[] sprites; // slice�� Ÿ�� �ϳ��� �迭�� �߰�
    public Vector2 gridSize; // ��ü ������
    public int pizelToUnits;

    public int tileId; // sprites�� index
    public GameObject tiles; // ���ӿ�����Ʈ �θ� ��ġ��
}
