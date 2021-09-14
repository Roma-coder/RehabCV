﻿using System;

namespace RehabCV.Models
{
    public class Child 
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Diagnosis { get; set; }
        public string Priority { get; set; }
        public string HomeAddress { get; set; }
    }
}
