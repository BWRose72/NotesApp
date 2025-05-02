using Azure;
using BusinessLogicLayer;
using Microsoft.VisualBasic;
using System.ComponentModel.Design;
using System.Threading.Channels;
using static Azure.Core.HttpHeader;

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
            Menu mainMenu = new Menu(" ----- Main Menu ----- ",
                new string[] { "Read notes", "Find Notes Filtered by Tag", "Create Note", "See All Tags",
                               "Create Tag", "Exit Application" });
            int comm = mainMenu.ExSM();
            switch (comm)
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
            Console.WriteLine(title);
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
            int comm = menu.ExSM();

            switch (comm)
            {
                case 0:
                    UpdateNoteContent(note.Item1);
                    break;
                case 1:
                    DeleteNote(note.Item1);
                    break;
                case 2:
                    AddTagToNote(note.Item1);
                    break;
                case 3:
                    ExportToFile(note.Item1);
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

        static void ExportToFile(int noteID) { throw new NotImplementedException(); }

        static void UpdateNoteContent(int noteID) { throw new NotImplementedException(); }

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
            Console.WriteLine(new string('-', 20) + "\n" + note.Item2);
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
