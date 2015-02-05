using System.Runtime.InteropServices;
using UnityEngine;

public class Grid : MonoBehaviour
{
    private Row[] _rows;

    public GridTile[][] Tiles;

    void Start()
    {
        _rows = GetComponentsInChildren<Row>();

        Tiles = new GridTile[_rows.Length][];

        for (int i = 0; i < _rows.Length; i++)
        {
            Row row = _rows[i];

            Tiles[i] = new GridTile[row.Childs.Length];

            for (int j = 0; j < row.Childs.Length; j++)
            {
                Tiles[i][j] = row.Childs[j];
            }
        }
    }
}
