using UnityEngine;

public class ClockHandRotator : MonoBehaviour
{
    public float degreesPerSecond = 10f;
    public Vector3 pivotPoint = Vector3.zero;
    public float stopDelayAfterPlayerDeath = 2f;

    private Rigidbody rb;
    private PlayerJump player;
    private bool playerDeathTimerRunning = false;
    private float deathTimer = 0f;
    private bool stopped = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }

        GameObject playerObject = GameObject.Find("Player");
        if (playerObject != null)
        {
            player = playerObject.GetComponent<PlayerJump>();
        }
    }

    void FixedUpdate()
    {
        if (stopped) return;

        if (player != null && player.isDead)
        {
            if (!playerDeathTimerRunning)
            {
                playerDeathTimerRunning = true;
                deathTimer = 0f;
            }

            deathTimer += Time.fixedDeltaTime;
            if (deathTimer >= stopDelayAfterPlayerDeath)
            {
                stopped = true;
                return;
            }
        }

        float angle = degreesPerSecond * Time.fixedDeltaTime;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.up);
        Vector3 newPosition = rot * (transform.position - pivotPoint) + pivotPoint;
        Quaternion newRotation = rot * transform.rotation;

        if (rb != null)
        {
            rb.MoveRotation(newRotation);
            rb.MovePosition(newPosition);
        }
        else
        {
            transform.position = newPosition;
            transform.rotation = newRotation;
        }
    }
}
