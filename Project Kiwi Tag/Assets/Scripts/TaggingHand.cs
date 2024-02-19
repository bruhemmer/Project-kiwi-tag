using UnityEngine;
using Photon.Pun;

public class TaggingHand : MonoBehaviourPunCallbacks
{
    // Tagging parameters
    public float taggingCooldown = 1f;
    private float lastTagTime = 0f;

    void Update()
    {
        // Check for tag input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time - lastTagTime >= taggingCooldown)
            {
                TagPlayer();
                lastTagTime = Time.time;
            }
        }
    }

    void TagPlayer()
    {
        // Perform a raycast to detect collisions with players
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            // Check if the object hit is a player
            if (hit.collider.CompareTag("Player"))
            {
                // Tag the player
                photonView.RPC("TagPlayer", RpcTarget.All, hit.collider.gameObject.GetPhotonView().ViewID);
            }
        }
    }

    [PunRPC]
    void TagPlayer(int playerViewID)
    {
        GameObject playerObj = PhotonView.Find(playerViewID).gameObject;
        Renderer playerRenderer = playerObj.GetComponent<Renderer>();

        // Change the player's color
        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.red; // Change color to red upon tagging
        }
    }
}
