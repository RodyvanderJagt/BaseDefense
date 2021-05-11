using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleOutOfBounds : MonoBehaviour
{
    [SerializeField] float maxZ = 1000;
    [SerializeField] float minZ = -50;

    void Update()
    {
        if (transform.position.z < minZ || transform.position.z > maxZ)
        {
            this.gameObject.SetActive(false);
        }
    }
}
