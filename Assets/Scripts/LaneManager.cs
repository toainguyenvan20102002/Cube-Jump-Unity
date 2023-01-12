using UnityEngine;
using System.Collections.Generic;

public class LaneManager : MonoBehaviour
{
    public static LaneManager instance = null;

    [SerializeField] private Transform[] startPositions;

    [SerializeField] private List<GameObject> listCube = new List<GameObject>();

    private int minScale = 2;
    private int maxScale = 3;

    private static float speedCube = 5;

    public static float GetSpeed()
    {
        return speedCube;
    }
    public static void AddSpeed(float amount)
    {
        if (amount <= 0) return;
        speedCube += amount;
    }

    public void SetSpeed()
    {
        for(int i=0; i < listCube.Count; i++)
        {
            listCube[i].GetComponent<LandCubeMove>().SpeedMove = speedCube;
        }
    }
    private void Start()
    {
        if(instance == null) instance = this;

        // Set velocity for cube
        foreach (var cube in listCube)
        {
            cube.GetComponent<LandCubeMove>().SpeedMove = speedCube;
        }
    }

    public void RotateLandCube()
    {
        // Get first cube from list
        GameObject cube = listCube[listCube.Count - 1];
        listCube.RemoveAt(listCube.Count - 1);

        // Calculate line
        GameObject lastCube = listCube[0];
        int currentLine;
        do
        {
            int lastCubeLine = lastCube.GetComponent<LandCubeMove>().NumOrder;
            int offsetLine = Random.Range(-1, 2);
            currentLine = lastCubeLine + offsetLine;
            if (currentLine < 0) currentLine = 0;
            else if (currentLine > 2) currentLine = 2;
        } while (CheckLoopLine(currentLine));

        cube.GetComponent<LandCubeMove>().NumOrder = currentLine;
        // Calculate Scale
        int scale = Random.Range(minScale, maxScale + 1);
        cube.transform.localScale = new Vector3(cube.transform.localScale.x,cube.transform.localScale.y,scale);

        cube.transform.position = new Vector3(startPositions[currentLine].position.x, 
                                                startPositions[currentLine].position.y,
                                                lastCube.transform.position.z + lastCube.transform.localScale.z * 1/2
                                                + scale * 1/2.0f + 0.1f);
        cube.GetComponent<LandCubeMove>().IsRotated = false;
        listCube.Insert(0, cube);
    }

    bool CheckLoopLine(int line)
    {
        //if (listCube[0].GetComponent<LandCubeMove>().NumOrder != listCube[1].GetComponent<LandCubeMove>().NumOrder)
        //    return false;
        if (line != listCube[0].GetComponent<LandCubeMove>().NumOrder) return false;
        return true;
    }
}
