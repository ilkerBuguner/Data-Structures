namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> legion;

        public Legion()
        {
            this.legion = new OrderedSet<IEnemy>();
        }

        public int Size => this.legion.Count;

        public bool Contains(IEnemy enemy)
        {
            return this.legion.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
            this.legion.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            IEnemy targetEnemy = null;
            foreach (var enemy in this.legion)
            {
                if (enemy.AttackSpeed == speed)
                {
                    targetEnemy = enemy;
                }
            }

            return targetEnemy;
        }

        //Can be refactored
        public List<IEnemy> GetFaster(int speed)
        {
            return this.legion.Where(e => e.AttackSpeed > speed).ToList();
        }

        public IEnemy GetFastest()
        {
            if (this.Size <= 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            return this.legion.GetFirst();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            if (this.Size <= 0)
            {
                return new IEnemy[0];
            }

            return this.legion.OrderByDescending(x => x.Health).ToArray();
        }

        //Can be refactored
        public List<IEnemy> GetSlower(int speed)
        {
            return this.legion.Where(e => e.AttackSpeed < speed).ToList();
        }

        public IEnemy GetSlowest()
        {
            if (this.Size <= 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            return this.legion.GetLast();
        }

        public void ShootFastest()
        {
            if (this.Size <= 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            this.legion.RemoveFirst();
        }

        public void ShootSlowest()
        {
            if (this.Size <= 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            this.legion.RemoveLast();
        }
    }
}
