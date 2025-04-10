using DataAcessLayer;
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
    }
}
