using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChecker : MonoBehaviour
{
    [SerializeField] protected Animator notiAnim;
    // private float lastChase;
    // private const float CHASE_REFRESH = 0.25f;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        // lastChase = 0f;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            {
                //Debug.Log("here");
                // lastChase = Time.time;
                this.transform.parent.GetComponent<PathMover>().Chase();
                notiAnim.Play("Show");
            }
    }
    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            // lastChase = Time.time;
            this.transform.parent.GetComponent<PathMover>().StopChasing();
            notiAnim.Play("Hide");
        }
    }
}
