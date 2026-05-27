using System.Collections;
using UnityEngine;

public class NpcController : MonoBehaviour
{
    private PlayerController playerController;
    private bool isOnGround;
    private Rigidbody npcRb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        npcRb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
         
    }

    private void OnTriggerEnter(Collider other)
    {

        StartCoroutine(NpcJump());
    }

    private IEnumerator NpcJump()
    {
        yield return new WaitForSeconds(Random.Range(0f, 1f));
        if (isOnGround)
        {
            npcRb.AddForce(Vector3.up * playerController.jumpForce, ForceMode.Impulse);
        }
    }
}
