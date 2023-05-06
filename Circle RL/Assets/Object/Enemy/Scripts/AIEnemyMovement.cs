using Pathfinding;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AIEnemyMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float nextWayPointDistance = 1f;
    public Transform Target { get; private set; }
    public bool AccessMoving { get; private set; } = true;
    private Seeker seeker;
    private Rigidbody2D rb;
    private Transform tr;

    private Path path;
    private int currentWaypoint;


    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        tr = GetComponent<Transform>();
        Target = PlayerController.Instance.Transform;
        InvokeRepeating("PathUpdate", 0f, 2f);
    }

    private void PathUpdate()
    {
        if (seeker.IsDone())
            seeker.StartPath(rb.position, Target.position, OnPathComplete);
    }

    void OnPathComplete(Path path)
    {
        if (path.error)
            return;

        this.path = path;
        currentWaypoint = 0;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Debug.Log(AccessMoving);
        if (path == null || !AccessMoving)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
            return;

        Vector2 nextWaypoint = (Vector2)path.vectorPath[currentWaypoint];
        Vector2 direction = (nextWaypoint - rb.position).normalized;
        //Vector2 force = direction * speed * Time.fixedDeltaTime;
        //rb.AddForce(force);

        /*float angleDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        tr.rotation = Quaternion.AngleAxis(angleDirection, Vector3.forward);*/

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        float distant = Vector2.Distance(rb.position, nextWaypoint);

        if (distant < nextWayPointDistance)
            ++currentWaypoint;
    }

    private void RayCastHit()
    {
        RaycastHit2D hit;
    }

    public void BlockMovement() => AccessMoving = false;
    public void UnblockMovement() => AccessMoving = true;
}
