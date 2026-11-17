using UnityEngine;

public class DestroyOffscreen : MonoBehaviour
{
    private float distance = 7.5f;

    void Update()
    {
        if (Vector3.Distance(Vector3.zero, transform.position) > distance)
        {
            Destroy(gameObject);
        }
    }
}
