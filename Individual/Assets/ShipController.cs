using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    Rigidbody bod;
    Vector3 euler;
    // Start is called before the first frame update
    void Start()
    {
        bod = GetComponent<Rigidbody>();
        euler = new Vector3(0, 0, 100);
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        Quaternion rotation = Quaternion.Euler(euler * h);
        Debug.Log("h: " + h);
        bod.MoveRotation(bod.rotation * rotation);

    }
}
