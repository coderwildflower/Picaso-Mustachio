using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testContacts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (var c in collision.contacts)
        {
            Debug.DrawLine(transform.position, c.point, Color.red);
        }
    }
}
