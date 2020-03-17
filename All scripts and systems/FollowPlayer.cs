using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    private GameObject player;
    private Vector3 previousPosition;
    private bool moving;
    private Animator animator;
    private float distanceFromPlayer;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<OVRPlayerController>().gameObject;
        animator = GetComponent<Animator>();
        previousPosition = player.transform.position;
        distanceFromPlayer = Vector3.Magnitude(player.transform.position - transform.position);
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = player.transform.position;
        Vector3 movement = newPosition - previousPosition;
        if (movement.magnitude > 0.001f && !moving) {
            moving = true;
            animator.SetBool("walking", true);
        } else if (movement.magnitude < 0.001f && moving) {
            moving = false;
            animator.SetBool("walking", false);
        }
        float scale = Vector3.Magnitude(player.transform.position - transform.position) - distanceFromPlayer;
        transform.position += scale * movement;
        transform.LookAt(new Vector3(player.transform.position.x, 0, player.transform.position.z));
        previousPosition = newPosition;
	}
}
