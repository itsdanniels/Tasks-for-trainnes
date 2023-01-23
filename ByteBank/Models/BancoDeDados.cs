using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteBank.Models
{
    internal static class BancoDeDados
    {
        public static List<ContaCorrente> Contas { get; set; } = GetContas();

        private static List<ContaCorrente> GetContas()
        {
            var contas = new List<ContaCorrente>();

            contas.Add(new ContaCorrente(new Cliente("Daniel", "11122233344", 22), "7777",
                "0001",
                "b73cc36f8c98678c1a05a0c045fcc26aacd1e1fcc082b284a24134627aa3c059761992f5f8a6d807e294b0c735af7d554dc4f5fa568a56cccef6ddea4f61fdd2",
                true));

            contas.Add(new ContaCorrente(new Cliente("Gabriel", "11133322255", 18), "1111",
                "0001", "3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2"));

            contas.Add(new ContaCorrente(new Cliente("Gustavo", "22233355599", 18), "3333",
                "0001", "3c9909afec25354d551dae21590bb26e38d53f2173b8d3dc3eee4c047e7ab1c1eb8b85103e3be7ba613b31bb5c9c36214dc9f14a42fd7a2fdb84856bca5c44c2"));

            contas.Add(new ContaCorrente(new Cliente("Wesley", "55566677788", 35), "1010",
                "0001", "47d68033f93e6ee5763cec8722d92cd8024118aaa5f9ccdfe0a8d4eaa8b7f5c76f62ac2c833ac0a70eb4e62980ca1510800525b213c476efa9dedbccbbe82765"));

            return contas;
        }
    }
}
