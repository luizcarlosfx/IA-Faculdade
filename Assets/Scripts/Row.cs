using UnityEngine;
using System.Collections;

public class Row : MonoBehaviour
{
    public int Index { get; private set; }

    public GridTile[] Childs { get; private set; }

    void Awake()
    {
        Index = transform.GetSiblingIndex();

        Childs = GetComponentsInChildren<GridTile>();
    }
}
