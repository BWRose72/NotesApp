using DataAcessLayer;

namespace BusinessLogicLayer
{
    public class NotesServices
    {
        private readonly NotesDAL notesDAL;

        public NotesServices()
        {
            notesDAL = new NotesDAL();
        }

        public List<(int, string)> GetNotesTitlesAndIDs()
        {
            return notesDAL.GetNotesTitlesAndIDs();
        }

        public string GetNoteContents(int noteID)
        {
            return notesDAL.GetNoteContent(noteID);
        }

        public bool CreateNote(string noteTitle, string noteContent)
        {
            return notesDAL.CreateNote(noteTitle, noteContent);
        }

        public bool UpdateNoteContents(int noteID, string noteContent)
        {
            return notesDAL.UpdateNoteContent(noteID, noteContent);
        }

        public bool DeleteNote(int noteID)
        {
            return notesDAL.DeleteNote(noteID);
        }

        public List<(int, string)> GetFilteredNotes(string tag)
        {
            return notesDAL.GetFilteredNotes(tag.ToLower());
        }
    }
}
