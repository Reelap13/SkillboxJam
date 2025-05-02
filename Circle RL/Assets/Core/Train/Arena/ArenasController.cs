using System.Collections;
using System.Collections.Generic;
using Train.AIConnection.Data;
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

        public void Initialize()
        {
            _arenas = new List<ArenaController>();
            for (int i = 0; i < _arena_area_size; ++i)
            {
                for (int j = 0; j < _arena_area_size; ++j)
                {
                    ArenaController arena = Instantiate(_arena_prefab);
                    arena.transform.parent = transform;
                    arena.Initialize(i * _arena_area_size + j, new Vector2(i, j) * _arenas_offset);
                    _arenas.Add(arena);
                }
            }
        }

        public void RecreateArenas()
        {
            foreach (var arena in _arenas)
                arena.RecreateArena();
        }
        
        public List<ArenaController> Arenas { get { return _arenas; } }
    }
}