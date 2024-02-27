using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilPersonaje : MonoBehaviour
{
    [SerializeField] private float velocidad;
    private bool hit;
    private BoxCollider2D boxCollider;
    private Animator anim;
    private float direction;
    private float lifetime;
    private PersonajeBase personajeScript;
    private EnemigoBase enemigoScript;
    private PalancaBase palanca;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        personajeScript = GetComponentInParent<PersonajeBase>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        float movementSpeed = velocidad * Time.deltaTime* direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if (lifetime > 3) gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        if (collision.CompareTag("Enemigo")){

            enemigoScript = collision.GetComponent<EnemigoBase>();
            if(!enemigoScript.enemyDead)
            {
                enemigoScript.enemigoRecibirDanio(personajeScript.danioDistancia);
            }
        }
        if (collision.CompareTag("Palanca"))
        {
            palanca = collision.GetComponent<PalancaBase>();
            if(palanca!=null){
                palanca.ActivatePalanca();
            }
        }
        boxCollider.enabled = false;
        anim.SetTrigger("Explota");
    }
    
     public void SetDirection(float Direction)
    {
        lifetime = 0;
        direction = Direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != Direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Desactivate()
    {
        gameObject.SetActive(false);
    }
}