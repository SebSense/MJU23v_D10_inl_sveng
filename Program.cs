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
            //FIXME: path is customized for navigating VS project directories. Change before publishing.
            string defaultPath = "..\\..\\..\\dict\\";
            string defaultFile = "sweeng.lis";
            Console.WriteLine("Welcome to the dictionary app!");
            ShowHelp();
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
                    if (argument.Length == 2) LoadDictionary(defaultPath, argument[1]);
                    else if (argument.Length == 1) LoadDictionary(defaultPath, defaultFile);
                }
                else if (command == "list")
                {
                    ShowDictionary();
                }
                else if (command == "new")
                {
                    if (argument.Length == 3)
                        AddWord(argument[1], argument[2]);
                    else if (argument.Length == 1)
                        AddWord(GetString("Write word in Swedish: "), GetString("Write word in English: "));
                }
                else if (command == "delete")
                {
                    if (argument.Length == 3)
                        DeleteWord(argument[1], argument[2]);
                    else if (argument.Length == 1)
                        DeleteWord(GetString("Write word in Swedish: "), GetString("Write word in English: "));
                }
                else if (command == "translate")
                {
                    if (argument.Length == 2) Console.WriteLine(TranslateWord(argument[1]));
                    else if (argument.Length == 1) Console.WriteLine(TranslateWord(GetString("Write word to be translated: ")));
                }
                else if (command == "help")
                {
                    ShowHelp();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{command}'");
                }
            }
            while (true);
        }

        private static void ShowHelp()
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

        private static string TranslateWord(string word_to_translate)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == word_to_translate) return $"English for {gloss.word_swe} is {gloss.word_eng}";
                if (gloss.word_eng == word_to_translate) return $"Swedish for {gloss.word_eng} is {gloss.word_swe}";
            }
            return $" '{word_to_translate}' not found in dictionary!";
        }

        private static void DeleteWord(string word_swe, string word_eng)
        {
            bool found = false;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == word_swe && gloss.word_eng == word_eng)
                {
                    Console.WriteLine(" '{0} - {1}' successfully removed.", gloss.word_swe, gloss.word_eng);
                    dictionary.RemoveAt(i);
                    found = true;
                    break;
                }
            }
            if (!found) Console.WriteLine(" Could not find any word '{0} - {1}' to delete.", word_swe, word_eng);
        }

        private static void AddWord(string word_swe, string word_eng)
        {
            dictionary.Add(new SweEngGloss(word_swe, word_eng));
            Console.WriteLine(" Added word to dictionary: '{0} - {1}'\n", word_swe, word_eng);
        }

        private static void ShowDictionary()
        {
            if (!dictionary.Any()) Console.WriteLine(" There are no words in the dictionary!");
            else
                foreach (SweEngGloss gloss in dictionary)
                    Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
        }

        private static void LoadDictionary(string path, string file)
        {
            try
            {

                //NYI: Crashes on incorrect filename FileNotFoundException. Add user feedback instead.
                //NYI: Check file formatting before emptying list and trying to load.
                using (StreamReader reader = new StreamReader(path + file))
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
                Console.WriteLine("\n " + file[1] + " succesfully loaded!\n");
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
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