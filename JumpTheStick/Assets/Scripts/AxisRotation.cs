using UnityEngine;

public class AxisRotation : MonoBehaviour

{
    public float degreesPerSecond = 15f;
    private PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerController.gameOver)
        {
            transform.Rotate(Vector3.up, degreesPerSecond * Time.deltaTime);
        }
    }
}
