using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float rotSensitivity;
    public bool aimAssist;

    private float oldrotSensitivity;

    [SerializeField]
    private Camera Cam;

    [SerializeField]
    private Transform playerBody;

    private float xAxisClamp;

    private void Awake()
    {
        lockCursor();
        xAxisClamp = 0.0f;

    }

    private void Start()
    {
        oldrotSensitivity = rotSensitivity;
    }

    private void Update()
    {
        cameraRotation();
        if (aimAssist)
        {
            aimAssistance();
        }
    }

    private void aimAssistance()
    {

        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
        if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, 100.0f, layerMask))
        {
            Debug.Log(hit.transform.tag);
            if (hit.transform.tag == "Enemy")
            {
                rotSensitivity = oldrotSensitivity / 2;
            }
            else
            {
                rotSensitivity = oldrotSensitivity;
            }
        }
        else
        {
            rotSensitivity = oldrotSensitivity;
        }
    }

    private void lockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void cameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * rotSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * rotSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            clampXAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            clampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void clampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
