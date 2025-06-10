using BusinessLogicLayer;
using Microsoft.VisualBasic;
using System.Diagnostics;

namespace ConsolePresentationalLayer
{
    internal class Program
    {
        static readonly NotesServices notesServices = new NotesServices();
        static readonly TagsServices tagsServices = new TagsServices();

        static void Main(string[] args)
        {
            Console.Title = "MyNotes";
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Menu();
        }

        static void Menu()
        {
            Menu mainMenu = new Menu("----- Меню ----- ", new string[] { "Бележки", "Етикети", "Изход от приложението" });
            while (true)
            {
                Console.Clear();
                switch (mainMenu.ExSM())
                {
                    case 0:
                        NotesMenu();
                        break;
                    case 1:
                        TagMenu();
                        break;
                    case 2:
                        Environment.Exit(0);
                        break;
                    default:
                        Menu();
                        break;
                }
            }
        }

        static void NotesMenu()
        {
            Console.Clear();
            Menu notesMenu = new Menu("----- Меню ----- ", new string[] { "Всички бележки", "Търсене по съдържание", "Търсене по етикет", "Нова бележка", "Върни се в главното меню" });
            switch (notesMenu.ExSM())
            {
                case 0:
                    Console.Clear();
                    AlterNote(PrintNotes(notesServices.GetNotesTitlesAndIDs()));
                    break;
                case 1:
                    AlterFilteredNotesByContent();
                    break;
                case 2:
                    AlterFilteredNotesByTag();
                    break;
                case 3:
                    CreateNote();
                    break;
                case 6:
                    break;
                default:
                    Menu();
                    break;
            }
        }

        static void TagMenu()
        {
            Console.Clear();
            Menu mainMenu = new Menu("----- Меню ----- ", new string[] { "Всички етикети", "Нов етикет", "Изтриване на етикет", "Върни се в главното меню" });
            switch (mainMenu.ExSM())
            {
                case 0:
                    ReadAllTags();
                    break;
                case 1:
                    CreateTag();
                    break;
                case 2:
                    DeleteTag();
                    break;
                case 3:
                    break;
                default:
                    Menu();
                    break;
            }
        }

