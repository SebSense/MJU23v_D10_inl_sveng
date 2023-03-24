namespace MJU23v_D10_inl_sveng
{
    /// <summary>
    /// ********************************************
    /// MJU23v_D10_inl_sveng
    /// Inlämningsuppgift för kursen "Datalogiskt tänkande och problemlösning" vid programmet Mjukvaruutvecklare vid MÖLK
    /// ********************************************
    /// Av: Sebastian Senic
    /// Datum: 2023-03-10
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// List <c>dictionary</c> is a list of SweEngGloss the application uses for rw
        /// </summary>
        static List<SweEngGloss> dictionary = new();
        /// <summary>
        /// Class <c>SweEngGloss</c> models a word and contains the word in Swedish and the word in English
        /// </summary>
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
            //TODO: path is customized for navigating VS project directories. Change before publishing.
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
        /// <summary>
        /// Method <c>static void ShowHelp()</c> writes available commands to termingal.
        /// </summary>
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
        /// <summary>
        /// Method <c>static string TranslateWord(string word_to_translate)</c> searches in the dictionary List for word_to_translate and translates it.
        /// </summary>
        /// <param name="word_to_translate"></param>
        /// <returns>"word_to_translate" translated to the other language or an error message if no match was found</returns>
        private static string TranslateWord(string word_to_translate)
        {
            foreach (SweEngGloss gloss in dictionary)
            {
                if (gloss.word_swe == word_to_translate) return $"English for {gloss.word_swe} is {gloss.word_eng}";
                if (gloss.word_eng == word_to_translate) return $"Swedish for {gloss.word_eng} is {gloss.word_swe}";
            }
            return $" '{word_to_translate}' not found in dictionary!";
        }
        /// <summary>
        /// Method <c>static void DeleteWord(string word_swe, string word_eng)</c> searches the dictionary List for an entry that matches both Swedish and English words and deletes the entry. Prints a message to console if no match is found.
        /// </summary>
        /// <param name="word_swe"></param>
        /// <param name="word_eng"></param>
        private static void DeleteWord(string word_swe, string word_eng)
        {
            int index = -1;
            for (int i = 0; i < dictionary.Count; i++)
            {
                SweEngGloss gloss = dictionary[i];
                if (gloss.word_swe == word_swe && gloss.word_eng == word_eng)
                {
                    Console.WriteLine(" '{0} - {1}' successfully removed.", gloss.word_swe, gloss.word_eng);
                    index = i;
                    break;
                }
            }
            try { dictionary.RemoveAt(index); }
            catch (ArgumentOutOfRangeException) { Console.WriteLine(" Could not find any word '{0} - {1}' to delete.", word_swe, word_eng); }
        }
        /// <summary>
        /// Method <c>static void AddWord(string word_swe, string word_eng)</c> Adds a word to the dictionary List.
        /// </summary>
        /// <param name="word_swe"></param>
        /// <param name="word_eng"></param>
        private static void AddWord(string word_swe, string word_eng)
        {
            dictionary.Add(new SweEngGloss(word_swe, word_eng));
            Console.WriteLine(" Added word to dictionary: '{0} - {1}'\n", word_swe, word_eng);
        }
        /// <summary>
        /// Method <c>static void ShowDictionary()</c> Prints all elements of the dictionary list to console.
        /// </summary>
        private static void ShowDictionary()
        {
            if (!dictionary.Any()) Console.WriteLine(" There are no words in the dictionary!");
            else
                foreach (SweEngGloss gloss in dictionary)
                    Console.WriteLine($"{gloss.word_swe,-10}  - {gloss.word_eng,-10}");
        }
        /// <summary>
        /// Method <c>static void LoadDictionary(string path, string file)</c> clears current dictionary and loads elements from a file.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="file"></param>
        private static void LoadDictionary(string path, string file)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path + file))
                {
                    dictionary = new List<SweEngGloss>(); // Empty it!
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        SweEngGloss gloss = new SweEngGloss(line);
                        dictionary.Add(gloss);
                    }
                }
                Console.WriteLine("\n " + file + " succesfully loaded!\n");
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            catch(IndexOutOfRangeException)
            {
                Console.WriteLine("Error: Loading aborted. File is corrupt or is not a valid .lis dictionary file.");
            }
        }
        /// <summary>
        /// Method <c>static string[] GetArgs(string prompt)</c> Prompts the user for input and returns it as multiple strings.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>string[] of user input split by whitespaces</returns>
        private static string[] GetArgs(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine().Split();
        }
        /// <summary>
        /// Method <c>static string GetString(string prompt)</c> prompts the user for input and returns it as a string.
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns>string of user input</returns>
        private static string GetString(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine();
        }
    }
}