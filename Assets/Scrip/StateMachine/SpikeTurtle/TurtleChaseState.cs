using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurtleChaseState : StateMachineBehaviour
{
    NavMeshAgent agent;
    Transform player;

    public float chaseSpeed = 6f;
    public float stopChasingDistance = 21f;
    public float attackingDistance = 2.5f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // --- inIt --- 

        player = GameObject.FindGameObjectWithTag("Player").transform;

        agent = animator.GetComponent<NavMeshAgent>();

        agent.speed = chaseSpeed;


    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SoundManager.Instance.TurtleSpikeSource.isPlaying == false)
        {
            SoundManager.Instance.TurtleSpikeSource.clip = SoundManager.Instance.TurtleChaseClip;
            SoundManager.Instance.TurtleSpikeSource.PlayDelayed(2f);

        }

        //-------
        agent.SetDestination(player.position);
        animator.transform.LookAt(player);

        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // --- checking if agent should Stop Chasing ---
        if (distanceFromPlayer > stopChasingDistance)
        {
            animator.SetBool("isChasing", false);

            SoundManager.Instance.musicSource2.Stop();
            SoundManager.Instance.musicSource.Play();

          
        }


        // --- cheking if agent should Attack ---
        if (distanceFromPlayer < attackingDistance)
        {

            //animator.SetBool("isAttacking", true);
            //animator.transform.LookAt(player);
            int randomvalue = Random.Range(0, 2);
            if (randomvalue == 0)
            {
                animator.SetBool("isAttacking", true);
                animator.transform.LookAt(player);

            }
            else
            {
                animator.SetBool("isAttacking2", true);
                animator.transform.LookAt(player);

            }


        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // -- Stop the Agent --
        agent.SetDestination(animator.transform.position);
        SoundManager.Instance.TurtleSpikeSource.Stop();


    }
}
