using System;

namespace FactoryPassSystem.WebAPI.Entities
{
    public class Position : BaseEntity
    {
        public string Name { get; set; }

        public Position(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException($"Required input {nameof(name)} was null or empty.");

            Name = name;
        }
    }
}
