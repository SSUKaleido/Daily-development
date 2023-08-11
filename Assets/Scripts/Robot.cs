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

    bool IsWalk;
    bool IsLook;
    void NavMeshSet()
    {
        nav.updateRotation = false;
    }
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        collider = GetComponent<CapsuleCollider>();
        animator = transform.GetComponentInChildren<Animator>();
        nav = GetComponent<NavMeshAgent>();
        NavMeshSet();
    }
    // Start is called before the first frame update
    void Start()
    {
        target = GameManager.Instance.playerObject.transform;
        nav.destination = target.position;
        IsWalk = true;
        IsLook = true;
    }

    void FixedUpdate()
    {
        if(IsWalk)
        {
            nav.destination = GameManager.Instance.playerObject.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsWalk)
        {
            animator.SetBool("IsWalk", true);
        }
        else
        {
            animator.SetBool("IsWalk", false);
        }

        if(IsLook)
        {
            transform.LookAt(target.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != GameManager.Instance.playerObject)
            return;
        audioSource.Stop();
        IsWalk = false;
        IsLook = false;
        nav.enabled = false;
        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = false;
        GameManager.Instance.CMManager.cameraList[0].LookAt = deathCamLook; 
        GameManager.Instance.CMManager.PlayScene(0);
        animator.SetTrigger("DoJumpScare");
        GameManager.Instance.SoundManager.SetVolume((int)SOUND.SFX, 20f);
        GameManager.Instance.SoundManager.PlayAudio((int)SOUND.SFX, (int)SFX_NAME.JUMPSCARE, false);
    }
}
