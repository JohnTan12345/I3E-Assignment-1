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
    public AudioClip KeycardReaderBeep;
    public AudioClip DoorSFX;

    public void UseKeycard(PlayerBehaviour player) 
    {
        if (player.HasKeycard)
        {
            player.HasKeycard = false;
            AudioSource.PlayClipAtPoint(KeycardReaderBeep, this.gameObject.transform.position);

            door = this.gameObject.transform.parent.Find("door");
            doorLeft = door.Find("doorLeft");
            doorRight = door.Find("doorRight");

            StartCoroutine(MoveDoor(doorLeft, doorRight));
            AudioSource.PlayClipAtPoint(DoorSFX, door.transform.position);
            this.gameObject.tag = "Untagged";

            this.gameObject.GetComponent<ScoreScript>().canAddScore = true;
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

    IEnumerator MoveDoor(Transform doorLeft, Transform doorRight) // Door Sliding Animation
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
