using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour
{
    public Probe probe;
    private Rigidbody2D myRig;
    private SpawnParticles myParent;
    public Vector2 iniSpeed;
    public bool added;
    public bool DBAdded;
    public float X;
    public float Y;
    public int intX;
    public int intY;

    private void Start()
    {
        probe = this.transform.GetComponentInChildren<Probe>();
        myRig = this.GetComponent<Rigidbody2D>();
        myParent = this.transform.parent.GetComponent<SpawnParticles>();
        added = false;
        DBAdded = false;
        iniSpeed = myRig.velocity;
        iniSpeed.y = -2.0f; 
    }

    private void FixedUpdate()
    {
        Vector2 currenPos = this.transform.position;
        //Vector2 currentSpeed = myRig.velocity;
        if (currenPos.y < 5f && currenPos.y >= 4.7f) 
        {
            iniSpeed.y = -4f; //Add a strong force to suck the particle into bottom
            myRig.velocity = iniSpeed;
        }
        else if (currenPos.y < 4.7f )
        {
            iniSpeed.y = -2f;
            myRig.velocity = iniSpeed;
        }
        else
        {
            iniSpeed.y = -2f;
            myRig.velocity = iniSpeed; // Add 2f speed constantly
        }

        if (currenPos.y < 4.6f)
        {
            if (this.transform.tag == "D" && !DBAdded)
            {
                DBAdded = true;
                myParent.DBUnder++;
            }
            if (probe.isStable && !added)
            {
                AddToGrid();
                added = true;
            }
        }


    }

    void AddToGrid()
    {
        X = this.transform.position.x;
        Y = this.transform.position.y;
        if (X <= 1)
        {
            intX = 0;
        }
        else if (X <= 2)
        {
            intX = 1;
        }
        else if (X <= 3)
        {
            intX = 2;
        }
        else if (X <= 4)
        {
            intX = 3;
        }
        else if (X > 4) 
        {
            intX = 4;
        }

        if (Y <= 1)
        {
            intY = 0;
        }
        else if (Y <= 2)
        {
            intY = 1;
        }
        else if(Y <= 3)
        {
            intY = 2;
        }
        else if (Y <= 4)
        {
            intY = 3;
        }
        else if (Y > 4)
        {
            intY = 4;
        }
        myParent.grid[intX, intY] = this.transform;
        myParent.CheckForLines();
        myParent.CheckForColumns();
    }

}
