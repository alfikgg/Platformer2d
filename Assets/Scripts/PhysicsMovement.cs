using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float GravityModifier = 1f;
    [SerializeField] private Vector2 Velocity;
    [SerializeField] private LayerMask LayerMask;
    [SerializeField] private float _playerSpeed = 3f;
    [SerializeField] private float _playerJumpPower = 8f;

    private float MinGroundNormalY = .65f;

    protected Vector2 groundNormal;
    protected Rigidbody2D rb2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    public Vector2 targetVelocity { get; private set; }
    public bool grounded { get; private set; }

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(LayerMask);
        contactFilter.useLayerMask = true;        
    }

    void Update()
    {
        targetVelocity = new Vector2(Input.GetAxis("Horizontal")* _playerSpeed, 0);

        if (Input.GetKey(KeyCode.Space) && grounded)
            Velocity.y = _playerJumpPower;
    }

    void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;
        Velocity.x = targetVelocity.x;

        grounded = false;

        Vector2 deltaPosition = Velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > minMoveDistance)
        {
            int count = rb2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);

            hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > MinGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(Velocity, currentNormal);
                if (projection < 0)
                {
                    Velocity = Velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rb2d.position = rb2d.position + move.normalized * distance;
    }
}