using BusinessLogicLayer;
using Microsoft.VisualBasic;
using System.ComponentModel.Design;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Channels;

namespace ConsolePresentationalLayer
{
    internal class Program
    {
        static readonly NotesServices notesServices = new NotesServices();
        static readonly TagsServices tagsServices = new TagsServices();

        static void Main(string[] args)
        {
            Console.InputEncoding = System.Text.Encoding.UTF8;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Menu mainMenu = new Menu("----- Меню ----- ",  new string[] { "Всички бележки", "Търсене по съдържание", "Търсене по етикет",
                "Нова бележка", "Всички етикети", "Нов етикет", "Изход от приложението" });

            switch (mainMenu.ExSM())
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
                case 4:
                    ReadAllTags();
                    break;
                case 5:
                    CreateTag();
                    break;
                case 6:
                    Environment.Exit(0);
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
                new string[] { "Да", "Не"});
            int comm = yesNoMenu.ExSM();

            if (comm == 0)
            {
                if (notesServices.CreateNote(title, contents))
                {
                    Console.WriteLine("Бележката е запазена! Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                    Console.Clear();
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

            while (tagContent != "и" || !IsTagValid(tagContent))
            {
                Console.Write("Съдържание ('и' за изход): ");
                tagContent = Console.ReadLine().Trim().ToLower();
            }

            if (tagContent == "и")
            {
                return;
            }

            Menu yesNoMenu = new Menu($"Искате ли да запазите този етикет: {tagContent}?", new string[] { "Да", "Не" });
            int comm = yesNoMenu.ExSM();

            if (comm == 0)
            {
                if (tagsServices.CreateTag(tagContent))
                {
                    Console.WriteLine("Етикетът е запазен! Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Нещо се обърка. Моля опитайте отново. Натиснете Enter, за да се върнете в менюто.");
                    Console.ReadLine();
                    CreateTag();
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
                    ExportNoteToFile(note.Item1, note.Item2);
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
                Console.Clear();
                Menu();
            }

            Console.WriteLine();
            AlterNote(PrintNotes(filteredNotes));
        }

        static void AlterFilteredNotesByTag()
        {
            Console.Clear();

            int tagCount = PrintAllTags();

            Console.Write("Номер на етикета (въведете 0, за да се върнете към менюто): ");
            int tagID;
            while ((!int.TryParse(Console.ReadLine().Trim(), out tagID) || tagID - 1 > tagCount) && tagID != 0)
            {
                Console.WriteLine("Моля въведете валиден номер. Натиснете Enter, за да продължите.");
            }

            if (tagID == 0)
            {
                return;
            }

            var filteredNotes = notesServices.GetFilteredNotes(tagsServices.GetAllTags()[tagID - 1]);
            if (filteredNotes == null || filteredNotes.Count == 0)
            {
                Console.WriteLine("Няма бележки с този етикет. Натиснете Enter, за да се върнете в менюто.");
                Console.ReadLine();
                Console.Clear();
                Menu();
            }

            Console.WriteLine(  );
            AlterNote(PrintNotes(filteredNotes));
        }

        //REVISIT
        static void ExportNoteToFile(int noteID, string noteTitle)
        {
            Console.Clear();
            Console.WriteLine("----- Запазване на бележка като текстов файл -----");
            Menu menu = new Menu("Къде искате да запазите тази бележка?",
                new string[] { "На Работения плот", "В папката Документи", /*"В папка в папката Документи",*/ "Връщане в Меню" });

            switch (menu.ExSM())
            {
                case 0:
                    FileSaver.SaveFileToDesktop(noteTitle,
                        notesServices.GetNoteContents(noteID),
                        tagsServices.GetNoteTags(noteID).ToArray());
                    break;
                case 1:
                    FileSaver.SaveFileToDocuments(noteTitle,
                        notesServices.GetNoteContents(noteID),
                        tagsServices.GetNoteTags(noteID).ToArray());
                    break;
                case 2:
                    /*
                    Console.Write("Enter the name of the new folder, if it does not exist one will be created (or 'exit' to return): ");
                    string folderName = Console.ReadLine().Trim();
                    
                    while (string.IsNullOrWhiteSpace(folderName) || folderName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                    {
                        Console.Write("The folder name you entered is invalid. Make sure it does not contain any of these characters: + < > : \" / \\ | ? * ");
                        Console.WriteLine("Enter the name of the new folder (or 'exit' to return): ");
                        folderName = Console.ReadLine().Trim();
                    }

                    if (folderName.ToLower() == "exit")
                    {
                        ExportNoteToFile(noteID, noteTitle);
                    }

                    FileSaver.SaveFileToNewFolderInDocuments(folderName,
                        noteTitle,
                        notesServices.GetNoteContents(noteID),
                        tagsServices.GetNoteTags(noteID).ToArray());
                    break;
                case 3:*/

                    Menu();
                    break;
            }

            Console.WriteLine("Файлът е запазен успешно! Натиснете Enter, за да се върнете в менюто.");
            Console.ReadLine();
            Menu();
        }

        //Ready, but doesn't use ReadLine - REVISIT
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

            /*Menu menu = new Menu("\nКакво искате да направите?",
                new string[] { "Редактирайте реда", "Изтрийте реда", "Добави реда", "Запази промените", "Отхвърли промените", "<- Меню"});

            int comm = menu.ExSM();
            while (comm != 3 || comm != 4)
            {
                switch (comm)
                {
                    case 0:
                        Console.Write("Въведете номерът на реда, който искате да редактирате: ");
                        int lineNumber;

                        while (!int.TryParse(Console.ReadLine().Trim(), out lineNumber) || lineNumber - 1 > contents.Count())
                        {
                            Console.Write("Моля въведете валиден номер: ");
                        }

                        Console.WriteLine("Въведете ново съдържание за този ред: ");
                        contents[lineNumber - 1] = Console.ReadLine().Trim();
                        break;
                    case 1:
                        Console.Write("Въведете номерът на реда, който искате да изтриете: ");
                        int lineToDelete;

                        while (!int.TryParse(Console.ReadLine().Trim(), out lineToDelete) || lineToDelete - 1 > contents.Count())
                        {
                            Console.Write("Моля въведете валиден номер: ");
                        }

                        contents.RemoveAt(lineToDelete - 1);
                        break;
                    case 2:
                        Console.WriteLine("Въведете ново съдържание за този ред: ");
                        string newLine = Console.ReadLine().Trim();
                        contents.Add(newLine);
                        break;
                }
                Console.Clear();

                Console.WriteLine(noteTitle + "\n");
                for (int i = 0; i < contents.Count(); i++)
                {
                    Console.WriteLine($"{i + 1}: {contents[i]}");
                }

                comm = menu.ExSM();
            }

            if (comm == 3)
            {
                if (!notesServices.UpdateNoteContents(noteID, string.Join('\n', contents)))
                {
                    Console.WriteLine("Нещо се обърка. Моля пробвайте отново!");
                    UpdateNoteContents(noteID, noteTitle);
                }
                Console.WriteLine("Промените са запазени. Натиснете Enter, за да се върнете в менюто.");
                Console.ReadLine();
                Menu();
            }
            else if (comm == 4)
            {
                Console.WriteLine("Промените са отхвълени. Натиснете Enter, за да се върнете в менюто.");
                Console.ReadLine();
                Menu();
            }
            else
            {
                Menu();
            }*/
        }

        static void AddTagToNote(int noteID)
        {
            Console.WriteLine();
            int tagCount = PrintAllTags();

            Console.Write("Номер на етикета: ");
            int tagID;

            while (!int.TryParse(Console.ReadLine().Trim(), out tagID) || tagID - 1 > tagCount)
            {
                Console.WriteLine("Моля въведете валиден номер.\nНомер на етикета: ");
            }

            if (tagsServices.CheckIfNoteTagExists(tagID, noteID)) 
            {
                Console.WriteLine("Този етикет вече е добавен към бележката. Натиснете Enter, за да се върнете в мен.");
                Console.ReadLine();
                Menu();
            }

            try
            {
                tagsServices.AddTagToNote(noteID, tagID);
            }
            catch (Exception)
            {
                Console.WriteLine("Нещо се обърка. Натиснете Enter, за да продължите.");
                Console.ReadLine();
                throw;
            }
        }

        static int  PrintAllTags()
        {
            var tagsList = tagsServices.GetAllTags();

            if (tagsList == null || tagsList.Count == 0)
            {
                Console.WriteLine("Няма етикети. Натиснете Enter, за да се върнете в менюто.");
                Console.ReadLine();
                Console.Clear();
                Menu();
            }

            Console.WriteLine("----- Всички етикети ----- ");

            for (int i = 0; i < tagsList.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {tagsList[i]}");
            }
            return tagsList.Count;
        }

        static void ReadAllTags()
        {
            Console.Clear();
            PrintAllTags();
            Console.WriteLine("\nНатиснете Enter, за да се върнете в менюто.");
            Console.ReadLine();
            Console.Clear();
        }

        static (int, string) PrintNotes(List<(int, string)> notes)
        {
            Console.WriteLine(" ----- Бележки ----- ");
            foreach (var it in notes)
            {
                Console.WriteLine($"{it.Item1 + 1}: {it.Item2}");
            }

            Console.Write("Въведете номерът на бележката: ");
            int noteID;
            while (!int.TryParse(Console.ReadLine(), out noteID) || !notes.Select(it => it.Item1).Contains(noteID - 1))
            {
                Console.WriteLine("Нещо се обърка. Моля въведете валиден номер!");
                Console.Write("Въведете номерът на бележката: ");
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
            Console.CursorVisible = true;
            Console.WriteLine("\nСъдържание (въведете 'стоп', за да приключите с писането): ");
            string contents = "";
            string line = Console.ReadLine().Trim();

            while (line.ToLower() != "стоп")
            {
                contents += "\n" + line;
                line = Console.ReadLine();
            }
            Console.CursorVisible = false;
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
