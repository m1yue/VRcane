using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class Teleport : MonoBehaviour {
  private Vector3 startingPosition;

  public Material inactiveMaterial;
  public Material gazedAtMaterial;

  void Start() {
    startingPosition = transform.localPosition;
    SetGazedAt(false);
  }

  void LateUpdate() {
    GvrViewer.Instance.UpdateState();
    if (GvrViewer.Instance.BackButtonPressed) {
      Application.Quit();
    }
  }

  public void SetGazedAt(bool gazedAt) {
        if(GameObject.FindWithTag("Player") != null)
        {
            GameObject localPlayer = GameObject.FindGameObjectWithTag("Player");
            if(localPlayer.GetComponent<PlayerController>() != null)
            {
                PlayerController controller = localPlayer.GetComponent<PlayerController>();
                if(controller.player != null)
                {
                    Player mainPlayer = controller.player;
                    if (mainPlayer.getSpellIndex() == 0 ||
                (gazedAt == false &&
                localPlayer.GetComponent<PlayerController>().player.getSpellIndex() != 0))
                    {
                        if (inactiveMaterial != null && gazedAtMaterial != null)
                        {
                            GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
                            return;
                        }
                        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
                    }
                }
            }
        }
  }

  public void Reset() {
    transform.localPosition = startingPosition;
  }

    public void TeleportTo()
    {
        GameObject localPlayer = GameObject.FindWithTag("Player");
        if(localPlayer.GetComponent<PlayerController>().player.getSpellIndex() == 0 && 
            localPlayer.GetComponent<PlayerController>().player.getMana() > 0.2f)
        {
            Vector3 playerPos = new Vector3(this.transform.position.x, localPlayer.transform.position.y, this.transform.position.z);
            localPlayer.GetComponent<PlayerController>().teleport(true, playerPos);
        }
    }
}
