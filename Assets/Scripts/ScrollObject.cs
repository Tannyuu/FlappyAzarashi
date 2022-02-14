using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    public float speed = 1.0f;
    public float startPosition;
    public float endPosition;

    // Start is called before the first frame update
    void Update()
    {
        transform.Translate(-1 * speed * Time.deltaTime, 0, 0);

        if(transform.position.x <= endPosition)
        {
            ScrollEnd();
        }
    }

    void ScrollEnd()
    {
        /*
        Debug.Log(transform.position.x);
        transform.position = new Vector3(8.0f, 0, 0); //隙間ができる
        */

        
        float diff = transform.position.x - endPosition;
        Vector3 restartPosition = transform.position;
        restartPosition.x = startPosition + diff;
        transform.position = restartPosition;
        

        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);//引数受け取れない スペルミスを教えてくれない アタッチされてるオブジェクトだけ呼び出せる .gameObject → .GetComponentのほうが自由に呼び出せる
    }
}
