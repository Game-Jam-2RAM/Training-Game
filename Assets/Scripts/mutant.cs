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
    public AudioClip damageSound;
    public AudioClip scream;

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        if (health == null)
            health = FindObjectOfType<HealthScript>();

        gameObject.SetActive(false);
        Invoke(nameof(ActivateMonster), 40f);

        agent.speed = 8f;
    }

    void Update()
    {
        if (player == null) return;

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
                if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(scream);
        }

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
        if (damageSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(damageSound);
        }
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
