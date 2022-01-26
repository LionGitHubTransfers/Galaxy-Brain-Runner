using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTrigger : MonoBehaviour
{
    [SerializeField]
    float dist;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        if (dist < 10.0f)
        {

            Color color = this.GetComponent<Renderer>().material.color;
            if (color.a > 0.01f)
            {
                color.a -= 0.5f * Time.deltaTime;
            }

            this.GetComponent<Renderer>().material.color = color;

            // this.GetComponent<Renderer>().material.SetColor("Color", Color.red);
        }
    }
}
