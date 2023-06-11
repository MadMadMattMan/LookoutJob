using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlenderInBedSpawnScript : MonoBehaviour
{

    private float location = 0;

    // Update is called once per frame
    void Update()
    {
        if (location < 0.26f)
        {
            location += Time.deltaTime/4;
            this.GetComponent<Transform>().position = new Vector3(this.transform.position.x, this.transform.position.y + Time.deltaTime/4, this.transform.position.z);
        }
    }
}
