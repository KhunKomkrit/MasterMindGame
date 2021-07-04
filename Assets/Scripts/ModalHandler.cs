using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalHandler : MonoBehaviour
{

    public void OnShowHide(bool status) {
        this.gameObject.SetActive(status);
    }
}
