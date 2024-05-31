using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    [SerializeField]
    GameObject _interactionUI;
    public GameObject interactionUI { get {  return _interactionUI; } }
    public GameObject UIObj { get; set; }
    [SerializeField]
    bool _Keydown;
    public bool Keydown {  get { return _Keydown; } set {  _Keydown = value; } }
    public float KeydownTimer { get; set; } = 0;
    private void OnDisable()
    {
        if(UIObj != null)
        {
            Destroy(UIObj);
        }
    }
    public virtual void Interact()
    {
        GetComponent<Collider2D>().enabled = false;
    }
    public virtual void KeydownInteract()
    {

    }
    public virtual void PlayerIn()
    {
        if (UIObj != null)
        {
            UIObj.SetActive(true);
        }
    }
    public virtual void PlayerOut()
    {
        KeydownTimer = 0f;
        if (UIObj != null)
        {
            UIObj.SetActive(false);
        }
    }
}
