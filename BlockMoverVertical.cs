using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoverVertical: MonoBehaviour
{
    public float zSpot;
    private bool direction;
    public GameObject Spikeball;
    // Start is called before the first frame update
    void Start()
    {
        direction = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (direction == true)
        {
            Vector3 temp = new Vector3(0, 0.1f);
            Spikeball.transform.position -= temp;
        }
        else
        {
            Vector3 temp = new Vector2(0, 0.1f);
            Spikeball.transform.position += temp;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check the provided Collider2D parameter other to see if it is tagged "PickUp", if it is...
        if (other.gameObject.CompareTag("MovingTrigger"))
        {
            if (direction == true)
            {
                direction = false;
            }
            else
            {
                direction = true;
            }
        }
    }
}
