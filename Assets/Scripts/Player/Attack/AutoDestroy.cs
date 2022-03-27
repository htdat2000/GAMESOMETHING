using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DestroyThisObject()
    {
        Destroy(gameObject);
    }
    public void DestroyThisParent()
    {
        GameObject parent = GetComponentInParent<GameObject>();
        Destroy(parent);
    }
}
