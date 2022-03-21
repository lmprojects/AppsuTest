using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class MovableObject : MonoBehaviour
{
    public Action<float> DistanceUpdate;
    private List<Vector3> moves;
    private float lerpTime = 1.5f;
    private float lerpTimeDelta = 4f;
    private float t = 0;
    private Vector3 prevPos;
    private bool isSmooth = false;

    // Start is called before the first frame update
    void Start()
    {
        moves = new List<Vector3>();
        transform.position = Vector3.zero;
        prevPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (moves.Count > 0)
        {
            if (isSmooth)
            {
                lerpTime = lerpTimeDelta / Vector3.Distance(prevPos, moves[0]);
                transform.position = Vector3.Lerp(transform.position, moves[0], lerpTime * Time.deltaTime); 

                t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
                if (t > 0.9f)
                    EndMove();
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, moves[0], 4 * Time.deltaTime);
                if (Vector3.Distance(transform.position, moves[0]) <= 0.0001f)
                    EndMove();
            }
        }
    }

    private void EndMove()
    {
        t = 0;
        moves.RemoveAt(0);
        float distance = Vector3.Distance(prevPos, transform.position);
        DistanceUpdate.Invoke(distance);
        prevPos = transform.position;
    }

    public void AddMove(Vector3 pos)
    {
        isSmooth = false;
        moves.Add(new Vector3(pos.x, pos.y, 0));
    }

    public void Move(Vector3 pos)
    {
        isSmooth = true;
        Stop();
        moves.Add(new Vector3(pos.x, pos.y, 0));
    }

    public void Stop()
    {
        t = 0;
        moves = new List<Vector3>();
        float distance = Vector3.Distance(prevPos, transform.position);
        prevPos = transform.position;
        DistanceUpdate.Invoke(distance);
    }
}
