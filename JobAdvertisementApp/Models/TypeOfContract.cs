﻿namespace JobAdvertisementApp.Models
{
    public class TypeOfContract
    {
        public int Id { get; set; }
        public string? Type { get; set; }

        public override string ToString()
        {
            return Type;
        }
    }
}
