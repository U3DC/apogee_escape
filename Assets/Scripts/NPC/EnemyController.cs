using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour 
{
    public int health = 30;
    private ParticleSystem myPS;
	void Start () 
    {
        myPS = GetComponent<ParticleSystem>();
	}
	
	void Update () 
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
	}

    public void damage(int _damage)
    {
        health -= _damage;
        myPS.Emit(_damage);
        //TODO: impact sound

    }
}

