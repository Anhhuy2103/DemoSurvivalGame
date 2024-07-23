using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemAttackState : StateMachineBehaviour
{
    Transform player;
    NavMeshAgent agent;

    public float stopAttackingDistance = 2.5f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- inIt --- 

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (SoundManager.Instance.EnemySource.isPlaying == false)
        {
            SoundManager.Instance.GolemSound_Attack();
        
        }
        //--
        LookAtPlayer();
        animator.transform.LookAt(player);
        agent.transform.LookAt(player);
        // --- check if the agent should stop Attack Player --- 
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

      
        if (distanceFromPlayer > stopAttackingDistance)
        {            
                animator.SetBool("isAttacking", false);
             
                animator.SetBool("isAttacking2", false);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SoundManager.Instance.EnemySource.Stop();       
      
    }

    //--------------------------------------------------------------------------...
    private void LookAtPlayer()
    {
        Vector3 direction = player.position - agent.transform.position;
        agent.transform.rotation = Quaternion.LookRotation(direction);

        var yRotation = agent.transform.eulerAngles.y;
        agent.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

}
