using System.Collections;
using UnityEngine;

public class LeverBehaviour : MonoBehaviour
{
    private int leverRotated = 0;

    public GameObject affectedGameObject;
    private Transform leverHandle;

    public void ActivateLever()
    {
        leverHandle = this.gameObject.transform.Find("Lever Handle");
        Destroy(affectedGameObject);
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
        yield return null;
    }
}
