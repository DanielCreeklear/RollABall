using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Text scoreText;
    public Text countdownText;
    public float speed;

    int score;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // countdownText.text = "Countdown: " + countdown;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        //Debug.Log("x: " + x + "\t y: " + y);

        Vector3 force = new Vector3(x, 0, y);
        force.Normalize();
        force *= speed;

        //transform.position += offset * Time.deltaTime * speed;
        rb.AddForce(force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pick Up"))
        {
            score++;
            scoreText.text = "Score: " + score;
            other.gameObject.SetActive(false);
            // GameObject.Destroy(other.gameObject);
        }
    }
}
