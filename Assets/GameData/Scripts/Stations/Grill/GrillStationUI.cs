using UnityEngine;
using UnityEngine.UI;

namespace KitchenGame.Runtime
{
    public class GrillStationUI : MonoBehaviour, IStationUI
    {
        [Header("Buttons")]
        [SerializeField] Button cookBtn;
        [SerializeField] Button closeBtn;

        public PlayerController Controller { private get; set; }

        // Internal values
        private GrillStation currentStation;

        private void Start()
        {
            // Subscribe buttons
            closeBtn.onClick.AddListener(CloseUI);
        }

        public void OpenForStation(StationBase station)
        {
            currentStation = station as GrillStation;
            if (currentStation == null)
            {
                Debug.LogError("Station is not a GrillStation.");
                return;
            }

            gameObject.SetActive(true);
            // Populate UI with currentStation.Slots etc.
        }

        public void CloseUI()
        {
            currentStation.ExitStation(Controller);

            currentStation = null;
            gameObject.SetActive(false);
        }
    }
}