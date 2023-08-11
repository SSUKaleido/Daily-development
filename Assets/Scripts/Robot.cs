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

    bool IsChase;
    bool IsLook;
    void NavMeshSet()
    {
        nav.updateRotation = false;
    }

    void DeathScene()
    {
        audioSource.Stop();
        IsChase = false;
        IsLook = false;
        nav.enabled = false;    //다른 모든 로봇 nav도 멈춰야함
        animator.SetTrigger("DoJumpScare");

        GameManager.Instance.playerObject.GetComponent<FirstPersonController>().enabled = false;
        GameManager.Instance.CMManager.cameraList[0].LookAt = deathCamLook;
        GameManager.Instance.CMManager.PlayScene(0);
        GameManager.Instance.SoundManager.SetVolume((int)SOUND.SFX, 10f);
        GameManager.Instance.SoundManager.PlayAudio((int)SOUND.SFX, (int)SFX_NAME.JUMPSCARE, false);
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
        IsLook = true;
    }

    void FixedUpdate()
    {
        if (IsChase)
        {
            nav.destination = GameManager.Instance.playerObject.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsChase)
        {
            animator.SetBool("IsWalk", true);
            audioSource.enabled = true;
        }
        else
        {
            animator.SetBool("IsWalk", false);
            audioSource.enabled = false;
        }

        if (IsLook)
        {
            transform.LookAt(target.position);
        }

        if(transform.GetComponent<FieldOfView>().IsRecog)
        {
            target = GameManager.Instance.playerObject.transform;
            IsChase = true;
            IsLook = true;
        }
        else
        {
            IsChase = false;
            target = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject != GameManager.Instance.playerObject)
            return;
        DeathScene();
    }
}
