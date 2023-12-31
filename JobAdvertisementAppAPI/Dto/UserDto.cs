﻿using JobAdvertisementAppAPI.Models;

namespace JobAdvertisementAppAPI.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Adress { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Occupation { get; set; }
        public string? CareerSummary { get; set; }
        public string? Skills { get; set; }
        public bool IsAdmin { get; set; }
    }
}
