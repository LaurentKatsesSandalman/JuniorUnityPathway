using UnityEngine;

public class ProjectileMove : MonoBehaviour
{
    public float projectileSpeed = 5f;
    // public GameObject player;
    // private Vector3 offset = new Vector3(0, 0, 2);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // transform.position = player.transform.position + offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * projectileSpeed);
    }
}
