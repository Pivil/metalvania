using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftLegAnimation : MonoBehaviour
{
    private SpriteRenderer _leftLeg;
    // Start is called before the first frame update
    void Start()
    {
        _leftLeg = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion theRotation = _leftLeg.transform.localRotation;
        theRotation.z *= 270;
        _leftLeg.transform.localRotation = theRotation;
    }
}
