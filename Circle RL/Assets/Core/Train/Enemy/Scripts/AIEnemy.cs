using System;
using Train.AIConnection.Data;
using Train.Arena;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemy
{
    public class AIEnemy : MonoBehaviour
    {
        static public UnityEvent<AIEnemy> OnSpawned = new();
        static public UnityEvent<AIEnemy> OnDied = new();

        [NonSerialized] public UnityEvent<AIEnemy> OnDie = new();

        [field: SerializeField]
        public AIEnemyMovement EnemyMovement { get; private set; }
        [field: SerializeField]
        public AIEnemyBehaviour EnemyBehavior { get; private set; }
        [field: SerializeField]
        public EnemyAbility EnemyAbility { get; private set; }
        [field: SerializeField]
        public EnemyAIDataTaker DataTaker { get; private set; }

        [field: SerializeField]
        public EnemyPreset EnemyPreset { get; private set; }

        public EnemyParameters EnemyParameters { get; private set; }
        public ArenaController ArenaController { get; private set; }

        private bool _is_die = false;

        private void Start()
        {
            OnSpawned.Invoke(this);

            EnemyParameters = new EnemyParameters(EnemyPreset);
            EnemyParameters.OnDieing.AddListener(Die);
            EnemyPreset.ChangeTeam(TagEnum.ENEMY);
        }

        public void Initialize(ArenaController _controller)
        {
            ArenaController = _controller;
        }

        private void Die()
        {
            if (_is_die)
                return;

            _is_die = true;
            OnDie.Invoke(this);
            OnDied.Invoke(this);
            Destroy(gameObject);
        }

        public bool IsAlive
        {
            get
            {
                return EnemyParameters.IsAlive();
            }
        }

        public EnemyData GetData()
        {
            return DataTaker.GetData();
        }
    }
}