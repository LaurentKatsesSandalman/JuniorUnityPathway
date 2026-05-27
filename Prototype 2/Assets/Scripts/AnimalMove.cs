using UnityEngine;

public class AnimalMove : MonoBehaviour
{
    public float moveSpeed = 5f;    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed);
    }
}
