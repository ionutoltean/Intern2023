using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject selectedVisual;
    [SerializeField] private ClearCounter currentCounter;

    void Start()
    {
        Player.Instance.OnSelectedCounterChange += SelectedCounterChanged;
    }

    private void SelectedCounterChanged(object sender, Player.OnSelectedCounterEventArgs e)
    {
        selectedVisual.SetActive(e.selectedCounter == currentCounter);
    }

    // Update is called once per frame
}