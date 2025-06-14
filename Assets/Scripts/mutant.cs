using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class mutant : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    public HealthScript health;

    private float attackCooldown = 1.367f;
    private float lastAttackTime = 0f;
    private bool isAttacking = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (health == null)
            health = FindObjectOfType<HealthScript>();

        gameObject.SetActive(false);
        Invoke(nameof(ActivateMonster), 10f);

        agent.speed = 8f;
    }

    void Update()
    {
        if (player == null || !player.gameObject.activeInHierarchy)
        {
            float distance2 = Vector3.Distance(transform.position, new Vector3(0.500000238f, -0.476415694f, 6.15999985f));
            if (distance2 > 1)
            {
                agent.SetDestination(new Vector3(0.500000238f, -0.476415694f, 6.15999985f));
                agent.speed = 15f;
                ResetAnimationTriggers();
                animator.SetTrigger("walk");
            }
            else
            {
                ResetAnimationTriggers();
                animator.SetTrigger("idle");
            }
            return;
        }

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= 8f)
        {
            agent.ResetPath(); // stop moving

            if (health != null && health.GetCurrentHealth() <= 0)
            {
                // Player is dead, go idle
                ResetAnimationTriggers();
                animator.SetTrigger("idle");
                isAttacking = false;
                return;
            }

            if (!isAttacking && Time.time - lastAttackTime >= attackCooldown)
            {
                isAttacking = true;
                // add 0.5 second delay before attack
                Invoke(nameof(PerformAttack), 0.5f);


                ResetAnimationTriggers();
                animator.SetTrigger("attack");

                lastAttackTime = Time.time;

                StartCoroutine(WaitForAttackAnimation());
            }
        }
        else
        {
            ResetAnimationTriggers();
            animator.SetTrigger("walk");

            agent.SetDestination(player.position);
        }
    }

    private void PerformAttack()
    {
        health.UpdateHealth(-10);
    }

    IEnumerator WaitForAttackAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        isAttacking = false;
    }

    void ResetAnimationTriggers()
    {
        animator.ResetTrigger("idle");
        animator.ResetTrigger("walk");
        animator.ResetTrigger("attack");
    }

    void ActivateMonster()
    {
        gameObject.SetActive(true);
    }
}
