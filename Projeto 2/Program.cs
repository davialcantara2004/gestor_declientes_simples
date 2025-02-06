using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.InteropServices.ComTypes;

namespace Projeto_2
{
     class Program
     {
        [System.Serializable]

        struct Cliente
        {
            public string nome;
            public string email;
            public string cpf;
        }

        static List<Cliente> clientes = new List<Cliente>();


        enum Menu { Listagem = 1, Adicionar, Remover, Sair};


        static void Main(string[] args)
        {
            Carregar();

            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("=====ESCOLHA UMA OPÇÃO=====");
                Console.WriteLine("1-Listagem\n2-Adicionar\n3-Remover\n4-Sair");
                int intOp = int.Parse(Console.ReadLine());
                Menu opcao = (Menu)intOp;

                switch (opcao)
                {
                    case Menu.Listagem:
                        Listagem();
                        break;
                    case Menu.Adicionar:
                        Adicionar();
                        break;
                    case Menu.Remover:
                        Remover();
                        break;
                    case Menu.Sair:
                        sair = true;
                        break;
                }
                Console.Clear();
            }

        }


        static void Adicionar()
        {
            Cliente cliente = new Cliente();
            Console.WriteLine("Nome do Cliente:");
            cliente.nome = Console.ReadLine();
            Console.WriteLine("E-mail do Cliente:");
            cliente.email= Console.ReadLine();
            Console.WriteLine("CPF do Cliente:");
            cliente.cpf = Console.ReadLine();


            clientes.Add(cliente);
            Gravar();

            Console.WriteLine("Cliente Cadastrado com Sucesso!");
            Console.WriteLine("Aperte ENTER para sair");
            Console.ReadLine();
        }

        static void Listagem()
        {
            Console.WriteLine("Lista de Clientes");
            int i = 0;

            if(clientes.Count > 0)
            {
                foreach (Cliente cliente in clientes)
                {
                    Console.WriteLine($"ID [{i}]");
                    Console.WriteLine($"Nome: {cliente.nome}");
                    Console.WriteLine($"E-mail: {cliente.email}");
                    Console.WriteLine($"CPF: {cliente.cpf}");
                    Console.WriteLine();
                    Console.WriteLine();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Nenhum cliente Cadastrado em nosso Sistema!");
            }

           

            
            Console.WriteLine("Aperte ENTER para sair!");
            Console.ReadLine();
                
        }
        
        static void Gravar()
        {
            FileStream stream = new FileStream("clientes.clients", FileMode.OpenOrCreate);
            BinaryFormatter encoder = new BinaryFormatter();

            encoder.Serialize(stream, clientes);

            stream.Close();
        }
            
        static void Carregar()
        {
            FileStream stream = new FileStream("clientes.clients", FileMode.OpenOrCreate);
            try
            {
               
                BinaryFormatter encoder = new BinaryFormatter();

                clientes = (List<Cliente>)encoder.Deserialize(stream);
                
                if(clientes == null)
                {
                    clientes = new List<Cliente>();
                }
              
            }
            catch(Exception) 
            {
                clientes = new List<Cliente>();
            }
            stream.Close();
        }

        static void Remover()
        {
            Console.WriteLine("Selecione o ID do cliente que deseja remover");
            int id = int.Parse(Console.ReadLine()); 

            if(id >= 0 && id < clientes.Count)
            {
                clientes.RemoveAt(id);
                Gravar();
                Console.WriteLine("Cliente removido!");
            }
            else
            {
                Console.WriteLine("Número digitado é invalido");
                Console.ReadLine();
            }



        }

        }

 }

