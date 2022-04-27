﻿using MarvelousService.DataLayer.Enums;

namespace MarvelousService.DataLayer.Entities
{
    public class Resource
    {
        public int Id { get; set; }
        public ServiceType Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
