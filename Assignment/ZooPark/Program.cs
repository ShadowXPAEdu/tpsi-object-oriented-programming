using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Trabalho_Prático
{
    [Serializable]
    public enum EHabitat
    {
        Null = -1,
        Savana,
        Floresta,
        Oceano,
        Deserto,
        Tundra,
        Floresta_Tropical
    }

    public enum ETipoArea
    {
        Null = -1,
        Jaula,
        Vedado
    }

    class Program
    {
        const String Dir = @"Ficheiros\";
        const String FichAreas = Dir + "areas.txt";
        const String FichEspecies = Dir + "species.txt";
        const String FichAnimais = Dir + "animals.txt";
        const String PressKey = "\nPress any key to continue...";
        const String CancelCmd = "/cancel";
        public static List<int> listaSpecies_ID = new List<int>();
        const int NumHabitats = 6;

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.CursorVisible = false;
            // Posso mudar o nome do Zoo para Bunny Kingdom?
            Console.Title = "ZooPark Manager";
            Console.SetBufferSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            Console.SetWindowSize(Console.LargestWindowWidth, Console.LargestWindowHeight);
            List<Area> listaAreas = new List<Area>();
            List<Species> listaSpecies = new List<Species>();
            List<Animal> listaAnimals = new List<Animal>();
            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);

            // Load File...
            // Load File: Áreas
            if (!File.Exists(FichAreas))
            {
                // Sai se ficheiro areas.txt não existir
                return;
            }
            else
            {
                LoadAreasFromFile(listaAreas);
                VerificaListaAreas(listaAreas);
            }
            // Load File: Espécies
            if (File.Exists(FichEspecies))
            {
                // serialize não compatível com este programa
                //DeserializeSpeciesFromFile(listaSpecies);
                LoadSpeciesFromFile(listaSpecies, listaSpecies_ID);
            }
            // Load File: Animais
            if (File.Exists(FichAnimais))
            {
                LoadAnimalsFromFile(listaAnimals, listaAreas, listaSpecies);
                VerificaListaAnimal(listaAnimals);
            }

            // Menu DONE!
            while (Menu(listaAreas, listaSpecies, listaAnimals)) { }

            Console.Clear();
            Console.WriteLine("Salvando informação para ficheiros...");

            // Save File... 
            // Save File: Áreas
            SaveAreasIntoFile(listaAreas);
            // Save File: Espécies
            //SerializeSpeciesIntoFile(listaSpecies);
            SaveSpeciesIntoFile(listaSpecies);
            // Save File: Animais
            SaveAnimalsIntoFile(listaAnimals);

            Console.WriteLine("Done! Adeus =D");

            //Console.ReadKey(true);
        }

        // Funções do programa

        // Menu
        private static bool Menu(List<Area> XAreas, List<Species> YSpecies, List<Animal> ZAnimal)
        {
            try
            {
                int op;
                String Str;
                Console.Clear();
                MenuHeader("Menu Principal", ConsoleColor.Red);
                OpcoesMenuPrincipal(ConsoleColor.Red, XAreas, YSpecies, ZAnimal);
                UserInput(out Str);
                op = int.Parse(Str);

                switch (op)
                {
                    case 0:
                        return false;
                    case 1:
                        // Menu Áreas
                        while (MenuArea(XAreas, YSpecies, ZAnimal)) { }
                        break;
                    case 2:
                        // Menu Espécies
                        while (MenuEspecie(XAreas, YSpecies, ZAnimal)) { }
                        break;
                    case 3:
                        // Menu Animais
                        while (MenuAnimal(XAreas, YSpecies, ZAnimal)) { }
                        break;
                    case 4:
                        Console.Clear();
                        MenuHeader("Listagem de habitats", ConsoleColor.White);
                        Console.ForegroundColor = ConsoleColor.White;
                        for (int i = 0; i < NumHabitats; i++)
                        {
                            Console.WriteLine(" [{0}] - {1}", (i + 1), ((EHabitat)(i)).ToString().Replace('_', ' ').Trim());
                        }
                        Console.WriteLine("\n" + PressKey);
                        Console.ReadKey(true);
                        break;
                    case -22:
                        // Opção ULTRA secreta =D
                        // Testes aqui
                        break;
                    default:
                        PrintMsgErro("Opção não existe. Tente novamente.");
                        break;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        // Menu Área DONE!
        private static bool MenuArea(List<Area> XAreas, List<Species> YSpecies, List<Animal> ZAnimal)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Área", ConsoleColor.Blue);
                Console.WriteLine(" Número de áreas existentes: {0}\n", XAreas.Count);
                OpcoesMenuArea(ConsoleColor.Blue);
                int op;
                String Str;
                UserInput(out Str);
                op = int.Parse(Str);

                switch (op)
                {
                    case 0:
                        return false;
                    case 1:
                        if (XAreas.Count > 0)
                        {
                            while (ListarAreas(XAreas)) { };
                        }
                        else
                        {
                            PrintMsgErro("Não existe nenhuma área para listar.");
                        }
                        break;
                    case 2:
                        while (CriarAreas(XAreas)) { };
                        break;
                    case 3:
                        if (XAreas.Count > 0)
                        {
                            while (EliminarAreas(XAreas)) { };
                        }
                        else
                        {
                            PrintMsgErro("Não existe nenhuma área para eliminar.");
                        }
                        break;
                    case 4:
                        if (XAreas.Count > 0)
                        {
                            while (EditarAreas(XAreas)) { };
                        }
                        else
                        {
                            PrintMsgErro("Não existe nenhuma área para editar.");
                        }
                        break;
                    default:
                        PrintMsgErro("Opção não existe. Tente novamente.");
                        break;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool ListarAreas(List<Area> XAreas)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Área: Listagem de áreas", ConsoleColor.Blue);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [0] - Voltar atrás\n");
                for (int i = 0; i < XAreas.Count; i++)
                {
                    Console.WriteLine(" [{0}] - {1}", (i + 1), XAreas[i].GetNome());
                }
                Console.WriteLine("\n Selecione uma área para visualizar mais informação:");
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                if (op1 == 0)
                {
                    return false;
                }
                else if (op1 > 0 && op1 <= XAreas.Count)
                {
                    Console.Clear();
                    MenuHeader("Menu Área: Listagem de áreas: " + XAreas[op1 - 1].GetNome(), ConsoleColor.Blue);
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(XAreas[op1 - 1].GetInfo());
                    Console.WriteLine(PressKey);
                    Console.ReadKey(true);
                }
                else
                {
                    PrintMsgErro("Opção não existe.");
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool CriarAreas(List<Area> XAreas)
        {
            try
            {
                String nome, habitat, cap, numFront, front;
                EHabitat hab = EHabitat.Null;
                Double capMax = 0;
                int numFron = 0, frontInd = 0;
                List<String> fronteiras = new List<String>();
                Console.Clear();
                MenuHeader("Menu Área: Criar área", ConsoleColor.Blue);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" Use '" + CancelCmd + "' a qualquer momento para cancelar.\n Introduza o nome da nova área:\n");
                UserInput(out nome);
                if (nome != CancelCmd)
                {
                    if (nome.Replace('_', ' ').Trim() != "")
                    {
                        if (!Area.ListaAreas.Contains(nome))
                        {
                            do
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("\n Introduza o habitat da nova área:\n");
                                UserInput(out habitat);
                                if (habitat == CancelCmd)
                                {
                                    return false;
                                }
                                if (ToEHabitat(habitat) == EHabitat.Null)
                                {
                                    PrintMsgErro("Habitat não existe.");
                                }
                            } while (ToEHabitat(habitat) == EHabitat.Null);
                            hab = ToEHabitat(habitat);
                            bool notDoneParsing;
                            do
                            {
                                notDoneParsing = true;
                                try
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\n Introduza a capacidade máxima da nova área:\n");
                                    UserInput(out cap);
                                    if (cap != CancelCmd)
                                    {
                                        capMax = Double.Parse(cap);
                                        if (capMax > 0)
                                            notDoneParsing = false;
                                        else
                                            PrintMsgErro("A capacidade não pode ser negativa.");
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                catch (Exception e)
                                {
                                    PrintMsgErro("Erro: " + e.Message);
                                }
                            } while (notDoneParsing);
                            do
                            {
                                notDoneParsing = true;
                                try
                                {
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\n Introduza o número de fronteiras da nova área:\n");
                                    UserInput(out numFront);
                                    if (numFront != CancelCmd)
                                    {
                                        numFron = int.Parse(numFront);
                                        if (numFron <= 3 && numFron >= 0)
                                            notDoneParsing = false;
                                        else
                                            PrintMsgErro("Número de fronteiras deve estar entre 0 e 3.");
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                catch (Exception e)
                                {
                                    PrintMsgErro("Erro: " + e.Message);
                                }
                            } while (notDoneParsing || (numFron > 3 || numFron < 0));
                            do
                            {
                                notDoneParsing = true;
                                try
                                {
                                    for (int i = 0; i < numFron;)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("\n Visualizar a listagem de áreas para os números de cada área.\n Introduza o número da fronteira para adicionar á nova área: {0}º Fronteira Opções: [1] ~ [{1}]\n", (i + 1), XAreas.Count);
                                        UserInput(out front);
                                        if (front != CancelCmd)
                                        {
                                            frontInd = int.Parse(front);
                                            if (frontInd > 0 && frontInd <= XAreas.Count)
                                            {
                                                if (XAreas[frontInd - 1].GetCpFronteiras().Count < 3 && fronteiras.Count < 3)
                                                {
                                                    if (!fronteiras.Contains(XAreas[frontInd - 1].GetNome()))
                                                    {
                                                        fronteiras.Add(XAreas[frontInd - 1].GetNome());
                                                        i++;
                                                    }
                                                    else
                                                    {
                                                        PrintMsgErro("Fronteira já existe.");
                                                    }
                                                }
                                                else
                                                {
                                                    PrintMsgErro("Uma das áreas excedeu o número de fronteiras.");
                                                }
                                            }
                                            else
                                            {
                                                PrintMsgErro("Fronteira não existe.");
                                            }
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    notDoneParsing = false;
                                }
                                catch (Exception e)
                                {
                                    PrintMsgErro("Erro: " + e.Message);
                                }
                            } while (notDoneParsing);

                            try
                            {
                                XAreas.Add(new Area(nome, hab, capMax, new List<String>(fronteiras)));
                                for (int i = 0; i < fronteiras.Count; i++)
                                {
                                    XAreas[XAreas.Count - 1].AddFronteira(XAreas[Area.ListaAreas.IndexOf(fronteiras[i])]);
                                }
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("\nÁrea criada com sucesso.");
                                Console.WriteLine(PressKey);
                                Console.ReadKey(true);
                                return false;
                            }
                            catch (Exception e)
                            {
                                PrintMsgErro("Erro: " + e.Message);
                            }
                        }
                        else
                        {
                            PrintMsgErro("Nome da área já existe.");
                        }
                    }
                    else
                    {
                        PrintMsgErro("Nome da área não pode ser vazia ou '_'.");
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool EliminarAreas(List<Area> XAreas)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Área: Eliminar uma área", ConsoleColor.Blue);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [0] - Cancelar\n");
                Console.WriteLine("\n Visualizar listagem de áreas para os números de cada área.\n Selecione uma área para eliminar: [1] ~ [{0}]", XAreas.Count);
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                if (op1 == 0)
                {
                    return false;
                }
                else if (op1 > 0 && op1 <= XAreas.Count)
                {
                    // Remove área se não tiver animais dentro da área
                    if (XAreas[op1 - 1].GetAnimals().Count == 0)
                    {
                        for (int i = 0; i < XAreas.Count; i++)
                        {
                            if (i != (op1 - 1))
                            {
                                XAreas[i].RemoverFronteira(XAreas[op1 - 1]);
                            }
                        }
                        Area.ListaAreas.Remove(XAreas[op1 - 1].GetNome());
                        XAreas.RemoveAt(op1 - 1);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\nÁrea removida com sucesso.");
                        Console.WriteLine(PressKey);
                        Console.ReadKey(true);
                        return false;
                    }
                    else
                    {
                        PrintMsgErro("Não é possível remover uma área com animais dentro.");
                    }
                }
                else
                {
                    PrintMsgErro("Área selecionada não existe.");
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAreas(List<Area> XAreas)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Área: Editar uma área", ConsoleColor.Blue);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [0] - Cancelar");
                Console.WriteLine("\n Visualizar listagem de áreas para os números de cada área.\n Selecione uma área para editar informação: [1] ~ [{0}]", XAreas.Count);
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                if (op1 == 0)
                {
                    return false;
                }
                else if (op1 > 0 && op1 <= XAreas.Count)
                {
                    if (XAreas[op1 - 1].GetAnimals().Count == 0)
                    {
                        while (EditarAreasSubMenu(XAreas, op1 - 1)) { };
                    }
                    else
                    {
                        PrintMsgErro("Impossível proceder a obras numa área com animais dentro.\nCoitados dos animais...");
                    }
                }
                else
                {
                    PrintMsgErro("Opção não existe.");
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAreasSubMenu(List<Area> XAreas, int Index)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Área: Editar uma área: " + XAreas[Index].GetNome(), ConsoleColor.Blue);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(" [0] - Cancelar\n");
                Console.WriteLine("\n [1] - Editar nome\n [2] - Editar habitat\n [3] - Editar capacidade máxima\n [4] - Adicionar fronteira\n [5] - Remover fronteira");
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                switch (op1)
                {
                    case 0:
                        return false;
                    case 1:
                        while (EditarAreasSubMenuNome(XAreas, Index)) { };
                        break;
                    case 2:
                        while (EditarAreasSubMenuHabitat(XAreas, Index)) { };
                        break;
                    case 3:
                        while (EditarAreasSubMenuCapMax(XAreas, Index)) { };
                        break;
                    case 4:
                        while (EditarAreasSubMenuAddFronteira(XAreas, Index)) { };
                        break;
                    case 5:
                        while (EditarAreasSubMenuRemFronteira(XAreas, Index)) { };
                        break;
                    default:
                        PrintMsgErro("Opção não existe.");
                        break;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAreasSubMenuNome(List<Area> XAreas, int Index)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Introduza um novo nome:");
                String str2;
                UserInput(out str2);
                str2 = str2.Replace('_', ' ').Trim();
                if (str2 == CancelCmd)
                {
                    return false;
                }
                else
                {
                    if (str2 != "")
                    {
                        if (!Area.ListaAreas.Contains(str2))
                        {
                            for (int i = 0; i < XAreas.Count; i++)
                            {
                                if (i != Index)
                                {
                                    if (XAreas[i].GetCpFronteiras().Contains(XAreas[Index].GetNome()))
                                    {
                                        XAreas[i].RenomearFronteira(XAreas[Index].GetNome(), str2);
                                    }
                                }
                            }
                            Area.ListaAreas[Area.ListaAreas.IndexOf(XAreas[Index].GetNome())] = str2;
                            XAreas[Index].SetNome(str2);
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\nNome editado com sucesso.");
                            Console.WriteLine(PressKey);
                            Console.ReadKey(true);
                            return false;
                        }
                        else
                        {
                            PrintMsgErro("Nome já existe.");
                        }
                    }
                    else
                    {
                        PrintMsgErro("Nome inválido.");
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAreasSubMenuHabitat(List<Area> XAreas, int Index)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Introduza um novo habitat:");
                String str3;
                UserInput(out str3);
                if (str3 == CancelCmd)
                {
                    return false;
                }
                else
                {
                    if (ToEHabitat(str3) != EHabitat.Null)
                    {
                        if (ToEHabitat(str3) == ToEHabitat(XAreas[Index].GetHabitat()))
                        {
                            PrintMsgErro("Mudar habitat para o mesmo habitat?...");
                        }
                        else
                        {
                            XAreas[Index].SetHabitat(ToEHabitat(str3));
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("\nHabitat editado com sucesso.");
                            Console.WriteLine(PressKey);
                            Console.ReadKey(true);
                            return false;
                        }
                    }
                    else
                    {
                        PrintMsgErro("Novo habitat não existe.");
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAreasSubMenuCapMax(List<Area> XAreas, int Index)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Introduza uma nova capacidade máxima:");
                String str4;
                UserInput(out str4);
                if (str4 == CancelCmd)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        Double NovaCap = Double.Parse(str4);

                        if (NovaCap > 0)
                        {
                            if (NovaCap == XAreas[Index].GetCapacidade())
                            {
                                PrintMsgErro("Mudar capacidade para a mesma capacidade?...");
                            }
                            else
                            {
                                XAreas[Index].SetCapacidade(NovaCap);
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.WriteLine("\nCapacidade máxima editada com sucesso.");
                                Console.WriteLine(PressKey);
                                Console.ReadKey(true);
                                return false;
                            }
                        }
                        else
                        {
                            PrintMsgErro("Capacidade máxima tem de ser maior que 0...");
                        }
                    }
                    catch (Exception e)
                    {
                        PrintMsgErro("Erro: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAreasSubMenuAddFronteira(List<Area> XAreas, int Index)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Selecione a área para adicionar: [1] ~ [{0}]", XAreas.Count);
                String str5;
                UserInput(out str5);
                if (str5 == CancelCmd)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        int newIndex = int.Parse(str5);

                        if (newIndex > 0 && newIndex <= XAreas.Count)
                        {
                            if ((newIndex - 1) != Index)
                            {
                                if (XAreas[Index].GetCpFronteiras().Count > 2 || XAreas[newIndex - 1].GetCpFronteiras().Count > 2)
                                {
                                    PrintMsgErro("Uma das áreas não pode conter mais fronteiras.");
                                }
                                else
                                {
                                    if (!XAreas[Index].GetCpFronteiras().Contains(XAreas[newIndex - 1].GetNome()))
                                    {
                                        XAreas[Index].AddFronteira(XAreas[newIndex - 1]);
                                        Console.ForegroundColor = ConsoleColor.Blue;
                                        Console.WriteLine("\nFronteira adicionada com sucesso.");
                                        Console.WriteLine(PressKey);
                                        Console.ReadKey(true);
                                        return false;
                                    }
                                    else
                                    {
                                        PrintMsgErro("Fronteira já existente.");
                                    }
                                }
                            }
                            else
                            {
                                PrintMsgErro("Erro: Fronteira é igual à área que está a editar...");
                            }
                        }
                        else
                        {
                            PrintMsgErro("Área não existe.");
                        }
                    }
                    catch (Exception e)
                    {
                        PrintMsgErro("Erro: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAreasSubMenuRemFronteira(List<Area> XAreas, int Index)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Selecione a área para remover: [1] ~ [{0}]", XAreas.Count);
                String str6;
                UserInput(out str6);
                if (str6 == CancelCmd)
                {
                    return false;
                }
                else
                {
                    try
                    {
                        int newIndex2 = int.Parse(str6);

                        if (newIndex2 > 0 && newIndex2 <= XAreas.Count)
                        {
                            if ((newIndex2 - 1) != Index)
                            {
                                if (XAreas[Index].GetCpFronteiras().Contains(XAreas[newIndex2 - 1].GetNome()))
                                {
                                    XAreas[Index].RemoverFronteira(XAreas[newIndex2 - 1]);
                                    Console.ForegroundColor = ConsoleColor.Blue;
                                    Console.WriteLine("\nFronteira removida com sucesso.");
                                    Console.WriteLine(PressKey);
                                    Console.ReadKey(true);
                                    return false;
                                }
                                else
                                {
                                    PrintMsgErro("Fronteira não existe nesta área.");
                                }
                            }
                            else
                            {
                                PrintMsgErro("Erro: Fronteira é igual à área que está a editar...");
                            }
                        }
                        else
                        {
                            PrintMsgErro("Área não existe.");
                        }
                    }
                    catch (Exception e)
                    {
                        PrintMsgErro("Ocurreu um erro: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }
        // fim Menu Área

        // Menu Espécie DONE!
        private static bool MenuEspecie(List<Area> XAreas, List<Species> YSpecies, List<Animal> ZAnimal)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Espécie", ConsoleColor.Magenta);
                Console.WriteLine(" Número de espécies existentes: {0}\n", YSpecies.Count);
                OpcoesMenuEspecie(ConsoleColor.Magenta);
                int op;
                String Str;
                UserInput(out Str);
                op = int.Parse(Str);

                switch (op)
                {
                    case 0:
                        return false;
                    case 1:
                        if (YSpecies.Count > 0)
                        {
                            while (ListarEspecies(YSpecies, ZAnimal)) { };
                        }
                        else
                        {
                            PrintMsgErro("Não existe nenhuma espécie para listar.");
                        }
                        break;
                    case 2:
                        while (CriarEspecies(YSpecies)) { };
                        break;
                    case 3:
                        if (YSpecies.Count > 0)
                        {
                            while (EliminarEspecies(YSpecies, ZAnimal)) { };
                        }
                        else
                        {
                            PrintMsgErro("Não existe nenhuma espécie para eliminar.");
                        }
                        break;
                    case 4:
                        if (YSpecies.Count > 0)
                        {
                            while (EditarEspecies(YSpecies, XAreas, ZAnimal)) { };
                        }
                        else
                        {
                            PrintMsgErro("Não existe nenhuma espécie para editar.");
                        }
                        break;
                    default:
                        PrintMsgErro("Opção não existe. Tente novamente.");
                        break;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool ListarEspecies(List<Species> YSpecies, List<Animal> ZAnimal)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Espécie: Listagem de espécies", ConsoleColor.Magenta);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" [0] - Voltar atrás\n");
                Console.WriteLine(" [1] - Listagem de todas as espécies\n [2] - Listagem de espécies existentes no zoo");
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);

                switch (op1)
                {
                    case 0:
                        return false;
                    case 1:
                        while (ListagemEspecies(YSpecies)) { };
                        break;
                    case 2:
                        while (ListagemEspZoo(YSpecies, ZAnimal)) { };
                        break;
                    default:
                        PrintMsgErro("Opção não existe.");
                        break;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool ListagemEspecies(List<Species> YSpecies)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Espécie: Listagem de espécies: Listagem de todas as espécies", ConsoleColor.Magenta);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" [0] - Voltar atrás\n");
                for (int i = 0; i < YSpecies.Count; i++)
                {
                    Console.WriteLine(" [{0}] - {1}", (i + 1), YSpecies[i].GetNomeEspecie());
                }
                Console.WriteLine("\n Selecione uma espécie para visualizar mais informação:");

                try
                {
                    String strEsp;
                    UserInput(out strEsp);
                    int strEspInt = int.Parse(strEsp);

                    if (strEspInt == 0)
                    {
                        return false;
                    }
                    else if (strEspInt > 0 && strEspInt <= YSpecies.Count)
                    {
                        Console.Clear();
                        MenuHeader("Menu Espécie: Listagem de espécies: " + YSpecies[strEspInt - 1].GetNomeEspecie(), ConsoleColor.Magenta);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(YSpecies[strEspInt - 1].GetInfo());
                        Console.WriteLine(PressKey);
                        Console.ReadKey(true);
                    }
                    else
                    {
                        PrintMsgErro("Opção não existe.");
                    }
                }
                catch (Exception e)
                {
                    PrintMsgErro("Erro: " + e.Message);
                }

            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool ListagemEspZoo(List<Species> YSpecies, List<Animal> ZAnimal)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Espécie: Listagem de espécies: Listagem de espécies existentes no zoo", ConsoleColor.Magenta);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" [0] - Voltar atrás\n");
                List<String> listaTempEspecies = new List<String>();
                int count = 0;
                for (int i = 0; i < ZAnimal.Count; i++)
                {
                    // Se espécie ainda não apareceu lista no ecrã
                    if (!listaTempEspecies.Contains(ZAnimal[i].GetEspecie()))
                    {
                        // Adiciona para a lista para que não volte a aparecer mais tarde..
                        listaTempEspecies.Add(ZAnimal[i].GetEspecie());
                        Console.WriteLine(" [{0}] - {1}", (count + 1), ZAnimal[i].GetEspecie());
                        count++;
                    }
                }
                Console.WriteLine("\n Selecione uma espécie para visualizar mais informação:");

                try
                {
                    String strEsp2;
                    UserInput(out strEsp2);
                    int strEspInt2 = int.Parse(strEsp2);

                    if (strEspInt2 == 0)
                    {
                        return false;
                    }
                    else if (strEspInt2 > 0 && strEspInt2 <= count)
                    {
                        Console.Clear();
                        MenuHeader("Menu Espécie: Listagem de espécies: " + YSpecies[Species.ListaEspecies.IndexOf(listaTempEspecies[strEspInt2 - 1])].GetNomeEspecie(), ConsoleColor.Magenta);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine(YSpecies[Species.ListaEspecies.IndexOf(listaTempEspecies[strEspInt2 - 1])].GetInfo());
                        Console.WriteLine(PressKey);
                        Console.ReadKey(true);
                    }
                    else
                    {
                        PrintMsgErro("Opção não existe.");
                    }
                }
                catch (Exception e)
                {
                    PrintMsgErro("Erro: " + e.Message);
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool CriarEspecies(List<Species> YSpecies)
        {
            try
            {
                String nomeEsp, numEspStr, habitatStr;
                int numEsp = 0;
                List<EHabitat> habitats = new List<EHabitat>();
                Console.Clear();
                MenuHeader("Menu Espécie: Criar espécie", ConsoleColor.Magenta);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" Use '" + CancelCmd + "' a qualquer momento para cancelar.\n Introduza o nome da nova espécie:\n");
                UserInput(out nomeEsp);
                nomeEsp = nomeEsp.Replace('_', ' ').Trim();
                if (nomeEsp != CancelCmd)
                {
                    if (nomeEsp != "")
                    {
                        if (!Species.ListaEspecies.Contains(nomeEsp))
                        {
                            bool notDoneParsing;
                            do
                            {
                                notDoneParsing = true;
                                try
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("\n Introduza o número de habitats para esta espécie:\n");
                                    UserInput(out numEspStr);
                                    if (numEspStr != CancelCmd)
                                    {
                                        numEsp = int.Parse(numEspStr);
                                        if (numEsp > 0 && numEsp <= NumHabitats)
                                            notDoneParsing = false;
                                        else
                                            PrintMsgErro("A espécie tem de ter pelo menos 1 habitat e menos que " + NumHabitats + " habitats.");
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                catch (Exception e)
                                {
                                    PrintMsgErro("Erro: " + e.Message);
                                }
                            } while (notDoneParsing);

                            for (int i = 0; i < numEsp;)
                            {
                                do
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("\n Introduza o {0}º habitat para esta espécie:\n", (i + 1));
                                    UserInput(out habitatStr);
                                    habitatStr = habitatStr.Replace('_', ' ').Trim();
                                    if (habitatStr == CancelCmd)
                                    {
                                        return false;
                                    }
                                    if (ToEHabitat(habitatStr) == EHabitat.Null)
                                    {
                                        PrintMsgErro("Habitat não existe.");
                                    }
                                } while (ToEHabitat(habitatStr) == EHabitat.Null);

                                if (!habitats.Contains(ToEHabitat(habitatStr)))
                                {
                                    habitats.Add(ToEHabitat(habitatStr));
                                    i++;
                                }
                                else
                                {
                                    PrintMsgErro("Habitat já existe.");
                                }
                            }
                            try
                            {
                                YSpecies.Add(new Species(nomeEsp, new List<EHabitat>(habitats)));
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("\nEspécie criada com sucesso.");
                                Console.WriteLine(PressKey);
                                Console.ReadKey(true);
                                return false;
                            }
                            catch (Exception e)
                            {
                                PrintMsgErro("Occureu um erro: " + e.Message);
                            }
                        }
                        else
                        {
                            PrintMsgErro("Nome da espécie já existe.");
                        }
                    }
                    else
                    {
                        PrintMsgErro("Nome da espécie não pode ser vazia ou '_'.");
                    }
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EliminarEspecies(List<Species> YSpecies, List<Animal> ZAnimal)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Espécie: Eliminar uma espécie", ConsoleColor.Magenta);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" [0] - Cancelar\n");
                Console.WriteLine("\n Visualizar listagem de espécies para os números de cada espécie.\n Selecione uma espécie para eliminar: [1] ~ [{0}]", YSpecies.Count);
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                if (op1 == 0)
                {
                    return false;
                }
                else if (op1 > 0 && op1 <= YSpecies.Count)
                {
                    int cont = 0;
                    // só  podem  ser  removidas  espécies  que  não  estejam representadas no zoo
                    // Verifica se o animal com essa espécie existe no zoo
                    // se existir (cont > 0) então é porque a espécie está a ser representada no zoo
                    // e não dá para remover
                    for (int i = 0; i < ZAnimal.Count; i++)
                    {
                        if (ZAnimal[i].GetEspecie() == YSpecies[op1 - 1].GetNomeEspecie())
                        {
                            cont++;
                            // break... basta haver um animal com essa espécie.. escusa de verificar o resto...
                            break;
                        }
                    }

                    if (cont != 0)
                    {
                        PrintMsgErro("Impossível remover espécie pois há animais com essa espécie dentro do zoo.");
                    }
                    else
                    {
                        Species.ListaEspecies.Remove(YSpecies[op1 - 1].GetNomeEspecie());
                        YSpecies.RemoveAt(op1 - 1);
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("\nEspécie removida com sucesso.");
                        Console.WriteLine(PressKey);
                        Console.ReadKey(true);
                        return false;
                    }
                }
                else
                {
                    PrintMsgErro("Espécie selecionada não existe.");
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarEspecies(List<Species> YSpecies, List<Area> XAreas, List<Animal> ZAnimal)
        {
            try
            {
                // Editar nome
                // Adicionar e remover habitats

                Console.Clear();
                MenuHeader("Menu Espécie: Editar uma espécie", ConsoleColor.Magenta);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" [0] - Cancelar");
                Console.WriteLine("\n Visualizar listagem de espécies para os números de cada espécie.\n Selecione uma espécie para editar informação: [1] ~ [{0}]", YSpecies.Count);
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                if (op1 == 0)
                {
                    return false;
                }
                else if (op1 > 0 && op1 <= YSpecies.Count)
                {
                    while (EditarEspeciesSubMenu(YSpecies, XAreas, ZAnimal, (op1 - 1))) { };
                }
                else
                {
                    PrintMsgErro("Opção não existe.");
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarEspeciesSubMenu(List<Species> YSpecies, List<Area> XAreas, List<Animal> ZAnimal, int Index)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Espécie: Editar uma espécie: " + YSpecies[Index].GetNomeEspecie(), ConsoleColor.Magenta);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(" [0] - Cancelar\n");
                Console.WriteLine("\n [1] - Editar nome\n [2] - Adicionar habitat\n [3] - Remover habitat");
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                switch (op1)
                {
                    case 0:
                        return false;
                    case 1:
                        while (EditarEspeciesSubMenuNome(YSpecies, ZAnimal, Index)) { };
                        break;
                    case 2:
                        while (EditarEspeciesSubMenuAddHabitat(YSpecies, Index)) { };
                        break;
                    case 3:
                        while (EditarEspeciesSubMenuRemHabitat(YSpecies, XAreas, Index)) { };
                        break;
                    default:
                        PrintMsgErro("Opção não existe.");
                        break;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarEspeciesSubMenuNome(List<Species> YSpecies, List<Animal> ZAnimal, int Index)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Introduza um novo nome:");
                String str2;
                UserInput(out str2);
                str2 = str2.Replace('_', ' ').Trim();
                if (str2 == CancelCmd)
                {
                    return false;
                }
                else
                {
                    if (str2 != "")
                    {
                        if (!Species.ListaEspecies.Contains(str2))
                        {
                            for (int i = 0; i < ZAnimal.Count; i++)
                            {
                                if (ZAnimal[i].GetEspecie() == YSpecies[Index].GetNomeEspecie())
                                {
                                    ZAnimal[i].SetEspecie(str2);
                                }
                            }
                            Species.ListaEspecies[Species.ListaEspecies.IndexOf(YSpecies[Index].GetNomeEspecie())] = str2;
                            YSpecies[Index].SetNome(str2);
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            Console.WriteLine("\nNome editado com sucesso.");
                            Console.WriteLine(PressKey);
                            Console.ReadKey(true);
                            return false;
                        }
                        else
                        {
                            PrintMsgErro("Nome já existe.");
                        }
                    }
                    else
                    {
                        PrintMsgErro("Nome inválido.");
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarEspeciesSubMenuAddHabitat(List<Species> YSpecies, int Index)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Introduza o nome do habitat a adicionar:");
                String str3;
                UserInput(out str3);
                if (str3 == CancelCmd)
                {
                    return false;
                }
                else
                {
                    if (ToEHabitat(str3) != EHabitat.Null)
                    {
                        if (YSpecies[Index].GetHabitats().Contains(ToEHabitat(str3)))
                        {
                            PrintMsgErro("Habitat já existe.");
                        }
                        else
                        {
                            if (YSpecies[Index].AddHabitat(ToEHabitat(str3)))
                            {
                                Console.ForegroundColor = ConsoleColor.Magenta;
                                Console.WriteLine("\nHabitat adicionado com sucesso.");
                                Console.WriteLine(PressKey);
                                Console.ReadKey(true);
                                return false;
                            }
                            else
                            {
                                PrintMsgErro("Ocurreu um erro ao adicionar habitat.");
                            }
                        }
                    }
                    else
                    {
                        PrintMsgErro("Habitat não existe.");
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarEspeciesSubMenuRemHabitat(List<Species> YSpecies, List<Area> XAreas, int Index)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Introduza o nome do habitat a remover:");
                String str4;
                UserInput(out str4);
                if (str4 == CancelCmd)
                {
                    return false;
                }
                else
                {
                    if (ToEHabitat(str4) != EHabitat.Null)
                    {
                        if (!YSpecies[Index].GetHabitats().Contains(ToEHabitat(str4)))
                        {
                            PrintMsgErro("Habitat não existe.");
                        }
                        else
                        {
                            int cont = 0;

                            for (int i = 0; i < XAreas.Count; i++)
                            {
                                // Se existir uma área com o habitat a remover
                                // e se tiver animais nesse habitat
                                // impossível remover habitat
                                if ((ToEHabitat(XAreas[i].GetHabitat()) == ToEHabitat(str4)) && (XAreas[i].GetAnimals().Count != 0))
                                {
                                    for (int j = 0; j < XAreas[i].GetAnimals().Count; j++)
                                    {
                                        if (YSpecies[Index].GetNomeEspecie() == XAreas[i].GetAnimals()[j].GetEspecie())
                                        {
                                            cont++;
                                            break;
                                        }
                                    }
                                }
                            }

                            if (cont != 0)
                            {
                                PrintMsgErro("Impossível remover habitat pois há espécies nesse habitat dentro do zoo.");
                            }
                            else
                            {
                                if (YSpecies[Index].RemoverHabitat(ToEHabitat(str4)))
                                {
                                    Console.ForegroundColor = ConsoleColor.Magenta;
                                    Console.WriteLine("\nHabitat removido com sucesso.");
                                    Console.WriteLine(PressKey);
                                    Console.ReadKey(true);
                                    return false;
                                }
                                else
                                {
                                    PrintMsgErro("Ocurreu um erro ao remover habitat.");
                                }
                            }
                        }
                    }
                    else
                    {
                        PrintMsgErro("Habitat não existe.");
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }
        // fim Menu Espécie

        // Menu Animal DONE!
        private static bool MenuAnimal(List<Area> XAreas, List<Species> YSpecies, List<Animal> ZAnimal)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal", ConsoleColor.Green);
                Console.WriteLine(" Número de animais existentes: {0}\n", ZAnimal.Count);
                OpcoesMenuAnimal(ConsoleColor.Green);
                int op;
                String Str;
                UserInput(out Str);
                op = int.Parse(Str);

                switch (op)
                {
                    case 0:
                        return false;
                    case 1:
                        if (ZAnimal.Count > 0)
                        {
                            while (ListarAnimais(ZAnimal, XAreas)) { };
                        }
                        else
                        {
                            PrintMsgErro("Não existe nenhum animal para listar.");
                        }
                        break;
                    case 2:
                        while (CriarAnimais(ZAnimal, XAreas, YSpecies)) { };
                        break;
                    case 3:
                        if (ZAnimal.Count > 0)
                        {
                            while (EliminarAnimais(ZAnimal, XAreas)) { };
                        }
                        else
                        {
                            PrintMsgErro("Não existe nenhum animal para eliminar.");
                        }
                        break;
                    case 4:
                        if (ZAnimal.Count > 0)
                        {
                            while (EditarAnimais(ZAnimal, XAreas, YSpecies)) { };
                        }
                        else
                        {
                            PrintMsgErro("Não existe nenhum animal para editar.");
                        }
                        break;
                    case 5:
                        // Nascimento...
                        if (ZAnimal.Count > 0)
                        {
                            Random randomNascimento = new Random(DateTime.Now.GetHashCode());
                            Double probabilidadeNascimento = Math.Round(randomNascimento.NextDouble(), 3);
                            if (probabilidadeNascimento < 0.14)
                            {
                                while (NascimentoAnimais(ZAnimal, YSpecies, XAreas)) { };
                            }
                            else
                            {
                                PrintMsgErro("Não há nenhum nascimento a acontecer neste momento. Volte mais tarde.");
                            }
                        }
                        else
                        {
                            PrintMsgErro("Impossível assistir a um nascimento se não houver animais.");
                        }
                        break;
                    default:
                        PrintMsgErro("Opção não existe. Tente novamente.");
                        break;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool ListarAnimais(List<Animal> ZAnimal, List<Area> XAreas)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal: Listagem de animais", ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [0] - Voltar atrás\n");
                Console.WriteLine("\n [1] - Listar todos os animais do zoo\n [2] - Listagem por localização\n [3] - Listagem por habitat");

                String ListagemDe;
                UserInput(out ListagemDe);
                int op = int.Parse(ListagemDe);
                switch (op)
                {
                    case 0:
                        return false;
                    case 1:
                        while (ListagemTodosAnimais(ZAnimal)) { };
                        break;
                    case 2:
                        while (ListagemPorLocal(XAreas)) { };
                        break;
                    case 3:
                        while (ListagemPorHabitat(XAreas, ZAnimal)) { };
                        break;
                    default:
                        PrintMsgErro("Opção não existe.");
                        break;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool ListagemTodosAnimais(List<Animal> ZAnimal)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal: Listagem de animais: Listar tudo", ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [0] - Voltar atrás\n");
                for (int i = 0; i < ZAnimal.Count; i++)
                {
                    Console.WriteLine(" [{0}] - {1}", (i + 1), ZAnimal[i].GetNome());
                }
                Console.WriteLine("\n Selecione um animal para visualizar mais informação:");
                String str1;
                try
                {
                    UserInput(out str1);
                    int op1 = int.Parse(str1);
                    if (op1 == 0)
                    {
                        return false;
                    }
                    else if (op1 > 0 && op1 <= ZAnimal.Count)
                    {
                        Console.Clear();
                        MenuHeader("Menu Animal: Listagem de animais: " + ZAnimal[op1 - 1].GetNome(), ConsoleColor.Green);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(ZAnimal[op1 - 1].GetInfo());
                        Console.WriteLine(PressKey);
                        Console.ReadKey(true);
                    }
                    else
                    {
                        PrintMsgErro("Opção não existe.");
                    }
                }
                catch (Exception e)
                {
                    PrintMsgErro("Erro: " + e.Message);
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool ListagemPorLocal(List<Area> XAreas)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal: Listagem de animais: Listagem por localização", ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [0] - Voltar atrás\n");
                Console.WriteLine(" Visualize listagem de áreas na secção 'Gestão de áreas' para os números das áreas\n Selecione uma área para visualizar os animais da área: [1] ~ [{0}]", XAreas.Count);
                String str2;
                try
                {
                    UserInput(out str2);
                    int op2 = int.Parse(str2);
                    if (op2 == 0)
                    {
                        return false;
                    }
                    else if (op2 > 0 && op2 <= XAreas.Count)
                    {
                        if (XAreas[op2 - 1].GetAnimals().Count != 0)
                        {
                            while (ListagemPorLocal2(XAreas, (op2 - 1))) { };
                        }
                        else
                        {
                            PrintMsgErro("Área não contem animais.");
                        }
                    }
                    else
                    {
                        PrintMsgErro("Opção não existe.");
                    }
                }
                catch (Exception e)
                {
                    PrintMsgErro("Erro: " + e.Message);
                }

            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool ListagemPorLocal2(List<Area> XAreas, int Index)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal: Listagem de animais: Listagem por localização: " + XAreas[Index].GetNome(), ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [0] - Voltar atrás\n");
                for (int i = 0; i < XAreas[Index].GetAnimals().Count; i++)
                {
                    Console.WriteLine(" [{0}] - {1}", (i + 1), XAreas[Index].GetAnimals()[i].GetNome());
                }
                Console.WriteLine("\n Selecione um animal para visualizar mais informação:");
                String st;
                try
                {
                    UserInput(out st);
                    int opst = int.Parse(st);
                    if (opst == 0)
                    {
                        return false;
                    }
                    else if (opst > 0 && opst <= XAreas[Index].GetAnimals().Count)
                    {
                        Console.Clear();
                        MenuHeader("Menu Animal: Listagem de animais: " + XAreas[Index].GetAnimals()[opst - 1].GetNome(), ConsoleColor.Green);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine(XAreas[Index].GetAnimals()[opst - 1].GetInfo());
                        Console.WriteLine(PressKey);
                        Console.ReadKey(true);
                    }
                    else
                    {
                        PrintMsgErro("Opção não existe.");
                    }
                }
                catch (Exception e)
                {
                    PrintMsgErro("Erro: " + e.Message);
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool ListagemPorHabitat(List<Area> XAreas, List<Animal> ZAnimal)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal: Listagem de animais: Listagem por habitat", ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Use '" + CancelCmd + "' para cancelar e voltar atrás\n");
                Console.WriteLine(" Selecione um habitat para visualizar os animais desse tipo de habitat:");
                String strhab;
                UserInput(out strhab);
                if (strhab != CancelCmd)
                {
                    if (ToEHabitat(strhab) == EHabitat.Null)
                    {
                        PrintMsgErro("Habitat não existe");
                    }
                    else
                    {
                        while (ListagemPorHabitat2(XAreas, ZAnimal, strhab)) { };
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool ListagemPorHabitat2(List<Area> XAreas, List<Animal> ZAnimal, String strhab)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal: Listagem de animais: Listagem por habitat: " + strhab, ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [0] - Voltar atrás\n");
                int count = 0;
                List<String> listaTempAnimais = new List<String>();
                for (int i = 0; i < XAreas.Count; i++)
                {
                    if (XAreas[i].GetHabitat() == strhab)
                    {
                        for (int j = 0; j < XAreas[i].GetAnimals().Count; j++)
                        {
                            Console.WriteLine(" [{0}] - {1}", (count + 1), XAreas[i].GetAnimals()[j].GetNome());
                            listaTempAnimais.Add(XAreas[i].GetAnimals()[j].GetNome());
                            count++;
                        }
                    }
                }

                if (count == 0)
                {
                    Console.WriteLine(" Nenhum animal foi encontrado.");
                    Console.WriteLine(PressKey);
                    Console.ReadKey(true);
                    return false;
                }
                else
                {
                    try
                    {
                        Console.WriteLine("\n Selecione um animal para visualizar mais informação:");
                        String str3;
                        UserInput(out str3);
                        int str3int = int.Parse(str3);

                        if (str3int == 0)
                        {
                            return false;
                        }
                        else if (str3int > 0 && str3int <= count)
                        {
                            Console.Clear();
                            MenuHeader("Menu Animal: Listagem de animais: " + ZAnimal[Animal.ListaAnimais.IndexOf(listaTempAnimais[str3int - 1])], ConsoleColor.Green);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine(ZAnimal[Animal.ListaAnimais.IndexOf(listaTempAnimais[str3int - 1])].GetInfo());
                            Console.WriteLine(PressKey);
                            Console.ReadKey(true);
                        }
                        else
                        {
                            PrintMsgErro("Opção não existe.");
                        }
                    }
                    catch (Exception e)
                    {
                        PrintMsgErro("Erro: " + e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool CriarAnimais(List<Animal> ZAnimal, List<Area> XAreas, List<Species> YSpecies)
        {
            try
            {
                String strEsp, nomeAni, strArea, strPeso, strPais, strFilhos, numPais, numFilhos;
                String nomeEsp, nomeArea = "";
                Double pesoAni = 0;
                int iPais, iFilhos;
                List<String> Pais, Filhos;
                Pais = new List<String>();
                Filhos = new List<String>();

                Console.Clear();
                MenuHeader("Menu Animal: Criar animal", ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" Use '" + CancelCmd + "' a qualquer momento para cancelar.\n Introduza o número correspondente à espécie:\n");
                UserInput(out strEsp);
                strEsp = strEsp.Replace('_', ' ').Trim();
                if (strEsp != CancelCmd)
                {
                    int iEsp = int.Parse(strEsp);
                    if (iEsp > 0 && iEsp <= YSpecies.Count)
                    {
                        nomeEsp = Species.ListaEspecies[iEsp - 1];

                        bool NomeError = true;

                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n Introduza um nome para o animal:\n");
                            UserInput(out nomeAni);
                            nomeAni = nomeAni.Replace('_', ' ').Trim();
                            if (nomeAni != CancelCmd)
                            {
                                if (nomeAni != "")
                                {
                                    if (Animal.ListaAnimais.Contains(nomeAni))
                                    {
                                        PrintMsgErro("Nome já existe.");
                                    }
                                    else
                                    {
                                        NomeError = false;
                                    }
                                }
                                else
                                {
                                    PrintMsgErro("Nome não pode ser '_' ou vazio.");
                                }
                            }
                            else
                            {
                                return false;
                            }
                        } while (NomeError);

                        bool PesoError = true;

                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n Introduza o peso do animal:\n");
                            UserInput(out strPeso);
                            if (strPeso != CancelCmd)
                            {
                                try
                                {
                                    pesoAni = Double.Parse(strPeso);
                                    if (pesoAni <= 0)
                                    {
                                        PrintMsgErro("Peso não pode ser negativo.");
                                    }
                                    else
                                    {
                                        PesoError = false;
                                    }
                                }
                                catch (Exception e)
                                {
                                    PrintMsgErro("Erro: " + e.Message);
                                }
                            }
                            else
                            {
                                return false;
                            }
                        } while (PesoError);

                        bool AreaError = true;

                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n Introduza o número correspondente a uma área:\n");
                            UserInput(out strArea);
                            strArea = strArea.Replace('_', ' ').Trim();

                            if (strArea != CancelCmd)
                            {
                                try
                                {
                                    int iArea = int.Parse(strArea);
                                    if (iArea > 0 && iArea <= XAreas.Count)
                                    {
                                        if (YSpecies[iEsp - 1].GetHabitats().Contains(ToEHabitat(XAreas[iArea - 1].GetHabitat())))
                                        {
                                            nomeArea = Area.ListaAreas[iArea - 1];
                                            AreaError = false;
                                        }
                                        else
                                        {
                                            PrintMsgErro("Impossível adicionar esta espécie nesta área.");
                                        }
                                    }
                                    else
                                    {
                                        PrintMsgErro("Área não existe.");
                                    }
                                }
                                catch (Exception e)
                                {
                                    PrintMsgErro("Erro: " + e.Message);
                                }
                            }
                            else
                            {
                                return false;
                            }
                        } while (AreaError);

                        bool NumPaisError = true;

                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n Introduza o número de pais deste animal:\n");
                            UserInput(out numPais);
                            numPais = numPais.Replace('_', ' ').Trim();
                            if (numPais != CancelCmd)
                            {
                                try
                                {
                                    iPais = int.Parse(numPais);

                                    if (iPais < 0 || iPais > 2)
                                    {
                                        PrintMsgErro("Número de pais inválido.");
                                    }
                                    else
                                    {
                                        for (int i = 0; i < iPais;)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\n Introduza o número do {0}º pai deste animal:\n", (i + 1));
                                            UserInput(out strPais);
                                            strPais = strPais.Replace('_', ' ').Trim();
                                            if (strPais != CancelCmd)
                                            {
                                                try
                                                {
                                                    int indexAniPai = int.Parse(strPais);

                                                    if (indexAniPai > 0 && indexAniPai <= ZAnimal.Count)
                                                    {
                                                        if (ZAnimal[indexAniPai - 1].GetEspecie() == nomeEsp)
                                                        {
                                                            if (!Pais.Contains(ZAnimal[indexAniPai - 1].GetNome()))
                                                            {
                                                                Pais.Add(ZAnimal[indexAniPai - 1].GetNome());
                                                                i++;
                                                            }
                                                            else
                                                            {
                                                                PrintMsgErro("Animal já adicionado como pai.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            PrintMsgErro("Animais não são da mesma espécie.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        PrintMsgErro("Animal não existe.");
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    PrintMsgErro("Erro: " + e.Message);
                                                }
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }

                                        NumPaisError = false;
                                    }
                                }
                                catch (Exception e)
                                {
                                    PrintMsgErro("Ocurreu um erro: " + e.Message);
                                }
                            }
                            else
                            {
                                return false;
                            }
                        } while (NumPaisError);

                        bool NumFilhosError = true;

                        do
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\n Introduza o número de filhos deste animal:\n");
                            UserInput(out numFilhos);
                            numFilhos = numFilhos.Replace('_', ' ').Trim();
                            if (numFilhos != CancelCmd)
                            {
                                try
                                {
                                    iFilhos = int.Parse(numFilhos);

                                    if (iFilhos < 0)
                                    {
                                        PrintMsgErro("Número de filhos inválidos");
                                    }
                                    else
                                    {
                                        for (int i = 0; i < iFilhos;)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Green;
                                            Console.WriteLine("\n Introduza o número do {0}º filho deste animal:\n", (i + 1));
                                            UserInput(out strFilhos);
                                            strFilhos = strFilhos.Replace('_', ' ').Trim();
                                            if (strFilhos != CancelCmd)
                                            {
                                                try
                                                {
                                                    int indexAniFilho = int.Parse(strFilhos);

                                                    if (indexAniFilho > 0 && indexAniFilho <= ZAnimal.Count)
                                                    {
                                                        if (ZAnimal[indexAniFilho - 1].GetEspecie() == nomeEsp)
                                                        {
                                                            if (!Pais.Contains(ZAnimal[indexAniFilho - 1].GetNome()))
                                                            {
                                                                if (ZAnimal[indexAniFilho - 1].GetPais().Count < 2)
                                                                {
                                                                    if (!Filhos.Contains(ZAnimal[indexAniFilho - 1].GetNome()))
                                                                    {
                                                                        Filhos.Add(ZAnimal[indexAniFilho - 1].GetNome());
                                                                        i++;
                                                                    }
                                                                    else
                                                                    {
                                                                        PrintMsgErro("Animal já adicionado como filho.");
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    PrintMsgErro("Animal introduzido não pode ter mais pais.");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                PrintMsgErro("Animal não pode conter outro animal que seja pai e filho ao mesmo tempo.");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            PrintMsgErro("Animais não são da mesma espécie.");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        PrintMsgErro("Animal não existe.");
                                                    }
                                                }
                                                catch (Exception e)
                                                {
                                                    PrintMsgErro("Erro: " + e.Message);
                                                }
                                            }
                                            else
                                            {
                                                return false;
                                            }
                                        }

                                        NumFilhosError = false;
                                    }
                                }
                                catch (Exception e)
                                {
                                    PrintMsgErro("Ocureu um erro: " + e.Message);
                                }
                            }
                            else
                            {
                                return false;
                            }
                        } while (NumFilhosError);

                        ZAnimal.Add(new Animal(nomeEsp, nomeAni, pesoAni, nomeArea, new List<String>(Pais), new List<String>(Filhos)));
                        if (XAreas[Area.ListaAreas.IndexOf(ZAnimal[Animal.ListaAnimais.IndexOf(nomeAni)].GetLocal())].AddAnimal(ZAnimal[Animal.ListaAnimais.IndexOf(nomeAni)], YSpecies))
                        {
                            for (int i = 0; i < ZAnimal.Count; i++)
                            {
                                if (Pais.Contains(ZAnimal[i].GetNome()))
                                {
                                    ZAnimal[i].AddFilhos(new List<String> { nomeAni });
                                }
                                if (Filhos.Contains(ZAnimal[i].GetNome()))
                                {
                                    ZAnimal[i].AddPais(new List<String> { nomeAni });
                                }
                            }
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nAnimal criado com sucesso.");
                            Console.WriteLine(PressKey);
                            Console.ReadKey(true);
                        }
                        else
                        {
                            ZAnimal.Remove(ZAnimal[Animal.ListaAnimais.IndexOf(nomeAni)]);
                            Animal.ListaAnimais.Remove(nomeAni);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nAnimal não criado.\nOcurreu um erro.");
                            Console.WriteLine(PressKey);
                            Console.ReadKey(true);
                        }
                        return false;
                    }
                    else
                    {
                        PrintMsgErro("Espécie não existe.");
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool EliminarAnimais(List<Animal> ZAnimal, List<Area> XAreas)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal: Eliminar um animal", ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [0] - Cancelar\n");
                Console.WriteLine("\n Visualizar listagem de animais para os números de cada animal.\n Selecione um animal para eliminar: [1] ~ [{0}]", ZAnimal.Count);
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                if (op1 == 0)
                {
                    return false;
                }
                else if (op1 > 0 && op1 <= ZAnimal.Count)
                {
                    for (int i = 0; i < XAreas.Count; i++)
                    {
                        if (XAreas[i].GetAnimals().Contains(ZAnimal[op1 - 1]))
                        {
                            XAreas[i].RemoverAnimal(ZAnimal[op1 - 1]);
                            // Animal só pertence a uma área não vale a pena continuar no ciclo se encontrar esse animal...
                            break;
                        }
                    }

                    for (int i = 0; i < ZAnimal.Count; i++)
                    {
                        if (i != (op1 - 1))
                        {
                            if (ZAnimal[i].GetPais().Contains(ZAnimal[op1 - 1].GetNome()))
                            {
                                ZAnimal[i].RemovePai(ZAnimal[op1 - 1].GetNome());
                            }
                            if (ZAnimal[i].GetFilhos().Contains(ZAnimal[op1 - 1].GetNome()))
                            {
                                ZAnimal[i].RemoveFilho(ZAnimal[op1 - 1].GetNome());
                            }
                        }
                    }

                    for (int i = 0; i < ZAnimal[op1 - 1].GetPais().Count; i++)
                    {
                        ZAnimal[op1 - 1].RemovePai(ZAnimal[op1 - 1].GetPais()[i]);
                    }
                    for (int i = 0; i < ZAnimal[op1 - 1].GetFilhos().Count; i++)
                    {
                        ZAnimal[op1 - 1].RemoveFilho(ZAnimal[op1 - 1].GetFilhos()[i]);
                    }

                    Animal.ListaAnimais.Remove(ZAnimal[op1 - 1].GetNome());
                    ZAnimal.RemoveAt(op1 - 1);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nAnimal removido com sucesso.");
                    Console.WriteLine(PressKey);
                    Console.ReadKey(true);
                    return false;
                }
                else
                {
                    PrintMsgErro("Animal não existe.");
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAnimais(List<Animal> ZAnimal, List<Area> XAreas, List<Species> YSpecies)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal: Editar um animal", ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [0] - Cancelar");
                Console.WriteLine("\n Visualizar listagem de animais para os números de cada animal.\n Selecione um animal para editar informação: [1] ~ [{0}]", ZAnimal.Count);
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                if (op1 == 0)
                {
                    return false;
                }
                else if (op1 > 0 && op1 <= ZAnimal.Count)
                {
                    while (EditarAnimaisSubMenu(ZAnimal, XAreas, YSpecies, (op1 - 1))) { };
                }
                else
                {
                    PrintMsgErro("Opção não existe.");
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAnimaisSubMenu(List<Animal> ZAnimal, List<Area> XArea, List<Species> YSpecies, int Index)
        {
            try
            {
                Console.Clear();
                MenuHeader("Menu Animal: Editar um animal: " + ZAnimal[Index].GetNome(), ConsoleColor.Green);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(" [0] - Cancelar\n");
                Console.WriteLine("\n [1] - Editar nome\n [2] - Mover animal");
                String str1;
                UserInput(out str1);
                int op1 = int.Parse(str1);
                switch (op1)
                {
                    case 0:
                        return false;
                    case 1:
                        while (EditarAnimaisSubMenuNome(ZAnimal, Index)) { };
                        break;
                    case 2:
                        while (EditarAnimaisSubMenuMoverAnimal(ZAnimal, XArea, YSpecies, Index)) { };
                        break;
                    default:
                        PrintMsgErro("Opção não existe.");
                        break;
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAnimaisSubMenuNome(List<Animal> ZAnimal, int Index)
        {
            try
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Introduza um novo nome:");
                String str2;
                UserInput(out str2);
                str2 = str2.Replace('_', ' ').Trim();
                if (str2 == CancelCmd)
                {
                    return false;
                }
                else
                {
                    if (str2 != "")
                    {
                        if (Animal.ListaAnimais.Contains(str2))
                        {
                            PrintMsgErro("Nome já existe.");
                        }
                        else
                        {
                            for (int i = 0; i < ZAnimal.Count; i++)
                            {
                                if (i != Index)
                                {
                                    if (ZAnimal[i].GetFilhos().Contains(ZAnimal[Index].GetNome()))
                                    {
                                        ZAnimal[i].RemoveFilho(ZAnimal[Index].GetNome());
                                        ZAnimal[i].AddFilhos(new List<String> { str2 });
                                    }
                                    if (ZAnimal[i].GetPais().Contains(ZAnimal[Index].GetNome()))
                                    {
                                        ZAnimal[i].RemovePai(ZAnimal[Index].GetNome());
                                        ZAnimal[i].AddPais(new List<String> { str2 });
                                    }
                                }
                            }
                            Animal.ListaAnimais[Animal.ListaAnimais.IndexOf(ZAnimal[Index].GetNome())] = str2;
                            ZAnimal[Index].SetNome(str2);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nNome editado com sucesso.");
                            Console.WriteLine(PressKey);
                            Console.ReadKey(true);
                            return false;
                        }
                    }
                    else
                    {
                        PrintMsgErro("Nome inválido.");
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool EditarAnimaisSubMenuMoverAnimal(List<Animal> ZAnimal, List<Area> XArea, List<Species> YSpecies, int Index)
        {
            try
            {
                // Mover animal para outro local adjacente desde que seja do habitat da espécie
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n Use '" + CancelCmd + "' para cancelar a operação.\n Introduza uma área:\n");
                int contagem = 0;
                List<String> listaTempAreas = new List<String>();
                for (int i = 0; i < XArea[Area.ListaAreas.IndexOf(ZAnimal[Index].GetLocal())].GetCpFronteiras().Count; i++)
                {
                    String nomeArea = XArea[Area.ListaAreas.IndexOf(ZAnimal[Index].GetLocal())].GetCpFronteiras()[i];
                    // Não se importem com o comboio =D
                    if (YSpecies[Species.ListaEspecies.IndexOf(ZAnimal[Index].GetEspecie())].GetHabitats().Contains(ToEHabitat(XArea[Area.ListaAreas.IndexOf(nomeArea)].GetHabitat())))
                    {
                        Console.WriteLine(" [{0}] - {1}", (contagem + 1), nomeArea);
                        listaTempAreas.Add(nomeArea);
                        contagem++;
                    }
                }
                if (contagem == 0)
                {
                    PrintMsgErro("Não é possível mover este animal para nenhuma área adjacente.");
                    return false;
                }
                else
                {
                    String str3;
                    UserInput(out str3);
                    str3 = str3.Replace('_', ' ').Trim();
                    if (str3 == CancelCmd)
                    {
                        return false;
                    }
                    else
                    {
                        int str3int = int.Parse(str3);

                        if (str3int > 0 && str3int <= contagem)
                        {
                            String localBackup = ZAnimal[Index].GetLocal();
                            XArea[Area.ListaAreas.IndexOf(localBackup)].RemoverAnimal(ZAnimal[Index]);
                            if (XArea[Area.ListaAreas.IndexOf(listaTempAreas[str3int - 1])].AddAnimal(ZAnimal[Index], YSpecies))
                            {
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\nAnimal movido com sucesso.");
                                Console.WriteLine(PressKey);
                                Console.ReadKey(true);
                            }
                            else
                            {
                                // Caso o animal não seja movido devido a alguma imcompatibilidade...
                                // Como o peso excedido
                                XArea[Area.ListaAreas.IndexOf(localBackup)].AddAnimal(ZAnimal[Index], YSpecies);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("\n Ocurreu um erro...\n Animal não movido.");
                                Console.WriteLine(PressKey);
                                Console.ReadKey(true);
                            }
                            return false;
                        }
                        else
                        {
                            PrintMsgErro("Opção não existe.");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                PrintMsgErro("Ocurreu um erro: " + e.Message);
            }

            return true;
        }

        private static bool NascimentoAnimais(List<Animal> ZAnimal, List<Species> YSpecies, List<Area> XAreas)
        {
            try
            {
                Random randomGen = new Random(DateTime.Now.GetHashCode());
                int numPais = randomGen.Next(1, 3);
                String numPaisStr = "";
                String nomeDoPaiPrincipal = ZAnimal[randomGen.Next(0, ZAnimal.Count)].GetNome();
                String nomeDoPaiSecundario = "";
                String nomeDoFilho;
                int contador = 0;
                if (numPais == 2)
                {
                    // Assegura-se que existem animais da mesma espécie sem ser o mesmo caso haja 2 pais
                    // e que ambos estão na mesma área
                    // se não número de pais passa a ser 1
                    for (int i = 0; i < ZAnimal.Count; i++)
                    {
                        if (Animal.ListaAnimais.IndexOf(nomeDoPaiPrincipal) != i)
                        {
                            if (ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiPrincipal)].GetEspecie() == ZAnimal[i].GetEspecie())
                            {
                                if (ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiPrincipal)].GetLocal() == ZAnimal[i].GetLocal())
                                {
                                    contador++;
                                }
                            }
                        }
                    }
                    if (contador > 0)
                    {
                        do
                        {
                            nomeDoPaiSecundario = ZAnimal[randomGen.Next(0, ZAnimal.Count)].GetNome();
                        } while ((nomeDoPaiPrincipal == nomeDoPaiSecundario) || (ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiPrincipal)].GetLocal() != ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiSecundario)].GetLocal()) || (ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiPrincipal)].GetEspecie() != ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiSecundario)].GetEspecie()));
                    }
                    else
                    {
                        numPais = 1;
                    }
                }
                if (numPais == 1)
                    numPaisStr = " pai";
                else
                    numPaisStr = " pais";

                bool validaNome = true;

                do
                {
                    Console.Clear();
                    MenuHeader("Menu Animal: Nascimento de um novo animal: " + numPais + numPaisStr, ConsoleColor.Cyan);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(" Está a acontecer um nascimento! Que momento mágnifico...\n\n Pai [1]: " + nomeDoPaiPrincipal);
                    if (numPais == 2)
                    {
                        Console.WriteLine(" Pai [2]: " + nomeDoPaiSecundario);
                    }

                    Console.WriteLine("\n Introduza um nome para o filho:");
                    UserInput(out nomeDoFilho);
                    nomeDoFilho = nomeDoFilho.Replace('_', ' ').Trim();
                    if (nomeDoFilho == "")
                    {
                        PrintMsgErro("Introduza um nome válido.");
                    }
                    else if (Animal.ListaAnimais.Contains(nomeDoFilho))
                    {
                        PrintMsgErro("Nome já existe.");
                    }
                    else
                    {
                        validaNome = false;
                    }
                } while (validaNome);

                Double pesoDoFilho = (ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiPrincipal)].GetPeso() * 0.2);
                List<String> pais = new List<String> { nomeDoPaiPrincipal };
                if (numPais == 2)
                {
                    pesoDoFilho += (ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiSecundario)].GetPeso() * 0.2);
                    pais.Add(nomeDoPaiSecundario);
                }

                // Adicionar animal
                ZAnimal.Add(new Animal(ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiPrincipal)].GetEspecie(), nomeDoFilho, pesoDoFilho, ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiPrincipal)].GetLocal(), pais, new List<String>()));

                // Adicionar animal ao local antes de adicionar este filho aos pais para dar menos trabalho depois
                if (XAreas[Area.ListaAreas.IndexOf(ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoPaiPrincipal)].GetLocal())].AddAnimal(ZAnimal[Animal.ListaAnimais.IndexOf(nomeDoFilho)], YSpecies))
                {
                    // Adicionar filho aos pais
                    for (int i = 0; i < numPais; i++)
                    {
                        ZAnimal[Animal.ListaAnimais.IndexOf(pais[i])].AddFilhos(new List<String> { nomeDoFilho });
                    }

                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\nNascimento efetuado com sucesso.");
                    Console.WriteLine(PressKey);
                    Console.ReadKey(true);
                }
                else
                {
                    ZAnimal.RemoveAt(Animal.ListaAnimais.IndexOf(nomeDoFilho));
                    Animal.ListaAnimais.Remove(nomeDoFilho);
                    PrintMsgErro("Infelizmente teremos de tirar o animal do zoo. Pois a área não consegue aguentar com mais um animal...");
                }
                return false;
            }
            catch (Exception e)
            {
                PrintMsgErro("Erro: " + e.Message);
            }

            return true;
        }
        // fim Menu Animal

        // Opções...
        private static void OpcoesMenuPrincipal(ConsoleColor Color, List<Area> XAreas, List<Species> YSpecies, List<Animal> ZAnimal)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(" [0] - Sair");
            Console.WriteLine("\n [1] - Gestão das áreas ({0} áreas)\n [2] - Gestão das espécies ({1} espécies)\n [3] - Gestão dos animais ({2} animais)\n [4] - Listagem de habitats disponíveis", XAreas.Count, YSpecies.Count, ZAnimal.Count);
        }

        private static void OpcoesMenuArea(ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(" [0] - Voltar atrás");
            Console.WriteLine("\n [1] - Listar áreas\n [2] - Criar área\n [3] - Eliminar área\n [4] - Editar área");
        }

        private static void OpcoesMenuEspecie(ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(" [0] - Voltar atrás");
            Console.WriteLine("\n [1] - Listar espécies\n [2] - Criar espécie\n [3] - Eliminar espécie\n [4] - Editar espécie");
        }

        private static void OpcoesMenuAnimal(ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            Console.WriteLine(" [0] - Voltar atrás");
            Console.WriteLine("\n [1] - Listar animais\n [2] - Criar animais\n [3] - Eliminar animais\n [4] - Editar animais\n [5] - Assistir a um nascimento");
        }
        // fim Menu

        // Menu Style
        private static void MenuHeader(String XStr, ConsoleColor Color)
        {
            Random randomNum = new Random(DateTime.Now.GetHashCode());
            PrintFullWidthScreen('#');
            PrintFullWidthScreen('#');
            PrintHalfWidthScreenWithString('#', "ZooPark", (ConsoleColor)randomNum.Next(1, 16), Color);
            PrintHalfWidthScreenWithString('#', XStr, ConsoleColor.White, Color);
            PrintFullWidthScreen('#');
            PrintFullWidthScreen('#');
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void PrintFullWidthScreen(Char XCh, ConsoleColor Color)
        {
            Console.ForegroundColor = Color;
            for (int i = 0; i < (Console.WindowWidth - 1); i++)
                Console.Write(XCh);
            Console.WriteLine();
        }

        private static void PrintFullWidthScreen(Char XCh)
        {
            PrintFullWidthScreen(XCh, ConsoleColor.White);
        }

        private static void PrintHalfWidthScreenWithString(Char XCh, String YStr, ConsoleColor StrColor, ConsoleColor ZColor1)
        {
            Console.ForegroundColor = ZColor1;
            for (int i = 0; i < (((Console.WindowWidth - 1) / 2) - ((YStr.Length + 2) / 2)); i++)
            {
                Console.Write(XCh);
            }
            Console.ForegroundColor = StrColor;
            Console.Write(" " + YStr + " ");
            Console.ForegroundColor = ZColor1;
            for (int i = 0; i < (((Console.WindowWidth - 1) / 2) - ((YStr.Length + 2) / 2)); i++)
            {
                Console.Write(XCh);
            }
            if (YStr.Length % 2 == 0)
            {
                Console.Write(XCh);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        private static void PrintHalfWidthScreenWithString(Char XCh, String YStr, ConsoleColor StrColor)
        {
            PrintHalfWidthScreenWithString(XCh, YStr, StrColor, ConsoleColor.White);
        }

        private static void PrintHalfWidthScreenWithString(Char XCh, String YStr)
        {
            PrintHalfWidthScreenWithString(XCh, YStr, ConsoleColor.White);
        }

        private static void PrintMsgErro(String Str)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("\n" + Str + PressKey);
            Console.ReadKey(true);
            Console.ResetColor();
        }

        private static void UserInput(out String XStr)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\n > ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.CursorVisible = true;
            XStr = Console.ReadLine();
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.White;
        }
        // fim Menu Style

        // Save and Load functions
        // Guarda informação das áreas para um ficheiro
        private static void SaveAreasIntoFile(List<Area> Area)
        {
            FileStream file = File.Create(FichAreas);
            StreamWriter writer = new StreamWriter(file);

            for (int i = 0; i < Area.Count; i++)
                writer.Write(Area[i].InfoToFile());

            writer.Dispose();
            file.Dispose();
            writer.Close();
            file.Close();
        }

        // Carrega informação das áreas a partir de um ficheiro
        private static void LoadAreasFromFile(List<Area> XArea)
        {
            FileStream file = File.OpenRead(FichAreas);
            StreamReader reader = new StreamReader(file);

            while (!reader.EndOfStream)
            {
                String info = reader.ReadLine();
                String[] infoSplit = info.Split(' ');
                // 0 -> Nome da área
                // 1 -> Habitat
                // 2 -> Capacidade máxima
                // 3 -> Número de áreas adjacentes
                // 4+ -> Áreas adjacentes
                try
                {
                    String nomeArea = infoSplit[0].Replace('_', ' ').Trim();
                    if (nomeArea != "")
                    {
                        if (!Area.ListaAreas.Contains(nomeArea))
                        {
                            EHabitat habitat = ToEHabitat(infoSplit[1].Replace('_', ' ').Trim());
                            if (habitat != EHabitat.Null)
                            {
                                Double capM = Double.Parse(infoSplit[2]);
                                if (capM > 0)
                                {
                                    int numAreasAdj = int.Parse(infoSplit[3]);
                                    if (numAreasAdj >= 0 && numAreasAdj <= 3)
                                    {
                                        List<String> fronteiras = new List<String>();

                                        for (int i = 0; i < numAreasAdj && i < (infoSplit.Length - 4); i++)
                                        {
                                            String areaAdj = infoSplit[i + 4].Replace('_', ' ').Trim();
                                            if (areaAdj != "")
                                            {
                                                if (!fronteiras.Contains(areaAdj))
                                                {
                                                    fronteiras.Add(areaAdj);
                                                }
                                            }
                                        }

                                        XArea.Add(new Area(nomeArea, habitat, capM, new List<String>(fronteiras)));
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    // Ignora erros... Não cria Área se occurer um erro
                }
            }

            reader.Dispose();
            file.Dispose();
            reader.Close();
            file.Close();
        }

        private static void VerificaListaAreas(List<Area> X)
        {
            // Remove fronteiras que não existam no zoo
            List<int> indexos;
            for (int i = 0; i < X.Count; i++)
            {
                indexos = new List<int>();
                for (int j = 0; j < X[i].GetCpFronteiras().Count; j++)
                {
                    if (!Area.ListaAreas.Contains(X[i].GetCpFronteiras()[j]))
                    {
                        if (!indexos.Contains(j))
                            indexos.Add(j);
                    }
                }
                indexos.Sort();
                for (int k = 0; k < indexos.Count; k++)
                {
                    X[i].GetFronteiras().Remove(X[i].GetCpFronteiras()[indexos[k] - k]);
                    X[i].SetNumFronteiras(X[i].GetNumFronteiras() - 1);
                }
            }

            // Remove fronteiras de áreas cujo a fronteira não tenha área como fronteira...
            for (int i = 0; i < X.Count; i++)
            {
                for (int j = 0; j < X.Count; j++)
                {
                    if (i != j)
                    {
                        if (X[i].GetCpFronteiras().Contains(X[j].GetNome()))
                        {
                            if (!X[j].GetCpFronteiras().Contains(X[i].GetNome()))
                            {
                                X[i].GetFronteiras().Remove(X[j].GetNome());
                                X[i].SetNumFronteiras(X[i].GetNumFronteiras() - 1);
                            }
                        }
                    }
                }
            }
        }

        // Guarda informação das espécies para um ficheiro
        private static void SaveSpeciesIntoFile(List<Species> Species)
        {
            FileStream file = File.Create(FichEspecies);
            StreamWriter writer = new StreamWriter(file);

            for (int i = 0; i < Species.Count; i++)
                writer.Write(Species[i].InfoToFile());

            writer.Dispose();
            file.Dispose();
            writer.Close();
            file.Close();
        }

        private static void SerializeSpeciesIntoFile(List<Species> Specie)
        {
            Stream file = File.Create(FichEspecies + ".dat");
            FileStream file2 = File.Create(FichEspecies + "static.dat");
            BinaryFormatter writer = new BinaryFormatter();
            StreamWriter writer2 = new StreamWriter(file2);

            for (int i = 0; i < Specie.Count; i++)
                writer.Serialize(file, Specie[i]);

            for (int i = 0; i < Species.ListaEspecies.Count; i++)
                writer2.WriteLine(Species.ListaEspecies[i].Replace(' ', '_'));

            writer2.Dispose();
            file2.Dispose();
            writer2.Close();
            file2.Close();
            file.Dispose();
            file.Close();
        }

        // Carrega informação das espécies a partir de um ficheiro
        private static void LoadSpeciesFromFile(List<Species> Species, List<int> X)
        {
            FileStream file = File.OpenRead(FichEspecies);
            StreamReader reader = new StreamReader(file);
            while (!reader.EndOfStream)
            {
                String info = reader.ReadLine();
                String[] infoSplit = info.Split(' ');
                List<EHabitat> TempList = new List<EHabitat>();

                for (int i = 1; i < infoSplit.Length; i++)
                {
                    EHabitat novoHabitat = ToEHabitat(infoSplit[i]);
                    if (novoHabitat != EHabitat.Null)
                        if (!TempList.Contains(novoHabitat))
                            TempList.Add(novoHabitat);
                }

                try
                {
                    // Se a espécie não tiver pelo menos um habitat não cria a espécie
                    if (TempList.Count > 0)
                    {
                        if (infoSplit[0].Replace('_', ' ').Trim() != "")
                        {
                            Species.Add(new Species(infoSplit[0], new List<EHabitat>(TempList)));
                            X.Add(0);
                        }
                    }
                    TempList = null;
                }
                catch (Exception e)
                {
                    // Ignora erros... Não cria Espécie se occurer um erro
                }
            }

            reader.Dispose();
            file.Dispose();
            reader.Close();
            file.Close();
        }

        private static void DeserializeSpeciesFromFile(List<Species> Specie)
        {
            Stream file = File.OpenRead(FichEspecies + ".dat");
            Stream file2 = File.OpenRead(FichEspecies + "static.dat");
            BinaryFormatter reader = new BinaryFormatter();
            StreamReader reader2 = new StreamReader(file2);

            while (file.Position != file.Length)
                Specie.Add((Species)reader.Deserialize(file));

            while(!reader2.EndOfStream)
                Species.ListaEspecies.Add(reader2.ReadLine().Replace('_', ' ').Trim());

            reader2.Dispose();
            file2.Dispose();
            reader2.Close();
            file2.Close();
            file.Dispose();
            file.Close();
        }

        // Guarda informação dos animais para um ficheiro
        private static void SaveAnimalsIntoFile(List<Animal> Animals)
        {
            FileStream file = File.Create(FichAnimais);
            StreamWriter writer = new StreamWriter(file);

            for (int i = 0; i < Animals.Count; i++)
                writer.Write(Animals[i].InfoToFile());

            writer.Dispose();
            file.Dispose();
            writer.Close();
            file.Close();
        }

        // Carrega informação dos animais a partir de um ficheiro
        private static void LoadAnimalsFromFile(List<Animal> Animals, List<Area> YAreas, List<Species> ZSpecies)
        {
            FileStream file = File.OpenRead(FichAnimais);
            StreamReader reader = new StreamReader(file);

            while (!reader.EndOfStream)
            {
                String info = reader.ReadLine();
                String[] infoSplit = info.Split(' ');
                try
                {
                    // 0 -> Especie
                    // 1 -> Nome
                    // 2 -> Peso
                    // 3 -> Localização
                    // 4 -> Num_Filhos
                    // 5 + j -> Filhos
                    // 5 + j + k -> Pais
                    List<String> filhotes = new List<String>();
                    List<String> pais = new List<String>();

                    int j = 0;

                    for (; j < int.Parse(infoSplit[4]); j++)
                    {
                        filhotes.Add(infoSplit[5 + j]);
                    }

                    for (int k = 0; k < (infoSplit.Length - (5 + j)); k++)
                    {
                        pais.Add(infoSplit[(5 + j) + k]);
                    }

                    Double peso = Double.Parse(infoSplit[2]);

                    // Mais limpo que LoadSpeciesFromFile... mas não vou mudar... T_T
                    if (infoSplit[0].Replace('_', ' ').Trim() != "" && infoSplit[1].Replace('_', ' ').Trim() != "")
                        Animals.Add(new Animal(infoSplit[0], infoSplit[1], peso, infoSplit[3], new List<String>(pais), new List<String>(filhotes)));

                    // Adiciona Animal para a area indicada no infoSplit[3]
                    // Se capacidade exceder não adiciona animal à area e elemina animal
                    // Porque um animal só pode estar no zoo se tiver area onde pode residir
                    if (YAreas[Area.ListaAreas.IndexOf(infoSplit[3].Trim().Replace('_', ' '))].AddAnimal(Animals[Animal.ListaAnimais.IndexOf(infoSplit[1].Trim().Replace('_', ' '))], ZSpecies)) { }
                    else
                    {
                        Animals.Remove(Animals[Animal.ListaAnimais.IndexOf(infoSplit[1].Trim().Replace('_', ' '))]);
                        Animal.ListaAnimais.Remove(infoSplit[1]);
                    }
                }
                catch (Exception e)
                {
                    // Erro...
                }
            }

            reader.Dispose();
            file.Dispose();
            reader.Close();
            file.Close();
        }

        private static void VerificaListaAnimal(List<Animal> X)
        {
            List<int> indexos;
            // remove filhos se não existirem no zoo
            for (int i = 0; i < X.Count; i++)
            {
                indexos = new List<int>();
                for (int j = 0; j < X[i].GetFilhos().Count; j++)
                {
                    if (!Animal.ListaAnimais.Contains(X[i].GetFilhos()[j]))
                    {
                        if (!indexos.Contains(j))
                            indexos.Add(j);
                    }
                }
                indexos.Sort();
                for (int k = 0; k < indexos.Count; k++)
                    X[i].RemoveFilho(X[i].GetFilhos()[indexos[k] - k]);
            }

            // remove pais se não existirem no zoo
            for (int i = 0; i < X.Count; i++)
            {
                indexos = new List<int>();
                for (int j = 0; j < X[i].GetPais().Count; j++)
                {
                    if (!Animal.ListaAnimais.Contains(X[i].GetPais()[j]))
                    {
                        if (!indexos.Contains(j))
                            indexos.Add(j);
                    }
                }
                indexos.Sort();
                for (int k = 0; k < indexos.Count; k++)
                    X[i].RemovePai(X[i].GetPais()[indexos[k] - k]);
            }

            // Se animal conter a si próprio como filho ou pai remove pai ou filho respetivamente...
            for (int i = 0; i < X.Count; i++)
            {
                if (X[i].GetPais().Contains(X[i].GetNome()))
                {
                    X[i].RemovePai(X[i].GetNome());
                }
            }
            for (int i = 0; i < X.Count; i++)
            {
                if (X[i].GetFilhos().Contains(X[i].GetNome()))
                {
                    X[i].RemoveFilho(X[i].GetNome());
                }
            }

            // remove pais de um filho que o pai não o tem como filho
            for (int i = 0; i < X.Count; i++)
            {
                for (int j = 0; j < X.Count; j++)
                {
                    if (i != j)
                    {
                        if (X[i].GetPais().Contains(X[j].GetNome())) // Se o pai de X[i] for X[j] então X[j] deve conter X[i] como filho
                        {
                            if (X[j].GetFilhos().Contains(X[i].GetNome()))
                            {
                                // Se sim sai do loop...
                                break;
                            }
                            // Se X[j] não tiver X[i] como filho então X[i] deve remover X[j] como pai
                            X[i].RemovePai(X[j].GetNome());
                        }
                    }
                }
            }

            // remove filhos de um pai que o filho não o tem como pai
            for (int i = 0; i < X.Count; i++)
            {
                for (int j = 0; j < X.Count; j++)
                {
                    if (i != j)
                    {
                        if (X[i].GetFilhos().Contains(X[j].GetNome())) // Se X[i] conter X[j] como filho então X[j] deve conter X[i] como pai
                        {
                            if (X[j].GetPais().Contains(X[i].GetNome()))
                            {
                                // Se sim sai do loop...
                                break;
                            }
                            // Se X[j] não tiver X[i] como pai então X[i] deve remover X[j] como filho
                            X[i].RemoveFilho(X[j].GetNome());
                        }
                    }
                }
            }

            // Remove familia que não seja da mesma espécie
            for (int i = 0; i < X.Count; i++)
            {
                for (int j = 0; j < X.Count; j++)
                {
                    if (i != j)
                    {
                        if (X[i].GetPais().Contains(X[j].GetNome()))
                        {
                            if (X[i].GetEspecie() != X[j].GetEspecie())
                            {
                                X[i].RemovePai(X[j].GetNome());
                                X[j].RemoveFilho(X[i].GetNome());
                            }
                        }

                        if (X[i].GetFilhos().Contains(X[j].GetNome()))
                        {
                            if (X[i].GetEspecie() != X[j].GetEspecie())
                            {
                                X[i].RemoveFilho(X[j].GetNome());
                                X[j].RemovePai(X[i].GetNome());
                            }
                        }
                    }
                }
            }
        }
        // fim Save and Load functions

        // Converte String para EHabitat devolve EHabitat.Null se não conhecer a String
        public static EHabitat ToEHabitat(String str)
        {
            switch (str)
            {
                case "Deserto":
                    return EHabitat.Deserto;
                case "Savana":
                    return EHabitat.Savana;
                case "Oceano":
                    return EHabitat.Oceano;
                case "Floresta":
                    return EHabitat.Floresta;
                case "Floresta Tropical":
                case "Floresta_Tropical":
                    return EHabitat.Floresta_Tropical;
                case "Tundra":
                    return EHabitat.Tundra;
                default:
                    return (EHabitat)(-1);
            }
        }
    }
}


// Testes realizados (Colocar no main)

//int temp = 0; // ID_Animal
//for (int i = 0; i < listaSpecies.Count; i++)
//{
//    if (listaSpecies[i].GetNomeEspecie() == "Elefante")
//        temp = ++listaSpecies_ID[i];
//}
//Console.WriteLine("{0} -> {1}", 1, temp);

//for (int i = 0; i < listaAreas.Count; i++)
//    Console.WriteLine(Area.ListaAreas[i]);
//for (int i = 0; i < listaSpecies.Count; i++)
//    Console.WriteLine(Species.ListaEspecies[i]);
//for (int i = 0; i < listaAnimals.Count; i++)
//    Console.WriteLine(Animal.ListaAnimais[i]);

//try
//{
//    listaAnimals.Add(new Animal("Camelo", "Nome_1", 200.2, "Deserto [1]", new List<String> { "Olá", "Olé", "Hah" }, new List<String>()));
//    listaAnimals.Add(new Animal("Camelo", "Nome_2", 129.21, "Deserto [1]", new List<String> { "Olá", "Olé" }, new List<String> { "Hah" }));
//    listaAnimals.Add(new Animal("Elefante", "Nome_3", 1000.291, "Savana [1]", new List<String> { "Hah" }, new List<String>()));
//}
//catch (Exception e)
//{
//    Console.WriteLine("Erro: " + e.Message);
//}
//for (int i = 0; i < listaAnimals.Count; i++)
//{
//    Console.WriteLine(listaAnimals[i].GetInfo());
//    Console.WriteLine(listaAnimals[i].GetID_Animal());
//}

//for (int i = 0; i < listaAreas.Count; i++)
//    Console.WriteLine(listaAreas[i].GetInfo());


//for (int i = 0; i < listaSpecies.Count; i++)
//    Console.WriteLine(listaSpecies[i].GetInfo());


// Fim "Testes realizados (Colocar no main)"