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
            Menu();
        }

        static void Menu()
        {
            Console.Clear();
            Console.CursorVisible = false;
            Menu mainMenu = new Menu(" ----- Main Menu ----- ",
                new string[] { "Read notes", "Find Notes Filtered by Tag", "Create Note", "See All Tags",
                               "Create Tag", "Exit Application" });

            switch (mainMenu.ExSM())
            {
                case 0:
                    Console.Clear();
                    ReadNote(PrintNotes(notesServices.GetNotesTitlesAndIDs()));
                    break;
                case 1:
                    ReadFilteredNotesByTag();
                    break;
                case 2:
                    CreateNote();
                    break;
                case 3:
                    ReadAllTags();
                    break;
                case 4:
                    CreateTag();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Menu();
                    break;
            }
            Menu();
        }

        static void CreateNote()
        {
            Console.Clear();
            Console.WriteLine(" ----- Note Creation ----- ");

            Console.Write("Note title: ");
            string title = Console.ReadLine().Trim();

            while (string.IsNullOrEmpty(title) && title.ToLower() != "exit")
            {
                Console.WriteLine("Note title cannot be empty. Please enter a valid note title. Enter 'exit' to exit.");
                Console.Write("Note title: ");
                title = Console.ReadLine().Trim();
            }

            if (title.ToLower() == "exit")
            {
                Menu();
            }

            Console.WriteLine("\nContents (enter command 'end' to stop writing): ");
            string contents = "";
            string line = Console.ReadLine().Trim();

            while (line.ToLower() != "end")
            {
                contents += "\n" + line;
                line = Console.ReadLine();  
            }

            Console.WriteLine("\n--- Note ---");
            Console.WriteLine(title + "\n");
            Console.WriteLine(contents);
            Console.WriteLine("--- End Note ---\n");

            Menu yesNoMenu = new Menu("Do you want to save this note?",
                new string[] { "Yes, save it", "No, return to note creation", "No, return to menu" });
            int comm = yesNoMenu.ExSM();

            if (comm == 0)
            {
                if (notesServices.CreateNote(title, contents))
                {
                    Console.WriteLine("Note saved! Press enter to return to menu.");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Something went wrong. Please try again. Press enter to continue.");
                    Console.ReadLine();
                    Menu();
                }
            }
            else if (comm == 1)
            {
                CreateNote();
            }
            else
            {
                Menu();
            }
        }

        static void CreateTag()
        {
            Console.Clear();
            Console.WriteLine(" ----- Tag Creation -----");
            Console.WriteLine("What should the content of the tag be about (max 100 symbols, will be converted to lower-case): ");

            string tagContent = Console.ReadLine().Trim().ToLower();

            if (tagContent.Length > 100)
            {
                Console.WriteLine("Tag is too long. Press Enter to continue...");
                Console.ReadLine();
                CreateTag();
            }

            if (tagsServices.GetAllTags().Contains(tagContent))
            {
                Console.WriteLine("Tag already exists. Press Enter to return to menu...");
                Console.ReadLine();
                Menu();
            }

            if (string.IsNullOrWhiteSpace(tagContent))
            {
                Console.WriteLine("Tag cannot be empty. Press Enter to continue...");
                Console.ReadLine();
                CreateTag();
            }

            Menu yesNoMenu = new Menu("Do you want to save this tag?", new string[] { "Yes, save it", "No, return to tag creation", "No, return to menu" });
            int comm = yesNoMenu.ExSM();

            if (comm == 0)
            {
                if (tagsServices.CreateTag(tagContent))
                {
                    Console.WriteLine("Tag saved! Press Enter to continue...");
                    Console.ReadLine();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Something went wrong. Please try again.");
                    Console.ReadLine();
                    CreateTag();
                }
            }
            else if (comm == 1)
            {
                CreateTag();
            }
            else
            {
                Console.Clear();
                Menu();
            }
            Menu();
        }

        //needs a lot more work
        static void ReadNote((int, string) note)
        {
            Console.Clear();
            PrintNote(note);

            Menu menu = new Menu("\nWhat do you want to do with this note:",
                new string[] { "Update its contents", "Delete it", "Add a tag", "Export to file", "Return to menu" });

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
                    Console.WriteLine("Something went wrong. Press Enter to return to menu...");
                    Console.ReadLine();
                    Menu();
                    break;
            }
        }

        static void DeleteNote(int noteID)
        {
            if (notesServices.DeleteNote(noteID))
            { 
                Console.WriteLine("Note successfully deleted;");
            }
            else
            {
                Console.WriteLine("Something went wrong. Please try again.");
            }
            return;
        }

        static void ReadFilteredNotesByTag()
        {
            Console.Clear();

            int tagCount = PrintAllTags();

            Console.Write("Number of the tag: ");
            int tagID;
            while (!int.TryParse(Console.ReadLine().Trim(), out tagID) || tagID - 1 > tagCount)
            {
                Console.WriteLine("Please enter a valid tag number. Press Enter to continue...");
            }

            var filteredNotes = notesServices.GetFilteredNotes(tagsServices.GetAllTags()[tagID - 1]);
            if (filteredNotes == null || filteredNotes.Count == 0)
            {
                Console.WriteLine("There are no notes with this tag. Press enter to return to menu.");
                Console.ReadLine();
                Console.Clear();
                Menu();
            }

            Console.WriteLine(  );
            ReadNote(PrintNotes(filteredNotes));
        }

        static void ExportNoteToFile(int noteID, string noteTitle)
        {
            Console.Clear();
            Console.WriteLine("----- Export Note to File -----");
            Menu menu = new Menu("Where do you want to save the note?",
                new string[] { "Desktop", "Documents folder", "Folder in Documents folder", "Return to Menu" });

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
                case 3:
                    Menu();
                    break;
            }

            Console.WriteLine("File successfully saved! Press Enter to return to menu.");
            Console.ReadLine();
            Menu();
        }

        static void UpdateNoteContents(int noteID, string noteTitle)
        {
            Console.Clear();
            Console.WriteLine("----- Change Note Contents -----");
            List<string> contents = notesServices.GetNoteContents(noteID).Split('\n').ToList();

            Console.WriteLine(noteTitle + "\n");
            for (int i = 0; i < contents.Count(); i++)
            {
                Console.WriteLine($"{i + 1}: {contents[i]}");
            }

            Menu menu = new Menu("\nWhat do you want to do?",
                new string[] { "Edit line", "Delete line", "Add line", "Save changes", "Discard changes", "Return to Menu"});

            int comm = menu.ExSM();
            while (comm != 3 || comm != 4)
            {
                switch (comm)
                {
                    case 0:
                        Console.Write("Enter the number of the line you want to edit: ");
                        int lineNumber;

                        while (!int.TryParse(Console.ReadLine().Trim(), out lineNumber) || lineNumber - 1 > contents.Count())
                        {
                            Console.Write("Please enter a valid line number: ");
                        }

                        Console.WriteLine("Enter new content for this line: ");
                        contents[lineNumber - 1] = Console.ReadLine().Trim();
                        break;
                    case 1:
                        Console.Write("Enter the number of the line you want to delete: ");
                        int lineToDelete;

                        while (!int.TryParse(Console.ReadLine().Trim(), out lineToDelete) || lineToDelete - 1 > contents.Count())
                        {
                            Console.Write("Please enter a valid line number: ");
                        }

                        contents.RemoveAt(lineToDelete - 1);
                        break;
                    case 2:
                        Console.WriteLine("Enter the content of the new line: ");
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
                    Console.WriteLine("Something went wrong, please try again...");
                    UpdateNoteContents(noteID, noteTitle);
                }
                Console.WriteLine("Changes saved. Press Enter to return to Menu.");
                Console.ReadLine();
                Menu();
            }
            else if (comm == 4)
            {
                Console.WriteLine("Changes discarded. Press Enter to return to Menu.");
                Console.ReadLine();
                Menu();
            }
            else
            {
                Menu();
            }
        }

        static void AddTagToNote(int noteID)
        {
            Console.WriteLine();
            int tagCount = PrintAllTags();

            Console.Write("Number of tag to add: ");
            int tagID;

            while (!int.TryParse(Console.ReadLine().Trim(), out tagID) || tagID - 1 > tagCount)
            {
                Console.WriteLine("Please enter a valid tag number. Press Enter to continue...");
            }

            try
            {
                tagsServices.AddTagToNote(noteID, tagID);
            }
            catch (Exception)
            {
                Console.WriteLine("This tag is already attached to the note. Press Enter to continue...");
                Console.ReadLine();
                throw;
            }
        }

        static int  PrintAllTags()
        {
            var tagsList = tagsServices.GetAllTags();

            if (tagsList == null || tagsList.Count == 0)
            {
                Console.WriteLine("There are no tags. Press enter to return to menu.");
                Console.ReadLine();
                Console.Clear();
                Menu();
            }

            Console.WriteLine(" ----- All tags ----- ");

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
            Console.WriteLine("\nPress Enter to return to menu.");
            Console.ReadLine();
            Console.Clear();
        }

        static (int, string) PrintNotes(List<(int, string)> notes)
        {
            Console.WriteLine(" ----- Notes ----- ");
            foreach (var it in notes)
            {
                Console.WriteLine($"{it.Item1 + 1}: {it.Item2}");
            }

            Console.Write("Enter note ID: ");
            int noteID;
            while (!int.TryParse(Console.ReadLine(), out noteID) || !notes.Select(it => it.Item1).Contains(noteID - 1))
            {
                Console.WriteLine("Something went wrong. Please enter a valid number.");
                Console.Write("Enter note ID: ");
            }

            return notes.First(it => it.Item1 == noteID - 1);
        }

        static void PrintNote((int, string) note)
        {
            Console.WriteLine(new string('-', 20) + "\n" + note.Item2 + "\n");
            Console.WriteLine(notesServices.GetNoteContents(note.Item1) + "\n" + new string('-', 20) + "\n");

            var noteTags = tagsServices.GetNoteTags(note.Item1);
            if (noteTags == null || noteTags.Count == 0)
            {
                Console.WriteLine("This note has no tags.");
            }
            else
            {
                Console.WriteLine("Tags: " + string.Join(", ", noteTags));
            }

        }
    }
}
