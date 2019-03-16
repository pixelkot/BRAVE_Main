using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(SlideOpen());
    }

    public IEnumerator SlideOpen()
    {
        WaitForSeconds delay = new WaitForSeconds(0);
        while (transform.position.y > -1)
        {
            //Debug.Log(transform.position.y);
            yield return delay;
            lower();
        }
    }

    public void lower()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, -1, transform.position.z), 0.01f);
    }
}
