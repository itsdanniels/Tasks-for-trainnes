using ByteBank.Models;


Console.WriteLine("#####################");
Console.WriteLine("Bem Vindo ao ByteBank");
Console.WriteLine("#####################");
Console.WriteLine();


while (true)
{
    try
    {
        Sistema.Entrar();
    }
    catch
    {
        Util.TemplateError("Ocorreu um erro inesperado!");

        Sistema.Entrar();
    }
}