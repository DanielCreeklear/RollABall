using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int score;
    public Text scoreText;
    public Text gameOverText;
    public Text countdownText;

    public float Countdown;
    float cooldownTime;

    Rigidbody rb;
    LineRenderer lineRenderer;

    Vector3 lookDirection = Vector3.forward;
    float lookDistance;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        gameOverText.gameObject.SetActive(false);
        rb = GetComponent<Rigidbody>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Countdown = Mathf.Clamp(Countdown - Time.deltaTime, 0, 100);
        countdownText.text = "Countdown: " + Mathf.CeilToInt(Countdown);

        if (Countdown <= 0 && !gameOverText.gameObject.activeSelf)
        {
            gameOverText.text = "Game Over!";
            gameOverText.gameObject.SetActive(true);
        }

        if (score == 16 && !gameOverText.gameObject.activeSelf)
        {
            gameOverText.text = "You win!";
            gameOverText.gameObject.SetActive(true);
        }

        //Vector3[] lineVertices = { new Vector3(-7, 0.5f, 7), new Vector3(7, 0.5f, -7) };
        //lineRenderer.SetPositions(lineVertices);
        //lineRenderer.startWidth = lineRenderer.endWidth = 0.1f;
    }

    private void LateUpdate()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, transform.position + lookDirection * lookDistance);
        lineRenderer.startWidth = lineRenderer.endWidth = 0.1f;
    }

    private void FixedUpdate()
    {
        // Picking
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << LayerMask.NameToLayer("Selectable");
        if (Physics.Raycast(mouseRay, out RaycastHit hit, Mathf.Infinity, layerMask))
        {
            hit.transform.GetComponent<Rotator>().speed++;
        }

        // Create look-at ray
        Ray playerRay = new Ray(transform.position, lookDirection);
        if (Physics.Raycast(playerRay, out hit, Mathf.Infinity))
        {
            lookDistance = hit.distance;

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Selectable"))
            {
                lookDirection = (hit.transform.position - transform.position).normalized;
                rb.AddForce(lookDirection * speed);
                cooldownTime = 0.25f;
            }
        }

        if (cooldownTime == 0)
        {
            // Look around
            lookDirection = Quaternion.Euler(0, Time.fixedDeltaTime * 180, 0) * lookDirection;
        }
        else
        {
            // Deaccelerate when cooling down
            rb.velocity *= .95f;
        }

        cooldownTime = Mathf.Clamp(cooldownTime - Time.fixedDeltaTime, 0, Mathf.Infinity);

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 force = new Vector3(x, 0, y);
        force.Normalize();
        force *= speed;
        force = Quaternion.Euler(0, Camera.main.transform.localEulerAngles.y, 0) * force;

        rb.AddForce(force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickUp"))
        {
            //other.gameObject.SetActive(false); // Deactivate gameObject
            Destroy(other.gameObject); // Destroy gameObject

            score++;
            scoreText.text = "Points: " + score;
        }
    }
}
