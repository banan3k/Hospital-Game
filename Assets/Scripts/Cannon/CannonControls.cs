using UnityEngine;
using System.Collections;

//This is all fairly empty right now, but can be used to implement other behaviour (such as a charge meter, sounds, etc.)

public class CannonControls : MonoBehaviour
{
    public GameObject controlsHandler;

    private ControlsHandling controls;
    private CannonPosToAnim canpos;
    private FireCannonball fireC;
    private CannonSound cannonSound;

    private AudioSource audio;

    public bool reset;
    public float firePowerPerTick;
    public float firePowerMultiplier;

    private bool getSelect;
    private bool getSelectRelease;
    private bool hHasChanged;
    private bool vHasChanged;
    private int stage;
    private float firePower;

    // Use this for initialization
    void Start()
    {
        controlsHandler = GameObject.Find("ControlsHandler");
        controls = controlsHandler.GetComponent<ControlsHandling>();
        canpos = GetComponent<CannonPosToAnim>();
        fireC = GetComponent<FireCannonball>();
        cannonSound = GetComponent<CannonSound>();
        audio = GetComponent<AudioSource>();

        stage = 0; //0=init 1=horizontal, 2=vertical, 3=charging, 4=fired;
        reset = false;
        hHasChanged = false;
        vHasChanged = false;
        firePowerPerTick = 1;
        firePowerMultiplier = 50;
    }

    void registerControls()
    {
        getSelect = controls.getSelect;
        getSelectRelease = controls.getSelectRelease;
    }

    void checkHorizontal()
    {
        canpos.canHorizontal = true;
        if (controls.getHorizontal != 0)
        {
            audio.mute = false;
            hHasChanged = true;
            cannonSound.PlaySound(3);
        }
        else
        {
            audio.mute = true;
        }
        if (getSelectRelease && hHasChanged)
        {
            getSelectRelease = false; //to ensure it doesn't carry over to the next stage
            canpos.canHorizontal = false;
            stage = 2; //vertical
        }
    }

    void checkVertical()
    {
        canpos.canVertical = true;
        if (controls.getVertical != 0)
        {
            audio.mute = false;
            vHasChanged = true;
            cannonSound.PlaySound(3);
        }
        else
        {
            audio.mute = true;
        }
        if (getSelectRelease && vHasChanged)
        {
            audio.mute = true;
            getSelectRelease = false; //to ensure it doesn't carry over to the next stage
            canpos.canVertical = false;
            stage = 3; //charging
        }
    }

    void checkCharge()
    {
        canpos.charging = true;
        if (getSelect)
        {
            firePower += Time.deltaTime * firePowerPerTick;
            canpos.firePower = firePower;
        }
        else if (getSelectRelease && firePower > 1)
        {
            stage = 4; //fire
        }
    }

    void pushFire()
    {
        fireC.firePower = firePower * firePowerMultiplier;
        fireC.fireCannonball();
        canpos.firing = true;
        stage = 10; //nothing
    }

    void Update()
    {
        if (!fireC.HasDoctor())
            return;

        registerControls();
        switch (stage)
        {
            case 0:
                if (getSelectRelease)
                {
                    getSelectRelease = false;
                    stage = 1;
                }
                break;
            case 1:
                checkHorizontal();
                break;
            case 2:
                checkVertical();
                break;
            case 3:
                checkCharge();
                break;
            case 4:
                pushFire();
                break;
            default:
                break;
        }
        if (reset)
        {
            firePower = 0;
            canpos.canHorizontal = false;
            canpos.canVertical = false;
            canpos.charging = false;
            fireC.resetCannonball();
            reset = false;
            stage = 0;
        }
    }

    public void ResetCannon()
    {
        reset = true;
    }
}
