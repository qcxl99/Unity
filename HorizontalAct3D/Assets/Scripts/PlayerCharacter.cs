using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : Character
{
    public Transform grabPos;
    public Transform grabTrans;

    public float throwForce = 13f;
    
    public void Grab()
    {
        if(grabTrans == null)
        {
            Ray ray = new Ray(transform.position + new Vector3(0, 0.7f, 0), transform.right);
            RaycastHit hit;
            Debug.DrawLine(transform.position + new Vector3(0, 0.7f, 0), transform.position + new Vector3(1.1f, 0.7f, 0), Color.red);
            if (Physics.Raycast(ray, out hit,1.1f , LayerMask.GetMask("Box")))
            {
                Transform box = hit.transform;
                box.parent = grabPos;
                box.localPosition = Vector3.zero;
                box.GetComponent<Rigidbody>().isKinematic = true;
                grabTrans = box;
            }
        }

        else
        {
            grabTrans.parent = null;
            grabTrans.GetComponent<Rigidbody>().isKinematic = false;
            grabTrans.GetComponent<Rigidbody>().AddForce(transform.right * throwForce, ForceMode.Impulse);
            grabTrans.position = new Vector3(grabTrans.position.x,grabTrans.position.y, 0);
            grabTrans = null;
            
        }

        
    }

    private void Update()
    {
        if( grabTrans == null)
        {
            animator.SetBool("Grab", false);
        }
        else
        {
            animator.SetBool("Grab", true);
        }
    }
}
