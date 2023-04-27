using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GlobalSlotManager : MonoBehaviour
{
    [SerializeField] List<GameObject> unitInactive = new List<GameObject>();
    [SerializeField] List<Slot> allUnits = new List<Slot>();
    [SerializeField] TextMeshProUGUI slotCount;
    GameObject slotCountobjet;
    int currCount = 0;

    public GameObject unitTower;

    public static GlobalSlotManager instance;
    public static GlobalSlotManager Instance { get { return instance; } }

    PhoneChat phoneChat;
    public InputManager inputManager;

    // Start is called before the first frame update
    private void Start()
    {
        slotCountobjet = slotCount.gameObject;
        phoneChat = GameObject.FindObjectOfType<PhoneChat>();
        UpdateSlotCount();
    }

    private void OnEnable()
    {
        if (instance != null && instance != this)
        {
            //Destroy(this);
            instance = this;

        }
        else
        {
            instance = this;
        }


    }
    public bool CanBeSpawnedFrom()
    {
        if (currCount <= 19)
        {

            return true;
        }
        return false;
    }
    public void SpawnFromList(Vector3 pos)
    {
        SoundManager.instance.PlaySlotOut();
        unitTower.transform.GetChild(19 - currCount).gameObject.SetActive(false);
        inputManager.draggedObject = allUnits[currCount].gameObject;
        allUnits[currCount].transform.position = pos;
        allUnits[currCount].gameObject.SetActive(true);
        allUnits[currCount].GetComponent<Rigidbody>().isKinematic = false;
        UpdateSlotCount();
        inputManager.draggedObject = allUnits[currCount].gameObject;
        currCount++;


    }
    public void AddBackToList(GameObject draggedUnit)
    {
        if (currCount != 0)
        {
            LeanTween.scale(unitTower.transform.GetChild(20 - currCount).gameObject, Vector3.left * 1.1f, .3f).setEasePunch();
            SoundManager.instance.PlaySlotIn(0);

            draggedUnit.gameObject.SetActive(false);
            unitTower.transform.GetChild(20 - currCount).gameObject.SetActive(true);
            currCount--;
            UpdateSlotCount();

        }
    }
    public void AddToInvntory(SlotManager slotmanager, GameObject draggedUnit)
    {
        slotmanager.FillSlot();
        if (slotmanager.full == false)
        {
            draggedUnit.SetActive(false);
            unitInactive.Add(draggedUnit);

            if (unitInactive.Count == 20)
            {
                phoneChat.EnableSolutionCheckButton(true);
            }



        }
        UpdateSlotCount();

    }
    public void GetFromInventory(SlotManager slotmanager, Vector3 pos)
    {
        if (slotmanager.currSlot != 0)
        {
            Rigidbody rb = unitInactive[0].GetComponent<Rigidbody>();
            slotmanager.DeleteSlot();
            unitInactive[0].transform.position = pos;
            unitInactive[0].SetActive(true);
            LeanTween.scale(unitInactive[0], Vector3.one * 1.2f, 0.01f);
            LeanTween.scale(unitInactive[0], Vector3.one * 1.1f, 0.5f).setEase(LeanTweenType.easeOutElastic).setDelay(.05f);
            rb.isKinematic = false;

            inputManager.draggedObject = unitInactive[0];

            unitInactive.RemoveAt(0);
            if (unitInactive.Count == 19)
            {
                phoneChat.EnableSolutionCheckButton(false);

            }

        }
        UpdateSlotCount();

    }
    void UpdateSlotCount()
    {
        LeanTween.scale(slotCount.gameObject, Vector3.one * 1.2f, .5f).setEase(LeanTweenType.punch);

        slotCount.text = 20 - currCount + " / 20";
    }
    public void Restart()
    {
        foreach (GameObject unit in unitInactive)
        {
            unitInactive[0].SetActive(true);

        }
        unitInactive.Clear();
        foreach (Slot unit in allUnits)
        {
            unit.SetPosition();
        }
    }
}
