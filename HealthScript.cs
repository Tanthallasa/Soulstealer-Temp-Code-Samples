using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

    public int health;
    public int maxhealth;
    public int maxMana ;
    public int totalMana;
    public GameObject unit;
    public bool damaged;
    public bool healed;
    public bool colored;
    public int counter;
    public int maxcount;
    private Animator anim;

    // Use this for initialization
    void Start () {
        SpriteRenderer renderer = unit.GetComponent<SpriteRenderer>();
        anim = this.GetComponent<Animator>();
        totalMana = maxMana;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (health <= 0)
        {
            //temporary
            Application.LoadLevel(3);
        }

        if (damaged)
        {
            if (anim != null)
            {
                anim.Play("hurt");
                damaged = false;
            }
            else
            {
                counter++;
                if (!colored)
                {
                    GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                    colored = true;
                }
                else if (counter == maxcount)
                {
                    GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                    colored = false;
                    damaged = false;
                    counter = 0;
                }
            }
        }
	}
}
