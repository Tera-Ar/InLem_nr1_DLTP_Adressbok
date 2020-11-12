using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InLem_nr1_DLTP_Adressbok
{
    class Program
    {
        class PersonAdressbook
        {
            public string name;
            public string adress;
            public string telephone;
            public string email;
            public PersonAdressbook(string N, string A, string T, string Em)
            {
                name = N; adress = A; telephone = T; email = Em;
            }
        }
        static void Main(string[] args)
        {
            string[] command;
            string[] userIn = new string[4];
            string fileName = "";
            bool saved = true;
            List<PersonAdressbook> adressList = new List<PersonAdressbook>();
            Console.WriteLine("För att starta programet, skriv 'load' ");
            do
            {
                Console.Write("> ");
                command = Console.ReadLine().Split(' ');
                switch (command[0])
                {
                    case "help":
                        AdressbokHelp();
                        break;
                    case "load":
                        Console.WriteLine("ange fillnamnet");
                        fileName = Console.ReadLine();
                        Console.WriteLine("Reading file {0}", fileName);
                        adressList = ReadAdressFile(fileName);
                        break;
                    case "visa":
                        Console.WriteLine($"Nr {"Namn",-19}{"Adress",-16}{"Telefone",-16}email"); // shows contents of adressList
                        for (int i = 0; i < adressList.Count(); i++)
                        {
                            if (adressList[i] != null)
                            {
                                Console.WriteLine("{0}: {1,-18} {2,-15} {3,-15} {4}",
                                i + 1, adressList[i].name, adressList[i].adress, adressList[i].telephone, adressList[i].email);
                            }
                        }
                        break;
                    case "ny":
                        Console.Write("Ange nyt för- och efternamn: ");
                        userIn[0] = Console.ReadLine();
                        Console.Write("Ange nyt adress: ");
                        userIn[1] = Console.ReadLine();
                        Console.Write("Ange nyt telefone nummer: ");
                        userIn[2] = Console.ReadLine();
                        Console.Write("Ange nyt email: ");
                        userIn[3] = Console.ReadLine();
                        adressList.Add(new PersonAdressbook(userIn[0], userIn[1], userIn[2], userIn[3]));
                        saved = false;
                        break;
                    case "ta"://ta bort
                        if (command[1] == "bort")
                        {
                            Console.Write("Ange för och efternamn på person som du vill ta bort: ");
                            userIn[0] = Console.ReadLine();
                            for (int i = 0; i < adressList.Count(); i++)
                            {
                                if (userIn[0] == adressList[i].name)
                                {
                                    Console.WriteLine($"hittade {userIn[0]} på index {i}");
                                    adressList.RemoveAt(i);
                                }
                            }
                            saved = false;
                        }
                        break;
                    case "spara":
                        using (StreamWriter writer = new StreamWriter(fileName))
                        {
                            for (int i = 0; i < adressList.Count(); i++)
                            {
                                writer.WriteLine("{0}#{1}#{2}#{3}", adressList[i].name, adressList[i].adress, adressList[i].telephone, adressList[i].email);
                            }
                        }
                        saved = true;
                        break;
                    case "sluta":
                        if (saved == false)
                        { Console.WriteLine("skriv \"sluta!\" om du vill stänga programet. !! UTAN att spara! !!"); }
                        else { Console.WriteLine("Skriv \"sluta!\" om du vill stänga programet"); }
                        break;
                    default:
                        if (command[0] != "sluta!")
                        {
                            Console.WriteLine("Unknown command: {0}\nSkriv 'help' för alla commands.", command[0]);
                        }
                        break;
                }
            } while (command[0] != "sluta!");
        }
        static List<PersonAdressbook> ReadAdressFile(string fileName)
        {
            List<PersonAdressbook> todoList = new List<PersonAdressbook>();
            using (StreamReader sr = new StreamReader(fileName))
            {
                while (sr.Peek() >= 0) // Is next char an EndOfFile sign?
                {
                    string[] lword = sr.ReadLine().Split('#');
                    PersonAdressbook A = new PersonAdressbook(lword[0], lword[1], lword[2], lword[3]);
                    todoList.Add(A);
                }
            }
            return todoList;
        }
        static void AdressbokHelp()
        {
            Console.WriteLine("------Help menu------\n" +
                "help\t-\tTar fram denna lista.\n" +
                "load\t-\tLaddar upp adressboken så att man kan ändra, se och andra saker.\n" +
                "visa\t-\tVisar adressbokens inehåll.\n" +
                "ny\t.\tSkriv nya inlägg.\n" +
                "ta bort\t-\tTar bort spesifikt inlägg.\n" +
                "spara\t-\tSparar..\n" +
                "sluta\t-\tStänger av programet.\n" +
                "---------------------");
        }
    }
}
