using System.Collections;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public Grid Grid;

    public float MoveSpeed = 5;

    public bool UseSmartGoal = false;

    private bool[,] _swept;

    private int _rows;

    private int _columns;

    private int _currentRow;

    private int _currentColumn;

    void Start()
    {
        _rows = Grid.Tiles.Length;

        _columns = Grid.Tiles[0].Length;

        _currentRow = _rows / 2;

        _currentColumn = _columns / 2;

        _swept = new bool[_rows, _columns];

        GridTile firstTile = Grid.Tiles[_currentRow][_currentColumn];

        transform.position = firstTile.Position;

        _swept[_currentRow, _currentColumn] = true;

        firstTile.Clean();

        StartCoroutine(Sweep());
    }

    IEnumerator Sweep()
    {
        int row, column;

        while (UseSmartGoal ? SmartGoal(out row, out column) : Goal(out row, out column))
        {
            int rowDiff = _currentRow - row;

            int columnDiff = _currentColumn - column;

            if (Mathf.Abs(rowDiff) >= Mathf.Abs(columnDiff))
            {
                if (rowDiff < 0)
                {
                    ++_currentRow;
                }
                else
                {
                    --_currentRow;
                }
            }
            else
            {
                if (columnDiff < 0)
                {
                    ++_currentColumn;
                }
                else
                {
                    --_currentColumn;
                }
            }

            yield return StartCoroutine(MoveToPoint());
        }
    }

    private bool Goal(out int row, out int column)
    {
        GridTile targetTile = null;

        row = column = -1;

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                GridTile currentTile = Grid.Tiles[i][j];

                if (!_swept[i, j])
                {
                    if (targetTile == null || currentTile.Distance(transform) < targetTile.Distance(transform))
                    {
                        targetTile = currentTile;

                        row = i;
                        column = j;
                    }
                }
            }
        }

        return targetTile != null;
    }

    private bool SmartGoal(out int row, out int column)
    {
        GridTile targetTile = null;

        row = column = -1;

        for (int i = 0; i < _rows; i++)
        {
            for (int j = 0; j < _columns; j++)
            {
                GridTile currentTile = Grid.Tiles[i][j];

                if (currentTile.IsDirty)
                {
                    if (targetTile == null || currentTile.Distance(transform) < targetTile.Distance(transform))
                    {
                        targetTile = currentTile;

                        row = i;
                        column = j;
                    }
                }
            }
        }

        return targetTile != null;
    }

    IEnumerator MoveToPoint()
    {
        GridTile currentTile = Grid.Tiles[_currentRow][_currentColumn];

        Vector3 position = currentTile.Position;

        while (Vector3.Distance(transform.position, position) > .01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, position, MoveSpeed * Time.deltaTime);

            yield return new WaitForEndOfFrame();
        }

        currentTile.Clean();

        _swept[_currentRow, _currentColumn] = true;
    }
}
