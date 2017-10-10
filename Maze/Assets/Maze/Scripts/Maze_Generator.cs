using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze_Generator : MonoBehaviour {
    #region seed instructions
    /* a set of commands detailing how to generate the maze. 
     * Each 3 character command corresponds to the placement of a prefab
     * Below is a summary of what each code does:
     * ARN
     * A - the section name
     *      0 - no section
     *      E - an end point or dead end
     *      G - a special item. This creates a spawnpoint that will spawn the prefab in spawnablePrefabs
     *          Add a 1 to the end of this token to make the spawn trigger on start; otherwise, the spawn will spawn when triggered
     *      R - a 90 degree turn
     *      S - a straight section with no turns
     *      T - a 3-way intersection
     *      X - a 4-way intersection
     * R - the rotation of the object. 
     *      0 - no rotation
     *      1 - rotate 90 degrees
     *      2 - rotate 180 degrees
     *      3 - rotate 270 degrees
     * N - the index of the prefab to use. 0 is the base prefab. 
     * 
     * a new row of the maze is created everytime a N is found at the end of a normal token. 
     * a maze is ended when a J is read. 
     */
    #endregion
    public string seed = "E30 E10";
    public Vector3 startingPosition = Vector3.zero;
    public bool autoGenerate = false;

    public GameObject SpawnPoint;
    public GameObject[] spawnablePrefabs;
    public GameObject[] eSections;
    public GameObject[] rSections;
    public GameObject[] sSections;
    public GameObject[] tSections;
    public GameObject[] xSections;


    // Use this for initialization
    void Start()
    {
        if (autoGenerate)
            generateMaze();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //change seed programatically
    public void setSeed(string _seed)
    {
        seed = _seed;
    }

    public void generateMaze()
    {
        // keep track of current position in the maze. Starts at the starting position. 
        Vector3 currentPos = startingPosition;
        int x_increment = 10;
        int z_increment = 10;

        //split seed into substrings that we can use
        string[] seedTokens = seed.ToUpper().Split(' ');

        // process each seed token 
        for (int i = 0; i < seedTokens.Length; i++)
        {
            // make the seed easier to process 
            char[] token = seedTokens[i].ToCharArray();

            // determine what array of prefabs to use
            GameObject[] prefab = null;
            // get the first character in the token
            char prefabChar = token[0];
            // assign the appropriate section
            switch (prefabChar)
            {
                case 'E':
                    prefab = eSections;
                    break;
                case 'G':
                    prefab = spawnablePrefabs;
                    break;
                case 'R':
                    prefab = rSections;
                    break;
                case 'S':
                    prefab = sSections;
                    break;
                case 'T':
                    prefab = tSections;
                    break;
                case 'X':
                    prefab = xSections;
                    break;
                case '0':
                default:
                    break;
            };

            // create an object based on the indicated prefab
            char indexChar = token[2];
            int index = int.Parse(indexChar.ToString());
            if (prefab != null)
            {
                // special instructions to create a proper spawnpoint
                if (prefab == spawnablePrefabs)
                {
                    GameObject spawnpoint = Instantiate(SpawnPoint);
                    // rotate the object based on the indicated rotation
                    char rotChar = token[1];
                    int rot = int.Parse(rotChar.ToString());
                    spawnpoint.transform.Rotate(new Vector3(0, 90 * rot, 0));
                    // move the object to the appropriate location
                    spawnpoint.transform.position = currentPos;
                    if (token.Length > 3)
                        if (int.Parse(token[3].ToString()) == 1)
                            spawnpoint.GetComponent<WytriamSTD.Spawn>().spawnWhenTriggered = true;



                }
                else
                {
                    GameObject go = Instantiate(prefab[index]);
                    // rotate the object based on the indicated rotation
                    char rotChar = token[1];
                    int rot = int.Parse(rotChar.ToString());
                    go.transform.Rotate(new Vector3(0, 90 * rot, 0));
                    // move the object to the appropriate location
                    go.transform.position = currentPos;
                }
            }
            currentPos.x += x_increment;

            if (token.Length > 3)
            {
                if (token[3] == 'N')
                {
                    currentPos.x = startingPosition.x;
                    currentPos.z += z_increment;
                }
                //stop generation on a j
                else if (token[3] == 'J')
                    return;
            }
        }
    }
}
