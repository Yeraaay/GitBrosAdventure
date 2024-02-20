using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class holder : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-0.25f, transform.localScale.y, transform.localScale.z);
        } else
        {
            transform.localScale = new Vector3(0.25f, transform.localScale.y, transform.localScale.z);
        }
    }
}