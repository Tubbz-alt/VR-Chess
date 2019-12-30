using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    [Header("Movement Values")]
    public float movingSpeed = 1;
    public float distanceToHit = 1;
    public float distanceToIdle = 0.5f;

    [Header("Spell/FX Values")]
    [Tooltip("FXAnimationTrigge script is mandatory on the animation")]
    public GameObject Fx;
    public GameObject FxRoot;
    public Vector3 FxSpawnPositionOffset;
    public float FxFirerate;
    public float FxAmountToSpawn;
    public float FxDelay;

    Animator animator;
    bool moving = false;
    bool attack = false;
    Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(RandomAnimOffset());
        targetPos = Camera.main.transform.position;
    }

    void Update()
    {
        if (moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movingSpeed * Time.deltaTime);

            //If the character is close to target and needs to attacks
            if (Vector3.Distance(targetPos, transform.position) <= distanceToHit && attack)
            {
                animator.SetBool("Moving", false);
                moving = false;
                StartCoroutine(Attack());
            }
            else if (Vector3.Distance(targetPos, transform.position) <= distanceToIdle) {
                animator.SetBool("Moving", false) ;
                moving = false;                
            }
        }
    }

    public void MoveTo(float x, float z, bool attack)
    {
        targetPos = new Vector3(x, transform.position.y, z);
        transform.LookAt(targetPos);
        moving = true;
        animator.SetBool("Moving", moving);
        this.attack = attack;
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");

        //yield on a new YieldInstruction that waits for the duration of the attack animation.
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + 2);

        //Move to the final position after the attack animation is finished
        MoveTo(targetPos.x, targetPos.z, false);
    }

    IEnumerator RandomAnimOffset() {
        //Create an Offset between the same animations
        animator.speed = 0.3f;
        yield return new WaitForSeconds(Random.Range(0, 3));
        animator.speed = 1f;
    }

    IEnumerator SpawnSpell()
    {
        float timeToShoot = 1 / FxFirerate;
        yield return new WaitForSeconds(FxDelay);
        Vector3 castPosition;
        Transform parent;
        if (FxRoot != null)
        {
            castPosition = FxRoot.transform.position + FxSpawnPositionOffset;
            Fx.transform.localScale = new Vector3(100, 100, 100);
            parent = FxRoot.transform;
        }
        else {
            castPosition = FxSpawnPositionOffset + transform.position;
            parent = null;
        }
            
        
        for (int i = 0; i < FxAmountToSpawn; i++) {
            GameObject VFX = Instantiate(Fx, castPosition, Quaternion.identity, parent);
            VFX.transform.LookAt(targetPos);
            yield return new WaitForSeconds(timeToShoot);
        } 
    }

    public void TriggerSpell() {
        StartCoroutine(SpawnSpell());
    }
}
