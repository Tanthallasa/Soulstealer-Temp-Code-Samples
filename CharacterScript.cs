using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

    private Animator animator;
    public GameObject tile;
    public GameObject character;
    public int row;
    public int column;
    public int charge;
    public int charged;


    ArrayList playerEffects;
    int netDamageDone;

    int netDamageTaken;


    // Use this for initialization
    void Start () {
        //SpriteRenderer renderer = character.GetComponent<SpriteRenderer>();
        playerEffects = new ArrayList();
        animator = this.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {


        /*
        #####################################################
        ################### Basic Attack ####################
        #####################################################
        */
        if (Input.GetKey("space"))
        {
            charge++;
            if (charge == charged)
                GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }

        if (Input.GetKeyDown("space"))
        {
            charge++;
            GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }

        if (Input.GetKeyUp("space"))
        {
            animator.Play("fire");
        }


        /*
        #####################################################
        ############## Character Movement ###################
        #####################################################
        */
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            if (Input.GetKeyDown("left") || Input.GetKeyDown("a"))
            {
                TileScript tilescript = tile.GetComponent<TileScript>();
                if (tilescript.left != null)
                {
                    TileScript temp = tilescript.left.GetComponent<TileScript>();
                    if (temp.passable && temp.allied && temp.occupiedBy == null)
                    {
                        animator.Play("move");
                        tile = tilescript.left;
                        tilescript.occupiedBy = null;
                        temp.occupiedBy = character;
                        tilescript = temp;
                        
                        column--;
                    }
                }
            }


            else if (Input.GetKeyDown("right") || Input.GetKeyDown("d"))
            {
                TileScript tilescript = tile.GetComponent<TileScript>();
                if (tilescript.right != null)
                {
                    TileScript temp = tilescript.right.GetComponent<TileScript>();
                    if (temp.passable && temp.allied && temp.occupiedBy == null)
                    {
                        animator.Play("move");
                        tile = tilescript.right;
                        tilescript.occupiedBy = null;
                        temp.occupiedBy = character;
                        tilescript = temp;
                        //character.transform.position = tile.transform.position;
                        column++;
                    }
                }
            }


            else if (Input.GetKeyDown("up") || Input.GetKeyDown("w"))
            {
                TileScript tilescript = tile.GetComponent<TileScript>();
                if (tilescript.up != null)
                {
                    TileScript temp = tilescript.up.GetComponent<TileScript>();
                    if (temp.passable && temp.allied && temp.occupiedBy == null)
                    {
                        animator.Play("move");
                        tile = tilescript.up;
                        tilescript.occupiedBy = null;
                        temp.occupiedBy = character;
                        tilescript = temp;
                        //character.transform.position = tile.transform.position;
                        row++;
                    }
                }
            }


            else if (Input.GetKeyDown("down") || Input.GetKeyDown("s"))
            {
                TileScript tilescript = tile.GetComponent<TileScript>();
                if (tilescript.down != null)
                {
                    TileScript temp = tilescript.down.GetComponent<TileScript>();
                    if (temp.passable && temp.allied && temp.occupiedBy == null)
                    {
                        animator.Play("move");
                        tile = tilescript.down;
                        tilescript.occupiedBy = null;
                        temp.occupiedBy = character;
                        tilescript = temp;
                        //character.transform.position = tile.transform.position;
                        row--;
                    }
                }
            }
        }
    }

    public void addEffects(CardScript effectCard)
    {
        playerEffects.Add(effectCard);
    }

    public void removeEffect(int index)
    {
        playerEffects.RemoveAt(index);
    }

    public ArrayList getEffects()
    {
        return playerEffects;
    }

    public void shoot()
    {
        int damage;
        if (charge >= charged)
            damage = 10;
        else
            damage = 1;
        charge = 0;
        GetComponent<Renderer>().material.SetColor("_Color", Color.white);

        TileScript tilescript = tile.GetComponent<TileScript>();
        while (tilescript.right != null && damage != 0)
        {
            TileScript temp = tilescript.right.GetComponent<TileScript>();
            if (temp.occupiedBy != null)
            {
                HealthScript enemy = temp.occupiedBy.GetComponent<HealthScript>();
                enemy.health -= damage;
                enemy.damaged = true;
                damage = 0;
            }
            tilescript = temp;
        }
    }
    public void move()
    {
        character.transform.position = tile.transform.position;
    }
}
