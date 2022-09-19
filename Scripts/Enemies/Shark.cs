using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [Header("Stats")]
    private float lastDetectTime;
    public float detectRate;
    public float moveSpeed;
    public float attackRange;
    public bool isAttacking = true;
    public float disableAttack;
    Coroutine attackRoutine;

    [Header("Audio")]
    public AudioSource adSource;

    [Header("Components")]
    public Rigidbody2D rig;
    public Collider2D attackCollider;
    public GameObject[] enemyTarget;

    private void Update()
    {
        DetectEnemies();
    }

    void DetectEnemies()
    {
        enemyTarget = GameObject.FindGameObjectsWithTag("Fishes");
        //float minDist = Mathf.Infinity;

        if (Time.time - lastDetectTime > detectRate && isAttacking)
        {
            lastDetectTime = Time.time;

            foreach (GameObject fish in enemyTarget)
            {
                if (fish == this.gameObject)
                    continue;

                float dist = Vector2.Distance(transform.position, fish.transform.position);
                if (dist < attackRange)
                {
                    // get the angle
                    Vector3 dir = (fish.transform.position - transform.position).normalized;
                    float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    // rotate to angle
                    Quaternion rotation = new Quaternion();
                    rotation.eulerAngles = new Vector3(0, 0, angle - 270);
                    transform.rotation = rotation;

                    //Vector2 dir = fish.transform.position - transform.position;
                    rig.velocity = dir.normalized * moveSpeed;

                    attackCollider.enabled = true;

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fishes")
        {
            if(!adSource.isPlaying)
                adSource.Play();
            
            attackRoutine = StartCoroutine(_AttackTrigger());

            Destroy(other.gameObject);
        }
    }

    IEnumerator _AttackTrigger()
    {
        isAttacking = false;
        rig.velocity = Vector2.zero;
        attackCollider.enabled = false;
        yield return new WaitForSeconds(disableAttack);
        isAttacking = true;
        attackCollider.enabled = true;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawSphere(transform.position, attackRange);
    }

    //Transform GetClosestEnemy(Transform[] enemies)
    //{
    //    enemyTransform = Transform.FindObjectOfType<Wander2>();
    //        //.FindGameObjectsWithTag("Fishes");
    //    Transform tMin = null;
    //    float minDist = Mathf.Infinity;
    //    Vector3 currentPos = transform.position;
    //    foreach (Transform t in enemies)
    //    {
    //        float dist = Vector3.Distance(t.position, currentPos);
    //        if (dist < minDist)
    //        {
    //            tMin = t;
    //            minDist = dist;
    //        }
    //    }
    //    return tMin;
    //}
}
