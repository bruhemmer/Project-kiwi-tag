using UnityEngine;
using Photon.Pun;

public class TagScript : MonoBehaviourPunCallbacks
{
    // Tagging logic
    void OnCollisionEnter(Collision collision)
    {
        PhotonView photonView = GetComponent<PhotonView>();
        if (photonView == null || !photonView.IsMine) return; // Only execute on the local player

        // Check if the collided object has a PhotonView
        PhotonView otherPhotonView = collision.gameObject.GetComponent<PhotonView>();
        if (otherPhotonView != null && otherPhotonView.IsMine)
        {
            // Tag the other player
            photonView.RPC("TagPlayer", RpcTarget.All);
        }
    }

    // RPC method to tag the player across the network
    [PunRPC]
    void TagPlayer()
    {
        // Change the player's color
        Renderer playerRenderer = GetComponent<Renderer>();
        if (playerRenderer != null)
        {
            playerRenderer.material.color = Color.red; // Change color to red upon tagging
        }
    }
}
