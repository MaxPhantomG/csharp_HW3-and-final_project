using System;
using System.Collections.Generic;

namespace MyRepositoryApp.Repositories
{
    public class Repository<T> where T : IEntity
    {
        private readonly Dictionary<int, T> _items = new Dictionary<int, T>();
        public void Add(T item)
        {
            if (_items.ContainsKey(item.Id))
            {
                throw new InvalidOperationException($"Элемент с ID {item.Id} уже существует.");
            }
            _items[item.Id] = item;
        }

        public bool Remove(int id)
        {
            return _items.Remove(id);
        }
        
        public T? GetById(int id)
        {
            _items.TryGetValue(id, out var item);
            return item;
        }

        // Получить все элементы
        public IReadOnlyList<T> GetAll()
        {
            return new List<T>(_items.Values);
        }
        public int Count => _items.Count;

        // Поиск элементов по предикату
        public IReadOnlyList<T> Find(Predicate<T> predicate)
        {
            var result = new List<T>();
            foreach (var item in _items.Values)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }
}
