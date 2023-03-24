namespace MJU23v_D10_inl_sveng
{
    internal class Program
    {
        static List<SweEngGloss> dictionary = new();
        class SweEngGloss
        {
            public string word_swe { get; private set; }
            public string word_eng { get; private set; }
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
                string[] argument = GetArgs("> ");
                string command = argument[0];
                if (command == "quit")
                {
                    Console.WriteLine("Goodbye!");
                    return;
                }
                else if (command == "load")
                {
                    //TBD: Refactor repeating code
                    //TBD: Add console feedback for file not found.
                    if (argument.Length == 2)
                    {
                        //NYI: Crashes on incorrect filename FileNotFoundException
                        //NYI: Check file formatting before emptying list and trying to load.
                        using (StreamReader reader = new StreamReader(defaultPath + argument[1]))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = reader.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = reader.ReadLine();
                            }
                        }
                        Console.WriteLine("\n " + argument[1] + " succesfully loaded!\n");
                    }
                    else if (argument.Length == 1)
                    {
                        using (StreamReader reader = new StreamReader(defaultPath + defaultFile))
                        {
                            dictionary = new List<SweEngGloss>(); // Empty it!
                            string line = reader.ReadLine();
                            while (line != null)
                            {
                                SweEngGloss gloss = new SweEngGloss(line);
                                dictionary.Add(gloss);
                                line = reader.ReadLine();
                            }
                        }
                        Console.WriteLine("\n " + defaultFile + " succesfully loaded!\n");
                    }
                }
                else if (command == "list")
                {
                    if (!dictionary.Any()) Console.WriteLine(" There are no words in the dictionary!");
                    else
                        foreach (SweEngGloss gloss in dictionary)
                           Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                    {
                        dictionary.Add(new SweEngGloss(argument[1], argument[2]));
                        Console.WriteLine(" Added word to dictionary: '{0} - {1}'\n", argument[1], argument[2]);
                    }
                    else if (argument.Length == 1)
                    {
                        string swe_meaning = GetString("Write word in Swedish: ");
                        string eng_meaning = GetString("Write word in English: ");
                        dictionary.Add(new SweEngGloss(swe_meaning, eng_meaning));
                        Console.WriteLine(" Added word to dictionary: '{0} - {1}'\n", swe_meaning, eng_meaning);
                    }
                }
                else if (command == "delete")
                {
                    //TBD: Refactor two repeating instances of code.
                    if (argument.Length == 3)
                    {
                        bool found = false;
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == argument[1] && gloss.word_eng == argument[2])
                            {
                                Console.WriteLine(" '{0} - {1}' successfully removed.", gloss.word_swe, gloss.word_eng);
                                dictionary.RemoveAt(i);
                                found = true;
                                break;
                            }
                        }
                        if (!found) Console.WriteLine(" Could not find any word '{0} - {1}' to delete.", argument[1], argument[2]);
                    }
                    else if (argument.Length == 1)
                    {
                        bool found = false;
                        string swe_meaning = GetString("Write word in Swedish: ");
                        string eng_meaning = GetString("Write word in English: ");
                        for (int i = 0; i < dictionary.Count; i++)
                        {
                            SweEngGloss gloss = dictionary[i];
                            if (gloss.word_swe == swe_meaning && gloss.word_eng == eng_meaning)
                            {
                                Console.WriteLine(" '{0} - {1}' successfully removed.", gloss.word_swe, gloss.word_eng);
                                dictionary.RemoveAt(i);
                                found = true;
                                break;
                            }
                        }
                        if (!found) Console.WriteLine(" Could not find any word '{0} - {1}' to delete.", swe_meaning, eng_meaning);
                    }
                }
                else if (command == "translate")
                {
                    //TBD: Refactor repeating code.
                    if (argument.Length == 2)
                    {
                        bool found = false;
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == argument[1])
                            {
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                                found = true;
                            }
                            if (gloss.word_eng == argument[1])
                            {
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                                found = true;
                            }
                        }
                        if (!found) Console.WriteLine(" '{0}' not found in dictionary!", argument[1]);
                    }
                    else if (argument.Length == 1)
                    {
                        bool found = false;
                        string word_to_translate = GetString("Write word to be translated: ");
                        foreach (SweEngGloss gloss in dictionary)
                        {
                            if (gloss.word_swe == word_to_translate)
                            {
                                Console.WriteLine($"English for {gloss.word_swe} is {gloss.word_eng}");
                                found = true;
                            }
                            if (gloss.word_eng == word_to_translate)
                            {
                                Console.WriteLine($"Swedish for {gloss.word_eng} is {gloss.word_swe}");
                                found = true;
                            }
                        }
                        if (!found) Console.WriteLine(" '{0}' not found in dictionary!", word_to_translate);
                    }
                }
                else if (command == "help")
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

        private static string[] GetArgs(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine().Split();
        }
        private static string GetString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}