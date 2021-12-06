using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision2 : MonoBehaviour
{
    public string CollisionTag = "Agent";
    private YT_second _YT_second;
    // Start is called before the first frame update
    
    private void Start()
    {
        //Debug.Log("CollisionDetection.Start()");
        
        _YT_second = transform. parent.GetComponent<YT_second>();
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        
        if (_YT_second == null) { return; }
        if (!_YT_second._reachedTargetforsecond) { return; }
        Debug.Log("C2Work");
        GameObject going = other.gameObject;
        if (going.tag != CollisionTag) { return; }
        _YT_second.StartSecondConfigure(going);
        

    }
}
