﻿namespace StudentApi.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public List<Subject> subjects { get; set; } = new List<Subject>();
    }
}
