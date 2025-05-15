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
        public List<string> GetFreeTagsById(int noteId)
        {
            List<string> allTags = GetAllTags();
            List<string> noteTags = GetNoteTags(noteId);
            foreach (var item in noteTags)
            {
                if (allTags.Contains(item))
                {
                    allTags.Remove(item);
                }
            }
            return allTags;
        }
        public List<string> GetNoteTags(int noteID)
        {
            return tagsDAL.GetNoteTags(noteID);
        }

        public bool CreateTag(string tagContent)
        {
            return tagsDAL.CreateTag(tagContent.ToLower());
        }

        public bool AddTagToNote(int noteID, int tagID)
        {
            return tagsDAL.AddTagToNote(noteID, tagID);
        }

        public bool DoesNoteHaveTag(int noteID, int tagID)
        {
            return tagsDAL.DoesNoteHaveTag(noteID, tagID);
        }

        public bool DeleteTag(int tagID)
        {
            return tagsDAL.DeleteTag(tagID);
        }

        public int GetTagIDFromContent(string tagContent)
        {
            return tagsDAL.GetTagIDFromContent(tagContent);
        }

        public bool CheckIfNoteTagExists(int tagID, int noteID)
        {
            return tagsDAL.CheckIfNoteTagExists(tagID, noteID);
        }
    }
}
