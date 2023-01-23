using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Models
{
    internal class Emprestimo
    {
        public bool Ativo { get; set; }

        public double ValorDevedor { get; set; }

        public int QtdParcelas { get; set; }

        public double ValorSolicitado { get; set; }

        public double LimiteEmprestimo { get; set; } = VerificarLimiteEmprestimo();

        public double Juros { get; set; }


        public static double VerificarLimiteEmprestimo()
        {
            if (Cliente.Score >= 950)
                return 100000;

            if (Cliente.Score >= 850)
                return 50000;

            if (Cliente.Score >= 750)
                return 10000;

            if (Cliente.Score >= 700)
                return 2000;

            return 0;
        }
    }
}
