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

        /// <summary>
        /// This method is used to get a list of all tags.
        /// </summary>
        public List<string> GetAllTags()
        {
            return tagsDAL.GetAllTags();
        }

        /// <summary>
        /// This method is used to get a list of tags that have not been added to notes.
        /// </summary>
        /// <param name="noteId">The ID of the note</param>
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

        /// <summary>
        /// This method is used to get a list of note tags.
        /// </summary>
        /// <param name="noteID">The ID of the note.</param>
        public List<string> GetNoteTags(int noteID)
        {
            return tagsDAL.GetNoteTags(noteID);
        }

        /// <summary>
        /// This method is used to create tags.
        /// </summary>
        /// <param name="tagContent">The name of the tag.</param>
        /// <returns>If it's successful, returns the created tag.</returns>
        public bool CreateTag(string tagContent)
        {
            return tagsDAL.CreateTag(tagContent.ToLower());
        }

        /// <summary>
        /// This method is used to add tags to notes.
        /// </summary>
        /// <param name="noteID">The ID of the note.</param>
        /// <param name="tagID">The ID of the tag.</param>
        /// <returns>true if it has successfully added the tag to the note, otherwise false.</returns>
        public bool AddTagToNote(int noteID, int tagID)
        {
            return tagsDAL.AddTagToNote(noteID, tagID);
        }

        /// <summary>
        /// This method is used to delete tags.
        /// </summary>
        /// <param name="tagID">The ID of the tag.</param>
        /// <returns>true if it has successfully deleted the tag, otherwise false.</returns>
        public bool DeleteTag(int tagID)
        {
            return tagsDAL.DeleteTag(tagID);
        }

        /// <summary>
        /// This method is used to get tag ID from the content.
        /// </summary>
        /// <param name="tagContent">The name of the tag.</param>
        /// <returns>Returns the tag ID</returns>
        public int GetTagIDFromContent(string tagContent)
        {
            return tagsDAL.GetTagIDFromContent(tagContent.ToLower());
        }
    }
}
