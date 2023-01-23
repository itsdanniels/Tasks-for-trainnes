using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Models
{
    internal class ContaCorrente
    {
        public ContaCorrente(Cliente cliente, string conta, string agencia, string senhaCifrada, bool admin = false)
        {
            Cliente = cliente;
            Conta = conta;
            Agencia = agencia;
            SenhaCifrada = senhaCifrada;
            Admin = admin;
        }

        public Cliente Cliente { get; }

        public string Conta { get; }

        public string Agencia { get; }

        private double _saldo { get; set; }

        public string SenhaCifrada { get; }

        public double ValorEmprestado { get; private set; }

        public Emprestimo Emprestimo { get; set; }

        public bool Admin { get; private set; }

        public double GetSaldo()
        {
            return _saldo;
        }

        public bool Depositar(double valor)
        {
            if (valor <= 0)
            {
                Util.TemplateError("Valor de deposito não pode ser zero ou negativo!!!");
                return false;
            }

            _saldo += valor;
            Util.TemplateSucess($"Foi depositado R${valor.ToString("N2")} na sua conta!");
            return true;
        }

        public bool Sacar(double valor)
        {
            if(_saldo < valor)
            {
                Util.TemplateError("Saldo insuficiente!!!");
                return false;
            }
            if (valor <= 0)
            {
                Util.TemplateError("Valor de saque não pode ser zero ou negativo!!!");
                return false;
            }
            _saldo -= valor;
            Util.TemplateSucess($"Foi sacado R${valor.ToString("N2")} da sua conta!");
            return true;
        }

        public bool Transferir(ContaCorrente contaDestino, double valor)
        {
            if(_saldo < valor)
            {
                Util.TemplateError("Saldo insuficiente!!!");
                return false;
            }
            if (valor <= 0)
            {
                Util.TemplateError("Valor de saque não pode ser zero ou negativo!!!");
                return false;
            }

            _saldo -= valor;
            contaDestino.DepositoViaTransferencia(valor);
            Util.TemplateSucess($"Foi Transferido R${valor.ToString("N2")} para {contaDestino.Cliente.Nome}");
            return true;
        }

        private void DepositoViaTransferencia(double valor)
        {
            _saldo += valor;
        }

        public void PegarEmprestimo(double valor)
        {
            if(Cliente.Nome == "Daniel" && GetSaldo() * 10 >= valor)
            {
                Depositar(valor);
                ValorEmprestado = valor;
            }
        }

        public void PagarEmprestimo()
        {
            var valorComJuros = ValorEmprestado + ValorEmprestado * 0.1;
            if (GetSaldo() >= valorComJuros)
            {
                ValorEmprestado = 0;
                Sacar(valorComJuros);
            }
        }

        public void SaldoPagar()
        {
            Console.WriteLine("Valor a pagar é de " + (ValorEmprestado + ValorEmprestado * 0.1));
        }

        public double LimiteEmprestimo()
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
