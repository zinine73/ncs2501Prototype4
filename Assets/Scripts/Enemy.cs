using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    private bool isCatched;
    public GameObject fireIndicator;
    private void OnEnable()
    {
        Mine.OnMineReady += FireOn;
    }

    private void OnDisable()
    {
        Mine.OnMineReady -= FireOn;
    }

    private void FireOn()
    {
        fireIndicator.SetActive(true);
        StartCoroutine(FireOff());
    }

    private IEnumerator FireOff()
    {
        yield return new WaitForSeconds(1f);
        fireIndicator.SetActive(false);
    }

    private void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");    
        isCatched = false;
    }

    private void Update()
    {
        Vector3 lookDirection = (player.transform.position 
            - transform.position).normalized;
        if (!isCatched)
        {
            enemyRb.AddForce(lookDirection * speed);
        }
        if (transform.position.y < -10)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MINE"))
        {
            enemyRb.Sleep();
            Debug.Log($"MINE!!!:{other.name}");
            isCatched = true;
            other.GetComponent<Mine>().AddCatch();
        }
    }
}
