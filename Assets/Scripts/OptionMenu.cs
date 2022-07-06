using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Alpha
{
    public class OptionMenu : MonoBehaviour
    {
        #region Variables
        [SerializeField] PlayerInput dragAndShoot;
        [SerializeField] PhysicsMaterial2D playerphymat;
        [SerializeField] Slider forceSlider;
        [SerializeField] Slider bouncySlider;
        [SerializeField] TextMeshProUGUI forcetext;
        [SerializeField] TextMeshProUGUI bouncytext;
        #endregion
        bool optionactive;
        #region UnityMethods
        void Start()
        {
            forceSlider.value = dragAndShoot.maxForce ;
            bouncySlider.value = playerphymat.bounciness;
        }

        void Update()
        {
            if(optionactive)
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
            optionactive = true;
            Time.timeScale = 0;
        }
        public void ResetOptionactive()
        {
            optionactive = false;
            Time.timeScale = 1;
        }
        #endregion

        #region PrivateMethods

        #endregion

        #region GameEventListeners

        #endregion
    }
}