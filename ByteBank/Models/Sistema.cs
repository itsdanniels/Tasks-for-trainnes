using System.Text;
using XAct;
using XSystem.Security.Cryptography;

namespace ByteBank.Models
{
    public static class Sistema
    {
        public static void Entrar()
        {
            Console.WriteLine();
            Console.WriteLine("Para começar, digite sua conta para \nter acesso a todos os serviços");
            Console.Write("Conta: ");

            var userAccount = Console.ReadLine();

            Console.WriteLine("Agora digite sua senha");
            Console.Write("Senha: ");

            var userPass = Console.ReadLine();

            var contas = BancoDeDados.Contas;

            var conta = contas.Find(c => c.Conta.Equals(userAccount));

            if (Logar(conta, userPass))
            {
                var logado = true;

                Console.Clear();

                while (logado)
                {
                    OpcoesMenu(conta);

                    var opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1": // Consultar saldo
                            Util.TemplateMenu("Consulta de saldo");

                            Console.WriteLine("Titular: " + conta.Cliente.Nome);
                            Console.WriteLine($"Saldo: R${conta.GetSaldo().ToString("N2")}");
                            Console.WriteLine();
                            Console.WriteLine("Aperte qualquer tecla para voltar ao inicio.");
                            Console.ReadKey();
                            break;
                        case "2": // Depositar saldo
                            Util.TemplateMenu("Depositar saldo");

                            Console.Write("Digite o valor que deseja depositar: ");
                            try
                            {
                                var valor = double.Parse(Console.ReadLine().Replace('.', ','));
                                BancoDeDados.Contas[BancoDeDados.Contas.FindIndex(c => c.Conta.Equals(conta.Conta))].Depositar(valor);
                            }
                            catch
                            {
                                Util.ErrorValorDigitado();
                            }
                            break;
                        case "3": // Sacar saldo
                            Util.TemplateMenu("Sacar saldo");

                            Console.Write("Digite o valor que deseja sacar: ");
                            try
                            {
                                var valor = double.Parse(Console.ReadLine().Replace('.', ','));
                                BancoDeDados.Contas[BancoDeDados.Contas.FindIndex(c => c.Conta.Equals(conta.Conta))].Sacar(valor);
                            }
                            catch
                            {
                                Util.ErrorValorDigitado();
                            }
                            break;
                        case "4": // Transferencia
                            Util.TemplateMenu("Transferência");

                            Console.Write("Digite o numero da conta que deseja transferir o dinheiro: ");
                            var numConta = Console.ReadLine();
                            var contaDestino = BancoDeDados.Contas.Find(c => c.Conta.Equals(numConta));

                            if(contaDestino is null)
                            {
                                Util.TemplateError("Conta digitada não cadastrada!");
                                break;
                            }

                            Console.Write("Digite o valor que deseja transferir: ");
                            try
                            {
                                var valor = double.Parse(Console.ReadLine().Replace('.', ','));

                                BancoDeDados.Contas[BancoDeDados.Contas.FindIndex(c => c.Conta.Equals(conta.Conta))]
                                    .Transferir(BancoDeDados.Contas.Find(c => c.Conta.Equals(numConta)), valor);
                            }
                            catch
                            {
                                Util.ErrorValorDigitado();
                            }
                            break;
                        case "5": // Solicitar emprestimo
                            Util.TemplateMenu("Solicitação de emprestimo");

                            Console.WriteLine("Selecione uma opção:");
                            Console.WriteLine("1. Consultar Score");
                            Console.WriteLine("2. Pedir emprestimo");

                            var opcaoEmprestimo = Console.ReadLine();

                            if (opcaoEmprestimo.Equals("1"))
                            {
                                Util.TemplateSucess($"Seu Score é de {conta.Cliente.Score}" +
                                    $"\nHá um limite pré aprovado de R${conta.LimiteEmprestimo().ToString("N2")}");
                            }
                            else if (opcaoEmprestimo.Equals("2"))
                            {
                                if(conta.LimiteEmprestimo() > 0)
                                {                                    
                                    try
                                    {
                                        Console.Write("Digite o valor: ");
                                        var valor = double.Parse(Console.ReadLine().Replace('.', ','));

                                    }
                                    catch
                                    {
                                        Util.ErrorValorDigitado();
                                    }
                                    break;
                                }
                                else
                                {
                                    Util.TemplateError("Você não tem limite de emprestimo!!!");
                                }
                            }
                            else
                            {
                                Util.TemplateError("Opção invalida!!!");
                            }

                            break;
                        case "6": // Pagar emprestimo
                            Util.TemplateRelease("Função ainda não disponivel!");
                            break;
                        case "7": // Alterar senha
                            Util.TemplateRelease("Função ainda não disponivel!");
                            break;
                        case "8":
                            if (!conta.Admin)
                            {
                                Util.TemplateError("Opção invalida!!!");
                                continue;
                            }
                            Util.TemplateRelease("Função ainda não disponivel!");
                            break;
                        case "9":
                            if (!conta.Admin)
                            {
                                Util.TemplateError("Opção invalida!!!");
                                continue;
                            }
                            Util.TemplateRelease("Função ainda não disponivel!");
                            break;
                        case "0":
                            logado = false;
                            break;
                        default:
                            Util.TemplateError("Opção invalida!!!");
                            break;
                    }
                }

                Console.Clear();
                Console.WriteLine((string)("Você Escolheu sair, até mais " + conta.Cliente.Nome));
                
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Falha no login!");
            }
        }

        private static bool Logar(ContaCorrente conta, string senha)
        {
            if(conta is null)
                return false;

            if (GetHash(senha).Equals(conta.SenhaCifrada))
                return true;

            return false;
        }

        private static string GetHash(string texto)
        {
            var textoBytes = Encoding.ASCII.GetBytes(texto);

            var sha = new SHA512Managed();
            var hash = sha.ComputeHash(textoBytes);

            var hashMontado = new StringBuilder();

            foreach (var b in hash)
            {
                hashMontado.Append(b.ToString("x2"));
            }

            return hashMontado.ToString();
        }

        private static void OpcoesMenu(ContaCorrente conta)
        {
            Console.WriteLine();
            Console.WriteLine(("Olá " + conta.Cliente.Nome + "!"));


            Console.WriteLine();

            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1. Consultar saldo");
            Console.WriteLine("2. Depositar");
            Console.WriteLine("3. Sacar");
            Console.WriteLine("4. Transferir");
            Console.WriteLine("5. Solicitar emprestivo");
            Console.WriteLine("6. Pagar emprestimo");
            Console.WriteLine("7. Alterar senha");
            Console.WriteLine("0. Sair da conta");
            if (conta.Admin)
            {
                Console.WriteLine();
                Console.WriteLine("------------");
                Console.WriteLine("Painel Admin");
                Console.WriteLine("------------");
                Console.WriteLine();

                Console.WriteLine("8. Criar conta corrrente para clientes");
                Console.WriteLine("9. Excluir conta corrente de clientes");
            }


            Console.WriteLine();
        }
    }
}
