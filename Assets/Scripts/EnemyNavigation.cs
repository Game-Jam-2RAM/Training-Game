// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class EnemyNavigation : MonoBehaviour
// {
//     public Transform player;
//     private NavMeshAgent agent;
//     private Animator animator;

//     private bool isWalking = false;

//     void Start()
//     {
//         agent = GetComponent<NavMeshAgent>();
//         animator = GetComponent<Animator>();
//     }

//     void Update()
// {
//     if (player != null)
//     {
//         agent.SetDestination(player.position);
//         float distance = Vector3.Distance(transform.position, player.position);

//         if (distance > 2f) // enemy starts walking if player is far
//         {
//             if (!isWalking)
//             {
//                 animator.ResetTrigger("idle");
//                 animator.SetTrigger("walk");
//                 isWalking = true;
//             }
//         }
//         else
//         {
//             if (isWalking)
//             {
//                 animator.ResetTrigger("walk");
//                 animator.SetTrigger("idle");
//                 isWalking = false;
//             }
//         }
//     }
// }
// }
