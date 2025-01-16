// Arquivo: Models/Vaga.cs

using System;

namespace VagasAPI.Models
{
    public class Vaga
    {
        public int Id { get; set; } // O Id será gerado automaticamente pelo banco de dados

        public string Title { get; set; } // Título da vaga

        public string Status { get; set; } // Status da vaga (Ex: "Aberta", "Fechada")

        public DateTime Created_at { get; set; } // Data de criação (gerenciada automaticamente pelo banco de dados)

        public DateTime Updated_at { get; set; } // Data de atualização (gerenciada automaticamente pelo banco de dados)
    }
}
