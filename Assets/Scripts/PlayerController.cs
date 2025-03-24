using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private const float POWERUP_TIME = 7.0f;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float powerupStrength = 15.0f;
    public float speed = 5.0f;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public GameObject minePrefab;
    public bool isExistMine;

    private void OnEnable()
    {
        Mine.OnMineReady += MineIsReady;
    }

    private void OnDisable()
    {
        Mine.OnMineReady -= MineIsReady;
    }

    void Start()
    {
        powerupIndicator.gameObject.SetActive(false);
        playerRb = GetComponent<Rigidbody>();    
        focalPoint = GameObject.Find("FocalPoint");
        MineIsReady();
    }

    void Update()
    {
        float fowardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward 
            * speed * fowardInput);
        powerupIndicator.transform.position = transform.position
            + new Vector3(0, -0.5f, 0);
        if (!isExistMine && Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(minePrefab, transform.position, 
                minePrefab.transform.rotation);
            isExistMine = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("POWERUP"))
        {
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdown());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ENEMY") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position
                - transform.position);
            
            Debug.Log($"Power up!!! : {collision.gameObject.name}");
            enemyRb.AddForce(awayFromPlayer * powerupStrength,
                ForceMode.Impulse);
        }
    }

    IEnumerator PowerupCountdown()
    {
        yield return new WaitForSeconds(POWERUP_TIME);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    public void MineIsReady()
    {
        isExistMine = false;
    }
}
