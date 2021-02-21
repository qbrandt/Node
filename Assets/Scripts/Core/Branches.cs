using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Branches : MonoBehaviour
{
    public GameObject branch1;
    public GameObject branch2;
    public GameObject branch3;
    public GameObject branch4;
    public GameObject branch5;
    public GameObject branch6;
    public GameObject branch7;
    public GameObject branch8;
    public GameObject branch9;
    public GameObject branch10;
    public GameObject branch11;
    public GameObject branch12;
    public GameObject branch13;
    public GameObject branch14;
    public GameObject branch15;
    public GameObject branch16;
    public GameObject branch17;
    public GameObject branch18;
    public GameObject branch19;
    public GameObject branch20;
    public GameObject branch21;
    public GameObject branch22;
    public GameObject branch23;
    public GameObject branch24;
    public GameObject branch25;
    public GameObject branch26;
    public GameObject branch27;
    public GameObject branch28;
    public GameObject branch29;
    public GameObject branch30;
    public GameObject branch31;
    public GameObject branch32;
    public GameObject branch33;
    public GameObject branch34;
    public GameObject branch35;
    public GameObject branch36;

    public List<branch> BranchObjects = new List<branch>();


    public class branch
    {
        public bool owned = false;
        public int player = 0;
        //Add two node somehow
    }
        
    // Start is called before the first frame update
    void Start()
    {
        SetUpBranches();
    }

    void SetUpBranches()
    {
        for(int i = 0; i < 36; i++)
        {
            BranchObjects.Add(new branch());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
