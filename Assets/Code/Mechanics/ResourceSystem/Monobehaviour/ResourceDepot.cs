using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceDepot : MonoBehaviour
{
    #region Fields and Properties
    //[SerializeField]
    //private MissionCommand missionCommand;
    //public MissionCommand MissionCommand { get => missionCommand; set => missionCommand = value; }

    [SerializeField]
    private FactionAlignment factionAlignment;
    public FactionAlignment FactionAlignment { get => factionAlignment; set => factionAlignment = value; }

    public Transform[] navPointArray;

    public ResourceField[] AllResourceFields;
    public List<ResourceField> AvailableResourceFields;

    [SerializeField]
    private int totalResources;
    public int TotalResources { get => totalResources; set => totalResources = value; }


    #endregion
    #region Events

    #endregion

    #region Debug
    //public Text textLabel;
    //public Text resourceLabel;
    //public Text factionLabel;
    #endregion


    #region Monobehaviour
    // Use this for initialization
    void Start()
    {
        //missionCommand.GetComponentInParent<MissionCommand>();
        AllResourceFields = FindObjectsOfType<ResourceField>();
        AvailableResourceFields = new List<ResourceField>();

        for (int i = 0; i < AllResourceFields.Length; i++)
        {
            //AllResourceFields[i].OnCapture.AddListener(OnFieldCapture);

            if (AllResourceFields[i].FactionAlignment == FactionAlignment)
                AddToAvailable(AllResourceFields[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //resourceLabel.text = resourceAmount.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        SupplyTruckDriver supplyTruck = other.GetComponentInParent<SupplyTruckDriver>();
        if (supplyTruck == null)
            return;
        supplyTruck.UnloadResources();
        totalResources++;
    }
    public void OnFieldCapture(ResourceField resource)
    {
        if (resource.FactionAlignment == FactionAlignment)
            AddToAvailable(resource);
        else
            RemoveFromAvailable(resource);
    }
    public void AddToAvailable(ResourceField field)
    {
        if (!AvailableResourceFields.Contains(field))
            AvailableResourceFields.Add(field);
    }
    public void RemoveFromAvailable(ResourceField field)
    {
        if (AvailableResourceFields.Contains(field))
            AvailableResourceFields.Remove(field);
    }

    public ResourceField RecieveFieldAssignment()
    {
        if (AvailableResourceFields.Count > 0)
        {
            ResourceField lessWorkedField = AvailableResourceFields[0];
            for (int i = 1; i < AvailableResourceFields.Count; i++)
            {
                if (AvailableResourceFields[i].CollectorCount < lessWorkedField.CollectorCount)
                    lessWorkedField = AvailableResourceFields[i];
            }
            return lessWorkedField;
        }
        return null;
    }

    #endregion

}