        static void CreateNote()
        {
            Console.Clear();
            Console.WriteLine("----- Нова бележка ----- ");

            Console.Write("Заглавие: ");
            string title = Console.ReadLine().Trim();

            while (string.IsNullOrEmpty(title) && title.ToLower() != "изход")
            {
                Console.WriteLine("Загалвието не може да е празно. Моля въведете валидно заглавие.\nВъведете 'изход', за да се върнете в менюто.");
                Console.Write("Заглавие: ");
                title = Console.ReadLine().Trim();
            }

            if (title.ToLower() == "изход")
            {
                Menu();
            }

            string contents = GetNoteContents();

            Console.WriteLine("\n----- Бележка -----");
            Console.WriteLine(title);
            Console.WriteLine(contents);
            Console.WriteLine("----- Край на бележката -----\n");

            Menu yesNoMenu = new Menu("Искате ли да запазите тази бележка?",
                new string[] { "Да", "Не" });
            int comm = yesNoMenu.ExSM();

            if (comm == 0)
            {
                if (notesServices.CreateNote(title, contents))
                {
                    Console.WriteLine("Бележката е запазена! Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Нещо се обърка. Моля опитайте отново. Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                    Menu();
                }
            }
            else
            {
                Menu();
            }
        }

        static void CreateTag()
        {
            Console.Clear();
            Console.WriteLine(" ----- Нов етикет -----");
            Console.WriteLine("Съдържанието на етикета трябва да е максимум 100 символа (ще бъде преобразувано в малки букви).");
            Console.WriteLine("Въведете 'и' за изход, за да се върнете в менюто.");

            Console.Write("Съдържание: ");
            string tagContent = Console.ReadLine().Trim().ToLower();

            while (tagContent != "и" && !IsTagValid(tagContent))
            {
                Console.Write("Съдържание ('и' за изход): ");
                tagContent = Console.ReadLine().Trim().ToLower();
            }

            if (tagContent == "и")
            {
                return;
            }

            Menu yesNoMenu = new Menu($"Искате ли да запазите този етикет: {tagContent}", new string[] { "Да", "Не" });
            int comm = yesNoMenu.ExSM();

            if (comm == 0)
            {
                if (tagsServices.CreateTag(tagContent))
                {
                    Console.WriteLine("Етикетът е запазен! Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Нещо се обърка. Моля опитайте отново. Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                }
            }

            Menu();
        }

        static void AlterNote((int, string) note)
        {
            Console.Clear();
            PrintNote(note);

            Menu menu = new Menu("\nКакво искате да направите с тази бележка:",
                new string[] { "Промененяне на съдържанието", "Изтриване", "Добавяне на етикет", "Запазване като текстов файл", "Връщане в Меню" });

            switch (menu.ExSM())
            {
                case 0:
                    UpdateNoteContents(note.Item1, note.Item2);
                    break;
                case 1:
                    DeleteNote(note.Item1);
                    break;
                case 2:
                    AddTagToNote(note.Item1);
                    break;
                case 3:
                    ExportNoteToFile(note.Item1);
                    break;
                case 4:
                    Console.Clear();
                    return;
                default:
                    Console.WriteLine("Нещо се обърка. Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                    Menu();
                    break;
            }
        }

        static void DeleteNote(int noteID)
        {
            if (notesServices.DeleteNote(noteID))
            {
                Console.WriteLine("Бележката беше изтрита успешно.");
            }
            else
            {
                Console.WriteLine("Нещо се обърка. Моля пробвайте отново!");
            }

            return;
        }

        private static void DeleteTag()
        {
            Console.Clear();
            var allTags = tagsServices.GetAllTags();
            int tagCount = PrintTags(allTags);
            Console.Write("Номер на етикета (натиснете Enter, за да се върнете към менюто): ");
            int tagID;

            while ((!int.TryParse(Console.ReadLine().Trim(), out tagID) || tagID - 1 > tagCount) && tagID != 0)
            {
                Console.WriteLine("Моля въведете валиден номер. Натиснете Enter, за да продължите.");
            }

            if (tagID == 0)
            {
                return;
            }

            Menu yesNoMenu = new Menu("Сигурни ли сте, чеискате да изтриете този етикет?",
                new string[] { "Да", "Не" });
            int comm = yesNoMenu.ExSM();

            if (comm == 0)
            {
                if (tagsServices.DeleteTag(tagsServices.GetTagIDFromContent(allTags[tagID - 1])))
                {
                    Console.WriteLine("Етикета е изтрит! Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Нещо се обърка. Моля опитайте отново. Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                    Menu();
                }
            }
            else
            {
                Menu();
            }
        }

        static void AlterFilteredNotesByContent()
        {
            Console.Clear();

            Console.WriteLine("----- Търсене по съдържание -----");
            Console.Write("Ключов израз: ");
            string keyContent = Console.ReadLine();

            var filteredNotes = notesServices.GetNotesByContent(keyContent);

            if (filteredNotes == null || filteredNotes.Count == 0)
            {
                Console.WriteLine("Няма бележки с тaкова съдържание. Натиснете Enter, за да се върнете в менюто.");
                Console.ReadLine();
                Menu();
            }

            Console.WriteLine();
            AlterNote(PrintNotes(filteredNotes));
        }

        static void AlterFilteredNotesByTag()
        {
            Console.Clear();

            var allTags = tagsServices.GetAllTags();
            int tagCount = PrintTags(allTags);

            Console.Write("Номер на етикета (натиснете Enter, за да се върнете към менюто): ");
            int tagID;

            while ((!int.TryParse(Console.ReadLine().Trim(), out tagID) || tagID - 1 > tagCount) && tagID != 0)
            {
                Console.WriteLine("Моля въведете валиден номер. Натиснете Enter, за да продължите.");
            }

            if (tagID == 0)
            {
                return;
            }

            var filteredNotes = notesServices.GetFilteredNotes(allTags[tagID - 1]);

            if (filteredNotes == null || filteredNotes.Count == 0)
            {
                Console.WriteLine("Няма бележки с този етикет. Натиснете Enter, за да се върнете в менюто.");
                Console.ReadLine();
                Console.Clear();
                Menu();
            }

            Console.WriteLine();
            AlterNote(PrintNotes(filteredNotes));
        }

        static void ExportNoteToFile(int noteID)
        {
            Console.Clear();
            Console.WriteLine("----- Запазване на бележка като текстов файл -----");

            Console.Write("Име на файла: ");
            string fileName = Console.ReadLine();

            while (Path.GetInvalidFileNameChars().Any(fileName.Contains))
            {
                Console.WriteLine("Името не трябва да съдържа тези знаци: \" < > | ␦  * ? \\ /");
                Console.Write("Име на файла: ");
                fileName = Console.ReadLine();
            }

            Menu menu = new Menu("Къде искате да запазите тази бележка?",
                new string[] { "На Работения плот", "В папката Документи", "Връщане в Меню" });

            switch (menu.ExSM())
            {
                case 0:
                    FileSaver.SaveFileToDesktop(fileName,
                        notesServices.GetNoteContents(noteID),
                        tagsServices.GetNoteTags(noteID).ToArray());
                    break;
                case 1:
                    FileSaver.SaveFileToDocuments(fileName,
                        notesServices.GetNoteContents(noteID),
                        tagsServices.GetNoteTags(noteID).ToArray());
                    break;
                case 2:
                    Menu();
                    break;
            }

            Console.WriteLine("Файлът е запазен успешно! Натиснете Enter, за да се върнете в менюто.");
            Console.ReadLine();
            Menu();
        }

        static void UpdateNoteContents(int noteID, string noteTitle)
        {
            Console.Clear();
            Console.WriteLine("----- Промяна на съдържанието -----");
            PrintNote((noteID, noteTitle));

            string newContent = GetNoteContents();

            if (!notesServices.UpdateNoteContents(noteID, newContent))
            {
                Console.WriteLine("Нещо се обърка. Моля пробвайте отново!");
                UpdateNoteContents(noteID, noteTitle);
            }

            Console.WriteLine("Промените са запазени. Натиснете Enter, за да се върнете в менюто.");
            Console.ReadLine();
            Menu();
        }

        static void AddTagToNote(int noteID)
        {
            Console.WriteLine();
            var freeTags = tagsServices.GetFreeTagsById(noteID);
            int tagCount = PrintTags(freeTags);

            Console.Write("Номер на етикета (натиснете Enter, за да се върнете в менюто): ");
            int tagID;

            while ((!int.TryParse(Console.ReadLine().Trim(), out tagID) || freeTags.Count < tagID) && tagID != 0)
            {
                Console.Write("Моля въведете валиден номер.\nНомер на етикета: ");
            }

            if (tagID == 0)
            {
                return;
            }

            try
            {
                tagsServices.AddTagToNote(noteID, tagsServices.GetTagIDFromContent(freeTags[tagID - 1]));
            }
            catch (Exception)
            {
                Console.WriteLine("Нещо се обърка. Натиснете Enter, за да продължите.");
                Console.ReadLine();
                throw;
            }
        }

        static int PrintTags(List<string> tagsList)
        {
            if (tagsList == null || tagsList.Count == 0)
            {
                Console.WriteLine("Няма етикети. Натиснете Enter, за да се върнете в менюто.");
                Console.ReadLine();
                Console.Clear();
                Menu();
            }

            Console.WriteLine("----- Всички етикети ----- ");

            int tagsCount = tagsList.Count;

            for (int i = 0; i < tagsCount; i++)
            {
                Console.WriteLine($"{i + 1}: {tagsList[i]}");
            }

            return tagsCount;
        }

        static void ReadAllTags()
        {
            Console.Clear();
            PrintTags(tagsServices.GetAllTags());
            Console.WriteLine("\nНатиснете Enter, за да се върнете в менюто.");
            Console.ReadLine();
        }

        static (int, string) PrintNotes(List<(int, string)> notes)
        {
            Console.WriteLine(" ----- Бележки ----- ");

            foreach (var it in notes)
            {
                Console.WriteLine($"{it.Item1 + 1}: {it.Item2}");
            }

            Console.Write("Въведете номерът на бележката (натиснете Enter, за да се върнете в менюто): ");
            int noteID;

            while ((!int.TryParse(Console.ReadLine(), out noteID) || !notes.Select(it => it.Item1).Contains(noteID - 1)) && noteID != 0)
            {
                Console.WriteLine("Нещо се обърка. Моля въведете валиден номер!");
                Console.Write("Въведете номерът на бележката: ");
            }

            if (noteID == 0)
            {
                Menu();
            }

            return notes.First(it => it.Item1 == noteID - 1);
        }

        static void PrintNote((int, string) note)
        {
            Console.WriteLine(new string('-', 40) + "\n" + note.Item2);
            Console.WriteLine(notesServices.GetNoteContents(note.Item1) + "\n" + new string('-', 40) + "\n");

            var noteTags = tagsServices.GetNoteTags(note.Item1);
            if (noteTags == null || noteTags.Count == 0)
            {
                Console.WriteLine("Тази бележка няма етикети.");
            }
            else
            {
                Console.WriteLine("Етикети: " + string.Join(", ", noteTags));
            }
        }

        static string GetNoteContents()
        {
            Console.WriteLine("\nСъдържание (въведете 'стоп', за да приключите с писането): ");
            string contents = "";
            string line = Console.ReadLine().Trim();

            while (line.ToLower() != "стоп")
            {
                contents += "\n" + line;
                line = Console.ReadLine();
            }

            return contents;
        }

        static bool IsTagValid(string tagContent)
        {
            if (tagContent.Length > 100)
            {
                Console.WriteLine("Моля въведете съдържание по-малко от 100 символа.");
                return false;
            }

            if (tagsServices.GetAllTags().Contains(tagContent))
            {
                Console.WriteLine("Етикета вече съществува. Натиснете Enter, за да се върнете в менюто.");
                Console.ReadLine();
                Menu();
            }

            if (string.IsNullOrWhiteSpace(tagContent))
            {
                Console.WriteLine("Етикета не може да е празен.");
                return false;
            }

            return true;
        }
    }
}
