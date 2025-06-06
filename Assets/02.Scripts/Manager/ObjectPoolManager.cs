using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Pool;

using Sirenix.OdinInspector;

namespace _02.Scripts.Manager
{
    [Serializable]
    public class ObjectInfo
    {
        public string _objectName = string.Empty;
        public GameObject _prefab = null;
        public int _initialAmount = 0;
    }

    public class ObjectPoolManager : Singleton<ObjectPoolManager>
    {
        [Title("ObjectPool")]
        [SerializeField] private ObjectInfo[] _objectInfos = null;
        
        private Dictionary<string, IObjectPool<GameObject>> _objectPoolDict = new Dictionary<string, IObjectPool<GameObject>>();
        private Dictionary<string, GameObject> _objDict = new Dictionary<string, GameObject>();

        private string _objName = string.Empty;

        protected override void Awake()
        {
            base.Awake();
            
            Init();
        }

        private void Init()
        {
            if (_objectInfos == null)
                return;

            foreach (var item in _objectInfos)
            {
                IObjectPool<GameObject> pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool, ReturnToPool);
                
                _objDict.Add(item._objectName, item._prefab);
                _objectPoolDict.Add(item._objectName, pool);

                for (int index = 0; index < item._initialAmount; ++index)
                {
                    _objName = item._objectName;
                    
                    if (CreatePooledItem() == null)
                        break;
                    
                    Poolable poolable = CreatePooledItem().GetComponent<Poolable>();
                    poolable.Pool.Release(poolable.gameObject);
                }
            }
        }

        private GameObject CreatePooledItem()
        {
            if (_objName == string.Empty)
                return null;

            GameObject obj = Instantiate(_objDict[_objName]);
            obj.GetComponent<Poolable>().Pool = _objectPoolDict[_objName];

            return obj;
        }

        private void OnTakeFromPool(GameObject obj)
        {
            obj.SetActive(true);
        }

        private void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
        }

        public GameObject Get(string objName)
        {
            _objName = objName;

            if (_objDict.ContainsKey(objName) == false)
                return null;

            return _objectPoolDict[objName].Get();
        }
    }
}
