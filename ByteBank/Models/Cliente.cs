using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Models
{
    internal class Cliente
    {
        public Cliente(string nome, string cpf, int idade)
        {
            Nome = nome;
            Cpf = cpf;
            Idade = idade;
        }

        public string Nome { get; }

        public string Cpf { get; }

        public int Idade { get; set; }

        public static int Score { get; } = new Random().Next(0, 1000);
    }
}
