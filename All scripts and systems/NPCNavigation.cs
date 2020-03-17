using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Navigation randomly between provided points 
/// Ryan Quistorff
/// </summary>
public class NPCNavigation : MonoBehaviour {

    public List<Transform> positions;
    int goalIndex;
    Random r;
    NavMeshAgent agent;
    Animator animator;

    void Start()
    {
        r = new Random();
        animator = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating("StartRunning", 0, 10);
    }

    // Update is called once per frame
    void Update () {
		if (agent.remainingDistance <= 0.1f)
        {
            animator.SetBool("isRunning", false);
        }
	}

    void StartRunning()
    {
        animator.SetBool("isRunning", true);
        Invoke("StartMovingToGoal", 1);
        agent.destination = positions[GetNextGoal()].position;
        agent.isStopped = true;
    }

    void StartMovingToGoal()
    {
        agent.isStopped = false;
    }

    private int GetNextGoal()
    {
        int result = goalIndex;
        while(result == goalIndex)
        {
            result = Random.Range(0, positions.Count);
        }
        goalIndex = result;
        return result;
    }

}
