using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    public Transform deathCamLook;
    public AudioSource audioSource;
    Collider collider;
    Transform target;
    Animator animator;
    NavMeshAgent nav;
    Renderer renderer;

    bool IsChase;
    bool IsLook;
    bool IsCollider;
    bool IsOnce;
    bool IsWalk;
    bool IsWander;
    float currentTimer;
    float wanderingTime = 5f;
    void NavMeshSet()
    {
        
    }

    void DeathScene()
    {
        GetComponent<FieldOfView>().StopFOV();
        audioSource.Stop();
        IsChase = false;
        IsLook = false;
        IsWalk = false;
        IsWander = false;
        nav.isStopped = true;

        animator.SetTrigger("DoJumpScare");
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = false;
        GameManager.Instance.CMManager.cameraList[0].LookAt = deathCamLook;
        GameManager.Instance.CMManager.PlayScene(0);
        GameManager.Instance.SoundManager.SetVolume((int)SOUND.SFX, 5f);
        GameManager.Instance.SoundManager.PlayAudio((int)SOUND.SFX, (int)SFX_NAME.JUMPSCARE, false, false);
    }
    void DoWandering()
    {
        float range = GetComponent<FieldOfView>().viewRadius;
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * range;

        randomDirection += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, range, -1);
        
        nav.destination = navHit.position;

        currentTimer = 0;
        IsWander = true;
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<CapsuleCollider>();
        animator = transform.GetComponentInChildren<Animator>();
        renderer = transform.GetChild(0).GetChild(0).GetComponent<Renderer>();
        nav = GetComponent<NavMeshAgent>();
        NavMeshSet();
    }
    // Start is called before the first frame update
    void Start()
    {
        DoWandering();
    }

    void FixedUpdate()
    {
        if (IsChase)
        {
            nav.destination = GameManager.Instance.playerObject.transform.position;
            IsWalk = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsWalk)
        {
            audioSource.enabled = true;
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
            audioSource.enabled = false;
        }

        /*if (IsLook)
        {
            transform.LookAt(new Vector3(nav.destination.x, transform.position.y, nav.destination.z));
        }*/

        if (IsWander)
        {
            currentTimer += Time.deltaTime;
            IsWalk = true;
            IsLook = true;
            if (nav.remainingDistance <= nav.stoppingDistance)
            {
                if (!nav.hasPath || nav.velocity.sqrMagnitude == 0f)
                {
                    Invoke("DoWandering", Random.Range(3, 6));
                    IsWalk = false;
                    IsWander = false;
                }
            }
            if (currentTimer >= wanderingTime)
            {
                Invoke("DoWandering", Random.Range(3, 6));
                IsWalk = false;
                IsWander = false;
            }
        }

        if (transform.GetComponent<FieldOfView>().IsRecog)
        {
            renderer.material.SetColor("_EmissionColor", Color.red);
            IsChase = true;
            IsLook = true;
            IsCollider = true;
            GameManager.Instance.UIManager.HorrorChase(transform);
            IsOnce = false;
        }
        else
        {
            renderer.material.SetColor("_EmissionColor", Color.white);
            IsChase = false;
            target = transform;
            if(!IsOnce)
            {
                GameManager.Instance.UIManager.HorrorReset();
                DoWandering();
                IsOnce = true;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(IsCollider)
        {
            if (collision.gameObject != GameManager.Instance.playerObject)
                return;
            GameManager.Instance.playerObject.GetComponent<Player>().Heart--;
            if (GameManager.Instance.playerObject.GetComponent<Player>().Heart <= 0)
                DeathScene();
            else
                GameManager.Instance.UIManager.StartGetDamaged();
        }
    }
}
