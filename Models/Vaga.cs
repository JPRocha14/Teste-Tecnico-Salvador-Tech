// Arquivo: Models/Vaga.cs

using System;

namespace VagasAPI.Models
{
    public class Vaga
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Status { get; set; }

        public DateTime Created_at { get; set; } 
        public DateTime Updated_at { get; set; }
    }
}
