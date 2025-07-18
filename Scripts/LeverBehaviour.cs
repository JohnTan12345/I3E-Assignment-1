/*
* Author: Tan Hong Yan John
* Date: 13 June 2025
* Description: Lever Function and Animation
*/

using System.Collections;
using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{
    private int leverRotated = 0;

    public GameObject affectedGameObject;
    public AudioClip affectedGameObjectInteractSFX;
    private Transform leverHandle;

    public void ActivateLever()
    {
        leverHandle = this.gameObject.transform.Find("Lever Handle");
        this.gameObject.tag = "Untagged";
        StartCoroutine(LeverAnimations(leverHandle));
    }

    IEnumerator LeverAnimations(Transform leverHandle)
    {
        while (leverRotated < 120)
        {
            leverHandle.Rotate(new Vector3(leverHandle.rotation.x, leverHandle.rotation.y - -30f, leverHandle.rotation.z));
            leverRotated += 30;
            yield return new WaitForSeconds(.1f);
        }
        affectedGameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(affectedGameObjectInteractSFX, affectedGameObject.transform.position);
        yield return null;
    }
}
