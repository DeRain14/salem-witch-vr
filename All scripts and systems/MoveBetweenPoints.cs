using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveBetweenPoints : MonoBehaviour {

    Animator anim;
    Vector3 goalPosition;
    Vector3 startPosition;
    NavMeshAgent agent;

    bool movingToPosition;
    bool atPosition;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.isStopped = true;
        goalPosition = transform.Find("EndPosition").position;
        startPosition = transform.Find("StartPosition").position;
        transform.position = startPosition;
    }
	
	// Update is called once per frame
	void Update () {
        if (agent.remainingDistance <= 0.1f)
        {
            anim.SetBool("isWalking", false);
            atPosition = true;
            movingToPosition = false;
            agent.isStopped = true;
        }
    }

    public void MoveToGoal() {
        anim.SetBool("isWalking", true);
        agent.destination = goalPosition;
        agent.isStopped = false;
    }

    public void MoveToStart() {
        anim.SetBool("isWalking", true);
        agent.destination = startPosition;
        agent.isStopped = false;
    }
}
