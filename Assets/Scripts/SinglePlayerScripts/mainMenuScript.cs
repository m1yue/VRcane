using UnityEngine;
using System.Collections;

public class mainMenuScript : MonoBehaviour {

    /*public bool needWand;
    public bool isFirst;
    public GameObject pointer; 
    public static mainMenuScript mmsController;

    // Use this for initialization
    void Start () {
        mmsController = this;
        isFirst = true;
        needWand = true;
    }
	
	// Update is called once per frame
	void Update () {

        // Create Wand
        if (needWand)
        {
            createWand(isFirst);
        }

    }

    public void setNeedWand(bool needWand)
    {
        this.needWand = needWand;
    }

    public void createWand(bool isFirst)
    {
        if( !isFirst)
        {
            Destroy(GameObject.FindGameObjectWithTag("New"));
        }

        Quaternion wandRotate = new Quaternion(0, 0, 0, 0);

        GameObject newWand = GameObject.Instantiate(Resources.Load(GameControl.control.myWand), pointer.transform.position, wandRotate) as GameObject;

        newWand.tag = "Old";

        if(GameObject.Find("Controller") == null)
        {
            Debug.Log("Could not find controller");
        }

        if(GameObject.Find("Controller") != null)
        {
            Debug.Log("Found controller");
            newWand.transform.SetParent(GameObject.Find("Controller").transform);
        }

        newWand.tag = "New";

        Destroy(GameObject.FindGameObjectWithTag("Old"));

        //if (isFirst)
        //    newWand.transform.Rotate(new Vector3(100, 0));

        //else if (GameControl.control.myWand.Equals("PearlWand"))
        //    newWand.transform.Rotate(new Vector3(85, -17));

        //else if (GameControl.control.myWand.Equals("FlameWand"))
        //    newWand.transform.Rotate(new Vector3(85, 0));
        //else
        newWand.transform.Rotate(new Vector3(90, 0));

        needWand = false;
        isFirst = false;
    }*/
}
