using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using TMPro;

public class OptionMenu : MonoBehaviour
{
    #region Variables
    [SerializeField] PlayerInput dragAndShoot;
    [SerializeField] PhysicsMaterial2D playerphymat;
    [SerializeField] Slider forceSlider;
    [SerializeField] Slider bouncySlider;
    [SerializeField] TextMeshProUGUI forcetext;
    [SerializeField] TextMeshProUGUI bouncytext;
    public static UnityEvent optionMenuOpened;
    public static UnityEvent optionMenuClosed;
    #endregion
    bool optionactive;
    #region UnityMethods
    private void Awake()
    {
        if (optionMenuOpened == null)
        {
            optionMenuOpened = new UnityEvent();
        }
        if (optionMenuClosed == null)
        {
            optionMenuClosed = new UnityEvent();
        }
    }
    void Start()
    {
        
        forceSlider.value = dragAndShoot.maxForce;
        bouncySlider.value = playerphymat.bounciness;
    }

    void Update()
    {
        if (optionactive)
        {
            dragAndShoot.maxForce = forceSlider.value;
            forcetext.text = dragAndShoot.maxForce.ToString();

            playerphymat.bounciness = bouncySlider.value;
            bouncytext.text = playerphymat.bounciness.ToString();

        }
    }
    #endregion

    #region PublicMethods
    public void SetOptionactive()
    {
        optionMenuOpened.Invoke();
        optionactive = true;
        Time.timeScale = 0;
    }
    public void ResetOptionactive()
    {
        optionMenuClosed.Invoke();
        optionactive = false;
        Time.timeScale = 1;
    }
    #endregion

    #region PrivateMethods

    #endregion

    #region GameEventListeners

    #endregion
}