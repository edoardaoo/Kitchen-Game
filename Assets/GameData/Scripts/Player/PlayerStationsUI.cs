using UnityEngine;

namespace KitchenGame.Runtime
{
    public class PlayerStationsUI : MonoBehaviour
    {
        // Stations UIs
        [SerializeField] GrillStationUI grillUI;

        private void Start()
        {
            // Set UIs controllers
            grillUI.Controller = GetComponent<PlayerController>();
        }

        public void OpenStationUI(StationBase station)
        {
            switch (station)
            {
                case GrillStation grill:
                    grillUI.OpenForStation(grill);
                    break;
                    // New stations come here
            }
        }
    }
}
