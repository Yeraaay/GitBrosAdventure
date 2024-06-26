using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrullaEnemiga : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;    
    private float idleTimer;
    private Vector3 initScale;
    private bool movingLeft;
    [SerializeField] private Animator anim;
    private EnemigoBase enemigoScript;
    private AudioSource audioSource;
    private bool isWalking = false;

    // Start is called before the first frame update
    private void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnDisable()
    {
        anim.SetBool("Movimiento", false);
        StopWalkingSound();
    }
    
    
    private void Update()
    {
        if(!enemigoScript.enemyDead){
            if (movingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x){
                    MoveInDirection(-1);
                }
                else
                {
                    DirectionChange();
                }
            }
            else
            {
                if(enemy.position.x <= rightEdge.position.x){
                    MoveInDirection(1);
                }
                else
                {
                    DirectionChange();
                }
            }   
        }else
        {
            audioSource.Stop();
        }
    }
    
    
    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("Movimiento", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,enemy.position.y, enemy.position.z);
        if (!isWalking && !enemigoScript.PlayerInSightMelee() && !enemigoScript.PlayerInSightDistancia())
        {
            StartWalkingSound();
        }
    }
    private void DirectionChange()
    {
        anim.SetBool("Movimiento", false);
        StopWalkingSound();
        anim.ResetTrigger("ataqueDistancia");

        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
        movingLeft = !movingLeft;
    }
    private void StartWalkingSound()
    {
        if (!isWalking) // Si no está caminando actualmente
        {
            isWalking = true;
            audioSource.clip = enemigoScript.Caminar;
            audioSource.loop=true;
            audioSource.pitch = 2.0f;
            audioSource.Play();
        }
    }

    public void StopWalkingSound()
    {
        isWalking = false;
        audioSource.Stop();
    }
    private void Awake()
    {
        enemigoScript = GetComponentInChildren<EnemigoBase>();
        initScale = enemy.localScale;
    }
}