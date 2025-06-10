namespace BusinessLogicLayer.Tests
{
    public class NotesServicesTests
    {
        private NotesServices notesServices;

        [SetUp]
        public void Setup()
        {
            notesServices = new NotesServices();
        }

        [Test]
        public void TestGetTitlesAndIDs()
        {
            int expectedResult = 8;
            int actualResult;

            actualResult = notesServices.GetNotesTitlesAndIDs().Count();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestGetNoteContents()
        {
            notesServices.CreateNote("Note 1", "Content of note 1");
            var noteID = notesServices.GetNotesTitlesAndIDs().First(n => n.Item2 == "Note 1").Item1;
            string expectedResult = "Content of note 1";
            string actualResult;

            actualResult = notesServices.GetNoteContents(noteID);

            Assert.AreEqual(expectedResult, actualResult);

            notesServices.DeleteNote(noteID);
        }

        [Test]
        public void TestCreateNote()
        {
            string noteTitle = "New Note";
            string noteContent = "New note content";
            bool expectedResult = true;
            bool actualResult;

            actualResult = notesServices.CreateNote(noteTitle, noteContent);

            Assert.AreEqual(expectedResult, actualResult);

            notesServices.DeleteNote(notesServices.GetNotesTitlesAndIDs().First(n => n.Item2 == noteTitle).Item1);
        }

        [Test]
        public void TestDeleteNote() 
        {
            notesServices.CreateNote("Delete test", "Content");
            var noteID = notesServices.GetNotesTitlesAndIDs().First(n => n.Item2 == "Delete test").Item1;
            bool expectedResult = true;
            bool actualResult;

            actualResult = notesServices.DeleteNote(noteID);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestUpdateNoteContents()
        {
            notesServices.CreateNote("Update test", "Content");
            var noteID = notesServices.GetNotesTitlesAndIDs().First(n => n.Item2 == "Update test").Item1;
            string newContent = "New content";
            bool expectedResult = true;
            bool actualResult;

            actualResult = notesServices.UpdateNoteContents(noteID, newContent);

            Assert.AreEqual(expectedResult, actualResult);
            Assert.AreEqual(newContent, notesServices.GetNoteContents(noteID));

            notesServices.DeleteNote(noteID);
        }

        [Test]
        public void TestGetFilteredNotes()
        {
            notesServices.CreateNote("Filter test", "Content");
            var noteID = notesServices.GetNotesTitlesAndIDs().First(n => n.Item2 == "Filter test").Item1;

            string tag = "nunitTestTag";
            var tagsServices = new TagsServices();
            tagsServices.CreateTag(tag);
            tagsServices.AddTagToNote(noteID, tagsServices.GetTagIDFromContent(tag));

            bool expectedResult = true;
            bool actualResult;

            var filteredNotes = notesServices.GetFilteredNotes(tag).Where(n => n.Item1 == noteID);
            actualResult = filteredNotes.Count() > 0;

            Assert.AreEqual(expectedResult, actualResult);

            tagsServices.DeleteTag(tagsServices.GetTagIDFromContent(tag));
            notesServices.DeleteNote(noteID);
        }
    }
}