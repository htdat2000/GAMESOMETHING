using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChecker : MonoBehaviour
{
    [SerializeField] private Animator notiAnim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            {
                //Debug.Log("here");
                this.transform.parent.GetComponent<PathMover>().Chase();
                notiAnim.Play("Show");
            }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            this.transform.parent.GetComponent<PathMover>().StopChasing();
            notiAnim.Play("Hide");
        }
    }
}
