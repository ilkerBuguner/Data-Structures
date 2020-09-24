namespace _01.Loader
{
    using _01.Loader.Interfaces;
    using _01.Loader.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Loader : IBuffer
    {
        private List<IEntity> entities;

        public Loader()
        {
            this.entities = new List<IEntity>();
        }

        public int EntitiesCount => this.entities.Count;

        public void Add(IEntity entity)
        {
            this.entities.Add(entity);
        }

        public void Clear()
        {
            this.entities.Clear();
        }

        public bool Contains(IEntity entity)
        {
            return this.entities.Contains(entity);
        }

        public IEntity Extract(int id)
        {
            var element = this.entities.Find(e => e.Id == id);
            if (this.IsEmpty() || element == null)
            {
                return null;
            }

            entities.Remove(element);
            return element;
        }

        public IEntity Find(IEntity entity)
        {
            var element = this.entities.Find(x => x == entity);
            if (this.IsEmpty() || element == null)
            {
                return null;
            }

            return element;
        }

        public List<IEntity> GetAll()
        {
            return this.entities;
        }

        public IEnumerator<IEntity> GetEnumerator()
        {
            for (int i = 0; i < this.EntitiesCount; i++)
            {
                yield return this.entities[i];
            }
        }

        public void RemoveSold()
        {
            this.entities = this.entities.Where(e => e.Status != BaseEntityStatus.Sold).ToList();
        }

        public void Replace(IEntity oldEntity, IEntity newEntity)
        {
            var indexOfElement = this.entities.IndexOf(oldEntity);
            if (this.IsEmpty() || indexOfElement == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }

            this.entities[indexOfElement] = newEntity;
        }

        public List<IEntity> RetainAllFromTo(BaseEntityStatus lowerBound, BaseEntityStatus upperBound)
        {
            var result = new List<IEntity>();

            foreach (var entity in this.entities)
            {
                if (entity.Status >= lowerBound && entity.Status <= upperBound)
                {
                    result.Add(entity);
                }
            }

            return result;
        }

        public void Swap(IEntity first, IEntity second)
        {
            var firstElementIndex = this.entities.IndexOf(first);
            var secondElementIndex = this.entities.IndexOf(second);

            if (this.IsEmpty() || firstElementIndex == -1 || secondElementIndex == -1)
            {
                throw new InvalidOperationException("Entity not found");
            }

            var temp = this.entities[firstElementIndex];
            this.entities[firstElementIndex] = this.entities[secondElementIndex];
            this.entities[secondElementIndex] = temp;
        }

        public IEntity[] ToArray()
        {
            if (this.IsEmpty())
            {
                return new IEntity[0];
            }

            return this.entities.ToArray();
        }

        public void UpdateAll(BaseEntityStatus oldStatus, BaseEntityStatus newStatus)
        {
            this.entities.ForEach(entity =>
            {
                if (entity.Status == oldStatus)
                {
                    entity.Status = newStatus;
                }
            });
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private bool IsEmpty()
        {
            return this.entities.Count <= 0;
        }
    }
}
