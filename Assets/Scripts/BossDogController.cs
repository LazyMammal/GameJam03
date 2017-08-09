using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDogController : MonoBehaviour {

    public float bossDogHealth;

    private Animator bossdogAnimate;


    // Use this for initialization
    void Start () {

        GetComponent<SpriteRenderer>().color = new Color(bossDogHealth, 0f, 0f, 1f);

    }
	
	// Update is called once per frame
	void Update () {

        GetComponent<SpriteRenderer>().color = new Color(255f, bossDogHealth, bossDogHealth, 1f);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            GetComponent<Animator>().SetBool("BossDogHit", true);

            Invoke("SetBoolBack", 0.5f);

            bossDogHealth--;

            

            if (bossDogHealth == 0)
            {
                Destroy(gameObject);
            }

        }
    }
    private void SetBoolBack()
    {
        GetComponent<Animator>().SetBool("BossDogHit", false);
    }
}