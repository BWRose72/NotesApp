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

        /// <summary>
        /// This method is used to get a list of the titles of the notes.
        /// </summary>
        public List<(int, string)> GetNotesTitlesAndIDs()
        {
            return notesDAL.GetNotesTitlesAndIDs();
        }

        /// <summary>
        /// This method is used to get note content.
        /// </summary>
        /// <param name="noteID">The ID of the note.</param>
        /// <returns>Returns note content.</returns>
        public string GetNoteContents(int noteID)
        {
            return notesDAL.GetNoteContent(noteID);
        }

        /// <summary>
        /// This method is used to create note.
        /// </summary>
        /// <param name="noteTitle">The title of the note.</param>
        /// <param name="noteContent">The content of the note.</param>
        /// <returns>true if it has successfully created the note, otherwise false.</returns>
        public bool CreateNote(string noteTitle, string noteContent)
        {
            return notesDAL.CreateNote(noteTitle, noteContent);
        }

        /// <summary>
        /// This method is used to update the notes.
        /// </summary>
        /// <param name="noteID">The ID of the note.</param>
        /// <param name="noteContent">The content of the note.</param>
        /// <returns>true if it has successfully updated the note, otherwise false.</returns>
        public bool UpdateNoteContents(int noteID, string noteContent)
        {
            return notesDAL.UpdateNoteContent(noteID, noteContent);
        }

        /// <summary>
        /// This method is used to delete notes.
        /// </summary>
        /// <param name="noteID">The ID of the note.</param>
        /// <returns>true if it has successfully deleted the note, otherwise false.</returns>
        public bool DeleteNote(int noteID)
        {
            return notesDAL.DeleteNote(noteID);
        }

        /// <summary>
        /// This method is used to get a list of filtered notes by tag.
        /// </summary>
        /// <param name="tag">The name of the tag.</param>
        public List<(int, string)> GetFilteredNotes(string tag)
        {
            return notesDAL.GetFilteredNotes(tag.ToLower());
        }

        /// <summary>
        /// This method is used to get a list of notes by content.
        /// </summary>
        /// <param name="content">The content of the note.</param>
        public List<(int, string)> GetNotesByContent(string content)
        {
            return notesDAL.GetNotesByContent(content.ToLower());
        }
    }
}
