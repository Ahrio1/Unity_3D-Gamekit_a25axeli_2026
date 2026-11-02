using UnityEngine;

namespace Gamekit3D
{
    public class KeyUI : MonoBehaviour
    {
        public static KeyUI Instance { get; protected set; }

        public GameObject keyIconPrefab;
        public string[] keyNames;

        protected Animator[] m_KeyIconAnimators;

        protected readonly int m_HashActivePara = Animator.StringToHash("Active");
        protected const float k_KeyIconAnchorWidth = 0.041f;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            SetInitialKeyCount();
        }

        public void SetInitialKeyCount()
        {
            if (m_KeyIconAnimators != null && m_KeyIconAnimators.Length == keyNames.Length)
                return;

            m_KeyIconAnimators = new Animator[keyNames.Length];

            for (int i = 0; i < m_KeyIconAnimators.Length; i++)
            {
                GameObject healthIcon = Instantiate(keyIconPrefab);
                healthIcon.transform.SetParent(transform);
                RectTransform healthIconRect = healthIcon.transform as RectTransform;
                healthIconRect.anchoredPosition = Vector2.zero;
                healthIconRect.sizeDelta = Vector2.zero;
                healthIconRect.anchorMin += new Vector2(k_KeyIconAnchorWidth, 0f) * i;
                healthIconRect.anchorMax += new Vector2(k_KeyIconAnchorWidth, 0f) * i;
                m_KeyIconAnimators[i] = healthIcon.GetComponent<Animator>();
            }

            InventoryController inventory = FindFirstObjectByType<InventoryController>();
            if (inventory != null)
                ChangeKeyUI(inventory);
        }

        public void ChangeKeyUI(InventoryController controller)
        {
            if (controller == null)
            {
                Debug.LogError("KeyUI: InventoryController reference is missing.");
                return;
            }

            // Ensure the key icons have been created before updating them.
            if (m_KeyIconAnimators == null ||
                m_KeyIconAnimators.Length != keyNames.Length)
            {
                SetInitialKeyCount();
            }

            if (m_KeyIconAnimators == null ||
                m_KeyIconAnimators.Length != keyNames.Length)
            {
                Debug.LogError(
                    "KeyUI: The number of key icon animators doesn't match keyNames. " +
                    "Check the Key Icon Prefab and Key Names in the Inspector.");
                return;
            }

            for (int i = 0; i < keyNames.Length; i++)
            {
                if (m_KeyIconAnimators[i] == null)
                {
                    Debug.LogError($"KeyUI: Icon animator {i} is missing.");
                    continue;
                }
                m_KeyIconAnimators[i].SetBool(
                    m_HashActivePara,
                    controller.HasItem(keyNames[i]));
            }
        }
    }
}