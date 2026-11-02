using UnityEngine;

namespace Gamekit3D
{
    [RequireComponent(typeof(Damageable))]
    public class PlayerHealthDataPersister : MonoBehaviour, IDataPersister
    {
        [SerializeField]
        private DataSettings dataSettings = new DataSettings();

        private Damageable damageable;

        private void Awake()
        {
            damageable = GetComponent<Damageable>();
        }

        private void OnEnable()
        {
            PersistentDataManager.RegisterPersister(this);
        }

        private void OnDisable()
        {
            PersistentDataManager.UnregisterPersister(this);
        }

        private void Start()
        {
            // SceneController handles loading persistent data.
        }

        public DataSettings GetDataSettings()
        {
            return dataSettings;
        }

        public void SetDataSettings(string dataTag, DataSettings.PersistenceType persistenceType)
        {
            dataSettings.dataTag = dataTag;
            dataSettings.persistenceType = persistenceType;
        }

        public Data SaveData()
        {
            return new Data<int>(damageable.currentHitPoints);
        }

        public void LoadData(Data data)
        {
            if (damageable == null)
                damageable = GetComponent<Damageable>();

            if (data is Data<int> healthData)
            {
                damageable.SetCurrentHitPoints(healthData.value);
            }
        }
    }
}