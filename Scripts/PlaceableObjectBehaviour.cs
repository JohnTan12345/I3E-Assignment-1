/*
* Author: Tan Hong Yan John
* Date: 13 June 2025
* Description: Placeable object function
*/
using UnityEngine;

public class PlaceableObjectBehaviour : MonoBehaviour
{
    private GameObject interactableObject;
    private GameObject objectPlacement;
    public AudioClip pickUpSFX;
    public AudioClip placeSFX;
    public void PickUp(PlayerBehaviour player)
    {
        interactableObject = this.gameObject.transform.Find(this.gameObject.name).gameObject;
        objectPlacement = this.gameObject.transform.Find(string.Format("Place{0}", this.gameObject.name)).gameObject;

        interactableObject.SetActive(false);
        objectPlacement.SetActive(true);
        player.HasPlanks = true;
        AudioSource.PlayClipAtPoint(pickUpSFX, interactableObject.transform.position);
    }
    public void Place(PlayerBehaviour player)
    {
        interactableObject = this.gameObject.transform.Find(this.gameObject.name).gameObject;
        objectPlacement = this.gameObject.transform.Find(string.Format("Place{0}", this.gameObject.name)).gameObject;

        objectPlacement.GetComponent<MeshRenderer>().material = interactableObject.GetComponent<MeshRenderer>().material;
        objectPlacement.GetComponent<Collider>().isTrigger = false;

        foreach (Transform child in this.gameObject.transform)
        {
            child.gameObject.tag = "Untagged";
        };
        player.HasPlanks = false;
        AudioSource.PlayClipAtPoint(placeSFX, objectPlacement.transform.position);
    }
}
