using BusinessLogicLayer;
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
            Console.WriteLine("1  Read notes (UpdateContents/Delete/Add tag/ExportToFile)");
            Console.WriteLine("2  Find FilteredNotes (Add function to redirect to 1.)");
            Console.WriteLine("3  CreateNote");
            Console.WriteLine("4  SeeAllTags");
            Console.WriteLine("5  CreateTag");
            Console.WriteLine("6  Exit application");

            Console.Write("Please choose an action: ");
            int comm;
            if (!int.TryParse(Console.ReadLine(), out comm))
            {
                Console.WriteLine("Please enter a valid option number. Press Enter to continue...");
                Console.ReadLine();
                Menu();
            }

            switch (comm)
            {
                case 1:
                    ReadNote(PrintNotes(notesServices.GetNotesTitlesAndIDs()));
                    break;
                case 2:
                    ReadFilteredNotesByTag();
                    break;
                case 3:
                    CreateNote();
                    break;
                case 4:
                    Console.Clear();
                    SeeAllTags();
                    break;
                case 5:
                    CreateTag();
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please enter a valid option number. Press enter to continue...");
                    Console.ReadLine();
                    Menu();
                    break;
            }
        }

        static void CreateNote()
        {
            Console.Clear();
            Console.WriteLine(" ----- Note Creation ----- ");
            Console.WriteLine("Note title: ");
            string title = Console.ReadLine();

            Console.WriteLine("\nContents (enter command 'end' to stop writing): ");
            string contents = "";
            string comm = Console.ReadLine();
            while (comm.ToLower() != "end")
            {
                contents += "\n" + comm;
                comm = Console.ReadLine();  
            }

            Console.WriteLine("Do you wish to save this note? ('y' for yes, 'n' for don't save and delete, any other input is not accepted)");
            Console.WriteLine(title);
            Console.WriteLine(contents);
            string comm2 = Console.ReadLine().ToLower().Trim();
            if (comm2 == "y")
            {
                if (notesServices.CreateNote(title, contents))
                {
                    Console.WriteLine("Note saved!");
                    return;
                }
                else
                {
                    Console.WriteLine("Something went wrong. Please try again. Press enter to continue.");
                    Console.ReadLine();
                    Menu();
                }
            }
            else if (comm2 == "n")
            {
                Console.WriteLine("Note deleted! Press enter to continue.");
                Console.ReadLine();
                return;                
            }
        }

        static void CreateTag()
        {
            Console.Clear();
            Console.WriteLine("--- Tag Creation ---");
            Console.WriteLine("What should the content of the tag be about (max 100 symbols, will be converted to lower-case): ");

            string tagContent = Console.ReadLine().Trim().ToLower();

            Console.Write("Do you want to save this tag?\nEnter 'y' to save or 'n' to not save, delete, and return to menu: ");
            string comm = Console.ReadLine().ToLower().Trim();

            if (comm == "y")
            {
                tagsServices.CreateTag(tagContent);
            }
            else if (comm == "n")
            {
                Console.Clear();
                Menu();
            }
            else
            {
                return;
            }
            Console.ReadLine();
            Menu();
        }

        //needs a lot more work
        static void ReadNote((int, string) note)
        {
            Console.Clear();
            Console.WriteLine(note.Item2 + "\n");
            Console.WriteLine(notesServices.GetNoteContents(note.Item1) + "\n");
            Console.WriteLine("Tags: " + string.Join(", ", tagsServices.GetNoteTags(note.Item1)));

            Console.WriteLine("\nWhat do you want to do with this note: \n1. Update its contents\n2. Delete it\n3.Add a tag\n4. Export to file\n 5. Return");

            int comm;
            if (!int.TryParse(Console.ReadLine(), out comm))
            {
                Console.WriteLine("Please enter a valid option number. Press Enter to continue...");
                Console.ReadLine();
                Menu();
            }

            switch (comm)
            {
                case 1:
                    UpdateNoteContent(note.Item1);
                    break;
                case 2:
                    DeleteNote(note.Item1);
                    break;
                case 3:
                    AddTagToNote(note.Item1);
                    break;
                case 4:
                    ExportToFile(note.Item1);
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Please enter a valid option number. Press enter to continue...");
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

            int tagCount = SeeAllTags();

            Console.Write("Number of the tag: ");
            int tagNum;
            while (!int.TryParse(Console.ReadLine().Trim(), out tagNum) || tagNum - 1 > tagCount)
            {
                Console.WriteLine("Please enter a valid tag number. Press Enter to continue...");
            }

            ReadNote(PrintNotes(notesServices.GetFilteredNotes(tagsServices.GetAllTags()[tagNum - 1])));
        }

        static void ExportToFile(int noteID) { }

        static void UpdateNoteContent(int noteID) { }

        static void AddTagToNote(int noteID) { }

        static int SeeAllTags()
        {
            var tagsList = tagsServices.GetAllTags();

            if (tagsList == null || tagsList.Count == 0)
            {
                Console.WriteLine("There are no tags. Press enter to return to menu.");
                Console.ReadLine();
                Console.Clear();
                Menu();
            }

            Console.WriteLine("All tags: ");

            for (int i = 0; i < tagsList.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {tagsList[i]}");
            }
            return tagsList.Count;
        }

        static (int, string) PrintNotes(List<(int, string)> notes)
        {
            Console.WriteLine(" --- Notes --- ");
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
    }
}
