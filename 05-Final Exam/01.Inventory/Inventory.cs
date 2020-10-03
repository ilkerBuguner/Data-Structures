namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons;

        public Inventory()
        {
            this.weapons = new List<IWeapon>();
        }

        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public void Clear()
        {
            this.weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.weapons.Contains(weapon);
        }

        public void EmptyArsenal(Category category)
        {
            foreach (var weapon in this.weapons)
            {
                if (weapon.Category == category)
                {
                    weapon.Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            var targetWeapon = this.weapons.Find(x => x == weapon);

            if (targetWeapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (targetWeapon.Ammunition < ammunition)
            {
                return false;
            }

            targetWeapon.Ammunition -= ammunition;
            return true;
        }

        public IWeapon GetById(int id)
        {
            var weapon = this.weapons.Find(x => x.Id == id);

            if (weapon == null)
            {
                return null;
            }

            return weapon;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < this.weapons.Count; i++)
            {
                yield return this.weapons[i];
            }
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            var targetWeapon = this.weapons.Find(x => x == weapon);

            if (targetWeapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (targetWeapon.Ammunition + ammunition >= targetWeapon.MaxCapacity)
            {
                targetWeapon.Ammunition = targetWeapon.MaxCapacity;
            }
            else
            {
                targetWeapon.Ammunition += ammunition;
            }

            return targetWeapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            var targetWeapon = this.weapons.Find(x => x.Id == id);

            if (targetWeapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            this.weapons.Remove(targetWeapon);
            return targetWeapon;
        }

        public int RemoveHeavy()
        {
            var counter = 0;

            for (int i = 0; i < this.weapons.Count; i++)
            {
                var currentWeapon = this.weapons[i];
                if (currentWeapon.Category == Category.Heavy)
                {
                    this.weapons.Remove(currentWeapon);
                    i--;
                    counter++;
                }
            }

            return counter;
        }

        public List<IWeapon> RetrieveAll()
        {
            if (this.weapons.Count <= 0)
            {
                return new List<IWeapon>();
            }

            return new List<IWeapon>(this.weapons);
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            return this.weapons.Where(x => x.Category >= lower && x.Category <= upper).ToList();
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            var firstWeaponIndex = this.weapons.IndexOf(firstWeapon);
            var secondWeaponIndex = this.weapons.IndexOf(secondWeapon);

            if (firstWeaponIndex == -1 || secondWeaponIndex == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (firstWeapon.Category == secondWeapon.Category)
            {
                var temp = this.weapons[firstWeaponIndex];
                this.weapons[firstWeaponIndex] = this.weapons[secondWeaponIndex];
                this.weapons[secondWeaponIndex] = temp;
            }
        }
    }
}
