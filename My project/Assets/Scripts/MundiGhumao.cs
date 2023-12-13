using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MundiGhumao : MonoBehaviour
{
    private void Update()
    {
        //if a is pressed mundi ghumao
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Rotate(0, 0, 90);
        }
    }

    IEnumerator lerpRotation(Quaternion targetRotation, float duration)
    {
        Quaternion startRotation = transform.rotation;
        float time = 0;
        while (time < duration)
        {
            transform.rotation = Quaternion.Lerp(startRotation, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetRotation;
    }
}
