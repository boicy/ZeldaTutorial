using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    private const string KICK_ACTIVE = "kickActive";

    [Header("Position vars")]
    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    [Header("Animator")]
    public Animator anim;

    [Header("Postion Reset")]
    public VectorValue camMin;
    public VectorValue camMax;

    // Start is called before the first frame update
    void Start () {
        maxPosition = camMax.initialValue;
        minPosition = camMin.initialValue;
        anim = GetComponent<Animator>();
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    // smoothing out the camera jerkiness by using LateUpdate
    void LateUpdate () {
        if (transform.position != target.position) {
            Vector3 targetPosition = new Vector3 (target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp (targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp (targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp (transform.position, targetPosition, smoothing);

        }
    }

    public void BeginKick()
    {
        anim.SetBool(KICK_ACTIVE, true);
        StartCoroutine(KickCoroutine());
    }

    public IEnumerator KickCoroutine()
    {
        yield return null;
        anim.SetBool(KICK_ACTIVE, false);
    }
}