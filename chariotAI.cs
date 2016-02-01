using UnityEngine;
using System.Collections;

public class chariotAI : MonoBehaviour {

    public GameObject tile;
    public GameObject chariot;
    public GameObject character;
    public GameObject image1;
    public GameObject image2;
    public GameObject starttile;
    public GameObject drtarget;
    public GameObject slash1;
    public GameObject slash2;
    public int row;
    public int column;
    public int turnCounter;
    public int turnDelay;
    public int moveCount;
    public int moveMax;
    public int atkChance;
    public bool attacking;
    public bool afterimages;
    public bool rushing;
    public bool slashing;
    public bool deathring;
    public int rushcount;
    public int rushdelay;
    public int rushdamage;
    public float drcounter;
    public float drchargetime;
    public int deathringdamage;
    public float slashcounter;
    public float slashdelay;
    public float jumpcounter;
    public float jumptime;
    public bool jumped;
    public int slashcount;
    public int slashdamage;

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
                if (tilescript.occupiedBy == character)
                {
                    HealthScript charhealth = character.GetComponent<HealthScript>();
                    charhealth.health -= rushdamage;
                    charhealth.damaged = true;

                    rushing = false;
                    attacking = false;
                    tile = starttile;
                    chariot.transform.position = tile.transform.position;
                    rushcount = 0;
                }
                else if (rushcount >= rushdelay)
                {
                    rushcount = 0;
         
                    if (tilescript.left != null)
                    {
                        tile = tilescript.left;
                        chariot.transform.position = tile.transform.position;
                    }
                    else
                    {
                        rushing = false;
                        attacking = false;
                        tile = starttile;
                        chariot.transform.position = tile.transform.position;
                        rushcount = 0;
                    }
                }
            }
            if (deathring)
            {
                drcounter += Time.deltaTime;
                if (drcounter >= drchargetime)
                {
                    drcounter = 0;
                    attacking = false;
                    deathring = false;
                    SpriteRenderer targetrenderer = drtarget.GetComponent<SpriteRenderer>();
                    targetrenderer.material.SetColor("_Color", Color.white);
                    deathringdamagecheck();
                }
            }
            if (slashing)
            {
                slashcontrol();
            }
        }
        else
        {
            turnCounter++;
            if (turnCounter >= turnDelay)
            {
                turnCounter = 0;
                if (moveCount < moveMax)
                {
                    float action = Random.value;
                    if (action < (float)atkChance / 100f)
                        attack();
                    else
                    {
                        move();
                    }
                }
                else
                    attack();
            }
        }

    }

    void attack()
    {
        moveCount = 0;
        float attackchoice = Random.value;
        if (!afterimages)
        {
            if (attackchoice < 1f / 4f)
                afterimage();
            else if (attackchoice >= 1f / 4f && attackchoice < 2f / 4f)
                jumpslash();
            else if (attackchoice >= 2f / 4f && attackchoice < 3f / 4f)
                rush();
            else if (attackchoice >= 3f / 4f)
                ringofdeath();
        }
        else
        {
            if (attackchoice < 1f / 3f)
                jumpslash();
            else if (attackchoice >= 1f / 3f && attackchoice < 2f / 3f)
                rush();
            else if (attackchoice >= 2f / 3f)
                ringofdeath();
        }

    }

    void ringofdeath()
    {
        attacking = true;
        deathring = true;
        drcounter = 0;
        CharacterScript cscript = character.GetComponent<CharacterScript>();
        drtarget = cscript.tile;
        if(drtarget != null)
        {
            SpriteRenderer targetrenderer = drtarget.GetComponent<SpriteRenderer>();
            targetrenderer.material.SetColor("_Color", Color.yellow);
        }
    }

    void deathringdamagecheck()
    {
        if (drtarget != null)
        {
            TileScript checking = drtarget.GetComponent<TileScript>();
            if (checking.occupiedBy == character)
            {
                HealthScript charhealth = character.GetComponent<HealthScript>();
                charhealth.health -= deathringdamage;
                charhealth.damaged = true;
            }
            if (checking.left != null)
            {
                TileScript temp = checking.left.GetComponent<TileScript>();
                if (temp.occupiedBy == character)
                {
                    HealthScript charhealth = character.GetComponent<HealthScript>();
                    charhealth.health -= deathringdamage;
                    charhealth.damaged = true;
                }
            }
            if (checking.right != null)
            {
                TileScript temp = checking.right.GetComponent<TileScript>();
                if (temp.occupiedBy == character)
                {
                    HealthScript charhealth = character.GetComponent<HealthScript>();
                    charhealth.health -= deathringdamage;
                    charhealth.damaged = true;
                }
            }
            if (checking.up != null)
            {
                TileScript temp = checking.up.GetComponent<TileScript>();
                if (temp.occupiedBy == character)
                {
                    HealthScript charhealth = character.GetComponent<HealthScript>();
                    charhealth.health -= deathringdamage;
                    charhealth.damaged = true;
                }
            }
            if (checking.down != null)
            {
                TileScript temp = checking.down.GetComponent<TileScript>();
                if (temp.occupiedBy == character)
                {
                    HealthScript charhealth = character.GetComponent<HealthScript>();
                    charhealth.health -= deathringdamage;
                    charhealth.damaged = true;
                }
            }
            if (afterimages)
            {
                if (checking.left != null)
                {
                    TileScript temp1 = checking.left.GetComponent<TileScript>();
                    if (temp1.up != null)
                    {
                        TileScript temp2 = temp1.up.GetComponent<TileScript>();
                        if (temp2.occupiedBy == character)
                        {
                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= deathringdamage;
                            charhealth.damaged = true;
                        }
                    }
                    if (temp1.down != null)
                    {
                        TileScript temp2 = temp1.down.GetComponent<TileScript>();
                        if (temp2.occupiedBy == character)
                        {
                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= deathringdamage;
                            charhealth.damaged = true;
                        }
                    }
                }
                if (checking.right != null)
                {
                    TileScript temp1 = checking.right.GetComponent<TileScript>();
                    if (temp1.up != null)
                    {
                        TileScript temp2 = temp1.up.GetComponent<TileScript>();
                        if (temp2.occupiedBy == character)
                        {
                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= deathringdamage;
                            charhealth.damaged = true;
                        }
                    }
                    if (temp1.down != null)
                    {
                        TileScript temp2 = temp1.down.GetComponent<TileScript>();
                        if (temp2.occupiedBy == character)
                        {
                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= deathringdamage;
                            charhealth.damaged = true;
                        }
                    }
                }
                if (checking.up != null)
                {
                    TileScript temp1 = checking.up.GetComponent<TileScript>();
                    if (temp1.left != null)
                    {
                        TileScript temp2 = temp1.left.GetComponent<TileScript>();
                        if (temp2.occupiedBy == character)
                        {
                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= deathringdamage;
                            charhealth.damaged = true;
                        }
                    }
                    if (temp1.right != null)
                    {
                        TileScript temp2 = temp1.right.GetComponent<TileScript>();
                        if (temp2.occupiedBy == character)
                        {
                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= deathringdamage;
                            charhealth.damaged = true;
                        }
                    }
                }
                if (checking.down != null)
                {
                    TileScript temp1 = checking.down.GetComponent<TileScript>();
                    if (temp1.left != null)
                    {
                        TileScript temp2 = temp1.left.GetComponent<TileScript>();
                        if (temp2.occupiedBy == character)
                        {
                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= deathringdamage;
                            charhealth.damaged = true;
                        }
                    }
                    if (temp1.right != null)
                    {
                        TileScript temp2 = temp1.right.GetComponent<TileScript>();
                        if (temp2.occupiedBy == character)
                        {
                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= deathringdamage;
                            charhealth.damaged = true;
                        }
                    }
                }
            }
        }
    }

    void jumpslash()
    {
        jumpcounter = 0;
        slashcount = 0;
        jumped = false;

        CharacterScript cscript = character.GetComponent<CharacterScript>();
        TileScript ctile = cscript.tile.GetComponent<TileScript>();
        if (ctile.right != null)
        {
            attacking = true;
            slashing = true;
            starttile = tile;
            TileScript tilescript = tile.GetComponent<TileScript>();
            tilescript.occupiedBy = null;
            TileScript tempscript = ctile.right.GetComponent<TileScript>();
            tempscript.occupiedBy = chariot;
            tile = ctile.right;
            chariot.transform.position = ctile.right.transform.position;
        }
    }

    void slashcontrol()
    {
        jumpcounter += Time.deltaTime;
        if (!jumped && jumpcounter >= jumptime)
        {
            jumped = true;
            TileScript tilescript = tile.GetComponent<TileScript>();
            tilescript.occupiedBy = null;
            chariot.transform.position = new Vector2(500, 500);
        }
        else
        {
            slashcounter += Time.deltaTime;
            if (slashcounter >= slashdelay)
            {
                slashcounter = 0;
                slashcount += 1;
                if (slashcount == 1)
                {
                    TileScript tilescript = tile.GetComponent<TileScript>();
                    if (tilescript.up != null)
                    {
                        TileScript temp = tilescript.up.GetComponent<TileScript>();
                        if (temp.occupiedBy == character)
                        {
                            attacking = false;
                            slashing = false;
                            tile = starttile;
                            TileScript tscript = tile.GetComponent<TileScript>();
                            tscript.occupiedBy = chariot;
                            chariot.transform.position = tile.transform.position;

                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= slashdamage;
                            charhealth.damaged = true;
                        }
                        else
                        {
                            Renderer s1r = slash1.GetComponent<Renderer>();
                            s1r.enabled = true;
                            slash1.transform.position = tilescript.up.transform.position;
                        }
                    }
                }
                else if (slashcount == 2)
                {
                    TileScript tilescript = tile.GetComponent<TileScript>();
                    if (tilescript.left != null)
                    {
                        Renderer s1r = slash1.GetComponent<Renderer>();
                        TileScript temp = tilescript.left.GetComponent<TileScript>();
                        if (temp.occupiedBy == character)
                        {
                            s1r.enabled = false;
                            attacking = false;
                            slashing = false;
                            tile = starttile;
                            TileScript tscript = tile.GetComponent<TileScript>();
                            tscript.occupiedBy = chariot;
                            chariot.transform.position = tile.transform.position;

                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= slashdamage;
                            charhealth.damaged = true;
                        }
                        else
                        {
                            s1r.enabled = true;
                            slash1.transform.position = tilescript.left.transform.position;
                        }
                    }
                }
                else if (slashcount == 3)
                {
                    TileScript tilescript = tile.GetComponent<TileScript>();
                    if (tilescript.left != null)
                    {
                        Renderer s1r = slash1.GetComponent<Renderer>();
                        TileScript temp = tilescript.left.GetComponent<TileScript>();
                        if (temp.down != null)
                        {
                            TileScript temp2 = temp.down.GetComponent<TileScript>();
                            if (temp2.left != null)
                            {
                                TileScript temp3 = temp2.left.GetComponent<TileScript>();
                                if (temp3.occupiedBy == character)
                                {
                                    s1r.enabled = false;
                                    attacking = false;
                                    slashing = false;
                                    tile = starttile;
                                    TileScript tscript = tile.GetComponent<TileScript>();
                                    tscript.occupiedBy = chariot;
                                    chariot.transform.position = tile.transform.position;

                                    HealthScript charhealth = character.GetComponent<HealthScript>();
                                    charhealth.health -= slashdamage;
                                    charhealth.damaged = true;
                                }
                                else
                                {
                                    s1r.enabled = true;
                                    slash1.transform.position = temp2.left.transform.position;
                                }
                            }
                        }
                    }
                }
                if (slashcount == 4)
                {
                    Renderer s1r = slash1.GetComponent<Renderer>();
                    s1r.enabled = false;
                    TileScript tilescript = tile.GetComponent<TileScript>();
                    if (tilescript.down != null)
                    {
                        TileScript temp = tilescript.down.GetComponent<TileScript>();
                        if (temp.occupiedBy == character)
                        {
                            attacking = false;
                            slashing = false;
                            tile = starttile;
                            TileScript tscript = tile.GetComponent<TileScript>();
                            tscript.occupiedBy = chariot;
                            chariot.transform.position = tile.transform.position;

                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= slashdamage;
                            charhealth.damaged = true;
                        }
                        else
                        {
                            Renderer s2r = slash2.GetComponent<Renderer>();
                            s2r.enabled = true;
                            slash2.transform.position = tilescript.down.transform.position;
                        }
                    }
                }
                else if (slashcount == 5)
                {
                    TileScript tilescript = tile.GetComponent<TileScript>();
                    if (tilescript.left != null)
                    {
                        Renderer s2r = slash2.GetComponent<Renderer>();
                        TileScript temp = tilescript.left.GetComponent<TileScript>();
                        if (temp.occupiedBy == character)
                        {
                            s2r.enabled = false;
                            attacking = false;
                            slashing = false;
                            tile = starttile;
                            TileScript tscript = tile.GetComponent<TileScript>();
                            tscript.occupiedBy = chariot;
                            chariot.transform.position = tile.transform.position;

                            HealthScript charhealth = character.GetComponent<HealthScript>();
                            charhealth.health -= slashdamage;
                            charhealth.damaged = true;
                        }
                        else
                        {
                            s2r.enabled = true;
                            slash2.transform.position = tilescript.left.transform.position;
                        }
                    }
                }
                else if (slashcount == 6)
                {
                    TileScript tilescript = tile.GetComponent<TileScript>();
                    if (tilescript.left != null)
                    {
                        Renderer s2r = slash2.GetComponent<Renderer>();
                        TileScript temp = tilescript.left.GetComponent<TileScript>();
                        if (temp.up != null)
                        {
                            TileScript temp2 = temp.up.GetComponent<TileScript>();
                            if (temp2.left != null)
                            {
                                TileScript temp3 = temp2.left.GetComponent<TileScript>();
                                if (temp3.occupiedBy == character)
                                {
                                    s2r.enabled = false;
                                    attacking = false;
                                    slashing = false;
                                    tile = starttile;
                                    TileScript tscript = tile.GetComponent<TileScript>();
                                    tscript.occupiedBy = chariot;
                                    chariot.transform.position = tile.transform.position;

                                    HealthScript charhealth = character.GetComponent<HealthScript>();
                                    charhealth.health -= slashdamage;
                                    charhealth.damaged = true;
                                }
                                else
                                {
                                    s2r.enabled = true;
                                    slash2.transform.position = temp2.left.transform.position;
                                }
                            }
                        }
                    }
                }
                else if (slashcount > 6)
                {
                    Renderer s2r = slash2.GetComponent<Renderer>();
                    s2r.enabled = false;
                    attacking = false;
                    slashing = false;
                    tile = starttile;
                    TileScript tscript = tile.GetComponent<TileScript>();
                    tscript.occupiedBy = chariot;
                    chariot.transform.position = tile.transform.position;
                }
            }
        }
    }

    void rush()
    {
        rushing = true;
        attacking = true;
        float attackchoice = Random.value;
        TileScript tilescript = tile.GetComponent<TileScript>();
        if (attackchoice < 1f / 3f)
        {
            GameObject temptile = GameObject.Find("Tile 5");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 3;
                column = 2;
                tile = temptile;
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;


                afterimageScript image1script = image1.GetComponent<afterimageScript>();
                image1script.tile = GameObject.Find("Tile 11");
                image1script.rushing = true;
                image1script.attacking = true;
                image1script.starttile = image1script.tile;
                image1.transform.position = image1script.tile.transform.position;


                afterimageScript image2script = image2.GetComponent<afterimageScript>();
                image2script.tile = GameObject.Find("Tile 17");
                image2script.rushing = true;
                image2script.attacking = true;
                image2script.starttile = image2script.tile;
                image2.transform.position = image2script.tile.transform.position;
            }
        }
        else if (attackchoice >= 1f / 3f && attackchoice < 2f / 3f)
        {
            GameObject temptile = GameObject.Find("Tile 11");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 2;
                column = 2;
                tile = temptile;
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;


                afterimageScript image1script = image1.GetComponent<afterimageScript>();
                image1script.tile = GameObject.Find("Tile 5");
                image1script.rushing = true;
                image1script.attacking = true;
                image1script.starttile = image1script.tile;
                image1.transform.position = image1script.tile.transform.position;


                afterimageScript image2script = image2.GetComponent<afterimageScript>();
                image2script.tile = GameObject.Find("Tile 17");
                image2script.rushing = true;
                image2script.attacking = true;
                image2script.starttile = image2script.tile;
                image2.transform.position = image2script.tile.transform.position;
            }
        }
        else if (attackchoice >= 2f / 3f)
        {
            GameObject temptile = GameObject.Find("Tile 17");
            TileScript tempscript = temptile.GetComponent<TileScript>();
            if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
            {
                row = 1;
                column = 2;
                tile = temptile;
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;


                afterimageScript image1script = image1.GetComponent<afterimageScript>();
                image1script.tile = GameObject.Find("Tile 5");
                image1script.rushing = true;
                image1script.attacking = true;
                image1script.starttile = image1script.tile;
                image1.transform.position = image1script.tile.transform.position;


                afterimageScript image2script = image2.GetComponent<afterimageScript>();
                image2script.tile = GameObject.Find("Tile 11");
                image2script.rushing = true;
                image2script.attacking = true;
                image2script.starttile = image2script.tile;
                image2.transform.position = image2script.tile.transform.position;
            }
        }
        starttile = tile;
    }

    void afterimage()
    {
        afterimages = true;
        SpriteRenderer ai1renderer = image1.GetComponent<SpriteRenderer>();
        ai1renderer.enabled = true;

        SpriteRenderer ai2renderer = image2.GetComponent<SpriteRenderer>();
        ai2renderer.enabled = true;
    }

    void move()
    {
        moveCount++;
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
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;
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
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;
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
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;
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
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;
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
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;
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
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;
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
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;
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
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;
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
                tilescript.occupiedBy = null;
                tempscript.occupiedBy = chariot;
                chariot.transform.position = tile.transform.position;
            }
        }
    }

}
