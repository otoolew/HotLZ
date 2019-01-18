using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidBody;
    public Rigidbody RigidBody
    {
        get { return rigidBody; }
        private set { rigidBody = value; }
    }
    //[SerializeField]
    //private Starship starship;
    //public Starship Starship
    //{
    //    get { return starship; }
    //    private set { starship = value; }
    //}
    [SerializeField]
    private WeaponController weaponController;
    public WeaponController WeaponController
    {
        get { return weaponController; }
        private set { weaponController = value; }
    }

    public bool playerControllerInputBlocked;
    public bool externalInputBlocked;

    #region Primary Weapon Input
    public KeyCode primaryWeaponKey;
    private bool primaryWeaponFire;
    public bool PrimaryWeaponFire
    {
        get { return primaryWeaponFire && !playerControllerInputBlocked && !externalInputBlocked; }
    }
    #endregion

    #region Secondary Weapon Input
    public KeyCode secondaryWeaponKey;
    private bool secondaryWeaponFire;
    public bool SecondaryWeaponFire
    {
        get { return secondaryWeaponFire && !playerControllerInputBlocked && !externalInputBlocked; }
    }
    #endregion

    #region Properties and Variables
    public float ThrustInput;
    public float RotationInput;
    public Vector3 EulerAngleVelocity { get; set; }

    #endregion
    #region Events and Handlers
    private void HandleGameStateChanged(GameManager.GameState currentState, GameManager.GameState previousState)
    {
        switch (currentState)
        {
            case GameManager.GameState.PAUSED:
                FreezeControl();
                break;
            case GameManager.GameState.RUNNING:
                GainControl();
                break;
            default:
                Debug.Log("PlayerInput: Default Case Hit");
                FreezeControl();
                break;
        }
    }
    #endregion
    #region Monobehaviour
    // Use this for initialization
    void Start ()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
        //weaponController.WeaponComponents = GetComponentsInChildren<WeaponComponent>(); <- Prefab set for now
        //EulerAngleVelocity = new Vector3(0, Starship.HullComponent.RotationSpeed, 0);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }
	
	// Update is called once per frame
	void Update ()
    {
        RotationInput = Input.GetAxis("Horizontal");
        ThrustInput = Input.GetAxis("Vertical");

        primaryWeaponFire = Input.GetKeyDown(primaryWeaponKey);
        secondaryWeaponFire = Input.GetKeyDown(secondaryWeaponKey);
        if (PrimaryWeaponFire)
            WeaponController.WeaponComponents[0].FireWeapon();
        if (SecondaryWeaponFire)
            WeaponController.WeaponComponents[1].FireWeapon();

    }
    void FixedUpdate()
    {
        AccelerateStarship();
        RotateStarship();
    }

    #endregion
    public bool HaveControl()
    {
        return !externalInputBlocked;
    }

    public void FreezeControl()
    {
        externalInputBlocked = true;
    }

    public void GainControl()
    {
        externalInputBlocked = false;
    }
    public void AccelerateStarship()
    {
        //if (ThrustInput >= 0)
        //    RigidBody.AddForce(transform.forward * ThrustInput * Starship.EngineComponent.EnginePower);

        //// Limit Speed
        //if (RigidBody.velocity.magnitude > Starship.EngineComponent.EnginePower)
        //{
        //    RigidBody.velocity = Vector3.ClampMagnitude(RigidBody.velocity, Starship.EngineComponent.EnginePower);
        //}
    }

    public void RotateStarship()
    {
        Quaternion rotation = Quaternion.Euler(EulerAngleVelocity * RotationInput * Time.deltaTime);
        RigidBody.MoveRotation(RigidBody.rotation * rotation);
    }
   
}
