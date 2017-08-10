using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDogController : MonoBehaviour {

    public byte bossDogHealth;

    private Animator bossdogAnimate;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {

        GetComponent<SpriteRenderer>().color = new Color32(255, bossDogHealth, bossDogHealth, 255);

        Debug.Log(bossDogHealth);

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