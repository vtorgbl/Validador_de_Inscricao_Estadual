using System;

namespace ConsoleApp3
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Digite a UF");
            string pUF = Console.ReadLine();

            Console.WriteLine("Digite a IE");
            string pInscr = Console.ReadLine();

            fnValidaIE(pUF, pInscr);
        }
        private static bool fnValidaIE(string estadoUF, string IncricaoEstadual)
        {
            IncricaoEstadual = IncricaoEstadual.Trim().Replace(".", "").Replace("/", "").Replace("-", "");

            bool retorno = false;
            string PrimeiraBase, SegundaBase, InscricaoEstadualOriginal, PrimeiroDigitoVerificador, SegundoDigitoVerificador;
            int Posicao, Valor, Soma = 0, Resto, Numero, Peso, Digito;
            PrimeiraBase = "";
            SegundaBase = "";
            InscricaoEstadualOriginal = "";
            if (IncricaoEstadual.Trim().ToUpper() == "ISENTO")
            {
                return true;
            }
            for (Posicao = 1; Posicao <= IncricaoEstadual.Trim().Length; Posicao++)
            {
                if ((("0123456789P".IndexOf(IncricaoEstadual.Substring((Posicao - 1), 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                {
                    InscricaoEstadualOriginal = (InscricaoEstadualOriginal + IncricaoEstadual.Substring((Posicao - 1), 1));
                }
            }

            switch (estadoUF.ToUpper())
            {
                case "AC":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "00000000000").Substring(0, 11);
                    SegundaBase = (InscricaoEstadualOriginal.Trim() + "000000000000").Substring(0, 12);
                    if (PrimeiraBase.Substring(0, 2) == "01" && PrimeiraBase.Substring(0, 2) != "00")
                    {
                        Soma = 0;
                        int Contador = 4;
                        for (Posicao = 1; Posicao <= 11; Posicao++)
                        {

                            Valor = int.Parse(PrimeiraBase.Substring(Posicao - 1, 1));
                            Valor = (Valor * Contador);
                            Soma = Soma + Valor;

                            Resto = (Soma % 11);
                            PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                            SegundaBase = (PrimeiraBase.Substring(0, 11) + PrimeiroDigitoVerificador);

                            if (Contador == 2) { Contador = 10; }

                            Contador--;

                        }
                        int SomaSegundaBase = 0, ContadorSegundaBase = 5, ValorSgundaBase;
                        for (Posicao = 1; Posicao <= 12; Posicao++)
                        {
                            ValorSgundaBase = int.Parse(SegundaBase.Substring(Posicao - 1, 1));
                            ValorSgundaBase = (ValorSgundaBase * ContadorSegundaBase);
                            SomaSegundaBase = SomaSegundaBase + ValorSgundaBase;

                            int RestoSegundaBase;
                            RestoSegundaBase = (SomaSegundaBase % 11);

                            SegundoDigitoVerificador = ((RestoSegundaBase < 2) ? "0" : Convert.ToString((11 - RestoSegundaBase))).Substring((((RestoSegundaBase < 2) ? "0" : Convert.ToString((11 - RestoSegundaBase))).Length - 1));
                            SegundaBase = (SegundaBase.Substring(0, 12) + SegundoDigitoVerificador);

                            if (ContadorSegundaBase == 2) { ContadorSegundaBase = 10; }

                            ContadorSegundaBase--;

                            if (SegundaBase == InscricaoEstadualOriginal)
                            {
                                retorno = true;
                            }
                        }

                    }
                    break;

                case "AL":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    if (PrimeiraBase.Substring(0, 2) == "24")
                    {
                        Soma = 0;
                        for (Posicao = 1; (Posicao <= 8); Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (10 - Posicao);
                            Soma = (Soma + Valor);
                        }
                        Soma = (Soma * 10);
                        Resto = (Soma % 11);
                        PrimeiroDigitoVerificador = ((Resto == 10) ? "0" : Convert.ToString(Resto)).Substring((((Resto == 10) ? "0" : Convert.ToString(Resto)).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "AM":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    Soma = 0;
                    for (Posicao = 1; Posicao <= 8; Posicao++)
                    {
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * (10 - Posicao);
                        Soma = Soma + Valor;
                    }
                    if (Soma < 11)
                    {
                        PrimeiroDigitoVerificador = Convert.ToString((11 - Soma)).Substring((Convert.ToString((11 - Soma)).Length - 1));
                    }
                    else
                    {
                        Resto = Soma % 11;

                        PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    }
                    SegundaBase = (PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador);
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "AP":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    Peso = 0;
                    Digito = 0;
                    if (PrimeiraBase.Substring(0, 2) == "03")
                    {
                        Numero = int.Parse(PrimeiraBase.Substring(0, 8));
                        if (Numero >= 3000001 && Numero <= 3017000)
                        {
                            Peso = 5;
                            Digito = 0;
                        }
                        else if (Numero >= 3017001 && Numero <= 3019022)
                        {
                            Peso = 9;
                            Digito = 1;
                        }
                        else if (Numero >= 3019023)
                        {
                            Peso = 0;
                            Digito = 0;
                        }
                        Soma = Peso;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (10 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 11;
                        Valor = 11 - Resto;
                        if (Valor == 10)
                        {
                            Valor = 0;
                        }
                        else if (Valor == 11)
                        {
                            Valor = Digito;
                        }
                        PrimeiroDigitoVerificador = Convert.ToString(Valor).Substring((Convert.ToString(Valor).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "BA":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "00000000").Substring(0, 8);
                    if ((("0123458".IndexOf(PrimeiraBase.Substring(0, 1), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    {
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 6; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (8 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 10;
                        SegundoDigitoVerificador = ((Resto == 0) ? "0" : Convert.ToString((10 - Resto))).Substring((((Resto == 0) ? "0" : Convert.ToString((10 - Resto))).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 6) + SegundoDigitoVerificador;
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 7; Posicao++)
                        {
                            Valor = int.Parse(SegundaBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (9 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 10;
                        PrimeiroDigitoVerificador = ((Resto == 0) ? "0" : Convert.ToString((10 - Resto))).Substring((((Resto == 0) ? "0" : Convert.ToString((10 - Resto))).Length - 1));
                    }
                    else
                    {
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 6; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (8 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 11;
                        SegundoDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 6) + SegundoDigitoVerificador;
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 7; Posicao++)
                        {
                            Valor = int.Parse(SegundaBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (9 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = (Soma % 11);
                        PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    }
                    SegundaBase = PrimeiraBase.Substring(0, 6) + (PrimeiroDigitoVerificador + SegundoDigitoVerificador);
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "CE":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    Soma = 0;
                    for (Posicao = 1; Posicao <= 8; Posicao++)
                    {
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * (10 - Posicao);
                        Soma = Soma + Valor;
                    }
                    Resto = Soma % 11;
                    Valor = 11 - Resto;
                    if (Valor > 9)
                    {
                        Valor = 0;
                    }
                    PrimeiroDigitoVerificador = Convert.ToString(Valor).Substring((Convert.ToString(Valor).Length - 1));
                    SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "DF":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000000").Substring(0, 12);
                    if ((PrimeiraBase.Substring(0, 2) == "07"))
                    {
                        Soma = 0;
                        Peso = 5;
                        for (Posicao = 1; Posicao <= 11; Posicao++)
                        {
                            Peso--;
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * Peso;
                            Soma = Soma + Valor;
                            if (Peso == 2)
                            {
                                Peso = 10;
                            }
                        }
                        Resto = Soma % 11;
                        PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 11) + PrimeiroDigitoVerificador;
                        Soma = 0;
                        Peso = 6;
                        for (Posicao = 1; Posicao <= 12; Posicao++)
                        {
                            Peso--;
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * Peso;
                            Soma = Soma + Valor;
                            if (Peso == 2)
                            {
                                Peso = 10;
                            }
                        }
                        Resto = Soma % 11;
                        SegundoDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 12) + SegundoDigitoVerificador;
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "ES":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    Soma = 0;
                    for (Posicao = 1; Posicao <= 8; Posicao++)
                    {
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = (Valor * (10 - Posicao));
                        Soma = Soma + Valor;
                    }
                    Resto = Soma % 11;
                    PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    SegundaBase = (PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador);
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "GO":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    if ((("10,11,15".IndexOf(PrimeiraBase.Substring(0, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0))
                    {
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (10 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 11;
                        if (Resto == 0)
                        {
                            PrimeiroDigitoVerificador = "0";
                        }
                        else if (Resto == 1)
                        {
                            Numero = int.Parse(PrimeiraBase.Substring(0, 8));
                            PrimeiroDigitoVerificador = (((Numero >= 10103105) && (Numero <= 10119997)) ? "1" : "0").Substring(((((Numero >= 10103105) && (Numero <= 10119997)) ? "1" : "0").Length - 1));
                        }
                        else
                        {
                            PrimeiroDigitoVerificador = Convert.ToString((11 - Resto)).Substring((Convert.ToString((11 - Resto)).Length - 1));
                        }
                        SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "MA":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    if (PrimeiraBase.Substring(0, 1) == "1")
                    {
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (10 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 11;
                        PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "MT":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "0000000000").Substring(0, 10);
                    Soma = 0;
                    Peso = 4;
                    for (Posicao = 1; Posicao <= 10; Posicao++)
                    {
                        Peso--;
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * Peso;
                        Soma = Soma + Valor;
                        if (Peso == 2)
                        {
                            Peso = 10;
                        }
                    }
                    Resto = Soma % 11;
                    PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    SegundaBase = PrimeiraBase.Substring(0, 10) + PrimeiroDigitoVerificador;
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "MS":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    if (PrimeiraBase.Substring(0, 2) == "28")
                    {
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (10 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 11;
                        PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "MG":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "0000000000000").Substring(0, 13);
                    SegundaBase = PrimeiraBase.Substring(0, 3) + ("0" + PrimeiraBase.Substring(3, 8));
                    Numero = 2;
                    for (Posicao = 1; Posicao <= 12; Posicao++)
                    {
                        Valor = int.Parse(SegundaBase.Substring((Posicao - 1), 1));
                        Numero = (Numero == 2) ? 1 : 2;
                        Valor = (Valor * Numero);
                        if (Valor > 9)
                        {
                            PrimeiroDigitoVerificador = string.Format("00", Valor);
                            Valor = int.Parse(Valor.ToString().Substring(0, 1)) + int.Parse(Valor.ToString().Substring((Valor.ToString().Length - 1)));
                        }
                        Soma = Soma + Valor;
                    }
                    Valor = Soma;
                    double DezenaMaior = (Valor / 10);
                    int PegarInteiro = Convert.ToInt32(Math.Floor(DezenaMaior));
                    int Resultado = ((PegarInteiro + 1) * 10) - Valor;
                    string NumeroResultado = Convert.ToString(Resultado);
                    PrimeiroDigitoVerificador = NumeroResultado;
                    SegundaBase = (PrimeiraBase.Substring(0, 11) + PrimeiroDigitoVerificador);
                    Soma = 0;
                    Peso = 4;
                    for (Posicao = 1; Posicao <= 12; Posicao++)
                    {
                        Peso--;
                        Valor = int.Parse(SegundaBase.Substring((Posicao - 1), 1));
                        Valor = Valor * Peso;
                        Soma = Soma + Valor;

                        if (Peso == 2)
                        {
                            Peso = 12;
                        }
                    }
                    Resto = Soma % 11;
                    SegundoDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    SegundaBase = SegundaBase + SegundoDigitoVerificador;
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;

                    }
                    break;

                case "PA":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    if (PrimeiraBase.Substring(0, 2) == "15")
                    {
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (10 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 11;
                        PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                        SegundaBase = (PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador);
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "PB":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    Soma = 0;
                    for (Posicao = 1; Posicao <= 8; Posicao++)
                    {
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * (10 - Posicao);
                        Soma = Soma + Valor;
                    }
                    Resto = Soma % 11;
                    Valor = 11 - Resto;
                    if (Valor > 9)
                    {
                        Valor = 0;
                    }
                    PrimeiroDigitoVerificador = Convert.ToString(Valor).Substring((Convert.ToString(Valor).Length - 1));
                    SegundaBase = (PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador);
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "PE":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "00000000").Substring(0, 8);
                    SegundaBase = (InscricaoEstadualOriginal.Trim() + "0000000000").Substring(0, 9);
                    Soma = 0;
                    Peso = 8;
                    for (Posicao = 1; Posicao <= 7; Posicao++)
                    {
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * Peso;
                        Soma = Soma + Valor;
                        Peso--;
                    }
                    Resto = Soma % 11;
                    if (Resto == 1 | Resto == 0) { Valor = 0; }
                    else { Valor = 11 - Resto; }
                    PrimeiroDigitoVerificador = Convert.ToString(Valor).Substring((Convert.ToString(Valor).Length - 1));
                    int SegundoDigVerificador, ValorSegundoDig;
                    SegundaBase = PrimeiraBase.Substring(0, 7) + PrimeiroDigitoVerificador;
                    int SomaSegundoDigVerificador = 0;
                    int PesoSegundoDigVerificador = 9;
                    for (Posicao = 1; Posicao <= 8; Posicao++)
                    {
                        ValorSegundoDig = int.Parse(SegundaBase.Substring((Posicao - 1), 1));

                        ValorSegundoDig = ValorSegundoDig * PesoSegundoDigVerificador;

                        SomaSegundoDigVerificador = SomaSegundoDigVerificador + ValorSegundoDig;

                        PesoSegundoDigVerificador--;
                    }
                    Resto = SomaSegundoDigVerificador % 11;
                    if (Resto == 1 | Resto == 0) { ValorSegundoDig = 0; }
                    else { ValorSegundoDig = 11 - Resto; }
                    SegundoDigVerificador = ValorSegundoDig;
                    SegundaBase = SegundaBase.Substring(0, 8) + SegundoDigVerificador;
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "PI":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    Soma = 0;
                    for (Posicao = 1; Posicao <= 8; Posicao++)
                    {
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * (10 - Posicao);
                        Soma = Soma + Valor;
                    }
                    Resto = Soma % 11;
                    PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "PR":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "0000000000").Substring(0, 10);
                    Soma = 0;
                    Peso = 4;
                    for (Posicao = 1; Posicao <= 8; Posicao++)
                    {
                        Peso--;
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * Peso;
                        Soma = Soma + Valor;

                        if (Peso == 2) { Peso = 8; }

                    }
                    Resto = Soma % 11;
                    PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                    Soma = 0;
                    Peso = 5;
                    for (Posicao = 1; Posicao <= 9; Posicao++)
                    {
                        Peso--;
                        Valor = int.Parse(SegundaBase.Substring((Posicao - 1), 1));
                        Valor = Valor * Peso;
                        Soma = Soma + Valor;

                        if (Peso == 2) { Peso = 8; }
                    }
                    Resto = Soma % 11;
                    SegundoDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    SegundaBase = SegundaBase + SegundoDigitoVerificador;
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "RJ":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "00000000").Substring(0, 8);
                    Soma = 0;
                    Peso = 2;
                    for (Posicao = 1; Posicao <= 7; Posicao++)
                    {

                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * Peso;
                        Soma = Soma + Valor;

                        if (Peso == 2) { Peso = 8; }
                        Peso--;
                    }
                    Resto = Soma % 11;
                    PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    SegundaBase = PrimeiraBase.Substring(0, 7) + PrimeiroDigitoVerificador;
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "RN":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "0000000000").Substring(0, 10);
                    if(InscricaoEstadualOriginal.Length==9 && PrimeiraBase.Substring(0, 2) == "20")
                    {
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (10 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Soma = Soma * 10;
                        Resto = Soma % 11;
                        PrimeiroDigitoVerificador = ((Resto > 9) ? "0" : Convert.ToString(Resto)).Substring((((Resto > 9) ? "0" : Convert.ToString(Resto)).Length - 1));
                        SegundaBase = (PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador);
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    else if (InscricaoEstadualOriginal.Length == 10 && PrimeiraBase.Substring(0, 2) == "20")
                    {
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 9; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (11 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Soma = Soma * 10;

                        Resto = Soma % 11;
                        PrimeiroDigitoVerificador = ((Resto > 9) ? "0" : Convert.ToString(Resto)).Substring((((Resto > 9) ? "0" : Convert.ToString(Resto)).Length - 1));
                        SegundaBase = (PrimeiraBase.Substring(0, 9) + PrimeiroDigitoVerificador);
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "RO":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    SegundaBase = PrimeiraBase.Substring(3, 5);
                    Soma = 0;
                    for (Posicao = 1; Posicao <= 5; Posicao++)
                    {
                        Valor = int.Parse(SegundaBase.Substring((Posicao - 1), 1));
                        Valor = Valor * (7 - Posicao);
                        Soma = Soma + Valor;
                    }
                    Resto = (Soma % 11);
                    Valor = (11 - Resto);
                    if (Valor > 9)
                    {
                        Valor = Valor - 10;
                    }
                    PrimeiroDigitoVerificador = Convert.ToString(Valor).Substring((Convert.ToString(Valor).Length - 1));
                    SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "RR":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    if (PrimeiraBase.Substring(0, 2) == "24")
                    {
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (10 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 9;
                        PrimeiroDigitoVerificador = Convert.ToString(Resto).Substring((Convert.ToString(Resto).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "RS":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "0000000000").Substring(0, 10);
                    Numero = int.Parse(PrimeiraBase.Substring(0, 3));
                    if (Numero > 0 && Numero < 468)
                    {
                        Soma = 0;
                        Peso = 3;
                        for (Posicao = 1; Posicao <= 9; Posicao++)
                        {
                            Peso--;
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * Peso;
                            Soma = Soma + Valor;

                            if (Peso == 2)
                            {
                                Peso = 10;
                            }
                        }
                        Resto = (Soma % 11);
                        Valor = (11 - Resto);
                        if (Valor > 9)
                        {
                            Valor = 0;
                        }
                        PrimeiroDigitoVerificador = Convert.ToString(Valor).Substring((Convert.ToString(Valor).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 9) + PrimeiroDigitoVerificador;
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;

                case "SC":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    Soma = 0;
                    Peso = 9;
                    for (Posicao = 1; Posicao <= 8; Posicao++)
                    {
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * (10 - Posicao);
                        Soma = Soma + Valor;
                        Peso--;
                    }
                    Resto = Soma % 11;
                    PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                    SegundaBase = PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador;
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "SE":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000").Substring(0, 9);
                    Soma = 0;
                    for (Posicao = 1; Posicao <= 8; Posicao++)
                    {
                        Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                        Valor = Valor * (10 - Posicao);
                        Soma = Soma + Valor;
                    }
                    Resto = Soma % 11;
                    Valor = 11 - Resto;
                    if (Valor > 9)
                    {
                        Valor = 0;
                    }
                    PrimeiroDigitoVerificador = Convert.ToString(Valor).Substring((Convert.ToString(Valor).Length - 1));
                    SegundaBase = (PrimeiraBase.Substring(0, 8) + PrimeiroDigitoVerificador);
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "SP":
                    if (InscricaoEstadualOriginal.Substring(0, 1) == "P")
                    {
                        PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "0000000000000").Substring(0, 13);
                        SegundaBase = PrimeiraBase.Substring(1, 8);
                        Soma = 0;
                        Peso = 1;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * Peso;
                            Soma = Soma + Valor;
                            Peso++;
                            if (Peso == 2)
                            {
                                Peso = 3;
                            }
                            if (Peso == 9)
                            {
                                Peso = 10;
                                break;
                            }
                        }
                        Resto = Soma % 11;
                        PrimeiroDigitoVerificador = Convert.ToString(Resto).Substring((Convert.ToString(Resto).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 8) + (PrimeiroDigitoVerificador + PrimeiraBase.Substring(10, 3));
                    }
                    else
                    {
                        PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "000000000000").Substring(0, 12);
                        Soma = 0;
                        Peso = 1;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * Peso;
                            Soma = Soma + Valor;
                            Peso++;
                            if (Peso == 2)
                            {
                                Peso = 3;
                            }
                            if (Peso == 9)
                            {
                                Peso = 10;
                            }
                        }
                        Resto = Soma % 11;
                        PrimeiroDigitoVerificador = Convert.ToString(Resto).Substring((Convert.ToString(Resto).Length - 1));
                        SegundaBase = (PrimeiraBase.Substring(0, 8) + (PrimeiroDigitoVerificador + PrimeiraBase.Substring(9, 2)));
                        Soma = 0;
                        Peso = 4;
                        for (Posicao = 1; Posicao <= 11; Posicao++)
                        {
                            Peso--;
                            Valor = int.Parse(PrimeiraBase.Substring((Posicao - 1), 1));
                            Valor = Valor * Peso;
                            Soma = Soma + Valor;

                            if (Peso == 2)
                            {
                                Peso = 11;
                            }
                        }
                        Resto = Soma % 11;
                        SegundoDigitoVerificador = Convert.ToString(Resto).Substring((Convert.ToString(Resto).Length - 1));
                        SegundaBase = SegundaBase + SegundoDigitoVerificador;
                    }
                    if (SegundaBase == InscricaoEstadualOriginal)
                    {
                        retorno = true;
                    }
                    break;

                case "TO":
                    PrimeiraBase = (InscricaoEstadualOriginal.Trim() + "00000000000").Substring(0, 11);
                    if (("01,02,03,99".IndexOf(PrimeiraBase.Substring(2, 2), 0, System.StringComparison.OrdinalIgnoreCase) + 1) > 0)
                    {
                        SegundaBase = PrimeiraBase.Substring(0, 2) + PrimeiraBase.Substring(4, 6);
                        Soma = 0;
                        for (Posicao = 1; Posicao <= 8; Posicao++)
                        {
                            Valor = int.Parse(SegundaBase.Substring((Posicao - 1), 1));
                            Valor = Valor * (10 - Posicao);
                            Soma = Soma + Valor;
                        }
                        Resto = Soma % 11;
                        PrimeiroDigitoVerificador = ((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Substring((((Resto < 2) ? "0" : Convert.ToString((11 - Resto))).Length - 1));
                        SegundaBase = PrimeiraBase.Substring(0, 10) + PrimeiroDigitoVerificador;
                        if (SegundaBase == InscricaoEstadualOriginal)
                        {
                            retorno = true;
                        }
                    }
                    break;
            }
            return retorno;
        }
    }
}