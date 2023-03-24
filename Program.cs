namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary = new();
        class SweEngGloss
        {
            //TODO: Make private.
            public string word_swe, word_eng;
            public SweEngGloss(string word_swe, string word_eng)
            {
                this.word_swe = word_swe; this.word_eng = word_eng;
            }
            public SweEngGloss(string line)
            {
                string[] words = line.Split('|');
                this.word_swe = words[0]; this.word_eng = words[1];
            }
        }   
        static void Main(string[] args)
        {
            //FIXME: defaultPath is customized for navigating VS project directories. Change before publishing.
            string defaultPath = "..\\..\\..\\dict\\";
            string defaultFile = "sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            //TBD: Refactor into method:
            Console.WriteLine("\n Available commands:\n" +
                        "  new /swe/ /eng/       - Add new word to dictionary. /swe/ and /eng/ optional.\n" +
                        "  load                  - Load dictionary from default file.\n" +
                        "  load /filename/       - Load dictionary from /filename/." +
                        "  list                  - Display dictionary.\n" +
                        "  delete /swe/ /eng/    - Delete word from dictionary.\n" +
                        "  translate /word/      - Translate /word/ from Swedish to English or from English to Swedish.\n" +
                        "  help                  - Show this list of available commands.\n" +
                        "  quit                  - Exit application.");
            do
            {
                //TBD: Refactor following two lines to one method 'string[] str_arr = GetStringArray(string prompt)'
                Console.Write("> ");
                string[] argument = Console.ReadLine().Split();
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
                else if (command == "load")
                {
                    //TBD: Refactor repeating code
                    //TBD: Add console feedback confirming loaded file or file not found.
                    if(argument.Length == 2)
                    {
                        //NYI: Crashes on incorrect filename FileNotFoundException
                        //NYI: Check file formatting before emptying list and trying to load.
                        using (StreamReader sr = new StreamReader(defaultPath + argument[1]))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                        Console.WriteLine("\n " + argument[1] + " succesfully loaded!\n");
                    }
                    else if(argument.Length == 1)
                    {
                        using (StreamReader sr = new StreamReader(defaultPath + defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = sr.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = sr.ReadLine();
                            }
                        }
                        Console.WriteLine("\n " + defaultFile + " succesfully loaded!\n");
                    }
                }
                else if (command == "list")
                {
                    foreach(SweEngGloss gloss in dictionary)
                    {
                        Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                    }
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                        Console.WriteLine(" Added word to dictionary: '{0} - {1}'\n", argument[1], argument[2]);
                    }
                    else if(argument.Length == 1)
                    {
                        //TBD: Refactor two lines into one method.
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        dictionary.Add(new SweEngGloss(s, e));
                        Console.WriteLine(" Added word to dictionary: '{0} - {1}'\n", s, e);
                    }
                }
                else if (command == "delete")
                {
                    //TBD: Refactor two repeating instances of code.
                    if (argument.Length == 3)
                    {
                        for (int i = 0; i < dictionary.Count; i++) {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                            {
                                Console.WriteLine(" Word '{0} - {1}' successfully removed.\n", gloss.word_swe, gloss.word_eng);
                                dictionary.RemoveAt(i);
                                break;
                            }
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        //TBD: Refactor two lines into one method.
                        Console.WriteLine("Write word in Swedish: ");
                        string s = Console.ReadLine();
                        Console.Write("Write word in English: ");
                        string e = Console.ReadLine();
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == s && gloss.word_eng == e)
                            {
                                Console.WriteLine(" Word '{0} - {1}' successfully removed.\n", gloss.word_swe, gloss.word_eng);
                                dictionary.RemoveAt(i);
                                break;
                            }
                        }
                    }
                }
                else if (command == "translate")
                {
                    //TBD: Refactor repeating code.
                    if (argument.Length == 2)
                    {
                        foreach(SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == argument[1])
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                    else if (argument.Length == 1)
                    {
                        Console.WriteLine("Write word to be translated: ");
                        string s = Console.ReadLine();
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == s)
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                            if (gloss.word_eng == s)
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                        }
                    }
                }
                else if(command == "help")
                {
                    Console.WriteLine("\n Available commands:\n" +
                        "  new /swe/ /eng/       - Add new word to dictionary. /swe/ and /eng/ optional.\n" +
                        "  load                  - Load dictionary from default file.\n" +
                        "  load /filename/       - Load dictionary from /filename/." +
                        "  list                  - Display dictionary.\n" +
                        "  delete /swe/ /eng/    - Delete word from dictionary.\n" +
                        "  translate /word/      - Translate /word/ from Swedish to English or from English to Swedish.\n" +
                        "  help                  - Show this list of available commands.\n" +
                        "  quit                  - Exit application.");
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }
    }
}