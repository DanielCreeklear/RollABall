using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime * speed);

        speed = Mathf.Clamp(speed - Time.deltaTime, 1, 10);
    }
}
