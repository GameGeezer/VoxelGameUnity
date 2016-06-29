using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float speed = 5;

    public void Update()
    {
        float x = Input.GetAxis("Horizontal") * speed;

        float y = Input.GetAxis("Vertical") * speed;

        GameObject camera = GameObject.FindGameObjectsWithTag("MainCamera")[0];

        Vector2 forward = new Vector2(camera.transform.forward.x, camera.transform.forward.z);

        transform.Translate(new Vector3(x, 0, y));
    }
}