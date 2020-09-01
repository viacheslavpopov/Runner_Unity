using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float animationDuration = 3f;

    private Transform playerLocation;
    private Vector3 startOffset;
    private Vector3 moveVector;
    private Vector3 animationOffset = new Vector3(0, 4f, 0);


    private float transition;
    // Start is called before the first frame update


    void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;
        startOffset = transform.position - playerLocation.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = playerLocation.position + startOffset;
        moveVector.x = 0;
        moveVector.y = Mathf.Clamp(moveVector.y, 1, 6);

        if (transition > 1f)
        {
            transform.position = moveVector;
        }
        else
        {

            transform.position = Vector3.Lerp(moveVector + animationOffset, moveVector, transition);
            transition += Time.deltaTime * 1 / AnimationDuration;
            transform.LookAt(playerLocation.position + Vector3.up);
        }
    }

    
    public float AnimationDuration { get => animationDuration;}
}
