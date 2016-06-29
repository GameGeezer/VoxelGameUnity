using UnityEngine;

// WHAT hapens if node is null? perform a remove operation SetChild

public class OctreeLeafNode<T> : OctreeNode<T>
{
    private OctreeEntry<T>[] children = new OctreeEntry<T>[8];

    public OctreeLeafNode<T> Initialize(Octree<T> treeBase, int xMinimum, int yMinimum, int zMinimum)
    {
        BaseInitialize(treeBase, xMinimum, yMinimum, zMinimum, 0);

        for (int i = 0; i < OctreeConstants.NUMBER_OF_CHILDREN; ++i)
        {
            children[i] = null;
        }

        return this;
    }

    public override OctreeEntry<T> GetAt(int x, int y, int z)
    {
        int index = ChildRelativeTo(x, y, z);

        return children[index];
    }

    public override OctreeEntry<T> SetAt(int x, int y, int z, T value)
    {
        int index = ChildRelativeTo(x, y, z);

        return SetChild(index, value);
    }

    public override void Draw()
    {
        for (int i = 0; i < 8; ++i)
        {
            if (children[i] == null)
            {
                continue;
            }

            children[i].DrawWireFrame(Color.blue);
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(worldBounds.center, worldBounds.extents * 2);
    }

    protected OctreeEntry<T> SetChild(int index, T node)
    {
        if (children[index] != null)
        {
            // The entry already exists
            children[index].entry = node;

            return children[index];
        }

        OctreeEntry<T> fish = treeBase.entryPool.Catch();

        // Find the new child min
        int xMinimum, yMinimum, zMinimum;
        MinOfChildIndex(index, out xMinimum, out yMinimum, out zMinimum);

        // change the min from local to world space
        float xWorldMinimum = xMinimum * treeBase.leafDimensions.x;
        float yWorldMinimum = yMinimum * treeBase.leafDimensions.y;
        float zWorldMinimum = zMinimum * treeBase.leafDimensions.z;

        // change the min from local to world space
        float xWorldMaximum = xWorldMinimum + treeBase.leafDimensions.x;
        float yWorldMaximum = yWorldMinimum + treeBase.leafDimensions.y;
        float zWorldMaximum = zWorldMinimum + treeBase.leafDimensions.z;

        // Initialize the new node with world space bounds
        fish.Initialize(node, xMinimum, yMinimum, zMinimum, new Vector3(xWorldMinimum, yWorldMinimum, zWorldMinimum), new Vector3(xWorldMaximum, yWorldMaximum, zWorldMaximum));

        // Set index to the child
        children[(int)index] = fish;
        // Increase the child counter
        ++childCount;
        // Set the OctryEnty's value to what was passed
        children[(int)index].entry = node;

        return children[(int)index];
    }
}
