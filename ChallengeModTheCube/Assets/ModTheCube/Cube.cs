using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public MeshRenderer Renderer;
    public Vector3 furthestPosition = new Vector3(7, 8, 9);
    public float biggestScale = 4f;
    public float smallestScale = 0.5f;
   // public float startOpacity = 0.4f;
    //public Color startColor = new Color(0.5f, 1.0f, 0.3f);
    public float maxRotationSpeed = 500.0f;
    public float minRotationSpeed = 10.0f;
    private float rotationSpeed;
    private Material material;
    private bool[] increasing = new bool[4] { true, true, true, true };
    private float[] colorComponents = new float[4];

    void Start()
    {
        transform.position = new Vector3(Random.Range(0, furthestPosition.x), Random.Range(0, furthestPosition.y), Random.Range(0, furthestPosition.z));
        transform.localScale = Vector3.one * Random.Range(smallestScale, biggestScale);
        
       material = Renderer.material;

        colorComponents[0] = Random.Range(0f, 1f);
        colorComponents[1] = Random.Range(0f, 1f);
        colorComponents[2] = Random.Range(0f, 1f);
        colorComponents[3] = Random.Range(0f, 1f);

        material.color = new Color(colorComponents[0], colorComponents[1], colorComponents[2], colorComponents[3]);
        rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
    }
    
    void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0.0f, 0.0f);
        for (int i = 0; i < colorComponents.Length; i++) {
            if (colorComponents[i] >= 1.0f)
            {
                increasing[i] = false;
            }
            else if (colorComponents[i] <= 0.0f)
            {
                increasing[i] = true;
            }

            if (increasing[i])
            {
                colorComponents[i] += Time.deltaTime * 0.1f;
            }
            else
            {
                colorComponents[i] -= Time.deltaTime * 0.1f;
            }

        }
        //if(material.color.a >= 1.0f)
        //{
        //    alphaIncreasing = false;
        //}
        //else if(material.color.a <= 0.0f)
        //{
        //    alphaIncreasing = true;
        //}

        //if (alphaIncreasing) { 
        //alpha += Time.deltaTime * 0.1f;
        //} else {
        //    alpha -= Time.deltaTime * 0.1f;
        //}

        //if(material.color.r >= 1.0f)
        //{
        //    redIncreasing = false;
        //}
        //else if (material.color.r <= 0.0f)
        //{
        //    redIncreasing = true;
        //}

        //if(redIncreasing)
        //{
        //    red += Time.deltaTime * 0.1f;
        //}
        //else
        //{
        //    red -= Time.deltaTime * 0.1f;
        //}

        //if(material.color.g >= 1.0f)
        //{
        //    greenIncreasing = false;
        //}
        //else if (material.color.g <= 0.0f)
        //{
        //    greenIncreasing = true;
        //}

        //if(greenIncreasing)
        //{
        //    green += Time.deltaTime * 0.1f;
        //}
        //else
        //{
        //    green -= Time.deltaTime * 0.1f;
        //}

        //if(material.color.b >= 1.0f)
        //{
        //    blueIncreasing = false;
        //}
        //else if (material.color.b <= 0.0f)
        //{
        //    blueIncreasing = true;
        //}
        //if(blueIncreasing)
        //{
        //    blue += Time.deltaTime * 0.1f;
        //}
        //else
        //{
        //    blue -= Time.deltaTime * 0.1f;
        //}

        material.color = new Color(colorComponents[0], colorComponents[1], colorComponents[2], colorComponents[3]);
    }
}
