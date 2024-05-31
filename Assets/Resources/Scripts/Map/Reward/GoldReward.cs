using UnityEngine;

public class GoldReward : Interaction
{
    private void Start()
    {
        if (interactionUI != null)
        {
            UIObj = Instantiate(interactionUI, UIManager.current.InteractionObj.transform);
            UIObj.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,
                    transform.position.y - 1f, transform.position.z));
            UIObj.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (UIObj != null)
        {
            UIObj.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x,
                    transform.position.y - 1f, transform.position.z));
        }
    }
    public override void Interact()
    {
        int randGold = 450 + (((StageManager.current.stage[0] - 1) * 2 + StageManager.current.stage[1]) * 75);
        GameManager.current.gold += Random.Range(randGold - 10, randGold + 11);
        Destroy(gameObject);
    }
}
