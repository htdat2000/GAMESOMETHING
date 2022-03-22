using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChecker : MonoBehaviour
{
    [SerializeField] private Animator notiAnim;
    private float lastChase;
    private const float CHASE_REFRESH = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        lastChase = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && lastChase + CHASE_REFRESH < Time.time)
            {
                //Debug.Log("here");
                lastChase = Time.time;
                this.transform.parent.GetComponent<PathMover>().Chase();
                notiAnim.Play("Show");
            }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            lastChase = Time.time;
            this.transform.parent.GetComponent<PathMover>().StopChasing();
            notiAnim.Play("Hide");
        }
    }
}
