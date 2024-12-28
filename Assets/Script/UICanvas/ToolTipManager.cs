using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipManager : MonoBehaviour
{
    public GameObject toolTipBox;
    public Text tipText;
    [Header("ÊÂ¼þ¼àÌý")]
    public ToolTipEventSO ToolTipEvent;



    private void Awake()
    {
        ToolTipEvent.OnEventRaised += ShowTip;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        ToolTipEvent.OnEventRaised -= ShowTip;
    }

    void ShowTip(string text, float time)
    {
        tipText.text = text;
        toolTipBox.SetActive(true);
        StartCoroutine(CloseToolTip(time));
    }
    IEnumerator CloseToolTip(float time)
    {
        yield return new WaitForSeconds(time);
        toolTipBox.SetActive(false);
    }
}
