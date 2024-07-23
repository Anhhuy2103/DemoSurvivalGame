using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private float anglePerSec;
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float minPitch;
    [SerializeField] private float maxPitch;

    [SerializeField] private float pitch;
    private void Start()
    {
        camerainvisible();
    }
    private void Update()
    {
        UpdateYaw();
        UpdatePitch();
    }

    private void UpdateYaw()
    {
        float mouseX = Input.GetAxis("Mouse X");

        float yaw = mouseX * anglePerSec * Time.deltaTime;
        transform.Rotate(0, yaw, 0);
    }
    private void UpdatePitch()
    {
        float mouseY = Input.GetAxis("Mouse Y");

        float deltaPitch = -mouseY * anglePerSec * Time.deltaTime;
        pitch = Mathf.Clamp(pitch + deltaPitch, minPitch, maxPitch);
        cameraHolder.localEulerAngles = new Vector3(pitch, 0, 0);

    }
    private void camerainvisible()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
