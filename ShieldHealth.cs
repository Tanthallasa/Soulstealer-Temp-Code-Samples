using UnityEngine;
using System.Collections;

public class ShieldHealth : HealthScript {

    public bool good;
    public int damage;
    public int dsnum;
    public GameObject character;
    public GameObject mephisto;

	void Start () {
        SpriteRenderer renderer = unit.GetComponent<SpriteRenderer>();
    }
	
	void Update () {
        if (health <= 0)
        {
            health = 1;
            if (good)
            {
                SpriteRenderer gsrenderer = unit.GetComponent<SpriteRenderer>();
                gsrenderer.enabled = false;

                mephistoAI mephistoAI = mephisto.GetComponent<mephistoAI>();

                TileScript tile = mephistoAI.gstile.GetComponent<TileScript>();
                tile.occupiedBy = null;

                TileScript tile1 = mephistoAI.ds1tile.GetComponent<TileScript>();
                tile1.occupiedBy = null;

                TileScript tile2 = mephistoAI.ds2tile.GetComponent<TileScript>();
                tile2.occupiedBy = null;

                SpriteRenderer ds1renderer = mephistoAI.deathshield1.GetComponent<SpriteRenderer>();
                ds1renderer.enabled = false;

                SpriteRenderer ds2renderer = mephistoAI.deathshield2.GetComponent<SpriteRenderer>();
                ds2renderer.enabled = false;
            }
            else if (!good)
            {
                HealthScript charhealth = character.GetComponent<HealthScript>();
                charhealth.health -= damage;
                charhealth.damaged = true;

                SpriteRenderer dsrenderer = unit.GetComponent<SpriteRenderer>();
                dsrenderer.enabled = false;

                mephistoAI mephistoAI = mephisto.GetComponent<mephistoAI>();
                if (dsnum == 1)
                {
                    TileScript tile = mephistoAI.ds1tile.GetComponent<TileScript>();
                    tile.occupiedBy = null;
                }
                else if (dsnum == 2)
                {
                    TileScript tile = mephistoAI.ds2tile.GetComponent<TileScript>();
                    tile.occupiedBy = null;
                }
            }
        }
    }
}
