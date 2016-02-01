using UnityEngine;
using System.Collections;

public class mephistoAI : MonoBehaviour {

    public GameObject tile;
    public GameObject mephisto;
    public GameObject character;
    public GameObject goodshield;
    public GameObject deathshield1;
    public GameObject deathshield2;
    public GameObject gstile;
    public GameObject ds1tile;
    public GameObject ds2tile;
    public GameObject fireball1;
    public GameObject fireball2;
    public GameObject fireball3;
    public GameObject f1tile;
    public GameObject f2tile;
    public GameObject f3tile;
    public GameObject ct1;
    public GameObject ct2;
    public GameObject ct3;
    public GameObject ct4;
    public int row;
    public int column;
    public int turnCounter;
    public int turnDelay;
    public int moveCount;
    public int moveMax;
    public int atkChance;
    public bool fireballing1;
    public bool fireballing2;
    public bool fireballing3;
    public int fDelay;
    public int betweenf;
    public int fc;
    public int fc1;
    public int fc2;
    public int fc3;
    public int fireballdamage;
    public bool cardthrowing;
    public bool cardthrowing1;
    public bool cardthrowing2;
    public bool cardthrowing3;
    public bool cardthrowing4;
    public int ctspeed;
    public int ctdamage;
    public int ctdelay;
    public int ctc;
    public GameObject ct1target;
    public GameObject ct2target;
    public GameObject ct3target;
    public GameObject ct4target;
    public Vector2 ct1initial;
    public Vector2 ct2initial;
    public Vector2 ct3initial;
    public Vector2 ct4initial;
    public float ct1distance;
    public float ct2distance;
    public float ct3distance;
    public float ct4distance;
    public float ct1step;
    public float ct2step;
    public float ct3step;
    public float ct4step;

