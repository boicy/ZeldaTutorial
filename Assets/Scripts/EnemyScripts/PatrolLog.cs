using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{

    [Header("Patrol points")]
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    protected override void DoChasingBehaviour()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position,
                                           target.position,
                                           moveSpeed * Time.deltaTime);
        changeAmim(temp - transform.position);
        myRigidBody.MovePosition(temp);
    }

    protected override void DoRestingBehaviour()
    {
        if (Vector3.Distance(transform.position, path[currentPoint].position) > roundingDistance)
        {
            Vector3 temp = Vector3.MoveTowards(transform.position,
                                        path[currentPoint].position,
                                        moveSpeed * Time.deltaTime);
            changeAmim(temp - transform.position);
            myRigidBody.MovePosition(temp);
        }
        else
        {
            ChangeGoal();
        }
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
