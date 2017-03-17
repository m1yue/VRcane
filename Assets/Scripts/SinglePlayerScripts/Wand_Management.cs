using UnityEngine;
using System.Collections;

public class Wand_Management : MonoBehaviour {

    public void SelectPearlWand()
    {
        PersistentData.data.myWand = "PearlWand";
        createWand();
    }

    public void SelectFlameWand()
    {
        PersistentData.data.myWand= "FlameWand";
        createWand();
    }

    public void SelectAshWand()
    {
        PersistentData.data.myWand = "AshWand";
        createWand();
    }

    void createWand()
    {
        Quaternion wandRotate = new Quaternion(0,0,0,0);
        if(GameObject.Find("Old") != null)
        {
            wandRotate = GameObject.Find("Old").transform.rotation;
        }

        Vector3 wandPos = new Vector3(0, 2, 0);

        GameObject controller = null;

        if(GameObject.Find("Controller") != null)
        {
            Debug.Log("Found controller");
            controller = GameObject.Find("Controller");
            wandPos = controller.transform.position;
        }

        GameObject newWand = GameObject.Instantiate(Resources.Load(PersistentData.data.myWand), wandPos, wandRotate) as GameObject;

        newWand.tag = "New";

        Destroy(GameObject.FindWithTag("Old"));

        if (GameObject.Find("Controller") != null)
        {
            newWand.transform.SetParent(GameObject.Find("Controller").transform);
        }

        newWand.tag = "Old";

        newWand.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
    }
}
