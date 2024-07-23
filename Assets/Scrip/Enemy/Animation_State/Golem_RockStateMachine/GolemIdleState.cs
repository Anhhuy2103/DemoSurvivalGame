using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class GolemIdleState : StateMachineBehaviour
{
    float timer;
    [SerializeField] private float idleTime = 0f;
    Transform player;

   [SerializeField] private float detectionAreaRadius = 18f;

    [SerializeField] private enum typeOfEnemy
    {
        SmallCreep,
        BigCreep,
    }
    [SerializeField] private typeOfEnemy thisTypeOfEnemy;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        timer = 0;
       
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // -- Chuyen sang di vong vong tims player -- 

        timer += Time.deltaTime;
        if(timer > idleTime)
        {
            animator.SetBool("isPatroling", true);
        }

        // --  Chuyen sang ruot theo Player sau khi di vong vong --

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);
        if (distanceFromPlayer < detectionAreaRadius)
        {
            if (thisTypeOfEnemy == typeOfEnemy.BigCreep)
            {
                animator.SetBool("isChasing", true);             
                SoundManager.Instance.musicSource2.PlayDelayed(2f);
            }
            if(thisTypeOfEnemy == typeOfEnemy.SmallCreep)
            {
                animator.SetBool("isChasing", true);
            }
        }
    }


}
