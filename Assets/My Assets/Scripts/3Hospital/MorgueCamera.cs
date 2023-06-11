using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MorgueCamera : MonoBehaviour
{
    private void Update()
    {
        if (CameraControllerHosp.CameraNum == 3)
        {
            if (Input.GetKey(KeyCode.D) && transform.eulerAngles.y < 139)
            {
                transform.Rotate(new Vector3(0, 7.5f * Time.deltaTime, 0));
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKey(KeyCode.A) && transform.eulerAngles.y > 43)
            {
                transform.Rotate(-new Vector3(0, 7.5f * Time.deltaTime, 0));
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }
        else if (CameraControllerHosp.CameraNum == 1)
        {
            float CurrentCameraAngle = transform.eulerAngles.y;
            if (CurrentCameraAngle < 200)
                CurrentCameraAngle += 360;
            if (Input.GetKey(KeyCode.D) && CurrentCameraAngle < 405)
            {
                transform.Rotate(new Vector3(0, 7.5f * Time.deltaTime, 0));
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKey(KeyCode.A) && CurrentCameraAngle > 310)
            {
                transform.Rotate(-new Vector3(0, 7.5f * Time.deltaTime, 0));
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }
        }
    }
}
