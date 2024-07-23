using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemPatrollingState : StateMachineBehaviour
{
    float timer;
    public float patrolingTime = 10f;

    Transform player;
    NavMeshAgent agent;

    [SerializeField] private float detectionArea = 18f;
    [SerializeField] private float patrolSpeed = 2f;

    List<Transform> waypointsList = new List<Transform>();

    [SerializeField]
    private enum TypeOfArea
    {
        Area1,
        Area2,
        boss_Area
    }
    [SerializeField] private TypeOfArea thisTypeArea;

    [SerializeField]
    private enum TypeOfEnemy
    {
        Creep,
        Boss,       
    }
    [SerializeField] private TypeOfEnemy thisTypeEnemy;

    [SerializeField]
    private enum KindOfEnemy
    {
       Golem,
       Spike
    }
    [SerializeField] private KindOfEnemy thisKindEnemy;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // --- inIt --- 

        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = animator.GetComponent<NavMeshAgent>();
        agent.speed = patrolSpeed;

        timer = 0;

        // --Get all waypoints and  Move to first waypoint -- 

        // case 1 Golem_area1
        if (thisTypeArea == TypeOfArea.Area1 && thisKindEnemy == KindOfEnemy.Golem)
        {
            GameObject waypointCluster = GameObject.FindGameObjectWithTag("GolemArea1");
            foreach (Transform t in waypointCluster.transform)
            {
                waypointsList.Add(t);
            }
        }
        // case 2 Golem_area2
        if (thisTypeArea == TypeOfArea.Area2 && thisKindEnemy == KindOfEnemy.Golem)
        {
            GameObject waypointCluster = GameObject.FindGameObjectWithTag("GolemArea2");
            foreach (Transform t in waypointCluster.transform)
            {
                waypointsList.Add(t);
            }
        }
        // case 3 Golem_bossArea
        if (thisTypeArea == TypeOfArea.boss_Area && thisKindEnemy == KindOfEnemy.Golem)
        {
            GameObject waypointCluster = GameObject.FindGameObjectWithTag("GolemBossArea");
            foreach (Transform t in waypointCluster.transform)
            {
                waypointsList.Add(t);
            }
        }
        //----------------- Spike Turtle ------------
        // case 1 Spike_area1
        if (thisTypeArea == TypeOfArea.Area1 && thisKindEnemy == KindOfEnemy.Spike)
        {
            GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints_1");
            foreach (Transform t in waypointCluster.transform)
            {
                waypointsList.Add(t);
            }
        }

        // case 2 Spike_area2
        
        if (thisTypeArea == TypeOfArea.Area2 && thisKindEnemy == KindOfEnemy.Spike)
        {
            GameObject waypointCluster = GameObject.FindGameObjectWithTag("Waypoints_2");
            foreach (Transform t in waypointCluster.transform)
            {
                waypointsList.Add(t);
            }
        }

        // case 3 Spike_BossArea
        if (thisTypeArea == TypeOfArea.boss_Area && thisKindEnemy == KindOfEnemy.Spike)
        {
            GameObject waypointCluster = GameObject.FindGameObjectWithTag("SpikeBossArea");
            foreach (Transform t in waypointCluster.transform)
            {
                waypointsList.Add(t);
            }
        }
        Vector3 nextPosition = waypointsList[Random.Range(0, waypointsList.Count)].position;
        agent.SetDestination(nextPosition);
    }



    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distanceFromPlayer = Vector3.Distance(player.position, animator.transform.position);

        // ---  Check if agent da den Waypoint va chuyen tiep sang diem waypoint tiep theo ---


        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.SetDestination(waypointsList[Random.Range(0, waypointsList.Count)].position);

        }


        // --- Chuyen lai thanh IdleState ---
        timer += Time.deltaTime;
        if (timer > patrolingTime)
        {
            animator.SetBool("isPatroling", false);

        }

        // --- Chuyen thanh ChasingState ---

        if (distanceFromPlayer < detectionArea)
        {
            animator.SetBool("isChasing", true);
            if (thisTypeEnemy == TypeOfEnemy.Boss)
            {
             
                SoundManager.Instance.musicSource2.PlayDelayed(1f);
            }


        }
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        // -- Stop the Agent --
        agent.SetDestination(agent.transform.position);
       
    }

}
