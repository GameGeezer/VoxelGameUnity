using UnityEngine;

public class MainBehaviour : MonoBehaviour {
    Octree<int> octree = new Octree<int>(new Vector3(4, 4, 4));
    // Use this for initialization
    void Start () {

        
        octree.SetAt(1, 1, 1, 3);
        octree.SetAt(-1, -1, -1, 4);

        int v = octree.GetAt(1, 1, 1).entry;
        v = octree.GetAt(-1, -1, -1).entry;
    }
    public void OnDrawGizmos()
    {
        if (octree != null)
        {
            octree.DrawGizmos();
        }

    }

    // Update is called once per frame
    void Update () {
	    
	}
}
