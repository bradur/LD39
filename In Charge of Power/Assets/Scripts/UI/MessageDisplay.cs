// Date   : 30.07.2017 14:48
// Project: In Charge of Power
// Author : bradur

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MessageDisplay : MonoBehaviour
{

    [SerializeField]
    private SingleMessage messagePrefab;

    [SerializeField]
    private Transform container;

    private SingleMessage staticMessage;


    public void SpawnMessage(Vector2 position, Sprite sprite, string message)
    {
        SingleMessage newMessage = Instantiate(messagePrefab);
        newMessage.transform.SetParent(container, false);
        newMessage.Init(position, sprite, message, false, false);
    }


    public void SpawnMessage(Vector2 position, Sprite sprite, string message, float width)
    {
        SingleMessage newMessage = Instantiate(messagePrefab);
        newMessage.transform.SetParent(container, false);
        newMessage.Init(position, sprite, message, false, false, width);
    }

    public void SpawnStaticMessage(Vector2 position, Sprite sprite, string message, bool followMouse)
    {
        ClearStaticMessage();
        staticMessage = Instantiate(messagePrefab);
        staticMessage.transform.SetParent(container, false);
        staticMessage.Init(position, sprite, message, true, followMouse);
    }

    public void ClearStaticMessage()
    {
        if (staticMessage != null)
        {
            staticMessage.Kill();
        }
    }

}
