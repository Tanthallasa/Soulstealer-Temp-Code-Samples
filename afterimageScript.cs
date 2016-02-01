using UnityEngine;
using System.Collections;

public class afterimageScript : MonoBehaviour {

    public GameObject tile;
    public GameObject image;
    public GameObject character;
    public GameObject starttile;
    public GameObject chariot;
    public int row;
    public int column;
    public int turnCounter;
    public int turnDelay;
    public bool rushing;
    public bool attacking;
    public int rushcount;
    public int rushdelay;
    public int rushdamage;
    public int rushdamagereset;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (attacking)
        {
            if (rushing)
            {
                rushcount++;
                TileScript tilescript = tile.GetComponent<TileScript>();
                chariotAI cai = chariot.GetComponent<chariotAI>();
                if (tilescript.occupiedBy == character && cai.afterimages)
                {
                    if (rushdamage != 0)
                    {
                        HealthScript charhealth = character.GetComponent<HealthScript>();
                        charhealth.health -= rushdamage;
                        charhealth.damaged = true;
                        rushdamage = 0;
                    }
                    //rushing = false;
                    //attacking = false;
                    //tile = starttile;
                    //image.transform.position = tile.transform.position;
                    //rushcount = 0;
                }
                if (rushcount >= rushdelay)
                {
                    rushcount = 0;

                    if (tilescript.left != null)
                    {
                        tile = tilescript.left;
                        image.transform.position = tile.transform.position;
                    }
                    else
                    {
                        rushing = false;
                        attacking = false;
                        tile = starttile;
                        image.transform.position = tile.transform.position;
                        rushcount = 0;
                        rushdamage = rushdamagereset;
                    }
                }
            }
        }
        else
        {
            turnCounter++;

            if (turnCounter >= turnDelay)
            {
                turnCounter = 0;
                move();
            }
        }
    }

    void move()
    {
        float movespace = Random.value;
        TileScript tilescript = tile.GetComponent<TileScript>();
        if (movespace < 1f / 9f)
        {
            GameObject temptile = GameObject.Find("Tile 4");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 3;
                column = 1;
                tile = temptile;
                image.transform.position = tile.transform.position;
            }
        }
        else if (movespace >= 1f / 9f && movespace < 2f / 9f)
        {
            GameObject temptile = GameObject.Find("Tile 5");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 3;
                column = 2;
                tile = temptile;
                image.transform.position = tile.transform.position;
            }
        }
        else if (movespace >= 2f / 9f && movespace < 3f / 9f)
        {
            GameObject temptile = GameObject.Find("Tile 6");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 3;
                column = 3;
                tile = temptile;
                image.transform.position = tile.transform.position;
            }
        }
        else if (movespace >= 3f / 9f && movespace < 4f / 9f)
        {
            GameObject temptile = GameObject.Find("Tile 10");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 2;
                column = 1;
                tile = temptile;
                image.transform.position = tile.transform.position;
            }
        }
        else if (movespace >= 4f / 9f && movespace < 5f / 9f)
        {
            GameObject temptile = GameObject.Find("Tile 11");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 2;
                column = 2;
                tile = temptile;
                image.transform.position = tile.transform.position;
            }
        }
        else if (movespace >= 5f / 9f && movespace < 6f / 9f)
        {
            GameObject temptile = GameObject.Find("Tile 12");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 2;
                column = 3;
                tile = temptile;
                image.transform.position = tile.transform.position;
            }
        }
        else if (movespace >= 6f / 9f && movespace < 7f / 9f)
        {
            GameObject temptile = GameObject.Find("Tile 16");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 1;
                column = 1;
                tile = temptile;
                image.transform.position = tile.transform.position;
            }
        }
        else if (movespace >= 7f / 9f && movespace < 8f / 9f)
        {
            GameObject temptile = GameObject.Find("Tile 17");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 1;
                column = 2;
                tile = temptile;
                image.transform.position = tile.transform.position;
            }
        }
        else if (movespace >= 8f / 9f)
        {
            GameObject temptile = GameObject.Find("Tile 18");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 1;
                column = 3;
                tile = temptile;
                image.transform.position = tile.transform.position;
            }
        }
    }

}
