using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theCube : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Vector3.right;
        }

        transform.Translate(moveDirection.normalized * moveSpeed * Time.deltaTime);
    }
}
