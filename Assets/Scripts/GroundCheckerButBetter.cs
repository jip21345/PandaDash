using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckerButBetter : MonoBehaviour
{
    [SerializeField]
    private Transform groundCheckerFront;

    [SerializeField]
    private Transform groundCheckerBack;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private bool isGrounded;

    private void Update()
    {
        isGrounded = IsPointOverLappingGround(groundCheckerFront.position) || IsPointOverLappingGround(groundCheckerBack.position);



    }

    public bool IsGrounded()
    {

        return isGrounded;

    }

    bool IsPointOverLappingGround(Vector2 point)
    {

        bool IsOverLapping = Physics2D.OverlapPoint(point, groundLayer);

        return IsOverLapping;
    }
}
