using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ObjetoInteractable : MonoBehaviour
{

    public float distanciaInteraccion = 2f; // Distancia de interacción
    private bool cercaDelObjeto = false;
    private ControlDialogos controlDialogos; // Variable para almacenar el controlador de diálogos
    private bool cartelMostrado = false; // Variable para seguir el estado del cartel
    public Textos textos;
    public Textos textos1;
    public Textos textos2;
    public Textos textos3;
    public Textos textos4;
    public Textos textoFinal;
    private bool trasGirar=false;
    private Rogue rogue; // Person
    private Assassin assassin;
    private Archer archer;
    private ControladorScript controladorScript;

    void Start()
    {
        controlDialogos = ControlDialogos.Instance;
        controladorScript = GameObject.FindGameObjectWithTag("Controlador").GetComponent<ControladorScript>();
        if (SceneManager.GetActiveScene().name == "EscenaRogue1")
        {
            rogue = GameObject.FindGameObjectWithTag("Player").GetComponent<Rogue>();

        }
        if (SceneManager.GetActiveScene().name == "EscenaAssassin1")
        {
            assassin = GameObject.FindGameObjectWithTag("Player").GetComponent<Assassin>();

        }
        if (SceneManager.GetActiveScene().name == "EscenaArcher1")
        {
            archer = GameObject.FindGameObjectWithTag("Player").GetComponent<Archer>();

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDelObjeto = true;
        }
    }
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            cercaDelObjeto = false;
            if (controlDialogos != null && cartelMostrado)
            {
                controlDialogos.CierraCartel();
                cartelMostrado = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cercaDelObjeto = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            cercaDelObjeto = false;
            if (controlDialogos != null && cartelMostrado)
            {
                controlDialogos.CierraCartel();
                cartelMostrado = false;
            }
        }
        
    }

    void Update()
    {
        if (cercaDelObjeto && Input.GetKeyDown(KeyCode.E))
        {
            if(!controladorScript.juegoPausado || (controladorScript.juegoPausado && cartelMostrado))
            {
                if (controlDialogos != null && controlDialogos.PuedeMostrarSiguiente())
                {
                    if (SceneManager.GetActiveScene().name == "EscenaRogue1")
                    {
                        if (cartelMostrado)
                        {
                            controlDialogos.SiguienteFrase();
                        }
                        else if (rogue.yaHeHablado && rogue.pistas && rogue.tieneEspadas && rogue.espadasEntregada && gameObject.CompareTag("Npc"))
                        {
                            controlDialogos.ActivarCartel(textoFinal);
                            cartelMostrado = true;
                        }
                        else if (rogue.yaHeHablado && rogue.pistas && rogue.tieneEspadas && rogue.pedidas && !rogue.espadasEntregada && gameObject.CompareTag("Npc") )
                        {
                            controlDialogos.ActivarCartel(textos4);
                            cartelMostrado = true;
                        }
                        else if (rogue.yaHeHablado && rogue.pistas && rogue.tieneEspadas && !rogue.pedidas && gameObject.CompareTag("Npc") )
                        {
                            controlDialogos.ActivarCartel(textos3);
                            cartelMostrado = true;
                            rogue.pedidas=true;
                        }
                        else if (rogue.yaHeHablado && rogue.pistas && !rogue.tieneEspadas && gameObject.CompareTag("Npc") )
                        {
                            controlDialogos.ActivarCartel(textos2);
                            cartelMostrado = true;
                        }
                        else if (rogue.yaHeHablado && !rogue.pistas && !rogue.tieneEspadas && gameObject.CompareTag("Npc") )
                        {
                            controlDialogos.ActivarCartel(textos1);
                            cartelMostrado = true;
                            rogue.pistas=true;
                        }
                        else if (!rogue.yaHeHablado && !rogue.pistas && gameObject.CompareTag("Npc") )
                        {
                            controlDialogos.ActivarCartel(textos);
                            cartelMostrado = true;
                            rogue.yaHeHablado = true;
                        }
                        else{
                            controlDialogos.ActivarCartel(textos);
                            cartelMostrado = true;
                        }
                    }
                    if (SceneManager.GetActiveScene().name == "EscenaAssassin1")
                    {
                        if (cartelMostrado)
                        {
                            controlDialogos.SiguienteFrase();
                        }
                        else if (assassin.heAblado && assassin.tengoEspadas && assassin.tengoBaston && assassin.cosaEntregada && gameObject.CompareTag("Npc"))
                        {
                            controlDialogos.ActivarCartel(textoFinal);
                            cartelMostrado = true;
                        }
                        else if (assassin.heAblado && assassin.tengoEspadas && assassin.tengoBaston && gameObject.CompareTag("Npc") && trasGirar)
                        {
                            controlDialogos.ActivarCartel(textos4);
                            cartelMostrado = true;
                        }
                        else if (assassin.heAblado && assassin.tengoEspadas && assassin.tengoBaston && gameObject.CompareTag("Npc") && !trasGirar)
                        {
                            controlDialogos.ActivarCartel(textos3);
                            cartelMostrado = true;
                            trasGirar=true;
                            assassin.pedido=true;
                        }
                        else if (assassin.heAblado && assassin.tengoEspadas && !assassin.tengoBaston && gameObject.CompareTag("Npc"))
                        {
                            controlDialogos.ActivarCartel(textos2);
                            cartelMostrado = true;
                        }
                        else if (assassin.heAblado && !assassin.tengoEspadas && gameObject.CompareTag("Npc"))
                        {
                            controlDialogos.ActivarCartel(textos1);
                            cartelMostrado = true;
                        }
                        else if (!assassin.heAblado  && gameObject.CompareTag("Npc"))
                        {
                            controlDialogos.ActivarCartel(textos);
                            cartelMostrado = true;
                            assassin.heAblado = true;
                        }
                        else
                        {
                            controlDialogos.ActivarCartel(textos);
                            cartelMostrado = true;
                        }
                        
                    }
                    if (SceneManager.GetActiveScene().name == "EscenaArcher1")
                    {
                        if (cartelMostrado)
                        {
                            controlDialogos.SiguienteFrase();
                        }
                        else if (archer.Ablado && archer.tengoAlgo && archer.algoEntregada && gameObject.CompareTag("Npc") && archer.tengoLlave && archer.tengopocion)
                        {
                            controlDialogos.ActivarCartel(textoFinal);
                            cartelMostrado = true;
                            archer.Ablado = true;
                        }
                        else if (archer.Ablado && archer.tengoAlgo && !archer.algoEntregada && gameObject.CompareTag("Npc") && archer.tengoLlave && archer.tengopocion)
                        {
                            controlDialogos.ActivarCartel(textos4);
                            cartelMostrado = true;
                            archer.Ablado = true;
                            archer.yapedido=true;
                        }
                        else if (archer.Ablado && !archer.tengoAlgo && !archer.algoEntregada && gameObject.CompareTag("Npc") && archer.tengoLlave && archer.tengopocion)
                        {
                            controlDialogos.ActivarCartel(textos3);
                            cartelMostrado = true;
                            archer.Ablado = true;
                        }
                        else if (archer.Ablado && !archer.tengoAlgo && !archer.algoEntregada && gameObject.CompareTag("Npc") && archer.tengoLlave && !archer.tengopocion)
                        {
                            controlDialogos.ActivarCartel(textos2);
                            cartelMostrado = true;
                            archer.Ablado = true;
                        }
                        else if (archer.Ablado && !archer.tengoAlgo && !archer.algoEntregada && gameObject.CompareTag("Npc") && !archer.tengoLlave && !archer.tengopocion)
                        {
                            controlDialogos.ActivarCartel(textos1);
                            cartelMostrado = true;
                            archer.Ablado = true;
                        }
                        else if (!archer.Ablado && !archer.tengoAlgo && !archer.algoEntregada && gameObject.CompareTag("Npc") && !archer.tengoLlave && !archer.tengopocion)
                        {
                            controlDialogos.ActivarCartel(textos);
                            cartelMostrado = true;
                            archer.Ablado = true;
                        }
                        else
                        {
                            controlDialogos.ActivarCartel(textos);
                            cartelMostrado = true;
                        }
                    }
                }
            }
            
        }
    }
}