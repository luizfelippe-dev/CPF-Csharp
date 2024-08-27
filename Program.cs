using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Insira o CPF ou CNPJ:");
        string input = Console.ReadLine();

        // Remove qualquer caractere que não seja número
        string digitsOnly = Regex.Replace(input, @"\D", "");

        // Verifica se é CPF ou CNPJ com base na quantidade de dígitos
        if (digitsOnly.Length == 11)
        {
            // Validação e formatação do CPF
            if (IsValidCPF(digitsOnly))
            {
                string formattedCpf = FormatCPF(digitsOnly);
                Console.WriteLine("CPF formatado: " + formattedCpf);
            }
            else
            {
                Console.WriteLine("Erro: CPF inválido.");
            }
        }
        else if (digitsOnly.Length == 14)
        {
            // Validação e formatação do CNPJ
            if (IsValidCNPJ(digitsOnly))
            {
                string formattedCnpj = FormatCNPJ(digitsOnly);
                Console.WriteLine("CNPJ formatado: " + formattedCnpj);
            }
            else
            {
                Console.WriteLine("Erro: CNPJ inválido.");
            }
        }
        else
        {
            Console.WriteLine("Erro: Quantidade de dígitos inválida. Um CPF deve ter 11 dígitos e um CNPJ deve ter 14 dígitos.");
        }
    }

    static bool IsValidCPF(string cpf)
    {
        // Verifica se todos os dígitos são iguais (caso típico de CPF inválido)
        if (new string(cpf[0], cpf.Length) == cpf) return false;

        // Calcula os dígitos verificadores do CPF
        int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores1[i];

        int resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCpf = tempCpf + digito;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicadores2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cpf.EndsWith(digito);
    }

    static bool IsValidCNPJ(string cnpj)
    {
        // Verifica se todos os dígitos são iguais (caso típico de CNPJ inválido)
        if (new string(cnpj[0], cnpj.Length) == cnpj) return false;

        // Calcula os dígitos verificadores do CNPJ
        int[] multiplicadores1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicadores2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores1[i];

        int resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        string digito = resto.ToString();
        tempCnpj = tempCnpj + digito;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicadores2[i];

        resto = soma % 11;
        if (resto < 2)
            resto = 0;
        else
            resto = 11 - resto;

        digito = digito + resto.ToString();

        return cnpj.EndsWith(digito);
    }

    static string FormatCPF(string cpf)
    {
        return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }

    static string FormatCNPJ(string cnpj)
    {
        return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
    }
}
