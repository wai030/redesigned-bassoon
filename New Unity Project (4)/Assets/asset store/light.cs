using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class light : MonoBehaviour
{
    // Start is called before the first frame update
    Light a;

    private void Awake()
    {
        a = gameObject.GetComponent<Light>();
    }
    void Start()
    {
        a.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
