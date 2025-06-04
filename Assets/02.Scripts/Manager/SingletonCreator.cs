using UnityEngine;

using System.Collections.Generic;

using TMPro;

namespace _02.Scripts.Manager
{
    public class SingletonCreator : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _dropdown = null;
        [SerializeField] private List<Slot.Slot> _slotList = new List<Slot.Slot>();

        private void Awake()
        {
            InitSingletonObject<GameManager>();

            if (GameManager.Instance != null)
            {
                GameManager.Instance.SetupDropdown(_dropdown);
                GameManager.Instance.SetupSlotList(_slotList);
            }
        }

        private void InitSingletonObject<T>() where T : Singleton<T>
        {
            GameObject obj = new GameObject(typeof(T).Name);

            if (obj != null)
                obj.AddComponent(typeof(T));
        }
    }
}