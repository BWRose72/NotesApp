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
    }
}