using UnityEngine;

public class OctreeEntry<T>
{
    public Bounds bounds { get; private set; }
    public T entry { get; set; }
    public Vector3i cell { get; private set; }

    public OctreeEntry()
    {
        cell = new Vector3i();

        bounds = new Bounds();
    }

    public void Initialize(T entry, int cellX, int cellY, int cellZ, Vector3 min, Vector3 max)
    {
        this.entry = entry;

        cell.Set(cellX, cellY, cellZ);

        bounds.SetMinMax(min, max);
    }

    public void DrawWireFrame(Color color)
    {
        Gizmos.color = color;

        Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
    }

    public void Clean()
    {
        entry = default(T);
    }
}
