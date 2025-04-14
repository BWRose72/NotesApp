using BusinessLogicLayer;
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
            Console.WriteLine("4  CreateTag");
            Console.WriteLine("5  Exit application");

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
                    CreateTag();
                    break;
                case 5:
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

            Console.WriteLine("Do you wish to save this note? ('y' for yes, any other input will be taken as a 'no')");
            Console.WriteLine(title);
            Console.WriteLine(contents);
            if (Console.ReadLine().ToLower().Trim() == "y")
            {
                if (notesServices.CreateNote(title, contents))
                {
                    Console.WriteLine("Note saved!");
                    return;
                }
                else
                {
                    Console.WriteLine("Something went wrong.Please try again.");
                    Menu();
                }
            }
        }

        static void CreateTag() { }

        //needs a lot more work
        static void ReadNote((int, string) note)
        {
            Console.Clear();
            Console.WriteLine(note.Item2 + "\n");
            Console.WriteLine(notesServices.GetNoteContents(note.Item1) + "\n");
            Console.WriteLine("Tags: " + string.Join(", ", tagsServices.GetNoteTags(note.Item1)));
            Console.WriteLine("What do you want to do with this note: \n1. Update its contents\n2. Delete it\n3.Add a tag\n4. Export to file\n 5. Return");

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
            ReadNote(PrintNotes(notesServices.GetFilteredNotes("-----------------------tag---------------------")));
        }

        static void ExportToFile() { }

        static void UpdateNoteContent() { }

        static void AddTagToNote() { }

        static (int, string) PrintNotes(List<(int, string)> notes)
        {
            Console.WriteLine(" --- Notes --- ");
            foreach (var ti in notes)
            {
                Console.WriteLine($"{ti.Item1 + 1}: {ti.Item2}");
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
