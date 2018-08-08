using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneBehaviourDefence : MonoBehaviour
{

    public GameObject ship;
    private Vector3 target;
    private float angle;
    private SphereCollider mySC;


    [Header("Properties")]
    public float rateOfFire = 2f;
    private bool canShoot = true;
    public int damageAmount = 40;

    [Header("Randomisations - Movement")]
    public float maxRadius = 4f;
    public float minRadius = 8f;
    private float radius;

    public float maxSpeed = 3f;
    public float minSpeed = 1.5f;
    private float speed;

    [Header("Randomisations - Placement")]
    public float maxXOffset = 10f;
    public float minXOffset = 0f;
    private float xOffset;

    public float maxYOffset = 5f;
    public float minYOffset = -5f;
    private float yOffset;

    public float maxZOffset = 5f;
    public float minZOffset = -5f;
    private float zOffset;

    private Vector3 offsetPosition;

    void Start()
    {
        SetRandomisations();
        StartCoroutine("ROFTimer");
    }
    IEnumerator ROFTimer()
    {
            yield return new WaitForSeconds(rateOfFire);
            canShoot = true;
            yield break;
    }

    void SetRandomisations()
    {
        radius = Random.Range(minRadius, maxRadius);
        speed = Random.Range(minSpeed, maxSpeed);
        xOffset = Random.Range(minXOffset, maxXOffset);
        yOffset = Random.Range(minYOffset, maxYOffset);
        zOffset = Random.Range(minZOffset, maxZOffset);

        //Add to the ship position to create a new centre of orbit.
        offsetPosition = new Vector3(xOffset, yOffset, zOffset) + ship.transform.position;
        mySC = GetComponent<SphereCollider>();
    }

    void Update()
    {
        OrbitMovement();
    }

    void OrbitMovement()
    {
        // Get the direction from centre to our position
        Vector3 dir = transform.position - offsetPosition;

        // Calculate vector angel
        angle = Mathf.Atan2(dir.z, dir.x);

        // Translate to a point ahead of the orbit
        angle += speed * Mathf.Deg2Rad;

        // Calculate the vector3 position along the circle
        target = new Vector3(
            (Mathf.Cos(angle) * radius), 
            dir.y,
            (Mathf.Sin(angle) * radius)
        ) + offsetPosition;

        gameObject.transform.position = target;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Alien")
        {
            if (canShoot == true)
            {
                Debug.DrawLine(gameObject.transform.position, other.gameObject.transform.position, Color.red);

                other.GetComponent<EnemyController>().damage(damageAmount);
                canShoot = false;
                StartCoroutine("ROFTimer");
            }




        }   
        if (other.gameObject.tag == "Player")
        {
            Debug.DrawLine(gameObject.transform.position, other.gameObject.transform.position, Color.cyan);




        }
    }

}
