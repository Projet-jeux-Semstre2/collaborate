using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Manager : MonoBehaviour
{
    private float _maxSize, _speed;
    private GameObject _objectToGrow;
    private bool onGrow;
    


    public void ObjectToGrow(GameObject objectToGrow)
    {
        _objectToGrow = objectToGrow;
    }

    public void maxSize(float maxSize)
    {
        _maxSize = maxSize;
    }
    
    public void speed(float speed)
    {
        _speed = speed;
    }

    public void OnClick()
    {
        if (!onGrow)
        {
            StartCoroutine(GrowSizeCoroutine(_objectToGrow, _maxSize, _speed));
        }
        
    }

    

    public IEnumerator GrowSizeCoroutine(GameObject objectToGrow,float maxSize, float speed)
    {
        onGrow = true;
        Vector3 nativeSize = objectToGrow.transform.localScale;
        Vector3 sizeMax = new Vector3(maxSize, maxSize, maxSize);
        while (objectToGrow.transform.localScale.x < maxSize -0.002f)
        {
            objectToGrow.transform.localScale = Vector3.Lerp(objectToGrow.transform.localScale, sizeMax, Time.deltaTime * speed );
            yield return null;
        }

        while (objectToGrow.transform.localScale.x > nativeSize.x + 0.0002f)
        {
            objectToGrow.transform.localScale = Vector3.Lerp(objectToGrow.transform.localScale, nativeSize, Time.deltaTime * speed);
            yield return null;
        }

        objectToGrow.transform.localScale = nativeSize;
        onGrow = false;
        yield return null;
    }
}
