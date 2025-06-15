/*
* Author: Tan Hong Yan John
* Date: 14 June 2025
* Description: Show Hint
*/

using UnityEngine;

public class HintBehaviour : MonoBehaviour
{
    public GameObject hintUI;
    public void hint()
    {
        hintUI.SetActive(true);
    }
}
