using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossStates
{
    Idle,
    Rush,
    FireBall,
    FireRain,
    Explotion,
    TerrainFire,
}
public class BossState : Boss
{
    BossStates state;
    public float speed = 10;
    void Start()
    {
        state = BossStates.Idle;
    }

    // Update is called once per frame
    void Update()
    {
        if (hp / maxHp > 0.5)
        {
            switch (state)
            {
                case BossStates.Idle:
                    {
                        Debug.Log("111");
                        StartCoroutine(ChooseSkill());
                        int skill = Random.Range(1, 100);
                        skill = 45;
                        if (skill < 40)
                        {
                            state = BossStates.Rush;
                        }
                        else if (skill >= 40 && skill < 80)
                        {
                            state = BossStates.FireBall;
                        }
                        else if (skill >= 80 && skill <= 100)
                        {
                            state = BossStates.FireRain;
                        }
                        break;
                    }
                case BossStates.Rush:
                    {
                        Debug.Log("222");
                        float y = rigid.velocity.y;
                        Vector3 pos = transform.position;
                        rigid.velocity = new Vector3(-speed, y, 0);
                        if(transform.position.x < -36)
                        {
                            transform.position.Set( 36, pos.y, pos.z);
                        }
                        if(transform.position.x > 12)
                         {
                            transform.position += Vector3.left * speed/2 * Time.deltaTime;
                         }
                        state = BossStates.Idle;
                        break;
                    }
                case BossStates.FireBall:
                    {

                        break;
                    }
                case BossStates.FireRain:
                    {

                        break;
                    }
            }
        }
        else
        {
            switch (state)
            {
                case BossStates.Idle:
                    {
                        int skill = Random.Range(1, 4);
                        break;
                    }
                case BossStates.Rush:
                    {

                        break;
                    }
                case BossStates.FireBall:
                    {

                        break;
                    }
                case BossStates.FireRain:
                    {

                        break;
                    }
                case BossStates.TerrainFire:
                    {

                        break;
                    }
            }
        }
    }
    IEnumerator ChooseSkill()
    {
        yield return new WaitForSeconds(Random.Range(3,5));
    }
}