	void Update () {
        turnCounter++;
        fc++;
        ctc++;

        if (fireballing1)
            fireball1cast();

        if (fireballing2)
            fireball2cast();

        if (fireballing3)
            fireball3cast();

        if (cardthrowing1)
            cardthrow1cast();

        if (cardthrowing2)
            cardthrow2cast();

        if (cardthrowing3)
            cardthrow3cast();

        if (cardthrowing4)
            cardthrow4cast();

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

    void attack()
    {
        moveCount = 0;
        float attackchoice = Random.value;
        if (attackchoice < 1f / 3f)
            cardthrow();
        else if (attackchoice >= 1f / 3f && attackchoice < 2f / 3f)
            fireballbarrage();
        else if (attackchoice >= 2f / 3f)
            cardtrick();

    }

    void cardthrow()
    {
        if (!cardthrowing)
        {
            cardthrowing = true;
            cardthrowing1 = true;
            SpriteRenderer ct1renderer = ct1.GetComponent<SpriteRenderer>();
            ct1renderer.enabled = true;
            SpriteRenderer ct2renderer = ct2.GetComponent<SpriteRenderer>();
            ct2renderer.enabled = true;
            SpriteRenderer ct3renderer = ct3.GetComponent<SpriteRenderer>();
            ct3renderer.enabled = true;
            SpriteRenderer ct4renderer = ct4.GetComponent<SpriteRenderer>();
            ct4renderer.enabled = true;
        }
    }

    void cardthrow1cast()
    {
        if (ctc >= ctdelay && !cardthrowing2 && ct1distance != 0)
        {
            cardthrowing2 = true;
            ctc = 0;
        }

        if (ct1distance == 0)
        {
            ctc = 0;
            CharacterScript charscript = character.GetComponent<CharacterScript>();
            ct1target = charscript.tile;
            SpriteRenderer targetrenderer = charscript.tile.GetComponent<SpriteRenderer>();
            targetrenderer.material.SetColor("_Color", Color.yellow);

            ct1initial = ct1.transform.position;
            ct1distance = Vector2.Distance(ct1target.transform.position, ct1initial);
            ct1step = ct1distance / ctspeed;
        }
        if (ct1target.transform.position != ct1.transform.position)
        {
            ct1.transform.position = Vector2.MoveTowards(ct1.transform.position, ct1target.transform.position, ct1step);
        }
        else if (ct1target.transform.position == ct1.transform.position)
        {
            ct1distance = 0;
            cardthrowing1 = false;
            ct1.transform.position = ct1initial;
            SpriteRenderer ct1renderer = ct1.GetComponent<SpriteRenderer>();
            ct1renderer.enabled = false;
            SpriteRenderer targetrenderer = ct1target.GetComponent<SpriteRenderer>();
            targetrenderer.material.SetColor("_Color", Color.white);

            CharacterScript charscript = character.GetComponent<CharacterScript>();
            if (charscript.tile == ct1target)
            {
                HealthScript charhealth = character.GetComponent<HealthScript>();
                charhealth.health -= ctdamage;
                charhealth.damaged = true;
            }
        }
    }

    void cardthrow2cast()
    {
        if (ctc >= ctdelay && !cardthrowing3)
        {
            cardthrowing3 = true;
            ctc = 0;
        }

        if (ct2distance == 0)
        {
            CharacterScript charscript = character.GetComponent<CharacterScript>();
            ct2target = charscript.tile;
            SpriteRenderer targetrenderer = charscript.tile.GetComponent<SpriteRenderer>();
            targetrenderer.material.SetColor("_Color", Color.yellow);

            ct2initial = ct2.transform.position;
            ct2distance = Vector2.Distance(ct2target.transform.position, ct2initial);
            ct2step = ct2distance / ctspeed;
        }
        if (ct2target.transform.position != ct2.transform.position)
        {
            ct2.transform.position = Vector2.MoveTowards(ct2.transform.position, ct2target.transform.position, ct2step);
        }
        else if (ct2target.transform.position == ct2.transform.position)
        {
            ct2distance = 0;
            cardthrowing2 = false;
            ct2.transform.position = ct2initial;
            SpriteRenderer ct2renderer = ct2.GetComponent<SpriteRenderer>();
            ct2renderer.enabled = false;
            SpriteRenderer targetrenderer = ct2target.GetComponent<SpriteRenderer>();
            targetrenderer.material.SetColor("_Color", Color.white);

            CharacterScript charscript = character.GetComponent<CharacterScript>();
            if (charscript.tile == ct2target)
            {
                HealthScript charhealth = character.GetComponent<HealthScript>();
                charhealth.health -= ctdamage;
                charhealth.damaged = true;
            }
        }
    }

    void cardthrow3cast()
    {
        if (ctc >= ctdelay && !cardthrowing4)
        {
            cardthrowing4 = true;
            ctc = 0;
        }

        if (ct3distance == 0)
        {
            CharacterScript charscript = character.GetComponent<CharacterScript>();
            ct3target = charscript.tile;
            SpriteRenderer targetrenderer = charscript.tile.GetComponent<SpriteRenderer>();
            targetrenderer.material.SetColor("_Color", Color.yellow);

            ct3initial = ct3.transform.position;
            ct3distance = Vector2.Distance(ct3target.transform.position, ct3initial);
            ct3step = ct3distance / ctspeed;
        }
        if (ct3target.transform.position != ct3.transform.position)
        {
            ct3.transform.position = Vector2.MoveTowards(ct3.transform.position, ct3target.transform.position, ct3step);
        }
        else if (ct3target.transform.position == ct3.transform.position)
        {
            ct3distance = 0;
            cardthrowing3 = false;
            ct3.transform.position = ct3initial;
            SpriteRenderer ct3renderer = ct3.GetComponent<SpriteRenderer>();
            ct3renderer.enabled = false;
            SpriteRenderer targetrenderer = ct3target.GetComponent<SpriteRenderer>();
            targetrenderer.material.SetColor("_Color", Color.white);

            CharacterScript charscript = character.GetComponent<CharacterScript>();
            if (charscript.tile == ct3target)
            {
                HealthScript charhealth = character.GetComponent<HealthScript>();
                charhealth.health -= ctdamage;
                charhealth.damaged = true;
            }
        }
    }

    void cardthrow4cast()
    {
        if (ct4distance == 0)
        {
            CharacterScript charscript = character.GetComponent<CharacterScript>();
            ct4target = charscript.tile;
            SpriteRenderer targetrenderer = charscript.tile.GetComponent<SpriteRenderer>();
            targetrenderer.material.SetColor("_Color", Color.yellow);

            ct4initial = ct4.transform.position;
            ct4distance = Vector2.Distance(ct4target.transform.position, ct4initial);
            ct4step = ct4distance / ctspeed;
        }
        if (ct4target.transform.position != ct4.transform.position)
        {
            ct4.transform.position = Vector2.MoveTowards(ct4.transform.position, ct4target.transform.position, ct4step);
        }
        else if (ct4target.transform.position == ct4.transform.position)
        {
            cardthrowing = false;
            ct4distance = 0;
            cardthrowing4 = false;
            ct4.transform.position = ct4initial;
            SpriteRenderer ct4renderer = ct4.GetComponent<SpriteRenderer>();
            ct4renderer.enabled = false;
            SpriteRenderer targetrenderer = ct4target.GetComponent<SpriteRenderer>();
            targetrenderer.material.SetColor("_Color", Color.white);

            CharacterScript charscript = character.GetComponent<CharacterScript>();
            if (charscript.tile == ct4target)
            {
                HealthScript charhealth = character.GetComponent<HealthScript>();
                charhealth.health -= ctdamage;
                charhealth.damaged = true;
            }
        }
    }

    void cardtrick()
    {
        if (ds1tile != null)
        {
            TileScript tile1 = ds1tile.GetComponent<TileScript>();
            tile1.occupiedBy = null;
        }

        if (ds2tile != null)
        {
            TileScript tile2 = ds2tile.GetComponent<TileScript>();
            tile2.occupiedBy = null;
        }

        if (gstile != null)
        {
            TileScript tile3 = gstile.GetComponent<TileScript>();
            tile3.occupiedBy = null;
        }

        if (column == 1)
            move();
        if (column == 1)
            move();
        if (column == 1)
            move();

        float safetile = Random.value;
        if (safetile < 1f/3f)
        {
            bool gsplaced = false;
            if (!gsplaced)
            {
                GameObject temptile = GameObject.Find("Tile 4");
                TileScript tempscript = temptile.GetComponent<TileScript>();
                if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                {
                    gstile = temptile;
                    tempscript.occupiedBy = goodshield;
                    goodshield.transform.position = gstile.transform.position;
                    SpriteRenderer gsrenderer = goodshield.GetComponent<SpriteRenderer>();
                    gsrenderer.enabled = true;
                    gsplaced = true;
                }
            }
            if (!gsplaced)
            {
                GameObject temptile = GameObject.Find("Tile 5");
                TileScript tempscript = temptile.GetComponent<TileScript>();
                if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                {
                    gstile = temptile;
                    tempscript.occupiedBy = goodshield;
                    goodshield.transform.position = gstile.transform.position;
                    SpriteRenderer gsrenderer = goodshield.GetComponent<SpriteRenderer>();
                    gsrenderer.enabled = true;
                    gsplaced = true;
                }
            }
            if (!gsplaced)
            {
                GameObject temptile = GameObject.Find("Tile 6");
                TileScript tempscript = temptile.GetComponent<TileScript>();
                if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                {
                    gstile = temptile;
                    tempscript.occupiedBy = goodshield;
                    goodshield.transform.position = gstile.transform.position;
                    SpriteRenderer gsrenderer = goodshield.GetComponent<SpriteRenderer>();
                    gsrenderer.enabled = true;
                    gsplaced = true;
                }
            }
            if (gsplaced)
            {
                bool ds1placed = false;
                bool ds2placed = false;
                if (!ds1placed)
                {
                    GameObject temptile = GameObject.Find("Tile 10");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds1tile = temptile;
                        tempscript.occupiedBy = deathshield1;
                        deathshield1.transform.position = ds1tile.transform.position;
                        SpriteRenderer ds1renderer = deathshield1.GetComponent<SpriteRenderer>();
                        ds1renderer.enabled = true;
                        ds1placed = true;
                    }
                }
                if (!ds1placed)
                {
                    GameObject temptile = GameObject.Find("Tile 11");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds1tile = temptile;
                        tempscript.occupiedBy = deathshield1;
                        deathshield1.transform.position = ds1tile.transform.position;
                        SpriteRenderer ds1renderer = deathshield1.GetComponent<SpriteRenderer>();
                        ds1renderer.enabled = true;
                        ds1placed = true;
                    }
                }
                if (!ds1placed)
                {
                    GameObject temptile = GameObject.Find("Tile 12");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds1tile = temptile;
                        tempscript.occupiedBy = deathshield1;
                        deathshield1.transform.position = ds1tile.transform.position;
                        SpriteRenderer ds1renderer = deathshield1.GetComponent<SpriteRenderer>();
                        ds1renderer.enabled = true;
                        ds1placed = true;
                    }
                }
                if (!ds2placed)
                {
                    GameObject temptile = GameObject.Find("Tile 16");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds2tile = temptile;
                        tempscript.occupiedBy = deathshield2;
                        deathshield2.transform.position = ds2tile.transform.position;
                        SpriteRenderer ds2renderer = deathshield2.GetComponent<SpriteRenderer>();
                        ds2renderer.enabled = true;
                        ds2placed = true;
                    }
                }
                if (!ds2placed)
                {
                    GameObject temptile = GameObject.Find("Tile 17");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds2tile = temptile;
                        tempscript.occupiedBy = deathshield2;
                        deathshield2.transform.position = ds2tile.transform.position;
                        SpriteRenderer ds2renderer = deathshield2.GetComponent<SpriteRenderer>();
                        ds2renderer.enabled = true;
                        ds2placed = true;
                    }
                }
                if (!ds2placed)
                {
                    GameObject temptile = GameObject.Find("Tile 18");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds2tile = temptile;
                        tempscript.occupiedBy = deathshield2;
                        deathshield2.transform.position = ds2tile.transform.position;
                        SpriteRenderer ds2renderer = deathshield2.GetComponent<SpriteRenderer>();
                        ds2renderer.enabled = true;
                        ds2placed = true;
                    }
                }
            }
        }
        else if (safetile >= 1f / 3f && safetile < 2f / 3f)
        {
            bool gsplaced = false;
            if (!gsplaced)
            {
                GameObject temptile = GameObject.Find("Tile 10");
                TileScript tempscript = temptile.GetComponent<TileScript>();
                if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                {
                    gstile = temptile;
                    tempscript.occupiedBy = goodshield;
                    goodshield.transform.position = gstile.transform.position;
                    SpriteRenderer gsrenderer = goodshield.GetComponent<SpriteRenderer>();
                    gsrenderer.enabled = true;
                    gsplaced = true;
                }
            }
            if (!gsplaced)
            {
                GameObject temptile = GameObject.Find("Tile 11");
                TileScript tempscript = temptile.GetComponent<TileScript>();
                if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                {
                    gstile = temptile;
                    tempscript.occupiedBy = goodshield;
                    goodshield.transform.position = gstile.transform.position;
                    SpriteRenderer gsrenderer = goodshield.GetComponent<SpriteRenderer>();
                    gsrenderer.enabled = true;
                    gsplaced = true;
                }
            }
            if (!gsplaced)
            {
                GameObject temptile = GameObject.Find("Tile 12");
                TileScript tempscript = temptile.GetComponent<TileScript>();
                if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                {
                    gstile = temptile;
                    tempscript.occupiedBy = goodshield;
                    goodshield.transform.position = gstile.transform.position;
                    SpriteRenderer gsrenderer = goodshield.GetComponent<SpriteRenderer>();
                    gsrenderer.enabled = true;
                    gsplaced = true;
                }
            }
            if (gsplaced)
            {
                bool ds1placed = false;
                bool ds2placed = false;
                if (!ds1placed)
                {
                    GameObject temptile = GameObject.Find("Tile 4");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds1tile = temptile;
                        tempscript.occupiedBy = deathshield1;
                        deathshield1.transform.position = ds1tile.transform.position;
                        SpriteRenderer ds1renderer = deathshield1.GetComponent<SpriteRenderer>();
                        ds1renderer.enabled = true;
                        ds1placed = true;
                    }
                }
                if (!ds1placed)
                {
                    GameObject temptile = GameObject.Find("Tile 5");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds1tile = temptile;
                        tempscript.occupiedBy = deathshield1;
                        deathshield1.transform.position = ds1tile.transform.position;
                        SpriteRenderer ds1renderer = deathshield1.GetComponent<SpriteRenderer>();
                        ds1renderer.enabled = true;
                        ds1placed = true;
                    }
                }
                if (!ds1placed)
                {
                    GameObject temptile = GameObject.Find("Tile 6");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds1tile = temptile;
                        tempscript.occupiedBy = deathshield1;
                        deathshield1.transform.position = ds1tile.transform.position;
                        SpriteRenderer ds1renderer = deathshield1.GetComponent<SpriteRenderer>();
                        ds1renderer.enabled = true;
                        ds1placed = true;
                    }
                }
                if (!ds2placed)
                {
                    GameObject temptile = GameObject.Find("Tile 16");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds2tile = temptile;
                        tempscript.occupiedBy = deathshield2;
                        deathshield2.transform.position = ds2tile.transform.position;
                        SpriteRenderer ds2renderer = deathshield2.GetComponent<SpriteRenderer>();
                        ds2renderer.enabled = true;
                        ds2placed = true;
                    }
                }
                if (!ds2placed)
                {
                    GameObject temptile = GameObject.Find("Tile 17");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds2tile = temptile;
                        tempscript.occupiedBy = deathshield2;
                        deathshield2.transform.position = ds2tile.transform.position;
                        SpriteRenderer ds2renderer = deathshield2.GetComponent<SpriteRenderer>();
                        ds2renderer.enabled = true;
                        ds2placed = true;
                    }
                }
                if (!ds2placed)
                {
                    GameObject temptile = GameObject.Find("Tile 18");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds2tile = temptile;
                        tempscript.occupiedBy = deathshield2;
                        deathshield2.transform.position = ds2tile.transform.position;
                        SpriteRenderer ds2renderer = deathshield2.GetComponent<SpriteRenderer>();
                        ds2renderer.enabled = true;
                        ds2placed = true;
                    }
                }
            }
        }
        else if (safetile >= 2f / 3f)
        {
            bool gsplaced = false;
            if (!gsplaced)
            {
                GameObject temptile = GameObject.Find("Tile 16");
                TileScript tempscript = temptile.GetComponent<TileScript>();
                if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                {
                    gstile = temptile;
                    tempscript.occupiedBy = goodshield;
                    goodshield.transform.position = gstile.transform.position;
                    SpriteRenderer gsrenderer = goodshield.GetComponent<SpriteRenderer>();
                    gsrenderer.enabled = true;
                    gsplaced = true;
                }
            }
            if (!gsplaced)
            {
                GameObject temptile = GameObject.Find("Tile 17");
                TileScript tempscript = temptile.GetComponent<TileScript>();
                if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                {
                    gstile = temptile;
                    tempscript.occupiedBy = goodshield;
                    goodshield.transform.position = gstile.transform.position;
                    SpriteRenderer gsrenderer = goodshield.GetComponent<SpriteRenderer>();
                    gsrenderer.enabled = true;
                    gsplaced = true;
                }
            }
            if (!gsplaced)
            {
                GameObject temptile = GameObject.Find("Tile 18");
                TileScript tempscript = temptile.GetComponent<TileScript>();
                if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                {
                    gstile = temptile;
                    tempscript.occupiedBy = goodshield;
                    goodshield.transform.position = gstile.transform.position;
                    SpriteRenderer gsrenderer = goodshield.GetComponent<SpriteRenderer>();
                    gsrenderer.enabled = true;
                    gsplaced = true;
                }
            }
            if (gsplaced)
            {
                bool ds1placed = false;
                bool ds2placed = false;
                if (!ds1placed)
                {
                    GameObject temptile = GameObject.Find("Tile 4");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds1tile = temptile;
                        tempscript.occupiedBy = deathshield1;
                        deathshield1.transform.position = ds1tile.transform.position;
                        SpriteRenderer ds1renderer = deathshield1.GetComponent<SpriteRenderer>();
                        ds1renderer.enabled = true;
                        ds1placed = true;
                    }
                }
                if (!ds1placed)
                {
                    GameObject temptile = GameObject.Find("Tile 5");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds1tile = temptile;
                        tempscript.occupiedBy = deathshield1;
                        deathshield1.transform.position = ds1tile.transform.position;
                        SpriteRenderer ds1renderer = deathshield1.GetComponent<SpriteRenderer>();
                        ds1renderer.enabled = true;
                        ds1placed = true;
                    }
                }
                if (!ds1placed)
                {
                    GameObject temptile = GameObject.Find("Tile 6");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds1tile = temptile;
                        tempscript.occupiedBy = deathshield1;
                        deathshield1.transform.position = ds1tile.transform.position;
                        SpriteRenderer ds1renderer = deathshield1.GetComponent<SpriteRenderer>();
                        ds1renderer.enabled = true;
                        ds1placed = true;
                    }
                }
                if (!ds2placed)
                {
                    GameObject temptile = GameObject.Find("Tile 10");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds2tile = temptile;
                        tempscript.occupiedBy = deathshield2;
                        deathshield2.transform.position = ds2tile.transform.position;
                        SpriteRenderer ds2renderer = deathshield2.GetComponent<SpriteRenderer>();
                        ds2renderer.enabled = true;
                        ds2placed = true;
                    }
                }
                if (!ds2placed)
                {
                    GameObject temptile = GameObject.Find("Tile 11");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds2tile = temptile;
                        tempscript.occupiedBy = deathshield2;
                        deathshield2.transform.position = ds2tile.transform.position;
                        SpriteRenderer ds2renderer = deathshield2.GetComponent<SpriteRenderer>();
                        ds2renderer.enabled = true;
                        ds2placed = true;
                    }
                }
                if (!ds2placed)
                {
                    GameObject temptile = GameObject.Find("Tile 12");
                    TileScript tempscript = temptile.GetComponent<TileScript>();
                    if (tempscript.passable && !tempscript.allied && tempscript.occupiedBy == null)
                    {
                        ds2tile = temptile;
                        tempscript.occupiedBy = deathshield2;
                        deathshield2.transform.position = ds2tile.transform.position;
                        SpriteRenderer ds2renderer = deathshield2.GetComponent<SpriteRenderer>();
                        ds2renderer.enabled = true;
                        ds2placed = true;
                    }
                }
            }
        }
    }

    void fireballbarrage()
    {
        fireballing1 = true;
        fc = 0;
        TileScript tilescript = tile.GetComponent<TileScript>();
        if (tilescript.left != null)
        {
            fc1++;
            f1tile = tilescript.left;
            fireball1.transform.position = tilescript.left.transform.position;
            SpriteRenderer f1renderer = fireball1.GetComponent<SpriteRenderer>();
            f1renderer.enabled = true;
        }

        float nexttile = Random.value;

        if (nexttile < .5f)
        {
            if (row == 1)
            {
                if (column == 1)
                {
                    f2tile = GameObject.Find("Tile 4");
                    f3tile = GameObject.Find("Tile 10");
                    row = 2;
                    column = 1;
                }
                else if (column == 2)
                {
                    f2tile = GameObject.Find("Tile 5");
                    f3tile = GameObject.Find("Tile 11");
                    row = 2;
                    column = 2;
                }
                else if (column == 3)
                {
                    f2tile = GameObject.Find("Tile 6");
                    f3tile = GameObject.Find("Tile 12");
                    row = 2;
                    column = 3;
                }
            }
            else if (row == 2)
            {
                if (column == 1)
                {
                    f2tile = GameObject.Find("Tile 4");
                    f3tile = GameObject.Find("Tile 16");
                    row = 1;
                    column = 1;
                }
                else if (column == 2)
                {
                    f2tile = GameObject.Find("Tile 5");
                    f3tile = GameObject.Find("Tile 17");
                    row = 1;
                    column = 2;
                }
                else if (column == 3)
                {
                    f2tile = GameObject.Find("Tile 6");
                    f3tile = GameObject.Find("Tile 18");
                    row = 1;
                    column = 3;
                }
            }
            else if (row == 3)
            {
                if (column == 1)
                {
                    f2tile = GameObject.Find("Tile 10");
                    f3tile = GameObject.Find("Tile 16");
                    row = 1;
                    column = 1;
                }
                else if (column == 2)
                {
                    f2tile = GameObject.Find("Tile 11");
                    f3tile = GameObject.Find("Tile 17");
                    row = 1;
                    column = 2;
                }
                else if (column == 3)
                {
                    f2tile = GameObject.Find("Tile 12");
                    f3tile = GameObject.Find("Tile 18");
                    row = 1;
                    column = 3;
                }
            }
        }
        else if (nexttile >= .5f)
        {
            if (row == 1)
            {
                if (column == 1)
                {
                    f2tile = GameObject.Find("Tile 10");
                    f3tile = GameObject.Find("Tile 4");
                    row = 3;
                    column = 1;
                }
                else if (column == 2)
                {
                    f2tile = GameObject.Find("Tile 11");
                    f3tile = GameObject.Find("Tile 5");
                    row = 3;
                    column = 2;
                }
                else if (column == 3)
                {
                    f2tile = GameObject.Find("Tile 12");
                    f3tile = GameObject.Find("Tile 6");
                    row = 3;
                    column = 3;
                }
            }
            else if (row == 2)
            {
                if (column == 1)
                {
                    f2tile = GameObject.Find("Tile 16");
                    f3tile = GameObject.Find("Tile 4");
                    row = 3;
                    column = 1;
                }
                else if (column == 2)
                {
                    f2tile = GameObject.Find("Tile 17");
                    f3tile = GameObject.Find("Tile 5");
                    row = 3;
                    column = 2;
                }
                else if (column == 3)
                {
                    f2tile = GameObject.Find("Tile 18");
                    f3tile = GameObject.Find("Tile 6");
                    row = 3;
                    column = 3;
                }
            }
            else if (row == 3)
            {
                if (column == 1)
                {
                    f2tile = GameObject.Find("Tile 16");
                    f3tile = GameObject.Find("Tile 10");
                    row = 2;
                    column = 1;
                }
                else if (column == 2)
                {
                    f2tile = GameObject.Find("Tile 17");
                    f3tile = GameObject.Find("Tile 11");
                    row = 2;
                    column = 2;
                }
                else if (column == 3)
                {
                    f2tile = GameObject.Find("Tile 18");
                    f3tile = GameObject.Find("Tile 12");
                    row = 2;
                    column = 3;
                }
            }
        }

    }

    void fireball1cast()
    {
        turnCounter = 0;
        fc1++;
        TileScript f1script = f1tile.GetComponent<TileScript>();
        if (fc >= betweenf && !fireballing2)
        {
            fireballing2 = true;
            fc = 0;
            TileScript t2script = f2tile.GetComponent<TileScript>();
            TileScript tilescript = tile.GetComponent<TileScript>();
            if (t2script.left != null && t2script.passable && !t2script.allied && t2script.occupiedBy == null)
            {
                tile = f2tile;
                tilescript.occupiedBy = null;
                t2script.occupiedBy = mephisto;
                mephisto.transform.position = f2tile.transform.position;

                fc2++;
                f2tile = t2script.left;
                fireball2.transform.position = t2script.left.transform.position;
                SpriteRenderer f2renderer = fireball2.GetComponent<SpriteRenderer>();
                f2renderer.enabled = true;
            }
        }
        if (f1script.occupiedBy == character)
        {
            HealthScript charhealth = character.GetComponent<HealthScript>();
            charhealth.health -= fireballdamage;
            charhealth.damaged = true;
            fireballing1 = false;

            SpriteRenderer f1renderer = fireball1.GetComponent<SpriteRenderer>();
            f1renderer.enabled = false;
            fc1 = 0;
        }
        else if (fc1 >= fDelay)
        {
            fc1 = 0;

            if (f1script.left != null)
            {
                f1tile = f1script.left;
                fireball1.transform.position = f1tile.transform.position;
            }
            else
            {
                fireballing1 = false;
                SpriteRenderer f1renderer = fireball1.GetComponent<SpriteRenderer>();
                f1renderer.enabled = false;
            }
        }
    }

    void fireball2cast()
    {
        turnCounter = 0;
        fc2++;
        TileScript f2script = f2tile.GetComponent<TileScript>();
        if (fc >= betweenf && !fireballing3)
        {
            fireballing3 = true;
            fc = 0;
            TileScript t3script = f3tile.GetComponent<TileScript>();
            TileScript tilescript = tile.GetComponent<TileScript>();
            if (t3script.left != null && t3script.passable && !t3script.allied && t3script.occupiedBy == null)
            {
                tile = f3tile;
                tilescript.occupiedBy = null;
                t3script.occupiedBy = mephisto;
                mephisto.transform.position = f3tile.transform.position;

                fc3++;
                f3tile = t3script.left;
                fireball3.transform.position = t3script.left.transform.position;
                SpriteRenderer f3renderer = fireball3.GetComponent<SpriteRenderer>();
                f3renderer.enabled = true;
            }
        }
        if (f2script.occupiedBy == character)
        {
            HealthScript charhealth = character.GetComponent<HealthScript>();
            charhealth.health -= fireballdamage;
            charhealth.damaged = true;
            fireballing2 = false;

            SpriteRenderer f2renderer = fireball2.GetComponent<SpriteRenderer>();
            f2renderer.enabled = false;
            fc2 = 0;
        }
        else if (fc2 >= fDelay)
        {
            fc2 = 0;

            if (f2script.left != null)
            {
                f2tile = f2script.left;
                fireball2.transform.position = f2tile.transform.position;
            }
            else
            {
                fireballing2 = false;
                SpriteRenderer f2renderer = fireball2.GetComponent<SpriteRenderer>();
                f2renderer.enabled = false;
            }
        }
    }

    void fireball3cast()
    {
        turnCounter = 0;
        fc3++;

        TileScript f3script = f3tile.GetComponent<TileScript>();
        if (f3script.occupiedBy == character)
        {
            HealthScript charhealth = character.GetComponent<HealthScript>();
            charhealth.health -= fireballdamage;
            charhealth.damaged = true;
            fireballing3 = false;

            SpriteRenderer f3renderer = fireball3.GetComponent<SpriteRenderer>();
            f3renderer.enabled = false;
            fc3 = 0;
        }
        else if (fc3 >= fDelay)
        {
            fc3 = 0;

            if (f3script.left != null)
            {
                f3tile = f3script.left;
                fireball3.transform.position = f3tile.transform.position;
            }
            else
            {
                fireballing3 = false;
                SpriteRenderer f3renderer = fireball3.GetComponent<SpriteRenderer>();
                f3renderer.enabled = false;
            }
        }
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
                tempscript.occupiedBy = mephisto;
                mephisto.transform.position = tile.transform.position;
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
                tempscript.occupiedBy = mephisto;
                mephisto.transform.position = tile.transform.position;
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
                tempscript.occupiedBy = mephisto;
                mephisto.transform.position = tile.transform.position;
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
                tempscript.occupiedBy = mephisto;
                mephisto.transform.position = tile.transform.position;
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
                tempscript.occupiedBy = mephisto;
                mephisto.transform.position = tile.transform.position;
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
                tempscript.occupiedBy = mephisto;
                mephisto.transform.position = tile.transform.position;
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
                tempscript.occupiedBy = mephisto;
                mephisto.transform.position = tile.transform.position;
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
                tempscript.occupiedBy = mephisto;
                mephisto.transform.position = tile.transform.position;
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
                tempscript.occupiedBy = mephisto;
                mephisto.transform.position = tile.transform.position;
            }
        }
    }
}
