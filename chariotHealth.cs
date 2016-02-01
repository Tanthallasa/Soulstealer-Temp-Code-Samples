using UnityEngine;
using System.Collections;

public class chariotHealth : HealthScript {

    public GameObject chariot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (damaged)
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

            chariotAI chariotAI = chariot.GetComponent<chariotAI>();
            if (chariotAI.afterimages)
            {
                chariotAI.afterimages = false;

                SpriteRenderer ai1renderer = chariotAI.image1.GetComponent<SpriteRenderer>();
                ai1renderer.enabled = false;

                SpriteRenderer ai2renderer = chariotAI.image2.GetComponent<SpriteRenderer>();
                ai2renderer.enabled = false;
            }

            if (chariotAI.deathring)
            {
                chariotAI.deathring = false;
                chariotAI.attacking = false;

                SpriteRenderer targetrenderer = chariotAI.drtarget.GetComponent<SpriteRenderer>();
                targetrenderer.material.SetColor("_Color", Color.white);
            }
        }
	}
}
