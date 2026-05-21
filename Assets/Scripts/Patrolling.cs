using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrolling : MonoBehaviour
{
    [SerializeField] private float timeBetweenActions;
    [SerializeField] private List<Transform> waypoints;
    [SerializeField] private float rotationTime;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool onlyRotation = false;
    
    private Transform desiredTransform;
    private bool reached = true;
    private int index;
    float r;
    private void Start()
    {
        StartCoroutine(PatrolRoutine());
    }
    private IEnumerator PatrolRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(timeBetweenActions);

        while (true)
        {

            if (waypoints.Count == 0)
                break;

            if (index >= waypoints.Count)
                index = 0;
            desiredTransform = waypoints[index];
            reached = false;
            index++;

            yield return wait;
        }
    }

    private void Update()
    {
        if (waypoints.Count == 0)
            return;
        if(reached == false)
        {
            Rotate();
            if (onlyRotation == false)
                Move();
        }
    }

    private void Rotate()
    {
        float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.z, desiredTransform.eulerAngles.z, ref r, rotationTime);
        transform.rotation = Quaternion.Euler(0, 0, Angle);
    }
    private void Move()
    {
        Vector3 dir = desiredTransform.position - transform.position;
        transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, desiredTransform.position) <= 0.2f)
        {
            transform.position = desiredTransform.position;
            reached = true;
        }
    }
}
