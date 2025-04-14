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
                    ReadNote();
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

        static void CreateNote() { }

        static void CreateTag() { }

        static void ReadNote()
        {
            Console.Clear();
            Console.WriteLine("Notes: ");
            var note = PrintNotes(notesServices.GetNotesTitlesAndIDs());
            
            Console.Clear();
            Console.WriteLine(note.Item2 + "\n");
            Console.WriteLine(notesServices.GetNoteContents(note.Item1) + "\n");
            Console.WriteLine("Tags: " + string.Join(", ", tagsServices.GetNoteTags(note.Item1)));
            Console.ReadLine();
        }

        static void DeleteNote() { }

        static void ReadFilteredNotesByTag() { }

        static void ExportToFile() { }

        static void UpdateNoteContent() { }

        static void AddTagToNote() { }

        static (int, string) PrintNotes(List<(int, string)> notes)
        {
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
