using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EraserScript : MonoBehaviour
{
    public float aliveTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        aliveTimer += Time.deltaTime;
        if (aliveTimer >= 0.5f)
        {
            Destroy(this.gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log((other.gameObject.name));
        if (other.gameObject.CompareTag("PenLine"))
        {
            Destroy(other.gameObject);
        }
    }
}
