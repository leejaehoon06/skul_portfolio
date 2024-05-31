using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    Skill1Frame skill1Frame;
    [SerializeField]
    Skill1FrameMore skill1FrameMore;
    [SerializeField]
    Skill2Frame skill2Frame;
    [SerializeField]
    Skill2FrameMore skill2FrameMore;
    [SerializeField]
    ItemFrame itemFrame;
    [SerializeField]
    ItemFrameMore itemFrameMore;
    [SerializeField]
    StatFrame statFrame;
    [SerializeField]
    GameObject SkulObject;
    [SerializeField]
    GameObject QuintObject;
    [SerializeField]
    GameObject ItemObject;
    [SerializeField]
    GameObject SkulQuintitemObject;
    [SerializeField]
    GameObject SkulItemObject;
    [SerializeField]
    GameObject Detail;
    GameObject LastFrame;
    ItemSlot SelectItemSlot;
    [SerializeField]
    UnityEngine.UI.Image ItemDestroyFillImage;
    [SerializeField]
    Animator InvenAnim;
    [SerializeField]
    Animator InvenMaskAnim;
    [SerializeField]
    Animator InvenBackgroundAnim;


    PlayerManager player;
    public List<Skul> skuls { get; set; } = new List<Skul>();
    public List<Item> items { get; set; } = new List<Item>();
    List<GameObject> itemObjs = new List<GameObject>();
    public List<Inscription> inscriptions { get; set; } = new List<Inscription>();
    public List<GameObject> inscriptionObjs { get; set; } = new List<GameObject>();
    [SerializeField]
    Item[] StartItems;
    [SerializeField]
    Skul[] StartSkuls;

    [SerializeField]
    SkulSlot[] SkulSlots;
    [SerializeField]
    ItemSlot[] ItemSlots;
    [SerializeField]
    InscriptionSlot[] InscriptionSlots; //추후에 각인 기능 추가
    public InscriptionSlot[] _InscriptionSlots { get {  return _InscriptionSlots; } }

    private void Start()
    {
        player = FindObjectOfType<PlayerManager>();
        for (int i = 0; i < StartSkuls.Length; i++)
        {
            AddSkul(StartSkuls[i]);
        }
        for (int i = 0; i < StartItems.Length; i++)
        {
            AddItem(StartItems[i]);
        }
        ItemFreshSlot();
        InscripFreshSlot();
        SkulFreshSlot();
        InvenOff();
    }
    private void OnEnable()
    {
        if (SkulSlots[0].skul != null)
        {
            InvenOn();
        }
    }
    float timer = 0;
    bool IsDelItem = true;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
        {
            InvenOff();
        }
        if (Input.GetKey(KeyCode.S))
        {
            StatOn();
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            StatOff();
        }
        if (itemFrame.gameObject.activeSelf == true)
        {
            if (Input.GetKey(KeyCode.G))
            {
                DelItem();
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                IsDelItem = true;
            }
            else if (Input.GetKeyUp(KeyCode.G))
            {
                timer = 0;
                ItemDestroyFillImage.fillAmount = 1;
            }
        }
        if(skill2Frame.gameObject.activeSelf == true)
        {
            //스킬 교체
        }
    }
    public void InvenOff()
    {
        player.PlayerOn = true;
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }
    public void InvenOn()
    {
        gameObject.SetActive(true);
        player.PlayerOn = false;
        SkulSlots[0].Select();
        Time.timeScale = 0f;
        InvenAnim.SetTrigger("loop");
        InvenMaskAnim.SetTrigger("loop");
        InvenBackgroundAnim.SetTrigger("loop");
    }
    public void StatOn()
    {
        statFrame.gameObject.SetActive(true);
        statFrame.FrameOn();
        skill1Frame.gameObject.SetActive(false);
        skill2Frame.gameObject.SetActive(false);
        itemFrame.gameObject.SetActive(false);
        Detail.SetActive(false);
    }
    public void StatOff()
    {
        statFrame.gameObject.SetActive(false);
        if (LastFrame == null)
        {
            NoneOn();
        }
        else
        {
            LastFrame.gameObject.SetActive(true);
        }
        Detail.SetActive(true);
    }
    public void SkulSkill1On(SkulSlot skulslot)
    {
        LastFrame = skill1Frame.gameObject;
        skill1Frame.gameObject.SetActive(true);
        skill1Frame.FrameOn(skulslot.skul);
        if (Input.GetKey(KeyCode.F))
        {
            skill1FrameMore.gameObject.SetActive(true);
            skill1FrameMore.FrameOn(skulslot.skul);
        }
        skill2Frame.gameObject.SetActive(false);
        itemFrame.gameObject.SetActive(false);
        SkulObject.SetActive(true);
        QuintObject.SetActive(false);
        ItemObject.SetActive(false);
        SkulQuintitemObject.SetActive(true);
        SkulItemObject.SetActive(true);
    }
    public void SkulSkill2On(SkulSlot skulslot)
    {
        LastFrame = skill2Frame.gameObject;
        skill2Frame.gameObject.SetActive(true);
        skill2Frame.FrameOn(skulslot.skul);
        if (Input.GetKey(KeyCode.F))
        {
            skill2FrameMore.gameObject.SetActive(true);
            skill2FrameMore.FrameOn(skulslot.skul);
        }
        skill1Frame.gameObject.SetActive(false);
        itemFrame.gameObject.SetActive(false);
        SkulObject.SetActive(true);
        QuintObject.SetActive(false);
        ItemObject.SetActive(false);
        SkulQuintitemObject.SetActive(true);
        SkulItemObject.SetActive(true);
    }
    public void ItemOn(ItemSlot itemslot)
    {
        SelectItemSlot = itemslot;
        LastFrame = itemFrame.gameObject;
        itemFrame.gameObject.SetActive(true);
        itemFrame.FrameOn(itemslot.item);
        if (Input.GetKey(KeyCode.F))
        {
            itemFrameMore.gameObject.SetActive(true);
            itemFrameMore.FrameOn(itemslot.item);
        }
        skill1Frame.gameObject.SetActive(false);
        skill2Frame.gameObject.SetActive(false);
        SkulObject.SetActive(false);
        QuintObject.SetActive(false);
        ItemObject.SetActive(true);
        SkulQuintitemObject.SetActive(true);
        SkulItemObject.SetActive(true);
    }
    public void NoneOn()
    {
        LastFrame = null;
        skill1Frame.gameObject.SetActive(false);
        skill2Frame.gameObject.SetActive(false);
        itemFrame.gameObject.SetActive(false);
        SkulObject.SetActive(false);
        QuintObject.SetActive(false);
        ItemObject.SetActive(false);
        SkulQuintitemObject.SetActive(false);
        SkulItemObject.SetActive(false);
    }
    public void SkulFreshSlot()
    {
        int i = 0;
        for (; i < skuls.Count && i < SkulSlots.Length; i++)
        {
            SkulSlots[i].skul = skuls[i];
        }
        for (; i < SkulSlots.Length; i++)
        {
            SkulSlots[i].skul = null;
        }
    }
    public void ItemFreshSlot()
    {
        int i = 0;
        for (; i < items.Count && i < ItemSlots.Length; i++)
        {
            if (items[i] == null)
            {
                items.RemoveAt(i);
                if (i == items.Count)
                {
                    break;
                }
            }
            ItemSlots[i].item = items[i];
        }
        for (; i < ItemSlots.Length; i++)
        {
            ItemSlots[i].item = null;
        }
    }
    private void InscripFreshSlot()
    {
        int i = 0;
        for (; i < inscriptions.Count && i < InscriptionSlots.Length; i++)
        {
            if (inscriptions[i] == null)
            {
                inscriptions.RemoveAt(i);
                if (inscriptionObjs[i] != null)
                {
                    Destroy(inscriptionObjs[i]);
                }
                inscriptionObjs.RemoveAt(i);
                if (i == inscriptions.Count)
                {
                    break;
                }
                i--;
            }
            if (inscriptionObjs[i] != null)
            {
                InscriptionSlots[i].inscriptionSkill = inscriptionObjs[i].GetComponent<InscriptionSkill>();
                InscriptionSlots[i].inscriptionSkill.GetInscrip();
            }
            InscriptionSlots[i].inscription = inscriptions[i];
            for (int j = i + 1; j < inscriptions.Count; j++)
            {
                if (inscriptions[i] == inscriptions[j])
                {
                    inscriptions[j] = null;
                    InscriptionSlots[i].inscriptionSkill.InscripNum++;
                }
            }
        }
        for (; i < InscriptionSlots.Length; i++)
        {
            InscriptionSlots[i].inscription = null;
            InscriptionSlots[i].inscriptionSkill = null;
        }
        //InscriptionSlots.Sort(new Comparison<InscriptionSlot>((n1, n2) => 
        //    n1.inscriptionSkill.InscripNum.CompareTo(n2.inscriptionSkill.InscripNum)));

        /*List<InscriptionSlot> sortlist = new List<InscriptionSlot>();
        for (int a = 0; a < InscriptionSlots.Length; ++a)
        {
            if (InscriptionSlots[a].inscriptionSkill != null)
            {
                sortlist.Add(InscriptionSlots[a]);
            }
        }
        sortlist.Sort(delegate(InscriptionSlot n1, InscriptionSlot n2) 
        {
             return n1.inscriptionSkill.InscripNum.CompareTo(n2.inscriptionSkill.InscripNum);
        });
        for (int a = 0; a < sortlist.Count; ++a)
        {
            InscriptionSlots[a].inscription = sortlist[a].inscription;
            InscriptionSlots[a].inscriptionSkill = sortlist[a].inscriptionSkill;
        }*/
        for (int j = 1; j < inscriptionObjs.Count; j++)
        {
            InscriptionSkill InscripSkillTarget = InscriptionSlots[j].inscriptionSkill;
            InscriptionSlot InscripSlotsTarget = InscriptionSlots[j];
            Inscription InscripSlotTarget = InscriptionSlots[j].inscription;
            Inscription InscripTarget = inscriptions[j];
            GameObject InscripObjTraget = inscriptionObjs[j];
            int cur = j - 1;
            while (cur >= 0 && InscripSkillTarget.InscripNum >= InscriptionSlots[cur].inscriptionSkill.InscripNum)
            {
                if (InscripSkillTarget.InscripNum > InscriptionSlots[cur].inscriptionSkill.InscripNum)
                {
                    cur = InscripSort(cur);
                }
                else
                {
                    if (InscripSlotsTarget._inscripImage.sprite == InscripTarget.inscripFullActivedImage)
                    {
                        cur = InscripSort(cur);
                    }
                    else if (InscripSlotsTarget._inscripImage.sprite == InscripTarget.inscripImage)
                    {
                        if (InscriptionSlots[cur]._inscripImage.sprite == inscriptions[cur].inscripDeactivedImage)
                        {
                            cur = InscripSort(cur);
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            InscriptionSlots[cur + 1].inscriptionSkill = InscripSkillTarget;
            InscriptionSlots[cur + 1].inscription = InscripSlotTarget;
            inscriptions[cur + 1] = InscripTarget;
            inscriptionObjs[cur + 1] = InscripObjTraget;
        }
    }
    int InscripSort(int cur)
    {
        InscriptionSlots[cur + 1].inscriptionSkill = InscriptionSlots[cur].inscriptionSkill;
        InscriptionSlots[cur + 1].inscription = InscriptionSlots[cur].inscription;
        inscriptions[cur + 1] = inscriptions[cur];
        inscriptionObjs[cur + 1] = inscriptionObjs[cur];
        return cur - 1;
    }
    public void AddSkul(Skul _skul, ObjSkul objSkul = null)
    {
        if (skuls.Count < SkulSlots.Length)
        {
            skuls.Add(_skul);
            GameObject playerObj = Instantiate(_skul.playerObj, player.transform);
            player.playerObjs.Add(playerObj);
            player.players.Add(playerObj.GetComponent<Player>());
            if (objSkul != null) {
                player.players[player.players.Count - 1].skills = objSkul.skills;
            }
            SkulFreshSlot();
        }
        else
        {
            GameObject skulObj = Instantiate(skuls[0].skulObj, player.playerObjs[0].transform.position,
                player.playerObjs[0].transform.rotation);
            skulObj.GetComponent<ObjSkul>().player = player.players[0];
            skulObj.GetComponent<ObjDoMove>().ObjMove(skulObj);
            Destroy(player.playerObjs[0]);
            skuls[0] = _skul; 
            GameObject playerObj = Instantiate(_skul.playerObj, player.transform);
            player.playerObjs[0] = playerObj;
            player.players[0] = playerObj.GetComponent<Player>();
            player.players[0].skills = objSkul.skills;
            SkulFreshSlot();
        }
    }
    public void AddItem(Item _item, int index = 0)
    {
        if (items.Count < ItemSlots.Length)
        {
            items.Add(_item);
            player.playerStat.AddItemStats(player.playerStat, _item.itemCode);
            if (_item.ItemSkill != null)
            {
                itemObjs.Add(Instantiate(_item.ItemSkill.gameObject));
                ItemSkill itemSkill = itemObjs[itemObjs.Count - 1].GetComponent<ItemSkill>();
                itemSkill.GetItem();
            }
            else
            {
                itemObjs.Add(null);
            }
            ItemFreshSlot();
            AddInscrip(_item);
        }
        else
        {
            player.playerStat.MinItemStats(player.playerStat, items[index].itemCode);
            if (itemObjs[index] != null)
            {
                itemObjs[index].GetComponent<ItemSkill>().DelItem();
                Destroy(itemObjs[index]);
            }
            DelInscrip(_item);
            items[index] = _item;
            player.playerStat.AddItemStats(player.playerStat, _item.itemCode);
            if (_item.ItemSkill != null)
            {
                itemObjs[index] = Instantiate(_item.ItemSkill.gameObject);
                ItemSkill itemSkill = itemObjs[itemObjs.Count - 1].GetComponent<ItemSkill>();
                itemSkill.GetItem();
            }
            else
            {
                itemObjs[index] = null;
            }
            ItemFreshSlot();
            AddInscrip(_item);
        }
    }
    private void AddInscrip(Item _item)
    {
        for (int i = 0; i < _item.itemInscriptions.Length; i++)
        {
            inscriptions.Add(_item.itemInscriptions[i]);
            if (_item.itemInscriptions[i].inscriptionSkill != null)
            {
                inscriptionObjs.Add(Instantiate(_item.itemInscriptions[i].inscriptionSkill.gameObject));
            }
            else
            {
                inscriptionObjs.Add(null);
            }
        }
        InscripFreshSlot();
    }
    void DelItem()
    {
        if (IsDelItem)
        {
            timer += Time.unscaledDeltaTime;
            ItemDestroyFillImage.fillAmount = timer;
            if (timer >= 1f)
            {
                timer = 0;
                for (int i = 0; i < items.Count; i++)
                {
                    if (SelectItemSlot == ItemSlots[i])
                    {
                        player.playerStat.MinItemStats(player.playerStat, items[i].itemCode);
                        if (itemObjs[i] != null)
                        {
                            ItemSkill itemSkill = itemObjs[i].GetComponent<ItemSkill>();
                            itemSkill.DelItem();
                            Destroy(itemObjs[i]);
                        }
                        itemObjs.RemoveAt(i);
                        DelInscrip(items[i]);
                        items[i] = null;
                        ItemFreshSlot();
                        ItemSlots[i].Select();
                        break;
                    }
                }
                IsDelItem = false;
                ItemDestroyFillImage.fillAmount = 1;
            }
        }
    }
    private void DelInscrip(Item _item)
    {
        for (int i = 0; i < _item.itemInscriptions.Length; i++)
        {
            for (int j = 0; j < inscriptions.Count; j++)
            {
                if (_item.itemInscriptions[i] == InscriptionSlots[j].inscription)
                {
                    if (--InscriptionSlots[j].inscriptionSkill.InscripNum == 0)
                    {
                        InscriptionSlots[j].inscriptionSkill.DelInscrip();
                        inscriptions[j] = null;
                        InscriptionSlots[j].inscription = null;
                        InscriptionSlots[j].inscriptionSkill = null;
                    }
                    else
                    {
                        InscriptionSlots[j].inscription = InscriptionSlots[j].inscription;
                    }
                    break;
                }
            }
            InscripFreshSlot();
        }
    }
    public void SwitchSkul()
    {
        Skul SwitchSkul = skuls[0];
        skuls[0] = skuls[1];
        skuls[1] = SwitchSkul;
        SkulFreshSlot();
    }
}
