using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPoint : MonoBehaviour
{
    public float gravityScale, detectionRadius;
    private Collider2D[] planets;

    // Start is called before the first frame update
    void Start()
    {
        planets = Physics2D.OverlapCircleAll(transform.position, detectionRadius, LayerMask.GetMask("Planet"));
    }

    // Update is called once per frame
    void Update()
    {
        AttractToNearestPlanet();
    }

    private void AttractToNearestPlanet()
    {
        Collider2D closestPlanet = null;
        float minDistance = float.MaxValue;

        foreach (Collider2D planet in planets)
        {
            float distance = Vector3.Distance(planet.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestPlanet = planet;
            }
        }
        Debug.Log(closestPlanet);
        if (closestPlanet != null)
        {
            Vector3 dir = (closestPlanet.transform.position - transform.position) * gravityScale;
            GetComponent<Rigidbody2D>().AddForce(dir);
            transform.up = Vector3.MoveTowards(transform.up, -dir, gravityScale * Time.deltaTime);
        }
    }
}