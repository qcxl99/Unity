using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    Character character;
    bool jump = false;
    float inputX;
    public float jumpTime = 1;
    public float jumpChance = 30;
    public float changeMove = 2;
    void Start()
    {
        character = GetComponent<Character>();
        StartCoroutine(HorizontalMove());
        StartCoroutine(AIJump());
    }


    void Update()
    {
        if (character.transform.position.x <= -24)
        {
            inputX = 1f;
        }
        else if(character.transform.position.x >= 24)
        {
            inputX = -1f;
        }
        character.Move(inputX, jump);
        jump = false;
    }
    IEnumerator HorizontalMove()
    {
        while (true)
        {
          inputX = Random.Range(-1f, 1f);
          yield return new WaitForSeconds(changeMove);
        }
        
    }
    IEnumerator AIJump()
    {
        while (true)
        {
        int chance = Random.Range(0,100);
        if (chance < jumpChance)
        {
            jump = true;
        }

        yield return new WaitForSeconds(jumpTime); 
        }
       
    }
    private void OnTriggerEnter(Collider other)
    {
        string tag = other.gameObject.tag;
        if (tag == "Box") {character.Damage();}
        
    }
}
