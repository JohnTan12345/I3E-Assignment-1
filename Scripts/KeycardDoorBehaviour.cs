/*
* Author: Tan Hong Yan John
* Date: 13 June 2025
* Description: Keycard Door that opens after using a keycard
*/

using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class KeycardDoorBehaviour : MonoBehaviour
{
    public GameObject KeycardUI;
    private Transform door;
    private Transform doorLeft;
    private Transform doorRight;
    private float doorMoved = 0;

    public void UseKeycard(PlayerBehaviour player)
    {
        if (player.HasKeycard)
        {
            player.HasKeycard = false;

            door = this.gameObject.transform.parent.Find("door");
            doorLeft = door.Find("doorLeft");
            doorRight = door.Find("doorRight");

            StartCoroutine(MoveDoor(doorLeft, doorRight));
            this.gameObject.tag = "Untagged";
        }
        else
        {
            StartCoroutine(KeycardUIShow());
        }
    }

    IEnumerator KeycardUIShow()
    {
        KeycardUI.SetActive(true);
        yield return new WaitForSeconds(2f);
        KeycardUI.SetActive(false);
    }

    IEnumerator MoveDoor(Transform doorLeft, Transform doorRight) // Door Animations
    {
        while (doorMoved < 1)
        {
            doorLeft.position = new Vector3(doorLeft.position.x - .1f, doorLeft.position.y, doorLeft.position.z);
            doorRight.position = new Vector3(doorRight.position.x - -.1f, doorRight.position.y, doorRight.position.z);
            doorMoved += .1f;
            yield return new WaitForSeconds(.1f);
        }
        yield return null;
    }

}
