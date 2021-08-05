using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace PixQrCodeGeneratorOffline.Extention
{
    public static class StringExtention
    {
        public static decimal IsCurrencyToDecimalByCulture(this string currency)
        {
            if (string.IsNullOrEmpty(currency))
                return decimal.Zero;

            if (decimal.TryParse(currency, NumberStyles.Currency, new CultureInfo("pt-BR"), out decimal decimalReturn))
                return decimalReturn;

            else
            {
                if (decimal.TryParse(currency, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal decimalReturnByCurrrentCulture))
                    return decimalReturnByCurrrentCulture;
            }

            return decimal.Zero;
        }

        public static bool ToBoolean(this string text)
        {
            return bool.Parse(text.Replace(" ", "").Trim());
        }

        public static int ToInt(this string text)
        {
            return int.Parse(text.Replace(" ", "").Trim());
        }

        public static TimeSpan ToTimeSpan(this string time)
        {
            if (TimeSpan.TryParse(time, out TimeSpan timeOutput))
                return timeOutput;

            return TimeSpan.MinValue;
        }

        public static string RemoveLastChar(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            return text.Remove(text.Length - 1);
        }

        public static bool IsCPF(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            cpf = cpf.Replace(".", "").Replace("-", "");
            cpf = cpf.Trim();
            if (cpf.Length != 11)
                return false;

            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf;

            string digito;

            int soma;

            int resto;

            tempCpf = cpf.Substring(0, 9);

            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;

            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCpf = tempCpf + digito;

            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;

            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }

        public static bool IsCNPJ(this string cnpj)
        {
            if (string.IsNullOrEmpty(cnpj))
            {
                return false;
            }

            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

            cnpj = cnpj.Trim();

            if (cnpj.Length != 14)
            {
                return false;
            }

            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma;

            int resto;

            string digito;

            string tempCnpj;

            tempCnpj = cnpj.Substring(0, 12);

            soma = 0;

            for (int i = 0; i < 12; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            }

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;

            else
                resto = 11 - resto;

            digito = resto.ToString();

            tempCnpj += digito;

            soma = 0;

            for (int i = 0; i < 13; i++)
            {
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            }

            resto = (soma % 11);

            if (resto < 2)
                resto = 0;

            else
                resto = 11 - resto;

            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        public static bool IsEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            string emailRegex = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
         @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

            return (Regex.IsMatch(email, emailRegex, RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)));
        }

        public static bool IsGuid(this string guid)
        {
            return Guid.TryParse(guid, out _);
        }

        public static bool CpfCnpjValidator(string cpfCnpj)
        {
            if (string.IsNullOrEmpty(cpfCnpj))
                return false;

            string cpfCnpjRegex = @"([0-9]{2}[\.]?[0-9]{3}[\.]?[0-9]{3}[\/]?[0-9]{4}[-]?[0-9]{2})|([0-9]{3}[\.]?[0-9]{3}[\.]?[0-9]{3}[-]?[0-9]{2})";
            return Regex.IsMatch(cpfCnpj, cpfCnpjRegex);
        }

    }
}
