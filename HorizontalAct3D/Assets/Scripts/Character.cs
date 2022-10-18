using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    CharacterController cc;
    Vector3 pendingMove;
    protected Animator animator;
    public GameObject prefabSmoke;
    public float speed = 5;
    public int jumpCount = 2;
    public bool isGround { get; private set; }
    public float jumpSpeed = 10;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    // Manage the move and jump actions
    public void Move(float h, bool jump)
    {
        
        pendingMove.x = h * speed;
        // Set rotation
        if (Mathf.Abs(h) > 0.1f)
        {

/*            Quaternion left = Quaternion.LookRotation( Vector3.back);
            Quaternion right = Quaternion.LookRotation(Vector3.forward);*/
            if (h < 0) 
            {
              // Quaternion.Slerp(transform.rotation, left, 0.15f);
               transform.LookAt(transform.position + Vector3.back);
            }

            else if (h > 0)
            {
               //Quaternion.Slerp(transform.rotation,right, 0.15f);
               transform.LookAt(transform.position + Vector3.forward);
            }
        }
        // jump setting
        if (jump && jumpCount > 0)
        {
            pendingMove.y = jumpSpeed;
            isGround = false;
            jump = false;
            jumpCount--;
        }
        if (!isGround)
        {
            pendingMove.y += Physics.gravity.y * Time.deltaTime * 2.5f;
        }

        else if (isGround && !jump)
        {
            pendingMove.y = 0;
            jumpCount = 2;
        }

        cc.Move(pendingMove * Time.deltaTime);
        //Update animation
        UpdateAnim();
    }
    private void FixedUpdate()
    {
        isGround = false;
        Ray ray = new Ray(transform.position + new Vector3(0, 0.2f, 0), Vector3.down);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, 0.25f, LayerMask.GetMask("Default")))
        {
            isGround = true;
        }
        HitCheck();
    }
    void HitCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.position - new Vector3(0, 0.15f, 0), Color.red);
        if (Physics.Raycast(ray, out hit, 0.15f, LayerMask.GetMask("Character")))
        {
            if(hit.transform == transform)
            {
                return;
            }
            Character character = hit.transform.GetComponent<Character>();
            character.Damage();
            // Bounce
            pendingMove.y = jumpSpeed / 2;
        }
    }
    //Take damage, destroy attacting object and play the animation of death
    public void Damage()
    {
        Destroy(gameObject);
        GameObject deathEffect= Instantiate(prefabSmoke, transform.position, Quaternion.identity);
        Destroy(deathEffect, 2); // Delete the animation prefab after 2s
    }
    void UpdateAnim()
    {
        animator.SetFloat("Forward", cc.velocity.magnitude / speed);
        animator.SetBool("isGround", isGround);

    }
}
