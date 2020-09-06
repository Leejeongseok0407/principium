using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synopsis : MonoBehaviour
{
    float velocity = 10.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 1 * Time.deltaTime * velocity, 0);
    }
}
