using UnityEngine;

public class GearRotate : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 10 * Time.deltaTime);
    }
}
