using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTrigger : MonoBehaviour
{
    GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindWithTag("GameController");
    }

    void OnTriggerExit2D(Collider2D other)
    {
        gameController.SendMessage("IncreaseScore");//gameControllerオブジェクトにIncreaseScoreを発動させるようにメッセージを送っている 
    }
}
