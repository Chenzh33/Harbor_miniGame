using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OWController : MonoBehaviour {
    public GameObject[] switches;
    GameObject guideBall;
    private int currSwitchIdx;
    public float speed = 3.0f;

    public void Awake () {
        currSwitchIdx = 0;
        guideBall = GameObject.Find ("GuideBall");

    }

    public void BeginGuideProcess()
    {
        StartCoroutine(BeginGuideProcessCoro());
       

    }
    IEnumerator BeginGuideProcessCoro()
    {
        Vector2 dist, dir;
        Rigidbody2D rbody = guideBall.GetComponent<Rigidbody2D>();
        while (currSwitchIdx < switches.Length)
        {
            dist = switches[currSwitchIdx].transform.position - guideBall.transform.position ;
            while(dist.magnitude >= 0.2f)
            {
                dist = switches[currSwitchIdx].transform.position - guideBall.transform.position;
                dir = dist.normalized;
                Vector2 newPos = rbody.position + dir * speed * Time.fixedDeltaTime;
                rbody.MovePosition(newPos);
                yield return null;

            }
            OWSwitch ows = switches[currSwitchIdx].GetComponent<OWSwitch>();
            Animator ator = switches[currSwitchIdx].GetComponent<Animator>();

            ows.SetCurrent();
            while(!ator.GetBool("activated"))
            {
                yield return null;
            }
            ator.SetBool("triggered", true);
            ows.RemoveCurrent();


            ++currSwitchIdx;
            yield return new WaitForSeconds(2f);
        }
        GameManager.Instance.LeaveOtherWorld();
    }
}