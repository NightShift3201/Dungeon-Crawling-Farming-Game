using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float destroyTime = 1f;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
        transform.localPosition += offset;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position+= new Vector3(0, 1f)*Time.deltaTime;
    }
}
