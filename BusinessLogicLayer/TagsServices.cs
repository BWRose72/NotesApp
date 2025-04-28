using DataAcessLayer;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class TagsServices
    {
        private readonly TagsDAL tagsDAL;

        public TagsServices()
        {
            tagsDAL = new TagsDAL();
        }

        public List<string> GetAllTags()
        {
            return tagsDAL.GetAllTags();
        }

        public List<string> GetNoteTags(int noteID)
        {
            return tagsDAL.GetNoteTags(noteID);
        }

        public bool CreateTag(string tagContent)
        {
            return tagsDAL.CreateTag(tagContent);
        }

        public bool AddTagToNote(int noteID, int tagID)
        {
            return tagsDAL.AddTagToNote(noteID, tagID);
        }

        public bool DoesNoteHaveTag(int noteID, int tagID)
        {
            return tagsDAL.DoesNoteHaveTag(noteID, tagID);
        }

        public void AddTagToNote(int tagId, string? note)
        {
            throw new NotImplementedException();
        }
    }
}
