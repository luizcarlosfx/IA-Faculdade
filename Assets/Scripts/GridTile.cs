using UnityEngine;
using System.Collections;

public class GridTile : MonoBehaviour
{
    public Color CleanColor = Color.white;

    public Color DirtyColor = Color.red;

    public bool IsDirty = false;

    public int ChildIndex { get; private set; }

    public int Row { get; private set; }

    public Material _material;

    public Vector3 Position
    {
        get { return transform.position; }
    }

    void Awake()
    {
        int dirty = Random.Range(0, 2);

        if (dirty >= 1)
        {
            IsDirty = true;
        }

        _material = GetComponent<Renderer>().material;

        _material.color = IsDirty ? DirtyColor : CleanColor;

        ChildIndex = transform.GetSiblingIndex();

        Row = transform.parent.GetSiblingIndex();
    }

    public float Distance(Transform tr)
    {
        return Vector3.Distance(tr.position, transform.position);
    }

    public void Clean()
    {
        IsDirty = false;

        _material.color = CleanColor;
    }
}
