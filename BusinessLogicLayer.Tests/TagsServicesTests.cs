using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Tests
{
    public class TagsServicesTests
    {
        private TagsServices tagsServices;
        private NotesServices notesServices;

        [SetUp]
        public void Setup()
        {
            tagsServices = new TagsServices();
            notesServices = new NotesServices();
        }

        [Test]
        public void TestGetAllTags()
        {
            int expectedResult = 5/*?*/;
            int actualResult;

            actualResult = tagsServices.GetAllTags().Count();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestGetNoteTags()
        {
            notesServices.CreateNote("NUNIT Test Note", "Content");
            var noteID = notesServices.GetNotesTitlesAndIDs().First(n => n.Item2 == "NUNIT Test Note").Item1;

            List<string> expectedResult = new List<string> { "nunittesttag1", "nunittesttag2" };
            tagsServices.CreateTag("nunittesttag1");
            tagsServices.CreateTag("nunittesttag2");
            tagsServices.AddTagToNote(noteID, tagsServices.GetTagIDFromContent("nunittesttag1"));
            tagsServices.AddTagToNote(noteID, tagsServices.GetTagIDFromContent("nunittesttag2"));
            List<string> actualResult;

            actualResult = tagsServices.GetNoteTags(noteID);
            Assert.AreEqual(expectedResult, actualResult);

            notesServices.DeleteNote(noteID);
            tagsServices.DeleteTag(tagsServices.GetTagIDFromContent("nunittesttag1"));
            tagsServices.DeleteTag(tagsServices.GetTagIDFromContent("nunittesttag2"));
        }

        [Test]
        public void TestCreateTag()
        {
            string tagContent = "New NUNIT Tag";
            bool expectedResult = true;
            bool actualResult;

            actualResult = tagsServices.CreateTag(tagContent);

            Assert.AreEqual(expectedResult, actualResult);

            tagsServices.DeleteTag(tagsServices.GetTagIDFromContent(tagContent));
        }

        [Test]
        public void TestAddTagToNote()
        {
            notesServices.CreateNote("NUNIT Tag Note", "Content");
            var noteID = notesServices.GetNotesTitlesAndIDs().First(n => n.Item2 == "NUNIT Tag Note").Item1;

            string tagContent = "NUNIT Tag";
            tagsServices.CreateTag(tagContent);
            int tagID = tagsServices.GetTagIDFromContent(tagContent);

            bool expectedResult = true;
            bool actualResult;

            tagsServices.AddTagToNote(noteID, tagID);
            actualResult = tagsServices.GetNoteTags(noteID).Contains(tagContent.ToLower());

            Assert.AreEqual(expectedResult, actualResult);

            notesServices.DeleteNote(noteID);
            tagsServices.DeleteTag(tagID);
        }

        [Test]
        public void TestDeleteTag() 
        {
            string tagContent = "Delete Tag";
            tagsServices.CreateTag(tagContent);
            int tagID = tagsServices.GetTagIDFromContent(tagContent);

            int expectedResult = tagsServices.GetAllTags().Count() - 1;
            int actualResult;

            tagsServices.DeleteTag(tagID);
            actualResult = tagsServices.GetAllTags().Count();

            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void TestGetIDFromContent()
        {
            string tagContent = "IDTst Tag";
            tagsServices.CreateTag(tagContent);
            int expectedResult = 30/*?*/;
            int actualResult;

            actualResult = tagsServices.GetTagIDFromContent(tagContent);
            tagsServices.DeleteTag(actualResult);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
