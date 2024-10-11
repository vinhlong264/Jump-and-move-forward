using UnityEngine;

public class Enemy_Dealine : Enemy
{
    protected override void Start()
    {
        base.Start();
    }


    private void FixedUpdate()
    {
        rb.velocity = Vector2.up * 2;
    }
}
