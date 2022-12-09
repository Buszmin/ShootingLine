using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Camera mainCamera;
    //[SerializeField] private Rigidbody rb;
    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 20;

    public Quaternion currentRotation;
    public static PlayerRotation Instance;

    // Start is called before the first frame update
    void Start()
    {
        Instance=this;
    }

    // Update is called once per frame
    void Update()
    {
       // moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
       // moveVelocity = moveInput * moveSpeed;

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(transform.up, transform.position);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(transform.position , pointToLook, Color.cyan, 0, false);

            // transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));

            Vector3 lookVector = (new Vector3(pointToLook.x, transform.position.y, pointToLook.z) - transform.position).normalized;

            Quaternion newRotation = Quaternion.LookRotation(lookVector, Vector3.up);
            currentRotation = Quaternion.Lerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = currentRotation;
        }
    }
}
