using UnityEngine;

public class NPCJumpOnApproach : MonoBehaviour
{
    public float triggerDistance = 5f;
    public float resetDistance = 8f;
    public float jumpForce = 10f;
    public bool isDead = false;

    [SerializeField]
    private float intendedChancePercent = 50f;

    private enum JumpBehavior { Normal, HalfDistance, OneAndHalfDistance, NoJump }
    private JumpBehavior currentBehavior;

    private Transform clockHand;
    private Rigidbody rb;
    private bool hasJumped = false;
    private float radiusFromCenter;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GameObject ch = GameObject.Find("ClockHand");
        if (ch != null)
        {
            clockHand = ch.transform;
        }

        // Fixed distance from the world Y-axis (the circle's center); this NPC always
        // sits at this radius, so it only needs to be computed once.
        radiusFromCenter = new Vector2(transform.position.x, transform.position.z).magnitude;

        RollBehavior();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "ClockHand")
        {
            isDead = true;
        }
    }

    void Update()
    {
        if (isDead) return;
        if (clockHand == null || rb == null) return;
        if (currentBehavior == JumpBehavior.NoJump) return;

        float effectiveTriggerDistance = triggerDistance;
        if (currentBehavior == JumpBehavior.HalfDistance)
        {
            effectiveTriggerDistance = triggerDistance / 2f;
        }
        else if (currentBehavior == JumpBehavior.OneAndHalfDistance)
        {
            effectiveTriggerDistance = triggerDistance * 1.5f;
        }

        // See the fixed-radius shortcut explained previously: rather than projecting
        // onto the full bar segment, jump straight to the point on the bar sitting at
        // this NPC's own radius.
        float offsetAlongBar = radiusFromCenter - clockHand.localScale.y;
        Vector3 checkPoint = clockHand.position + clockHand.up * offsetAlongBar;
        float distance = Vector3.Distance(transform.position, checkPoint);

        if (distance <= effectiveTriggerDistance && !hasJumped)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            hasJumped = true;
            RollBehavior();
        }
        else if (distance > resetDistance)
        {
            hasJumped = false;
        }
    }

    private void RollBehavior()
    {
        float otherChance = (100f - intendedChancePercent) / 3f;
        float roll = Random.Range(0f, 100f);

        if (roll < intendedChancePercent)
        {
            currentBehavior = JumpBehavior.Normal;
        }
        else if (roll < intendedChancePercent + otherChance)
        {
            currentBehavior = JumpBehavior.HalfDistance;
        }
        else if (roll < intendedChancePercent + 2f * otherChance)
        {
            currentBehavior = JumpBehavior.OneAndHalfDistance;
        }
        else
        {
            currentBehavior = JumpBehavior.NoJump;
        }
    }
}
