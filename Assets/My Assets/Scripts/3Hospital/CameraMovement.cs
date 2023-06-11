using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public int SpeedMultiplier;

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            SpeedMultiplier = 5;
        }
        else
        {
            SpeedMultiplier = 1;
        }

        if (CameraControllerHosp.CameraNum == 3) //Morgue Room
        {
            if (Input.GetKey(KeyCode.D) && transform.eulerAngles.y < 139)
            {
                transform.Rotate(new Vector3(0, 7.5f * Time.deltaTime * SpeedMultiplier, 0));
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKey(KeyCode.A) && transform.eulerAngles.y > 43)
            {
                transform.Rotate(-new Vector3(0, 7.5f * Time.deltaTime * SpeedMultiplier, 0));
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }

            if (Input.GetKey(KeyCode.W) && transform.eulerAngles.x > 0)
            {
                transform.Rotate(-new Vector3(7.5f * Time.deltaTime * SpeedMultiplier, 0, 0));
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKey(KeyCode.S) && transform.eulerAngles.x < 50)
            {
                transform.Rotate(new Vector3(7.5f * Time.deltaTime * SpeedMultiplier, 0, 0));
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponent<Camera>().fieldOfView < 89)
            {
                GetComponent<Camera>().fieldOfView += Time.deltaTime * 1000;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && GetComponent<Camera>().fieldOfView > 21)
            {
                GetComponent<Camera>().fieldOfView -= Time.deltaTime * 1000;
            }
        }
        else if (CameraControllerHosp.CameraNum == 1)
        {
            float CurrentCameraAngle = transform.eulerAngles.y;
            if (CurrentCameraAngle < 200)
                CurrentCameraAngle += 360;
            if (Input.GetKey(KeyCode.D) && CurrentCameraAngle < 405)
            {
                transform.Rotate(new Vector3(0, 7.5f * Time.deltaTime * SpeedMultiplier, 0));
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKey(KeyCode.A) && CurrentCameraAngle > 310)
            {
                transform.Rotate(-new Vector3(0, 7.5f * Time.deltaTime * SpeedMultiplier, 0));
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }

            if (Input.GetKey(KeyCode.W) && transform.eulerAngles.x > 1)
            {
                transform.Rotate(-new Vector3(7.5f * Time.deltaTime * SpeedMultiplier, 0, 0));
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKey(KeyCode.S) && transform.eulerAngles.x < 50)
            {
                transform.Rotate(new Vector3(7.5f * Time.deltaTime * SpeedMultiplier, 0, 0));
                GetComponent<AudioSource>().Play();
            }
            else
            {
                GetComponent<AudioSource>().Stop();
            }

            if (Input.GetAxis("Mouse ScrollWheel") < 0 && GetComponent<Camera>().fieldOfView < 89)
            {
                GetComponent<Camera>().fieldOfView += Time.deltaTime * 1000;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") > 0 && GetComponent<Camera>().fieldOfView > 21)
            {
                GetComponent<Camera>().fieldOfView -= Time.deltaTime * 1000;
            }
        }

        if (transform.eulerAngles.z > 0.25f && transform.eulerAngles.z < 6)
        {
            transform.Rotate(-new Vector3(0, 0, Time.deltaTime * 4 * SpeedMultiplier));
        }
        else if (transform.eulerAngles.z > 254 && transform.eulerAngles.z < 359.75f)
        {
            transform.Rotate(new Vector3(0, 0, Time.deltaTime * 4 * SpeedMultiplier));
        }
    }
}
