﻿using System.Collections;
using System.Collections.ObjectModel;

namespace BookStore
{
    public class BookComposite : IComponent
    {
        protected readonly List<IComponent> children = new();
        public BookComposite(string name)
        {
            Name = name;
        }
        public string Name { get; }
        public virtual string Type => GetType().Name;
        public virtual int Count => children.Sum(child => child.Count);
        public virtual IEnumerable Children => new ReadOnlyCollection<IComponent>(children);
        public virtual void Add(IComponent bookComponent)
        {
            children.Add(bookComponent);
        }

        public virtual void Remove(IComponent bookComponent)
        {
            children.Remove(bookComponent);
        }
    }
}
