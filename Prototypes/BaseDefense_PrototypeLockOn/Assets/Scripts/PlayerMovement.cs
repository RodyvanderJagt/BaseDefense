using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] float rotationMinX;
    [SerializeField] float rotationMaxX;
    [SerializeField] float rotationCapY;

    void Update()
    {
        ConstrainPlayerMovement();
        MovePlayer();
    }
    void MovePlayer()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, verticalInput * rotationSpeed * Time.deltaTime, Space.Self);
    }

    void ConstrainPlayerMovement()
    {
        //Constrain horizontal rotation
        if (transform.eulerAngles.y > rotationCapY && transform.eulerAngles.y < 180)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationCapY, transform.eulerAngles.z);
        }
        if (transform.eulerAngles.y < 360 - rotationCapY && transform.eulerAngles.y > 180)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -rotationCapY, transform.eulerAngles.z);
        }

        //Constrain vertical rotation
        if (transform.localEulerAngles.x > rotationMaxX && transform.localEulerAngles.x < 180)
        {
            transform.localEulerAngles = new Vector3(rotationMaxX, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        if (transform.localEulerAngles.x < 360 - rotationMinX && transform.localEulerAngles.x > 180)
        {
            transform.localEulerAngles = new Vector3(360 - rotationMinX, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
    }
}
