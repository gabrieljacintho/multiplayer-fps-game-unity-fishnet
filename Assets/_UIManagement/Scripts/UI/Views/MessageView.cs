using System.Collections;
using TMPro;
using UnityEngine;

public class MessageView : View
{
    [SerializeField] private TextMeshProUGUI _messageText;


    public override void Show(object args = null)
    {
        if (args is string message)
        {
            _messageText.text = message;
        }
        else
        {
            _messageText.text = "No Data";
        }

        base.Show(args);
    }
}
