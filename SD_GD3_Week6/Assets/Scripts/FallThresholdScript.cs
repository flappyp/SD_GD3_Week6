using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallThresholdScript : MonoBehaviour
{
    public float fallThreshold; // this allows me to set the fall Threshold in the inspector
   

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < fallThreshold) // if the object with this script attached to it falls beneath the threshold then the object gets destroyed
            Destroy(gameObject);
    }
}
