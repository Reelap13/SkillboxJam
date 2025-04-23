using System.Collections.Generic;
using NUnit.Framework;
using Train.Arena;
using UnityEngine;

namespace Train
{
    public class ArenasController : MonoBehaviour
    {
        [SerializeField] private ArenaController _arena_prefab;
        [SerializeField] private int _arena_area_size = 3;
        [SerializeField] private float _arenas_offset = 20f;

        private List<ArenaController> _arenas;

        private void Awake()
        {
            Initialize();   
        }

        private void Initialize()
        {
            for (int i = 0; i < _arena_area_size; ++i)
            {
                for (int j = 0; j < _arena_area_size; ++j)
                {
                    ArenaController arena = Instantiate(_arena_prefab);
                    arena.transform.parent = transform;
                    arena.Initialize(i * _arena_area_size + j, new Vector2(i, j) * _arenas_offset);
                }
            }
        }
        
        public void GetEnemyData()
        {

        }
    }
}