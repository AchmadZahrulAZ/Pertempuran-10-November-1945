using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PlayerTriggerEnter : MonoBehaviour
{
    [Tooltip("Function to call when the player enters the trigger")]
    [SerializeField] private UnityEvent onPlayerEnter;
    [SerializeField] private string customText = "Tekan F untuk berinteraksi";    
    private bool playerEntered = false;
    private GameObject textMessage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerEntered = true;
            SpawnTextMessage();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        playerEntered = false;
        if(textMessage != null)
        {
            Destroy(textMessage);
        }
    }

    void Update()
    {
        if (playerEntered)
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                onPlayerEnter?.Invoke();
            }
        }
    }

    private void SpawnTextMessage()
    {
        textMessage = new GameObject();
        textMessage.transform.position = this.transform.position;
        textMessage.AddComponent<MeshRenderer>();
        textMessage.AddComponent<TextMeshPro>();
        TMP_Text tmp = textMessage.GetComponent<TextMeshPro>();
        textMessage.GetComponent<MeshRenderer>().material = tmp.fontMaterial;
        textMessage.GetComponent<RectTransform>().sizeDelta = new Vector2(7f, 1f);

        //TMP
        tmp.text = customText;
        tmp.fontSize = 4f;
        tmp.alignment = TextAlignmentOptions.Center;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, this.transform.localScale);
    }
}
