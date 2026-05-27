using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed = 1f;
    private float horizontalInput;
    private float midSize = 10;
    public GameObject projectilePrefab;
    private Vector3 projectileOffset = new Vector3(0, 0, 2);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * playerSpeed);
        // keep the player on screen with ability to loop around the screen
        if (transform.position.x< -midSize)
        {
            transform.position = new Vector3(midSize, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > midSize)
        {
            transform.position = new Vector3(-midSize, transform.position.y, transform.position.z);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
           Instantiate(projectilePrefab, transform.position + projectileOffset, projectilePrefab.transform.rotation);
        }
    }
}
