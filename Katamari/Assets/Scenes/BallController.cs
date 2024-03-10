using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float rollSpeed;
    [SerializeField] private Rigidbody rb;

    private float size; //size of the ball


    // Start is called before the first frame update
    void Start()
    {
        size = transform.localScale.magnitude;
    }

    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        Vector3 movement = (input.z * cameraTransform.forward + input.x * cameraTransform.right).normalized;

        // Using a logarithmic scale for speed adjustment
        float speedModifier = Mathf.Log(size + 1); // +1 to avoid log(0)
        rb.AddForce(movement * rollSpeed * Time.fixedDeltaTime * speedModifier);
    }

    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Prop") &&  collision.transform.localScale.magnitude <= size)
        {
            collision.transform.parent = transform;
            size += collision.transform.localScale.magnitude;

            // Scale up the ball
            float growthFactor = 0.1f; // Adjust this value as needed
            transform.localScale += Vector3.one * growthFactor * collision.transform.localScale.magnitude;
        }
    }
}
