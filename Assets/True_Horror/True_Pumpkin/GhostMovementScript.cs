using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovementScript : MonoBehaviour
{
    private void Update()
    {
        this.gameObject.GetComponent<Transform>().position = new Vector3(this.transform.position.x, 1.5f, this.transform.position.z);
    }
}
