using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;
    private float spacebarDelay = 1.0f;
    private float currentDelay = 0.0f;

    // Update is called once per frame
    void Update()
    {
        currentDelay += Time.deltaTime;
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && currentDelay >= spacebarDelay)
        {
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
            currentDelay = 0f;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(currentDelay);
        }
        
    }
}
