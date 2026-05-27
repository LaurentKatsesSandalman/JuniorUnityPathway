using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    private bool isOnGround;
    public bool gameOver = false;
    private Color death = Color.red;
    private MeshRenderer playerMesh;
    private Material playerMaterial;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       playerMesh = GetComponent<MeshRenderer>();
        playerMaterial = playerMesh.material;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }
        else if (collision.gameObject.CompareTag("Death"))
        {
            gameOver = true;
            playerMaterial.color = death;
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
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
        
    }
}
